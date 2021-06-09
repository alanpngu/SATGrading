using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SATGrading
{
    public class StudentScoreItem
    {
        private string mStudentName;
        private int mStudentID;

        public int StudentID
        {
            get { return mStudentID; }
            set { mStudentID = value; }
        }

        private int mRawMath;

        public int RawMath
        {
            get { return mRawMath; }
            set { mRawMath = value; }
        }
        private int mRawWriting;

        public int RawWriting
        {
            get { return mRawWriting; }
            set { mRawWriting = value; }
        }
        private int mRawReading;

        public int RawReading
        {
            get { return mRawReading; }
            set { mRawReading = value; }
        }

        private int mTranslatedMath;

        public int TranslatedMath
        {
            get { return mTranslatedMath; }
            set { mTranslatedMath = value; }
        }
        private int mTranslatedWriting;

        public int TranslatedWriting
        {
            get { return mTranslatedWriting; }
            set { mTranslatedWriting = value; }
        }
        private int mTranslatedReading;

        public int TranslatedReading
        {
            get { return mTranslatedReading; }
            set { mTranslatedReading = value; }
        }
        private int mTranslatedEnglish;

        public int TranslatedEnglish
        {
            get { return mTranslatedEnglish; }
            set { mTranslatedEnglish = value; }
        }

        public void SetTranslatedMath(int translMath)
        {
            this.TranslatedMath = translMath;
        }

        public void SetTranslatedWriting(int translWriting)
        {
            this.TranslatedWriting = translWriting;
        }

        public void SetTranslatedReading(int translReading)
        {
            this.TranslatedReading = translReading;
        }

        private int mFinalScore;

        public int FinalScore
        {
            get { return mFinalScore; }
            set { mFinalScore = value; }
        }

        public string StudentName
        {
            get { return mStudentName; }
            set { mStudentName = value; }
        }

        public StudentScoreItem()
        {
        }

        public StudentScoreItem(string studentName, int studentID)
        {
            mStudentName = studentName;
            mStudentID = studentID;
        }

        public string ToString()
        {
            this.TranslatedWriting = ScoreMap.Writing(RawWriting);
            this.TranslatedReading = ScoreMap.Reading(RawReading);
            this.TranslatedMath = ScoreMap.Math(RawMath);

            int EnglishScore = this.TranslatedWriting + this.TranslatedReading;            
            mFinalScore = EnglishScore + TranslatedMath;

            return mStudentName + "," + mStudentID + "," + TranslatedReading + "," + TranslatedWriting + "," + EnglishScore + "," + TranslatedMath + "," + mFinalScore;
        }

    }
}
