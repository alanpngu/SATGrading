using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace SATGrading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //LoadKeyFromFile(txtKeyFilePath.Text);
            //GradeIt();
            UpdateTestNumberTextBoxes();
        }

        #region Sample AnswerKey File
        //3|1|0|E||2
        //3|2|0|E||2
        //3|3|0|B||2
        //3|4|0|E||2
        //3|5|0|D||2
        //3|6|0|D||2
        //3|7|0|E||2
        //3|8|0|B||2
        //3|9|0|D||2
        //3|10|0|A||2
        //3|11|0|E||2
        //3|12|0|B||2
        //3|13|0|D||2
        //3|14|0|E||2
        //3|15|0|D||2
        //3|16|0|A||2
        //3|17|0|D||2
        //3|18|0|B||2
        //3|19|0|A||2
        //3|20|0|C||2
        //3|21|0|C||2
        //3|22|0|B||2
        //3|23|0|E||2
        //3|24|0|C||2
        //4|1|0|D||1
        //4|2|0|A||1
        //4|3|0|E||1
        //4|4|0|A||1
        //ScantronToTestSectionMapping:(1.1,2)|(1.2,3)|(1.3,4)|(2.1,5)|(2.2,7)|(2.3|9)|(3.1,8)|(3.2,10)|Math=2,6,9|Reading=4,7,8|Writing=5,10
        #endregion
        Dictionary<string, string> dictSectionMapping = new Dictionary<string, string>();
        Dictionary<string, QuestionAnswerKey> dictAnswerKey = new Dictionary<string, QuestionAnswerKey>();

        Dictionary<string, string> dictMathTable = new Dictionary<string, string>();
        Dictionary<string, string> dictReadingTable = new Dictionary<string, string>();
        Dictionary<string, string> dictWritingTable = new Dictionary<string, string>();
        Dictionary<string, string> dictStudentIdByEssayScore = new Dictionary<string, string>();
        Dictionary<string, string> dictStudentId = new Dictionary<string, string>();
        private string mathSections;
        private string readingSections;
        private string writingSections;

        private void btnLoadKeyFile_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            LoadKeyFromFile(txtKeyFilePath.Text);
            Cursor.Current = Cursors.Default;
        }

        private void LoadKeyFromFile(string fileName)
        {
            #region Load Answer Key
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() > 0)
                {
                    //ScantronToTestSectionMapping:(1.1,3)|(1.2,4)|(1.3,5)|(2.1,6)|(2.2,8)|(2.3,9)|(3.1,10)|(3.2,7)|Math=4,7,9|Reading=3,5,8|Writing=6,10
                    string line = sr.ReadLine();
                    if (line.Trim().Length > 1)
                    {
                        if (line.IndexOf(":") >= 0)
                        {
                            //Creat dictionary of mapping
                            string[] arr = line.Split(':');
                            string rawPair = arr[1].Replace("(", "").Replace(")", "");
                            string[] pairs = rawPair.Split('|');
                            int count = 1;
                            foreach (string pair in pairs)
                            {
                                if (count <= 8)
                                {
                                    string[] keys = pair.Split(',');
                                    dictSectionMapping.Add(keys[0], keys[1]);
                                }
                                else if (count == 9)
                                {
                                    //|Math=2,6,9|Reading=4,7,8|Writing=5,10                                    
                                    mathSections = pair;
                                }
                                else if (count == 10)
                                {
                                    readingSections = pair;
                                }
                                else if (count == 11)
                                {
                                    writingSections = pair;
                                }
                                count++;
                            }
                        }
                        else
                        {
                            QuestionAnswerKey qa = new QuestionAnswerKey(line);
                            dictAnswerKey.Add(qa.KeyReference, qa);
                        }
                    }                    
                }
                sr.Close();
            }
            #endregion

            #region Load MathTable, ReadingTable, WritingTable
            string folder = txtLookupFolder.Text + "\\";
            FillDictionary(dictMathTable, folder + "MathTable.txt");
            FillDictionary(dictReadingTable, folder + "ReadingTable.txt");
            FillDictionary(dictWritingTable, folder + "WritingTable.txt");
            #endregion

            #region Load Student2015
            FillDictionary(dictStudentId, folder + "Student2015.txt");
            #endregion

            #region Load EssayScore
            FillDictionaryNameId(dictStudentIdByEssayScore, txtEssayScore.Text);
            #endregion

        }

        private void FillDictionary(Dictionary<string, string> dict, string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show("File " + fileName + " does not exist.  Check file location and try again.");
                return;
            }

            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.Trim().Length > 1)
                    {
                        string[] keys = line.Split(',');
                        dict.Add(keys[0], keys[1]);
                    }
                }
                sr.Close();
            }
        }

        private void FillDictionaryNameId(Dictionary<string, string> dict, string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.Trim().Length > 1)
                    {
                        string[] keys = line.Split(',');
                        string key = keys[0];
                        if (key.IndexOf("|") >= 0)
                        {
                            string[] IdName = key.Split('|');
                            key = IdName[0];
                        }
                        dict.Add(key, keys[2]);
                    }
                }
                sr.Close();
            }
        }

        #region Sample Raw Data
        /*
            StudentID:117
            SheetNumber:1
            TestID:107
            CsvSection1:D,A,A,C,D,D,A,B,D,E,D,A,C,E,B,D,B,B,E,E,,,,,,,,,,,,,,,,,,,,
            CsvSection2:C,B,A,B,A,A,A,A,D,C,C,A,B,C,D,E,E,A,E,B,M,A,D,C,C,,,,,,,,,,,,,,,
            CsvSection3:D,E,C,C,B,C,C,A,D,A,A,B,D,C,B,C,D,C,E,B,C,D,A,E,C,D,E,C,E,E,C,C,A,E,B,,,,,
            
            StudentID:117
            SheetNumber:2
            TestID:107
            CsvSection1:D,B,C,E,A,D,B,C,B,E,E,E,B,B,C,A,B,E,B,E,D,B,E,D,,,,,,,,,,,,,,,,
            CsvSection2:B,D,C,C,B,C,A,A,A,C,A,E,B,E,D,E,,,,,,,,,,,,,,,,,,,,,,,,
            CsvSection3:C,B,A,E,A,E,A,A,C,D,D,C,B,C,A,D,E,C,,,,,,,,,,,,,,,,,,,,,,
            
            StudentID:117
            SheetNumber:3
            TestID:107
            CsvSection1:B,C,C,B,E,E,A,D,D,B,D,E,A,C,,,,,,
            CsvSection2:B,E,D,C,E,A,B,B,99/2,9135,32,1/15,1750,95,998,99,40,8/5
            CsvSection3:
            
            StudentID:128
            SheetNumber:1
            TestID:107
            CsvSection1:D,A,A,C,D,D,A,B,D,E,D,A,C,E,B,D,B,B,E,E,,,,,,,,,,,,,,,,,,,,
            CsvSection2:C,B,A,B,E,A,A,A,D,C,C,A,B,C,D,E,E,A,E,B,A,A,D,C,C,,,,,,,,,,,,,,,
            CsvSection3:D,E,C,C,B,C,C,A,C,A,A,B,D,C,B,C,D,C,E,B,C,D,A,E,C,D,B,C,E,E,C,C,A,E,B,,,,,
            
            StudentID:128
            SheetNumber:2
            TestID:107
            CsvSection1:D,C,D,E,A,D,B,C,B,E,C,E,B,B,D,C,B,E,B,E,D,B,E,D,,,,,,,,,,,,,,,,
            CsvSection2:B,D,C,C,B,C,A,A,A,C,A,E,B,E,D,E,,,,,,,,,,,,,,,,,,,,,,,,
            CsvSection3:C,B,C,D,A,A,C,A,C,D,B,C,B,D,E,D,D,C,,,,,,,,,,,,,,,,,,,,,,
         */
        #endregion
        //key by student:   key=studentID
        private Dictionary<string, StudentScore> dictStudents = new Dictionary<string, StudentScore>();
        private void btnGrade_Click(object sender, EventArgs e)
        {
                //LoadKeyFromFile(txtKeyFilePath.Text);
                //GradeIt();

            Cursor.Current = Cursors.WaitCursor;
            string outFile = "C:\\SATData\\Output.txt";

            QuickKeyAppGrading foo = new QuickKeyAppGrading("C:\\SATData\\", outFile);
            foo.RunIt();
            System.Diagnostics.Process.Start(outFile);
            Cursor.Current = Cursors.Default;
        }

        private void GradeIt()
        {
            Cursor.Current = Cursors.WaitCursor;
            //Load File To Grade
            string testID = "";
            string csv = "";
            using (StreamReader sr = new StreamReader(txtInputFile.Text))
            {
                string studentID = "";
                string sheetNumber = "";
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf("StudentID") >= 0)
                    {
                        studentID = ExtractValue(line);
                    }
                    else if (line.IndexOf("SheetNumber") >= 0)
                    {
                        sheetNumber = ExtractValue(line);
                    }
                    else if (line.IndexOf("TestID") >= 0)
                    {
                        testID = ExtractValue(line);
                    }
                    else if (line.IndexOf("CsvSection") >= 0)
                    {
                        csv = ExtractValue(line);
                        if (!string.IsNullOrEmpty(csv))
                        {
                            int colonPos = line.IndexOf(":");
                            string lastChar = line.Substring(colonPos - 1, 1);

                            string studentKey = studentID;
                            string sectionKey = sheetNumber + "." + lastChar;
                            StudentScore currentStudent = null;
                            if (!dictStudents.ContainsKey(studentKey))
                            {
                                currentStudent = new StudentScore(int.Parse(testID), int.Parse(studentID));
                                currentStudent.SetSectionCSV(sectionKey, csv);
                                currentStudent.SetSections(mathSections, readingSections, writingSections);
                                currentStudent.SetAdjustedScore(int.Parse(txtMath.Text), int.Parse(txtReading.Text), int.Parse(txtWriting.Text));
                                dictStudents.Add(studentID, currentStudent);
                            }
                            else
                            {
                                dictStudents[studentKey].SetSectionCSV(sectionKey, csv);
                            }
                        }
                    }
                }
                sr.Close();
            }

 
            //Now process all

            StreamWriter sw2 = new StreamWriter(txtOutputFile.Text + ".Summary.txt");

            using (StreamWriter sw = new StreamWriter(txtOutputFile.Text))
            {
                foreach (StudentScore student in dictStudents.Values)
                {
                    //try
                    //{
                        //Dictionary<string, string> dictSectionMapping = new Dictionary<string, string>();
                        //Dictionary<string, QuestionAnswer> dictAnswerKey = new Dictionary<string, QuestionAnswer>();
                        //student.Calculate(dictSectionMapping, dictAnswerKey);
                            //public void CalculateAndLookup(Dictionary<string, string> dictSectionMapping,
                                //Dictionary<string, string> dictMathTable,
                                //Dictionary<string, string> dictReadingTable,
                                //Dictionary<string, string> dictWritingTable,
                                //Dictionary<string, string> dictStudentID,
                                //Dictionary<string, string> dictStudentIdByEssayScore,
                                //Dictionary<string, QuestionAnswerKey> dictAnswerKey,
                                //int adjustedPoint)

                    student.CalculateAndLookup(dictSectionMapping, dictMathTable, dictReadingTable, dictWritingTable, dictStudentId, dictStudentIdByEssayScore, dictAnswerKey, int.Parse(txtAdjustedPoints.Text));
                        sw.WriteLine(student.ShowScore());
                        sw2.WriteLine(student.ShowScoreSummary());
                        student.CreateHtmlFile(txtHtmlRawRunning.Text, txtHtmlOutputFolder.Text, txtTestNumber.Text);
                    //}
                    //catch (Exception ex)
                    //{
                    //    error += "\r\n" + ex.ToString() + "\r\n" + student.StudentID;
                    //}
                }
                sw.Flush();
                sw.Close();
                sw2.Flush();
                sw2.Close();

            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Done");
        }

        private string ExtractValue(string input)
        {
            string[] arr = input.Split(':');
            return arr[1];
        }

        private void txtTestNumber_TextChanged(object sender, EventArgs e)
        {
            UpdateTestNumberTextBoxes();
        }

        private void UpdateTestNumberTextBoxes()
        {
            txtKeyFilePath.Text = "C:\\SATData\\Test" + txtTestNumber.Text + "Key.txt";
            txtInputFile.Text = "C:\\SATData\\Student" + txtTestNumber.Text + ".txt";
            txtEssayScore.Text = "C:\\SATData\\Student" + txtTestNumber.Text + "Essay.txt";
            txtTestID.Text = "12" + txtTestNumber.Text;
        }

        private void btnWriteStudentRawToDB_Click(object sender, EventArgs e)
        {
            //Go through the current file, get the information, write to DB
            Cursor.Current = Cursors.WaitCursor;
            int testID = int.Parse(txtTestNumber.Text);

            string csv = "";
            using (StreamReader sr = new StreamReader(txtInputFile.Text))
            {
                int studentID = -1;
                int sheetNumber = -1;
                int csvCount = 0;
                string section123 = "";
                string csv1 = "";
                string csv2 = "";
                string csv3 = "";


                //StudentID:344
                //SheetNumber:3
                //TestID:126
                //CsvSection1:D,B,B,C,E,D,C,E,D,C,B,E,C,D,,,,,,
                //CsvSection2:A,D,D,A,D,C,C,C,7.5,9,22,12,96,8,384,109,10,11
                //CsvSection3:
                //StudentID:344
                //SheetNumber:1
                //TestID:126
                //CsvSection1:A,B,E,E,B,D,D,A,B,A,E,C,D,D,B,B,E,B,A,D,,,,,,,,,,,,,,,,,,,,
                //CsvSection2:D,E,C,B,A,E,C,B,C,D,D,D,D,A,D,C,B,B,E,B,B,B,A,C,,,,,,,,,,,,,,,,
                //CsvSection3:B,D,C,B,E,B,B,B,C,E,A,B,E,C,B,B,E,E,B,E,E,E,C,C,D,E,D,D,E,D,E,B,C,C,E,,,,,
                //StudentID:344
                //SheetNumber:2
                //TestID:126
                //CsvSection1:B,A,B,C,B,B,D,B,A,E,B,E,D,C,A,A,D,E,C,B,D,A,B,D,,,,,,,,,,,,,,,,
                //CsvSection2:D,D,A,C,B,B,D,D,C,B,B,B,C,A,E,A,,,,,,,,,,,,,,,,,,,,,,,,
                //CsvSection3:D,B,A,C,A,C,A,C,B,A,A,B,E,B,B,D,A,E,D,,,,,,,,,,,,,,,,,,,,,


                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf("StudentID") >= 0)
                    {
                        //StudentID:344
                        studentID = int.Parse(ExtractValue(line));
                    }
                    else if (line.IndexOf("SheetNumber") >= 0)
                    {
                        //SheetNumber:2
                        sheetNumber = int.Parse(ExtractValue(line));
                    }
                    else if (line.IndexOf("TestID") >= 0)
                    {
                        //TestID:126
                        //testID = int.Parse(ExtractValue(line));
                        //testID = int.Parse(txtTestNumber.Text);
                    }
                    else if (line.IndexOf("CsvSection") >= 0)
                    {
                        //CsvSection1:A,B,E,E,B,D,D,A,B,A,E,C,D,D,B,B,E,B,A,D,,,,,,,,,,,,,,,,,,,,
                        //CsvSection2:D,E,C,B,A,E,C,B,C,D,D,D,D,A,D,C,B,B,E,B,B,B,A,C,,,,,,,,,,,,,,,,
                        //CsvSection3:B,D,C,B,E,B,B,B,C,E,A,B,E,C,B,B,E,E,B,E,E,E,C,C,D,E,D,D,E,D,E,B,C,C,E,,,,,

                        int colonPos = line.IndexOf(":");
                        section123 = line.Substring(colonPos - 1, 1);
                        csv = ExtractValue(line);
                        csvCount++;

                        if (csvCount == 1)
                        {
                            csv1 = csv;
                        }
                        else if (csvCount == 2)
                        {
                            csv2 = csv;
                        }
                        else
                        {
                            //WriteRawDataToDB(int studentID, int sheetNumber, int testID, string section1, string section2, string section3)
                            csv3 = csv;
                            WriteRawDataToDB(studentID, sheetNumber, testID, csv1, csv2, csv3);
                            csvCount = 0;
                        }
                    }
                }
                sr.Close();
            }

            Cursor.Current = Cursors.Default;


        }

        private void WriteRawDataToDB(int studentID, int sheetNumber, int testID, 
            string section1, string section2, string section3)
        {
            string connstr = @"Server=(local);DataBase=SAT;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connstr);

            //NC-19 Create SqlCommand and identify it as a stored procedure.
            SqlCommand cmdNewOrder = new SqlCommand("StudentTestRawAnswer_Insert", conn);
            cmdNewOrder.CommandType = CommandType.StoredProcedure;

            cmdNewOrder.Parameters.Add(new SqlParameter("@StudentID", SqlDbType.Int));
            cmdNewOrder.Parameters["@StudentID"].Value = studentID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@SheetNumber", SqlDbType.Int));
            cmdNewOrder.Parameters["@SheetNumber"].Value = sheetNumber;

            cmdNewOrder.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int));
            cmdNewOrder.Parameters["@TestID"].Value = testID;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Section1", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@Section1"].Value = section1;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Section2", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@Section2"].Value = section2;

            cmdNewOrder.Parameters.Add(new SqlParameter("@Section3", SqlDbType.VarChar));
            cmdNewOrder.Parameters["@Section3"].Value = section3;


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
            catch
            {
                //A simple catch.
                MessageBox.Show("Order could not be placed.");
            }
            finally
            {
                //Close connection.
                conn.Close();
            }
        }


        private void StudentTestAnswerInsert(int Test_TestID, int Student_StudentID, string KeyReference, 
            int ReferenceTestNumber, int ReferenceSectionNumber, int ReferenceQuestionNumber,
            string Answer, string CorrectAnswer, bool IsCorrected,
            bool IsSkip, bool IsFillin, decimal RawScore, int Subject_SubjectID)            
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

            cmdNewOrder.Parameters.Add(new SqlParameter("@RawScore", SqlDbType.Decimal));
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
                MessageBox.Show("Order could not be placed." + ex.ToString());
            }
            finally
            {
                //Close connection.
                conn.Close();
            }


        }

        private void buttonStudentTestAnswersInsert_Click(object sender, EventArgs e)
        {
            //StudentTestAnswerInsert(121, 343, "21.2.1", 21, 2, 1, "A", "A", true, false, false, decimal.Parse("1.0"), 1);

        }

        private void btnRunDB_Click(object sender, EventArgs e)
        {
            //LoadKeyFromFile(txtKeyFilePath.Text);
            //GradeItDB();
        }

        private void GradeItDB()
        {
            Cursor.Current = Cursors.WaitCursor;
            //Load File To Grade
            string testID = "";
            string csv = "";
            using (StreamReader sr = new StreamReader(txtInputFile.Text))
            {
                string studentID = "";
                string sheetNumber = "";
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    if (line.IndexOf("StudentID") >= 0)
                    {
                        studentID = ExtractValue(line);
                    }
                    else if (line.IndexOf("SheetNumber") >= 0)
                    {
                        sheetNumber = ExtractValue(line);
                    }
                    else if (line.IndexOf("TestID") >= 0)
                    {
                        testID = ExtractValue(line);
                    }
                    else if (line.IndexOf("CsvSection") >= 0)
                    {
                        csv = ExtractValue(line);
                        if (!string.IsNullOrEmpty(csv))
                        {
                            int colonPos = line.IndexOf(":");
                            string lastChar = line.Substring(colonPos - 1, 1);

                            string studentKey = studentID;
                            string sectionKey = sheetNumber + "." + lastChar;
                            StudentScore currentStudent = null;
                            if (!dictStudents.ContainsKey(studentKey))
                            {
                                currentStudent = new StudentScore(int.Parse(testID), int.Parse(studentID));
                                currentStudent.SetSectionCSV(sectionKey, csv);
                                currentStudent.SetSections(mathSections, readingSections, writingSections);
                                currentStudent.SetAdjustedScore(int.Parse(txtMath.Text), int.Parse(txtReading.Text), int.Parse(txtWriting.Text));
                                dictStudents.Add(studentID, currentStudent);
                            }
                            else
                            {
                                dictStudents[studentKey].SetSectionCSV(sectionKey, csv);
                            }
                        }
                    }
                }
                sr.Close();
            }

            //Now process all

            foreach (StudentScore student in dictStudents.Values)
            {
                student.CalculateAndLookup(dictSectionMapping, dictMathTable, dictReadingTable, dictWritingTable, dictStudentId, dictStudentIdByEssayScore, dictAnswerKey, int.Parse(txtAdjustedPoints.Text));
                
                //sw.WriteLine(student.ShowScore());
                //sw2.WriteLine(student.ShowScoreSummary());
                //student.CreateHtmlFile(txtHtmlRawRunning.Text, txtHtmlOutputFolder.Text, txtTestNumber.Text);

            }
      
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Done");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
    }
}
