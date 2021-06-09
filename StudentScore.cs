using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

namespace SATGrading
{
    public class StudentScore
    {
        public int TestID;
        public int StudentID;   
        private string mKeyReference; 
        private int mReferenceTestNumber; 
        private int mReferenceSectionNumber; 
        private int mReferenceQuestionNumber; 
        private string mAnswer; 
        private string mCorrectAnswer; 
        private bool mIsCorrected; 
        private bool mIsSkip; 
        private bool mIsFillin; 
        private double mRawScore;
        private int mSubject_SubjectID;
        private int mAdjustedPoint;
        private int mEssayScore;

        private Dictionary<string, string> SectionByCSV = new Dictionary<string, string>();
        public RawTotal RawTotalMath = new RawTotal();
        public RawTotal RawTotalReading = new RawTotal();
        public RawTotal RawTotalWriting = new RawTotal();

        private List<string> MathOutputError = new List<string>();
        private List<string> WritingOutputError = new List<string>();
        private List<string> ReadingOutputError = new List<string>();

        private Hashtable mathSections = new Hashtable();
        private Hashtable readingSections = new Hashtable();
        private Hashtable writingSections = new Hashtable();
        public string mError;
        public bool bWriteToDB;
        

        private int ActualMathScore;
        private int ActualReadingScore;
        private int ActualWritingScore;
        private int ActualEssayScore;
        private int ActualTotal;
        private string StudentName;

        //Math: section 2, 5, 8 ( _ = correct      x = incorrect        o = empty )
        //section 2:
        //CorrectAnswer: A B C D E A B C D E A B C D E A B C D E
        //StudentAnswer: A B C D D A B C - E A B C D D A B C D D
        //CompareResult: _ _ _ _ x _ _ _ o _ _ _ _ _ x _ _ _ _ x
        //section 5:
        //CorrectAnswer:
        //StudentAnswer:
        //CompareResult:
        //section 8:
        //CorrectAnswer:
        //StudentAnswer:
        //CompareResult:


        public StudentScore()
        {
        }

        public StudentScore(int testID, int studentID)
        {
            TestID = testID;
            StudentID = studentID;
        }

        public void SetSectionCSV(string sectionKey, string csv)
        {
            if (!SectionByCSV.ContainsKey(sectionKey))
                SectionByCSV.Add(sectionKey, csv);
        }


        //private List<string> MathOutput = new List<string>();
        //private List<string> WritingOutput = new List<string>();
        //private List<string> ReadingOutput = new List<string>();

        //Math: section 2, 5, 8 ( _ = correct      x = incorrect        o = empty )
        //section 2:
        //CorrectAnswer: A B C D E A B C D E A B C D E A B C D E
        //StudentAnswer: A B C D D A B C - E A B C D D A B C D D
        //CompareResult: _ _ _ _ x _ _ _ o _ _ _ _ _ x _ _ _ _ x
        //section 5:
        //CorrectAnswer:
        //StudentAnswer:
        //CompareResult:
        //section 8:
        //CorrectAnswer:
        //StudentAnswer:
        //CompareResult:


        //Dictionary<string, string> dictSectionMapping = new Dictionary<string, string>();
        //Dictionary<string, QuestionAnswer> dictAnswerKey = new Dictionary<string, QuestionAnswer>();
        /*
        public void Calculate(Dictionary<string, string> dictSectionMapping, Dictionary<string, QuestionAnswerKey> dictAnswerKey)
        {
            foreach (KeyValuePair<string, string> entry in SectionByCSV)
            {
                string scantronSection = entry.Key;
                if (!dictSectionMapping.ContainsKey(scantronSection))
                    continue;

                string testSection = dictSectionMapping[scantronSection];
                string csv = entry.Value;
                string[] arrAnswers = csv.Split(',');
                int i = 1;
                #region handle each answer
                foreach (string answer in arrAnswers)
                {
                    string sectionKey = testSection + "." + i;
                    i++;
                    if (!dictAnswerKey.ContainsKey(sectionKey))
                        continue;

                    QuestionAnswerKey answerKeyObj = dictAnswerKey[sectionKey];
                    if (string.IsNullOrEmpty(answer))
                    {
                        UpdateCount(answerKeyObj.Category, "blank");                        
                        continue;
                    }
                    
                    //What kind of answer is it? 
                    //ABCDE
                    //FillIn: single value, in a list, in a range?
                    if (answerKeyObj.IsFillIn)
                    {
                        #region FillIn
                        //FillIn: single value, in a list, in a range?
                        if (!string.IsNullOrEmpty(answerKeyObj.AnswerRange))
                        {
                            decimal val = decimal.Parse(answer);
                            if (val >= answerKeyObj.AnswerRangeLowerBound && val <= answerKeyObj.AnswerRangeUpperBound)
                                UpdateCount(answerKeyObj.Category, "correct"); 
                            else
                                UpdateCount(answerKeyObj.Category, "incorrect"); 
                        }
                        else if (answerKeyObj.AnswerChoiceList != null && answerKeyObj.AnswerChoiceList.Count > 0)
                        {
                            if (answerKeyObj.AnswerChoiceList != null && answerKeyObj.AnswerChoiceList.ContainsKey(answer))
                            {
                                UpdateCount(answerKeyObj.Category, "correct"); 
                            }
                            else
                            {
                                UpdateCount(answerKeyObj.Category, "incorrect"); 
                            }
                        }
                        else
                        {
                            //single value - is it fraction?
                            if (answer.IndexOf("/") >= 0)
                            {
                                string[] fractions = answer.Split('/');
                                try
                                {
                                    
                                    decimal quotient = decimal.Parse(fractions[0]) / decimal.Parse(fractions[1]);
                                    if ((quotient >= answerKeyObj.AnswerRangeLowerBound && quotient <= answerKeyObj.AnswerRangeUpperBound) || (quotient == decimal.Parse(answerKeyObj.AnswerRawForm)))
                                        UpdateCount(answerKeyObj.Category, "correct");
                                    else
                                        UpdateCount(answerKeyObj.Category, "incorrect");                                    
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                if (answer == answerKeyObj.AnswerRawForm)
                                    UpdateCount(answerKeyObj.Category, "correct");
                                else
                                    UpdateCount(answerKeyObj.Category, "incorrect");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (answer == answerKeyObj.AnswerRawForm)                        
                            UpdateCount(answerKeyObj.Category, "correct");                        
                        else                        
                            UpdateCount(answerKeyObj.Category, "incorrect");                        
                    }
                }
                #endregion
            }

        }
        */

        public void CalculateAndLookup(Dictionary<string, string> dictSectionMapping,
            Dictionary<string, string> dictMathTable,
            Dictionary<string, string> dictReadingTable,
            Dictionary<string, string> dictWritingTable,
            Dictionary<string, string> dictStudentID,
            Dictionary<string, string> dictStudentIdByEssayScore,
            Dictionary<string, QuestionAnswerKey> dictAnswerKey,
            int adjustedPoint)
        {         
            StringBuilder sbMath = new StringBuilder();
            StringBuilder sbWriting = new StringBuilder();
            StringBuilder sbReading = new StringBuilder();
            mAdjustedPoint = adjustedPoint;

            foreach (KeyValuePair<string, string> entry in SectionByCSV)
            {
                string scantronSection = entry.Key;
                if (!dictSectionMapping.ContainsKey(scantronSection))
                    continue;               
                string testSection = dictSectionMapping[scantronSection];
                string csv = entry.Value;
                string[] arrAnswers = csv.Split(',');
                int i = 1;
                #region handle each answer
                foreach (string answer in arrAnswers)
                {
                    string sectionKey = testSection + "." + i;
                    i++;
                    if (!dictAnswerKey.ContainsKey(sectionKey))
                        continue;

                    QuestionAnswerKey answerKeyObj = dictAnswerKey[sectionKey];

                    //update member variables to prepare to write to DB
                    mKeyReference = this.TestID.ToString() + "." + sectionKey;
                    mReferenceTestNumber = this.TestID;
                    mReferenceSectionNumber = int.Parse(testSection);
                    mReferenceQuestionNumber = i;
                    mAnswer = answer;
                    mIsSkip = (answer.Trim().Length == 0);
                    mIsFillin = answerKeyObj.IsFillIn;
                    mSubject_SubjectID = int.Parse(answerKeyObj.Category);
                    // mCorrectAnswer; mIsCorrected; mRawScore  ==> logic below
                    bool isFillInBadFormat = false;

                    if (string.IsNullOrEmpty(answer))
                    {
                        UpdateCount(answerKeyObj.Category, "blank");
                        mCorrectAnswer = answerKeyObj.AnswerRawForm;
                        mIsCorrected = false;
                        mRawScore = 0;
                        continue;
                    }

                    //What kind of answer is it? 
                    //ABCDE
                    //FillIn: 
                    if (answerKeyObj.IsFillIn)
                    {
                        //Step 1: convert the value to double if it's a fraction
                        //Step 2: set margin error to be 0.001% acceptable value
                        //Step 3: determine whether it's single value, in a list, in a range - then compare with acceptable error margin 

                        if (!isFillInBadFormat)
                        {
                            double val = 0;
                            #region Convert Fraction to double
                            if (answer.IndexOf("/") >= 0)
                            {

                                string[] fractions = answer.Split('/');
                                try
                                {
                                    val = double.Parse(fractions[0]) / double.Parse(fractions[1]);
                                }
                                catch (Exception ex)
                                {
                                    mError = ex.ToString();
                                }

                            }
                            else
                            {
                                //check for invalide value
                                if (double.TryParse(answer, out val))
                                    val = double.Parse(answer);
                                else
                                {
                                    UpdateVariablesIncorrect(answerKeyObj, sectionKey, answer);
                                    isFillInBadFormat = true;
                                }
                            }
                            #endregion
                            double marginError = Math.Abs(val * .00001);

                            //List
                            if (answerKeyObj.AnswerChoiceList != null && answerKeyObj.AnswerChoiceList.Count > 0)
                            {
                                #region ListOfPossibleCorrectValues
                                if (answerKeyObj.AnswerChoiceList != null && answerKeyObj.AnswerChoiceList.ContainsKey(answer))
                                    UpdateVariablesCorrect(answerKeyObj);
                                else
                                    UpdateVariablesIncorrect(answerKeyObj, sectionKey, answer);
                                #endregion
                            }
                            else if (!string.IsNullOrEmpty(answerKeyObj.AnswerRange))
                            {
                                bool greaterThan = (val.CompareTo(answerKeyObj.AnswerRangeLowerBound) >= 0);
                                bool lessThan = (val.CompareTo(answerKeyObj.AnswerRangeUpperBound) <= 0);
                                if (greaterThan && lessThan)
                                    UpdateVariablesCorrect(answerKeyObj);
                                else
                                    UpdateVariablesIncorrect(answerKeyObj, sectionKey, answer);
                            }
                            else
                            {
                                //Single value
                                bool closeEnough = (Math.Abs(val - double.Parse(answerKeyObj.AnswerRawForm)) <= marginError);
                                if (closeEnough)
                                    UpdateVariablesCorrect(answerKeyObj);
                                else
                                    UpdateVariablesIncorrect(answerKeyObj, sectionKey, answer);
                            }
                        }
                    }
                    else
                    {
                        #region Regular ABCDE choice
                        if (answer == answerKeyObj.AnswerRawForm)
                        {
                            UpdateVariablesCorrect(answerKeyObj);
                        }
                        else
                        {
                            mCorrectAnswer = answerKeyObj.AnswerRawForm;
                            mIsCorrected = false;
                            double penalty = -0.25;
                            mRawScore = penalty;
                            UpdateIncorrectCount(answerKeyObj.Category, "incorrect", answerKeyObj.IsFillIn, sectionKey, answerKeyObj.AnswerRawForm, answer);
                        }
                        #endregion
                    }

                    InsertResultIntoDB();

                }
                #endregion
            }

            mEssayScore = 5;

            if (dictStudentIdByEssayScore.ContainsKey(StudentID.ToString()))
            {
                try
                {
                    mEssayScore = int.Parse(dictStudentIdByEssayScore[StudentID.ToString()]);
                }
                catch { }
            }
            ComputeSubtotal(mEssayScore);

            #region StudentName
            if (dictStudentID.ContainsKey(StudentID.ToString()))
                StudentName = dictStudentID[StudentID.ToString()];
            #endregion

            #region Convert Raw score to Actual Score
            ActualMathScore = int.Parse(dictMathTable[RawTotalMath.GetGrandTotal().ToString()]);
            ActualReadingScore = int.Parse(dictReadingTable[RawTotalReading.GetGrandTotal().ToString()]);

            try
            {
                if (dictStudentIdByEssayScore.ContainsKey(StudentID.ToString()))
                    ActualEssayScore = int.Parse(dictStudentIdByEssayScore[StudentID.ToString()]);
            }
            catch { }
            if (ActualEssayScore == 0)
                ActualEssayScore = 5; //defautl to 5 points

            string essayKey = RawTotalWriting.GetGrandTotal().ToString() + "|" + ActualEssayScore;
            ActualWritingScore = int.Parse(dictWritingTable[essayKey]);

            ActualMathScore += mathAdjusted;
            ActualReadingScore += readingAdjusted;
            ActualWritingScore += writingAdjusted;

            ActualTotal = ActualMathScore + ActualReadingScore + ActualWritingScore;

            #endregion

        }

        private void UpdateVariablesIncorrect(QuestionAnswerKey answerKeyObj, string sectionKey, string answer)
        {
            UpdateVariables(answerKeyObj, false, 0);
            UpdateIncorrectCount(answerKeyObj.Category, "incorrect", answerKeyObj.IsFillIn, sectionKey, answerKeyObj.AnswerRawForm, answer);
        }

        private void UpdateVariablesCorrect(QuestionAnswerKey answerKeyObj)
        {
            UpdateVariables(answerKeyObj);
            UpdateCount(answerKeyObj.Category, "correct");
        }


        private void UpdateVariables(QuestionAnswerKey answerKeyObj, bool isCorrect = true, double rawScore = 1)
        {
            mCorrectAnswer = answerKeyObj.AnswerRawForm;
            mIsCorrected = isCorrect;
            mRawScore = rawScore;
        }

        public void InsertResultIntoDB()
        {
            //StudentTestAnswerInsert(int Test_TestID, int Student_StudentID, string KeyReference,
            //int ReferenceTestNumber, int ReferenceSectionNumber, int ReferenceQuestionNumber,
            //string Answer, string CorrectAnswer, bool IsCorrected,
            //bool IsSkip, bool IsFillin, decimal RawScore, int Subject_SubjectID)

            StudentTestAnswerInsert(this.TestID, this.StudentID, mKeyReference,
            mReferenceTestNumber, mReferenceSectionNumber, mReferenceQuestionNumber,
            mAnswer, mCorrectAnswer, mIsCorrected,
            mIsSkip, mIsFillin, mRawScore, mSubject_SubjectID);
        }

        private void StudentTestAnswerInsert(int Test_TestID, int Student_StudentID, string KeyReference,
            int ReferenceTestNumber, int ReferenceSectionNumber, int ReferenceQuestionNumber,
            string Answer, string CorrectAnswer, bool IsCorrected,
            bool IsSkip, bool IsFillin, double RawScore, int Subject_SubjectID)
        {
            //EXEC StudentTestAnswers_Insert 
            //    Test_TestID, Student_StudentID, KeyReference, 
            //    ReferenceTestNumber, ReferenceSectionNumber, ReferenceQuestionNumber, 
            //    Answer, CorrectAnswer, IsCorrected, 
            //    IsSkip, IsFillin, RawScore, Subject_SubjectID

            //EXEC StudentTestAnswers_Insert 121, 343, '21.2.1', 21, 2, 1, 'C', 'C', 1, 0, 0, 1.0, 1

            //EXEC StudentTestAnswers_Insert 
            //    Test_TestID, Student_StudentID, KeyReference, 
            //    ReferenceTestNumber, ReferenceSectionNumber, ReferenceQuestionNumber, 
            //    Answer, CorrectAnswer, IsCorrected, 
            //    IsSkip, IsFillin, RawScore, Subject_SubjectID


            string connstr = @"Server=(local);DataBase=SAT;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connstr);

            //NC-19 Create SqlCommand and identify it as a stored procedure.
            SqlCommand cmdNewOrder = new SqlCommand("StudentTestAnswers_Insert", conn);
            cmdNewOrder.CommandType = CommandType.StoredProcedure;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Test_TestID", SqlDbType.Int));
            cmdNewOrder.Parameters["@Test_TestID"].Value = Test_TestID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Student_StudentID", SqlDbType.Int));
            cmdNewOrder.Parameters["@Student_StudentID"].Value = Student_StudentID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@KeyReference", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@KeyReference"].Value = KeyReference;

            cmdNewOrder.Parameters.Add(new SqlParameter("@ReferenceTestNumber", SqlDbType.Int));
            cmdNewOrder.Parameters["@ReferenceTestNumber"].Value = ReferenceTestNumber;

            cmdNewOrder.Parameters.Add(new SqlParameter("@ReferenceSectionNumber", SqlDbType.Int));
            cmdNewOrder.Parameters["@ReferenceSectionNumber"].Value = ReferenceSectionNumber;

            cmdNewOrder.Parameters.Add(new SqlParameter("@ReferenceQuestionNumber", SqlDbType.Int));
            cmdNewOrder.Parameters["@ReferenceQuestionNumber"].Value = ReferenceQuestionNumber;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Answer", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@Answer"].Value = Answer;

            cmdNewOrder.Parameters.Add(new SqlParameter("@CorrectAnswer", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@CorrectAnswer"].Value = CorrectAnswer;

            cmdNewOrder.Parameters.Add(new SqlParameter("@IsCorrected", SqlDbType.Bit));
            cmdNewOrder.Parameters["@IsCorrected"].Value = IsCorrected;

            cmdNewOrder.Parameters.Add(new SqlParameter("@IsSkip", SqlDbType.Bit));
            cmdNewOrder.Parameters["@IsSkip"].Value = IsSkip;

            cmdNewOrder.Parameters.Add(new SqlParameter("@IsFillin", SqlDbType.Bit));
            cmdNewOrder.Parameters["@IsFillin"].Value = IsFillin;

            cmdNewOrder.Parameters.Add(new SqlParameter("@RawScore", SqlDbType.Float));
            cmdNewOrder.Parameters["@RawScore"].Value = RawScore;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Subject_SubjectID", SqlDbType.Int));
            cmdNewOrder.Parameters["@Subject_SubjectID"].Value = Subject_SubjectID;

            //try – catch - finally
            try
            {
                //Open connection.
                conn.Open();

                //Run the stored procedure.
                cmdNewOrder.ExecuteNonQuery();

                //NC-25 Display the order number.                
                //MessageBox.Show("test has been submitted.");
            }
            catch (Exception ex)
            {
                //A simple catch.
                mError = "StudentTestAnswers_Insert error " + ex.ToString();
            }
            finally
            {
                //Close connection.
                conn.Close();
            }


        }

        private void ComputeSubtotal(int essasyScore)
        {
           //CREATE PROCEDURE [dbo].[StudentTestAnswersSubtotal_Update] 
            //    @StudentID INT,
            //    @TestID INT,
            //    @EssayScore INT,
            //    @AdjustedPoint INT


            string connstr = @"Server=(local);DataBase=SAT;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connstr);

            //NC-19 Create SqlCommand and identify it as a stored procedure.
            SqlCommand cmdNewOrder = new SqlCommand("StudentTestAnswersSubtotal_Update", conn);
            cmdNewOrder.CommandType = CommandType.StoredProcedure;

            cmdNewOrder.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            cmdNewOrder.Parameters["@StudentID"].Value = this.StudentID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int));
            cmdNewOrder.Parameters["@TestID"].Value = this.TestID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@EssayScore", SqlDbType.Int));
            cmdNewOrder.Parameters["@EssayScore"].Value = essasyScore;

            cmdNewOrder.Parameters.Add(new SqlParameter("@AdjustedPoint", SqlDbType.Int));
            cmdNewOrder.Parameters["@AdjustedPoint"].Value = mAdjustedPoint;

            try
            {
                conn.Open();
                cmdNewOrder.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mError = "StudentTestAnswers_Insert error " + ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateCountDB()
        {

        }

        public void UpdateCount(string category, string type)
        {
            if (type.ToLower() == "correct")
            {
                if (category == "1") //Math
                    RawTotalMath.TotalCorrect++;
                else if (category == "2") //Reading
                    RawTotalReading.TotalCorrect++;
                else //Writing
                    RawTotalWriting.TotalCorrect++;
            }
            else if (type.ToLower() == "incorrect")
            {
                /*
                if (category == "1") //Math
                    RawTotalMath.TotalIncorrect++;
                else if (category == "2") //Reading
                    RawTotalReading.TotalIncorrect++;
                else //Writing
                    RawTotalWriting.TotalIncorrect++;
                 * */
            }
            else
            {         
                if (category == "1") //Math
                    RawTotalMath.TotalBlank++;
                else if (category == "2") //Reading
                    RawTotalReading.TotalBlank++;
                else //Writing
                    RawTotalWriting.TotalBlank++;
            }
        }

        public void SetSections(string sMathSections, string sReadingSections, string sWritingSections)
        {

            SetHashGiveCsv(this.mathSections, sMathSections);
            SetHashGiveCsv(this.readingSections, sReadingSections);
            SetHashGiveCsv(this.writingSections, sWritingSections);
        }

        private void SetHashGiveCsv(Hashtable table, string csv)
        {
            string[] tokens = csv.Split(',');
            foreach (string token in tokens)
            {
                if (!table.ContainsKey(token))
                    table.Add(token, token);
            }
        }

        public void UpdateIncorrectCount(string category, string type, bool isFillIn, string problemNumber, string actualAnswer, string studentAnswer)
        {
            if (isFillIn)
                return;

            //if (problemNumber.StartsWith("3") || problemNumber.StartsWith("6") || problemNumber.StartsWith("9")) //Math (category == "1")
            //{
            //    MathOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
            //    RawTotalMath.TotalIncorrect++;
            //}
            //else if (problemNumber.StartsWith("4") || problemNumber.StartsWith("7") || problemNumber.StartsWith("8")) //Reading  (category == "2")
            //{
            //    ReadingOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
            //    RawTotalReading.TotalIncorrect++;
            //}
            //else //Writing
            //{
            //    WritingOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
            //    RawTotalWriting.TotalIncorrect++;
            //}

            string[] tokens = problemNumber.Split('.');

            if (mathSections.ContainsKey(tokens[0]) && !isFillIn)
            {
                MathOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
                RawTotalMath.TotalIncorrect++;
            }
            else if (readingSections.ContainsKey(tokens[0]))
            {
                ReadingOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
                RawTotalReading.TotalIncorrect++;
            }
            else if (writingSections.ContainsKey(tokens[0]))
            {
                WritingOutputError.Add("problemNumber:" + problemNumber + "   actualAnswer:" + actualAnswer + "   StudentAnswer:" + studentAnswer);
                RawTotalWriting.TotalIncorrect++;
            }

        }

        int mathAdjusted;
        int readingAdjusted;
        int writingAdjusted;

        public void SetAdjustedScore(int math, int reading, int writing)
        {
            mathAdjusted = math;
            readingAdjusted = reading;
            writingAdjusted = writing;
        }

        public string ShowScore1()
        {
            return "Raw score of " + StudentName + " (StudentID " + StudentID + "):   Math=" + RawTotalMath.GetGrandTotal().ToString("0.#")
                + "   Reading:" + RawTotalReading.GetGrandTotal().ToString("0.#")
                + "   Writing:" + RawTotalMath.GetGrandTotal().ToString("0.#")
                + "   ActualMathScore:" + ActualMathScore
                + "   ActualRedingScore:" + ActualReadingScore
                + "   ActualWritingScore:" + ActualWritingScore
                + " --- Total Score:" + ActualTotal;
        }

        public string ShowScore()
        {
            string result =
             (StudentName == null ? "" + StudentID : StudentName.Replace("|", " ")) + " (StudentID " + StudentID
                + "):   \r\n"
                + "Math:\t\t" + ActualMathScore + "   (correct=" + RawTotalMath.TotalCorrect + ", incorrect=" + RawTotalMath.TotalIncorrect + ",unanswered=" + RawTotalMath.TotalBlank + ", SubTotal=" + RawTotalMath.GetGrandTotal().ToString("0.#") + ")\r\n"
                + "Reading:\t" + ActualReadingScore + "   (correct=" + RawTotalReading.TotalCorrect + ", incorrect=" + RawTotalReading.TotalIncorrect + ",unanswered=" + RawTotalReading.TotalBlank + ", SubTotal=" + RawTotalReading.GetGrandTotal().ToString("0.#") + ")\r\n"
                + "Writing:\t" + ActualWritingScore + "   (Essay=" + ActualEssayScore + ", correct=" + RawTotalWriting.TotalCorrect + ", incorrect=" + RawTotalWriting.TotalIncorrect + ",unanswered=" + RawTotalWriting.TotalBlank + ", SubTotal=" + RawTotalWriting.GetGrandTotal().ToString("0.#") + ")\r\n"
                + "Final Score:" + ActualTotal;

            //Build Math setion result:
            result += "\r\n-----------------------------------------------\r\nMath Errors\r\n";            
            foreach (string data in MathOutputError)
            {
                result += data + "\r\n";
            }
            result += "\r\n-----------------------------------------------\r\nReading Errors\r\n";
            foreach (string data in ReadingOutputError)
            {
                result += data + "\r\n";
            }
            result += "\r\n-----------------------------------------------\r\nWriting Errors\r\n";
            foreach (string data in WritingOutputError)
            {
                result += data + "\r\n";
            }
            
            result += "\r\n=========================================================================================";
            return result;
        }


        public string ShowScoreSummary()
        {
            return (StudentName == null ? "" + StudentID : StudentName.Replace("|", " ")) + "," + StudentID + "," + ActualTotal + "," + ActualMathScore + "," + ActualReadingScore + "," + ActualWritingScore + "," + ActualEssayScore; 
        }

        public void CreateHtmlFile(string runningRawFolder, string htmlOutputFolder, string testNumber)
        {
            //There are two folders:  1) RawRunning    2) FinalHTMLOutput
            //1) RawRunning: Create the first time then append after each test - without HTML header, body, etc
            //2) FinalHTMLOutput: Getting recreated everytime: with header, trailer, etc.


            if (StudentName == null)
                return;



            string result =
              "<hr><h4>Test " + testNumber + " result</h4>"+ 
             (StudentName == null ? "" + StudentID : StudentName.Replace("|", " ")) + " (StudentID " + StudentID
                + "):   <br />"
                + "Math:" + ActualMathScore + "   (correct=" + RawTotalMath.TotalCorrect + ", incorrect=" + RawTotalMath.TotalIncorrect + ",unanswered=" + RawTotalMath.TotalBlank + ", SubTotal=" + RawTotalMath.GetGrandTotal().ToString("0.#") + ")<br />"
                + "Reading:" + ActualReadingScore + "   (correct=" + RawTotalReading.TotalCorrect + ", incorrect=" + RawTotalReading.TotalIncorrect + ",unanswered=" + RawTotalReading.TotalBlank + ", SubTotal=" + RawTotalReading.GetGrandTotal().ToString("0.#") + ")<br />"
                + "Writing:" + ActualWritingScore + "   (Essay=" + ActualEssayScore + ", correct=" + RawTotalWriting.TotalCorrect + ", incorrect=" + RawTotalWriting.TotalIncorrect + ",unanswered=" + RawTotalWriting.TotalBlank + ", SubTotal=" + RawTotalWriting.GetGrandTotal().ToString("0.#") + ")<br />"
                + "Final Score:" + ActualTotal;

            //Build Math setion result:
            result += "<br />-----------------------------------------------<br />Math Errors<br />";
            foreach (string data in MathOutputError)
            {
                result += data + "<br />";
            }
            result += "<br />-----------------------------------------------<br />Reading Errors<br />";
            foreach (string data in ReadingOutputError)
            {
                result += data + "<br />";
            }
            result += "<br />-----------------------------------------------<br />Writing Errors<br />";
            foreach (string data in WritingOutputError)
            {
                result += data + "<br />";
            }

            result += "<br />=========================================================================================<br />";
            //result += "<hr><h4>Test 3 result: coming soon</h4>";

            string studentName = StudentName.Replace("|", "");
            
            //First let's do the Raw Create/Append to file if exists
            string fileName = runningRawFolder + studentName + ".txt";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true))
            {
                sw.WriteLine(result);
                sw.Close();
            }
            System.Threading.Thread.Sleep(100);
            //Now take the raw file and ceate html file: Read All, pre-append + append
            string header = @"<html><head><title>" + StudentName.Replace("|", " ") + "</title><body>";
            header += "<h4>Test 1 is being used for assessment and measure student's improvement and it will not be reviewed until the last week of class</h4>";
            //header += "<hr><h4>Test " + testNumber + " result</h4>";
            string trailer = "</body></html>";

            string rawData = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
            {
                rawData = sr.ReadToEnd();
                sr.Close();
            }


            string fileNameHtml = htmlOutputFolder + studentName + ".htm";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileNameHtml))
            {
                sw.WriteLine(header + rawData + trailer);
                sw.Close();
            }
        }

    }

    public class RawTotal
    {
        public int TotalCorrect;
        public int TotalIncorrect;
        public int TotalBlank;


        public RawTotal()
        {
        }

        public RawTotal(int correct, int incorrect, int blank)
        {
            TotalCorrect = correct;
            TotalIncorrect = incorrect;
            TotalBlank = blank;
        }

        public int GetGrandTotal()
        {
            //Incorrect less than 0.75 round down 
            double incorrect = (Math.Floor((double)((TotalIncorrect/4.0) + 0.5) * 10) / 10);

            int result = ((int)TotalCorrect - (int)incorrect);
            if (result < 1)
                result = 1;

            return result;
        }

    }
}
