using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SATGrading
{
    public class QuestionAnswerKey
    {
        public string KeyReference;
        public int SectionNumber;
        public int QuestionNumber;
        public bool IsFillIn;
        public string AnswerRawForm;
        public string AnswerSimple;  //A,B,C,D,E
        public string AnswerRange;   //decimal between lower and upper bound
        public string AnswerChoice;  //the choices comes from a list        
        public string Category;
        public double AnswerRangeLowerBound;
        public double AnswerRangeUpperBound;
        public Dictionary<string, bool> AnswerChoiceList;
        public string LineInput;

        public QuestionAnswerKey()
        {
            AnswerChoiceList = new Dictionary<string, bool>();
        }

        //SectionNumber|QuestionNumber|IsFillIn|Answer1|Answer2.FillIn|Category
        //3|1|0|E||2
        //7|11|1||[.166,.167]|1
        //7|13|1||115|1
        //7|13|1||(1,3,5)|1
        public QuestionAnswerKey(string  line)
        {
            if (AnswerChoiceList == null)
                AnswerChoiceList = new Dictionary<string, bool>();

            LineInput = line;


            string[] arr = line.Split('|');
            SectionNumber = int.Parse(arr[0]);
            QuestionNumber = int.Parse(arr[1]);
            IsFillIn = (arr[2] == "0") ? false : true;
            AnswerRawForm = (!IsFillIn) ? arr[3] : arr[4];
            Category = arr[5];
            KeyReference = SectionNumber + "." + QuestionNumber;

            if (IsFillIn)
            {
                if (AnswerRawForm.IndexOf("[") >= 0)
                {
                    //Range - AnswerRangeLowerBound, AnswerRangeUpperBound
                    AnswerRange = AnswerRawForm;
                    string[] data = AnswerRawForm.Replace("[", "").Replace("]", "").Split(',');
                    AnswerRangeLowerBound = double.Parse(data[0]);
                    AnswerRangeUpperBound = double.Parse(data[1]);
                }
                else if (AnswerRawForm.IndexOf("(") >= 0)
                {
                    //ListSeparated csv
                    string[] data = AnswerRawForm.Replace("(", "").Replace(")", "").Split(',');
                    foreach (string temp in data)
                    {
                        AnswerChoiceList.Add(temp, true);
                    }
                }
                else
                {
                    //Single value - use AnswerRawForm  AnswerRawForm
                }
            }

        }

    }
}
