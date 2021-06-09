using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SATGrading
{
    public static class ScoreMap
    {
        public static Dictionary<int, int> mLookupMath = new Dictionary<int, int>();
        public static Dictionary<int, int> mLookupWriting = new Dictionary<int, int>();
        public static Dictionary<int, int> mLookupReading = new Dictionary<int, int>();

        static ScoreMap()
        {
            InitMaps();
        }

        public static int Math(int key)
        {
            return mLookupMath[key];
        }

        public static int Writing(int key)
        {
            return mLookupWriting[key];
        }

        public static int Reading(int key)
        {
            return mLookupReading[key];
        }

        private static void InitMaps()
        {
            #region Math
            mLookupMath.Add(0, 200);
            mLookupMath.Add(1, 200);
            mLookupMath.Add(2, 210);
            mLookupMath.Add(3, 230);
            mLookupMath.Add(4, 240);
            mLookupMath.Add(5, 260);
            mLookupMath.Add(6, 280);
            mLookupMath.Add(7, 290);
            mLookupMath.Add(8, 310);
            mLookupMath.Add(9, 320);
            mLookupMath.Add(10, 330);
            mLookupMath.Add(11, 340);
            mLookupMath.Add(12, 360);
            mLookupMath.Add(13, 370);
            mLookupMath.Add(14, 380);
            mLookupMath.Add(15, 390);
            mLookupMath.Add(16, 410);
            mLookupMath.Add(17, 420);
            mLookupMath.Add(18, 430);
            mLookupMath.Add(19, 440);
            mLookupMath.Add(20, 450);
            mLookupMath.Add(21, 460);
            mLookupMath.Add(22, 470);
            mLookupMath.Add(23, 480);
            mLookupMath.Add(24, 480);
            mLookupMath.Add(25, 490);
            mLookupMath.Add(26, 500);
            mLookupMath.Add(27, 510);
            mLookupMath.Add(28, 520);
            mLookupMath.Add(29, 520);
            mLookupMath.Add(30, 530);
            mLookupMath.Add(31, 540);
            mLookupMath.Add(32, 550);
            mLookupMath.Add(33, 560);
            mLookupMath.Add(34, 560);
            mLookupMath.Add(35, 570);
            mLookupMath.Add(36, 580);
            mLookupMath.Add(37, 590);
            mLookupMath.Add(38, 600);
            mLookupMath.Add(39, 600);
            mLookupMath.Add(40, 610);
            mLookupMath.Add(41, 620);
            mLookupMath.Add(42, 630);
            mLookupMath.Add(43, 640);
            mLookupMath.Add(44, 650);
            mLookupMath.Add(45, 660);
            mLookupMath.Add(46, 670);
            mLookupMath.Add(47, 670);
            mLookupMath.Add(48, 680);
            mLookupMath.Add(49, 690);
            mLookupMath.Add(50, 700);
            mLookupMath.Add(51, 710);
            mLookupMath.Add(52, 730);
            mLookupMath.Add(53, 740);
            mLookupMath.Add(54, 750);
            mLookupMath.Add(55, 760);
            mLookupMath.Add(56, 780);
            mLookupMath.Add(57, 790);
            mLookupMath.Add(58, 800);
            #endregion

            #region writing
            mLookupWriting.Add(0, 100);
            mLookupWriting.Add(1, 100);
            mLookupWriting.Add(2, 100);
            mLookupWriting.Add(3, 100);
            mLookupWriting.Add(4, 110);
            mLookupWriting.Add(5, 120);
            mLookupWriting.Add(6, 130);
            mLookupWriting.Add(7, 130);
            mLookupWriting.Add(8, 140);
            mLookupWriting.Add(9, 150);
            mLookupWriting.Add(10, 160);
            mLookupWriting.Add(11, 160);
            mLookupWriting.Add(12, 170);
            mLookupWriting.Add(13, 180);
            mLookupWriting.Add(14, 190);
            mLookupWriting.Add(15, 190);
            mLookupWriting.Add(16, 200);
            mLookupWriting.Add(17, 210);
            mLookupWriting.Add(18, 210);
            mLookupWriting.Add(19, 220);
            mLookupWriting.Add(20, 230);
            mLookupWriting.Add(21, 230);
            mLookupWriting.Add(22, 240);
            mLookupWriting.Add(23, 250);
            mLookupWriting.Add(24, 250);
            mLookupWriting.Add(25, 260);
            mLookupWriting.Add(26, 260);
            mLookupWriting.Add(27, 270);
            mLookupWriting.Add(28, 280);
            mLookupWriting.Add(29, 290);
            mLookupWriting.Add(30, 290);
            mLookupWriting.Add(31, 300);
            mLookupWriting.Add(32, 300);
            mLookupWriting.Add(33, 310);
            mLookupWriting.Add(34, 320);
            mLookupWriting.Add(35, 320);
            mLookupWriting.Add(36, 330);
            mLookupWriting.Add(37, 340);
            mLookupWriting.Add(38, 340);
            mLookupWriting.Add(39, 350);
            mLookupWriting.Add(40, 360);
            mLookupWriting.Add(41, 370);
            mLookupWriting.Add(42, 380);
            mLookupWriting.Add(43, 390);
            mLookupWriting.Add(44, 400);

            #endregion

            #region reading
            mLookupReading.Add(0, 100);
            mLookupReading.Add(1, 100);
            mLookupReading.Add(2, 100);
            mLookupReading.Add(3, 110);
            mLookupReading.Add(4, 120);
            mLookupReading.Add(5, 130);
            mLookupReading.Add(6, 140);
            mLookupReading.Add(7, 150);
            mLookupReading.Add(8, 150);
            mLookupReading.Add(9, 160);
            mLookupReading.Add(10, 170);
            mLookupReading.Add(11, 170);
            mLookupReading.Add(12, 180);
            mLookupReading.Add(13, 190);
            mLookupReading.Add(14, 190);
            mLookupReading.Add(15, 200);
            mLookupReading.Add(16, 200);
            mLookupReading.Add(17, 210);
            mLookupReading.Add(18, 210);
            mLookupReading.Add(19, 220);
            mLookupReading.Add(20, 220);
            mLookupReading.Add(21, 230);
            mLookupReading.Add(22, 230);
            mLookupReading.Add(23, 240);
            mLookupReading.Add(24, 240);
            mLookupReading.Add(25, 250);
            mLookupReading.Add(26, 250);
            mLookupReading.Add(27, 260);
            mLookupReading.Add(28, 260);
            mLookupReading.Add(29, 270);
            mLookupReading.Add(30, 280);
            mLookupReading.Add(31, 280);
            mLookupReading.Add(32, 290);
            mLookupReading.Add(33, 292);
            mLookupReading.Add(34, 300);
            mLookupReading.Add(35, 300);
            mLookupReading.Add(36, 310);
            mLookupReading.Add(37, 310);
            mLookupReading.Add(38, 320);
            mLookupReading.Add(39, 320);
            mLookupReading.Add(40, 330);
            mLookupReading.Add(41, 330);
            mLookupReading.Add(42, 340);
            mLookupReading.Add(43, 350);
            mLookupReading.Add(44, 350);
            mLookupReading.Add(45, 360);
            mLookupReading.Add(46, 370);
            mLookupReading.Add(47, 370);
            mLookupReading.Add(48, 380);
            mLookupReading.Add(49, 380);
            mLookupReading.Add(50, 390);
            mLookupReading.Add(51, 400);
            mLookupReading.Add(52, 400);
            #endregion
        }

    }
}
