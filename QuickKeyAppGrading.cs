using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SATGrading
{
    public class QuickKeyAppGrading
    {
        const string S1_FILENAME = "S1.csv";
        const string S2_FILENAME = "S2.csv";
        const string S3A_FILENAME = "S3A.csv";
        const string S3B_FILENAME = "S3B.csv";
        const string S4A_FILENAME = "S4A.csv";
        const string S4B_FILENAME = "S4B.csv";
        const string S4C_FILENAME = "S4C.csv";

        private string mInputFolder;

        public string InputFolder
        {
            get { return mInputFolder; }
            set { mInputFolder = value; }
        }
        private string mOutputFileName;

        public string OutputFileName
        {
            get { return mOutputFileName; }
            set { mOutputFileName = value; }
        }
        private string mError;

        public string Error
        {
            get { return mError; }
            set { mError = value; }
        }

        private List<StudentScoreItem> mScoreList = new List<StudentScoreItem>();

        public QuickKeyAppGrading()
        {            
        }

        public QuickKeyAppGrading(string inputFolder, string outputFileName)
        {
            //Make sure the folder has trailing backslash - add if needed
            mInputFolder = (inputFolder.EndsWith("\\")) ? inputFolder : inputFolder + "\\";
            mOutputFileName = outputFileName;            
        }

        public void RunIt()
        {
            if (!IsAllFileSExist())
            {
                Error = "One of the files does not exist.";
                return;
            }

            Step1_ParseS1Reading();
            Step2_ParseS2Writing();
            Step3_ParseS3Math3();
            Step4_ParseS4Math4();
            CreateOutputFile();
        }

        private bool IsAllFileSExist()
        {
            bool exists = false;
            if (IsFileExists(S1_FILENAME) &&
                IsFileExists(S2_FILENAME) &&
                IsFileExists(S3A_FILENAME) &&
                IsFileExists(S3B_FILENAME) &&
                IsFileExists(S4A_FILENAME) &&
                IsFileExists(S4B_FILENAME) &&
                IsFileExists(S4C_FILENAME))
                exists = true;
            return exists;
        }

        private bool IsFileExists(string fileName)
        {
            return File.Exists(BuildFullFileName(fileName));
        }

        private void Step1_ParseS1Reading()
        {
            //Question Number,"","","",1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,# Wrong,# Correct,% Score
            //Brian Ramos,1041,1041,All My Students,-,-,-,-,-,-,-,-,-,-,-,-,-,C,D,-,-,-,-,C,-,-,-,-,-,-,-,-,B,-,-,-,-,D,-,-,-,-,-,-,-,-,-,-,-,C,B,-,-,-,-,-,7.0,45.0,86.5%
            //Tran Thomas,1012,1012,All My Students,-,-,-,C,-,-,-,B,-,B,-,-,C,-,-,-,-,-,-,-,-,-,B,-,-,D,-,A,-,-,-,-,-,D,-,D,D,-,A,-,C,-,-,B,B,-,-,B,D,D,-,C,18.0,34.0,65.4%

            //Assume the file name is S1.csv

            using (StreamReader sr = new StreamReader(BuildFullFileName(S1_FILENAME)))
            {
                int lineCount = 0;
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine().Trim();
                    lineCount++;
                    if (lineCount < 8 || line.Length == 0)
                        continue;

                    string[] array = line.Split(',');
                    int commaCount = array.Length;

                    //Create new item with StudentName and StudentID
                    string studentName = array[0];
                    int studentID = int.Parse(array[1]);
                    string sCorrect = array[commaCount - 2].Replace(".0", ""); ;
                    int correct = int.Parse(sCorrect);

                    StudentScoreItem newItem = new StudentScoreItem(studentName, studentID);
                    newItem.RawReading = correct;                    
                    mScoreList.Add(newItem);
                }
                sr.Close();
            }

        }

        private void Step2_ParseS2Writing()
        {
            //Similar to Step1_ParseS1Reading() but updat itme instead of creating new one
            using (StreamReader sr = new StreamReader(BuildFullFileName(S2_FILENAME)))
            {
                int lineCount = 0;
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine().Trim();
                    lineCount++;
                    if (lineCount < 8 || line.Length == 0)
                        continue;

                    string[] array = line.Split(',');
                    int commaCount = array.Length;

                    //Create new item with StudentName and StudentID
                    string studentName = array[0];
                    int studentID = int.Parse(array[1]);
                    string sCorrect = array[commaCount - 2].Replace(".0", ""); ;
                    int correct = int.Parse(sCorrect);
                    
                    StudentScoreItem item = FindItemByStudentID(studentName, studentID);
                    item.RawWriting = correct;
                }
                sr.Close();
            }
        }

        private StudentScoreItem FindItemByStudentID(string studentName, int studentID)
        {
            StudentScoreItem itemReturned = null;
            foreach (StudentScoreItem item in mScoreList)
            {
                if (item.StudentID == studentID)
                {
                    itemReturned = item;
                    break;
                }
            }

            if (itemReturned == null)
                itemReturned = new StudentScoreItem(studentName, studentID);

            return itemReturned;
        }


        private void Step3_ParseS3Math3()
        {
            CalculateMath(S3A_FILENAME);
            CalculateMath(S3B_FILENAME);
        }

        private void Step4_ParseS4Math4()
        {
            CalculateMath(S4A_FILENAME);
            CalculateMath(S4B_FILENAME);
            CalculateMath(S4C_FILENAME);            
        }

        private void CalculateMath(string fileName)
        {
            //Similar to Step1_ParseS1Reading() but updat itme instead of creating new one
            using (StreamReader sr = new StreamReader(BuildFullFileName(fileName)))
            {
                int lineCount = 0;
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine().Trim();
                    lineCount++;
                    if (lineCount < 8 || line.Length == 0)
                        continue;

                    string[] array = line.Split(',');
                    int commaCount = array.Length;

                    //Create new item with StudentName and StudentID
                    string studentName = array[0];
                    int studentID = int.Parse(array[1]);

                    if (studentID == 1012)
                        mError = "";

                    string sCorrect = array[commaCount - 2].Replace(".0", ""); ;
                    int correct = int.Parse(sCorrect);

                    StudentScoreItem item = FindItemByStudentID(studentName, studentID);
                    item.RawMath += correct;
                }
                sr.Close();
            }
        }


        private string BuildFullFileName(string fileName)
        {
            return mInputFolder + fileName;
        }

        private void CreateOutputFile()
        {
            if (mScoreList.Count == 0)
            {
                mError = "Empty list";
                return;
            }

            using (StreamWriter sw = new StreamWriter(mOutputFileName))
            {
                foreach (StudentScoreItem item in mScoreList)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.Flush();
                sw.Close();
            }

        }

    }
}
