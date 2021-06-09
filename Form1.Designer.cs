namespace SATGrading
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeyFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnLoadKeyFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.btnGrade = new System.Windows.Forms.Button();
            this.lstOutput = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLookupFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEssayScore = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHtmlRawRunning = new System.Windows.Forms.TextBox();
            this.txtHtmlOutputFolder = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTestNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMath = new System.Windows.Forms.TextBox();
            this.txtReading = new System.Windows.Forms.TextBox();
            this.txtWriting = new System.Windows.Forms.TextBox();
            this.btnWriteStudentRawToDB = new System.Windows.Forms.Button();
            this.buttonStudentTestAnswersInsert = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTestID = new System.Windows.Forms.TextBox();
            this.btnRunDB = new System.Windows.Forms.Button();
            this.txtAdjustedPoints = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 132);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key/Answer File:";
            // 
            // txtKeyFilePath
            // 
            this.txtKeyFilePath.Location = new System.Drawing.Point(192, 130);
            this.txtKeyFilePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyFilePath.Name = "txtKeyFilePath";
            this.txtKeyFilePath.Size = new System.Drawing.Size(337, 22);
            this.txtKeyFilePath.TabIndex = 1;
            this.txtKeyFilePath.Text = "C:\\SATData\\Test1Key.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Input File:";
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(192, 162);
            this.txtInputFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(337, 22);
            this.txtInputFile.TabIndex = 3;
            this.txtInputFile.Tag = "";
            this.txtInputFile.Text = "C:\\SATData\\Student1.txt";
            // 
            // btnLoadKeyFile
            // 
            this.btnLoadKeyFile.Location = new System.Drawing.Point(529, 345);
            this.btnLoadKeyFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnLoadKeyFile.Name = "btnLoadKeyFile";
            this.btnLoadKeyFile.Size = new System.Drawing.Size(103, 33);
            this.btnLoadKeyFile.TabIndex = 4;
            this.btnLoadKeyFile.Text = "Load Key";
            this.btnLoadKeyFile.UseVisualStyleBackColor = true;
            this.btnLoadKeyFile.Visible = false;
            this.btnLoadKeyFile.Click += new System.EventHandler(this.btnLoadKeyFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 233);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Output File:";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(192, 231);
            this.txtOutputFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(337, 22);
            this.txtOutputFile.TabIndex = 6;
            this.txtOutputFile.Text = "C:\\SATData\\Output.txt";
            // 
            // btnGrade
            // 
            this.btnGrade.Location = new System.Drawing.Point(532, 385);
            this.btnGrade.Margin = new System.Windows.Forms.Padding(4);
            this.btnGrade.Name = "btnGrade";
            this.btnGrade.Size = new System.Drawing.Size(103, 33);
            this.btnGrade.TabIndex = 7;
            this.btnGrade.Text = "Run";
            this.btnGrade.UseVisualStyleBackColor = true;
            this.btnGrade.Click += new System.EventHandler(this.btnGrade_Click);
            // 
            // lstOutput
            // 
            this.lstOutput.FormattingEnabled = true;
            this.lstOutput.HorizontalScrollbar = true;
            this.lstOutput.ItemHeight = 16;
            this.lstOutput.Location = new System.Drawing.Point(37, 345);
            this.lstOutput.Margin = new System.Windows.Forms.Padding(4);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new System.Drawing.Size(459, 164);
            this.lstOutput.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lookup Folder:";
            // 
            // txtLookupFolder
            // 
            this.txtLookupFolder.Location = new System.Drawing.Point(192, 14);
            this.txtLookupFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtLookupFolder.Name = "txtLookupFolder";
            this.txtLookupFolder.Size = new System.Drawing.Size(337, 22);
            this.txtLookupFolder.TabIndex = 10;
            this.txtLookupFolder.Tag = "";
            this.txtLookupFolder.Text = "C:\\SATData";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 42);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(421, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "MathTable.txt, ReadingTable.txt, WritingTable.txt, Student2015.txt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 192);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Essay Score:";
            // 
            // txtEssayScore
            // 
            this.txtEssayScore.Location = new System.Drawing.Point(192, 194);
            this.txtEssayScore.Margin = new System.Windows.Forms.Padding(4);
            this.txtEssayScore.Name = "txtEssayScore";
            this.txtEssayScore.Size = new System.Drawing.Size(337, 22);
            this.txtEssayScore.TabIndex = 13;
            this.txtEssayScore.Tag = "";
            this.txtEssayScore.Text = "C:\\SATData\\Student1Essay.txt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 272);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "HTML Raw Running:";
            // 
            // txtHtmlRawRunning
            // 
            this.txtHtmlRawRunning.Location = new System.Drawing.Point(192, 268);
            this.txtHtmlRawRunning.Margin = new System.Windows.Forms.Padding(4);
            this.txtHtmlRawRunning.Name = "txtHtmlRawRunning";
            this.txtHtmlRawRunning.Size = new System.Drawing.Size(337, 22);
            this.txtHtmlRawRunning.TabIndex = 15;
            this.txtHtmlRawRunning.Text = "C:\\SATData\\2015Scores\\RawDataAppending\\";
            // 
            // txtHtmlOutputFolder
            // 
            this.txtHtmlOutputFolder.Location = new System.Drawing.Point(192, 300);
            this.txtHtmlOutputFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtHtmlOutputFolder.Name = "txtHtmlOutputFolder";
            this.txtHtmlOutputFolder.Size = new System.Drawing.Size(337, 22);
            this.txtHtmlOutputFolder.TabIndex = 17;
            this.txtHtmlOutputFolder.Text = "C:\\SATData\\2015Scores\\HTMLOutput\\";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 304);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "HTML Output Folder:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 62);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 17);
            this.label9.TabIndex = 18;
            this.label9.Text = "Test Number:";
            // 
            // txtTestNumber
            // 
            this.txtTestNumber.Location = new System.Drawing.Point(192, 62);
            this.txtTestNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtTestNumber.Name = "txtTestNumber";
            this.txtTestNumber.Size = new System.Drawing.Size(36, 22);
            this.txtTestNumber.TabIndex = 19;
            this.txtTestNumber.Text = "7";
            this.txtTestNumber.TextChanged += new System.EventHandler(this.txtTestNumber_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(360, 81);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "Adjusted M/R/W";
            // 
            // txtMath
            // 
            this.txtMath.Location = new System.Drawing.Point(487, 78);
            this.txtMath.Margin = new System.Windows.Forms.Padding(4);
            this.txtMath.Name = "txtMath";
            this.txtMath.Size = new System.Drawing.Size(36, 22);
            this.txtMath.TabIndex = 21;
            this.txtMath.Text = "30";
            // 
            // txtReading
            // 
            this.txtReading.Location = new System.Drawing.Point(532, 78);
            this.txtReading.Margin = new System.Windows.Forms.Padding(4);
            this.txtReading.Name = "txtReading";
            this.txtReading.Size = new System.Drawing.Size(36, 22);
            this.txtReading.TabIndex = 22;
            this.txtReading.Text = "30";
            // 
            // txtWriting
            // 
            this.txtWriting.Location = new System.Drawing.Point(577, 78);
            this.txtWriting.Margin = new System.Windows.Forms.Padding(4);
            this.txtWriting.Name = "txtWriting";
            this.txtWriting.Size = new System.Drawing.Size(36, 22);
            this.txtWriting.TabIndex = 23;
            this.txtWriting.Text = "30";
            // 
            // btnWriteStudentRawToDB
            // 
            this.btnWriteStudentRawToDB.Location = new System.Drawing.Point(40, 517);
            this.btnWriteStudentRawToDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnWriteStudentRawToDB.Name = "btnWriteStudentRawToDB";
            this.btnWriteStudentRawToDB.Size = new System.Drawing.Size(289, 33);
            this.btnWriteStudentRawToDB.TabIndex = 24;
            this.btnWriteStudentRawToDB.Text = "Write Student Raw Answer to DB";
            this.btnWriteStudentRawToDB.UseVisualStyleBackColor = true;
            this.btnWriteStudentRawToDB.Click += new System.EventHandler(this.btnWriteStudentRawToDB_Click);
            // 
            // buttonStudentTestAnswersInsert
            // 
            this.buttonStudentTestAnswersInsert.Location = new System.Drawing.Point(40, 558);
            this.buttonStudentTestAnswersInsert.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStudentTestAnswersInsert.Name = "buttonStudentTestAnswersInsert";
            this.buttonStudentTestAnswersInsert.Size = new System.Drawing.Size(289, 33);
            this.buttonStudentTestAnswersInsert.TabIndex = 25;
            this.buttonStudentTestAnswersInsert.Text = "StudentTestAnswers_Insert";
            this.buttonStudentTestAnswersInsert.UseVisualStyleBackColor = true;
            this.buttonStudentTestAnswersInsert.Click += new System.EventHandler(this.buttonStudentTestAnswersInsert_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 97);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 17);
            this.label11.TabIndex = 26;
            this.label11.Text = "TestID:";
            // 
            // txtTestID
            // 
            this.txtTestID.Location = new System.Drawing.Point(192, 94);
            this.txtTestID.Margin = new System.Windows.Forms.Padding(4);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.Size = new System.Drawing.Size(36, 22);
            this.txtTestID.TabIndex = 27;
            this.txtTestID.Text = "127";
            // 
            // btnRunDB
            // 
            this.btnRunDB.Location = new System.Drawing.Point(529, 426);
            this.btnRunDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunDB.Name = "btnRunDB";
            this.btnRunDB.Size = new System.Drawing.Size(103, 33);
            this.btnRunDB.TabIndex = 28;
            this.btnRunDB.Text = "Run DB";
            this.btnRunDB.UseVisualStyleBackColor = true;
            this.btnRunDB.Click += new System.EventHandler(this.btnRunDB_Click);
            // 
            // txtAdjustedPoints
            // 
            this.txtAdjustedPoints.Location = new System.Drawing.Point(517, 530);
            this.txtAdjustedPoints.Margin = new System.Windows.Forms.Padding(4);
            this.txtAdjustedPoints.Name = "txtAdjustedPoints";
            this.txtAdjustedPoints.Size = new System.Drawing.Size(36, 22);
            this.txtAdjustedPoints.TabIndex = 30;
            this.txtAdjustedPoints.Text = "10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(403, 534);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 17);
            this.label12.TabIndex = 29;
            this.label12.Text = "Adjusted Points";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 635);
            this.Controls.Add(this.txtAdjustedPoints);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnRunDB);
            this.Controls.Add(this.txtTestID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonStudentTestAnswersInsert);
            this.Controls.Add(this.btnWriteStudentRawToDB);
            this.Controls.Add(this.txtWriting);
            this.Controls.Add(this.txtReading);
            this.Controls.Add(this.txtMath);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTestNumber);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtHtmlOutputFolder);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtHtmlRawRunning);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEssayScore);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLookupFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstOutput);
            this.Controls.Add(this.btnGrade);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLoadKeyFile);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKeyFilePath);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeyFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Button btnLoadKeyFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button btnGrade;
        private System.Windows.Forms.ListBox lstOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLookupFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEssayScore;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHtmlRawRunning;
        private System.Windows.Forms.TextBox txtHtmlOutputFolder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTestNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMath;
        private System.Windows.Forms.TextBox txtReading;
        private System.Windows.Forms.TextBox txtWriting;
        private System.Windows.Forms.Button btnWriteStudentRawToDB;
        private System.Windows.Forms.Button buttonStudentTestAnswersInsert;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTestID;
        private System.Windows.Forms.Button btnRunDB;
        private System.Windows.Forms.TextBox txtAdjustedPoints;
        private System.Windows.Forms.Label label12;
    }
}

