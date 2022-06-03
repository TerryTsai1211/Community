namespace ScoreSample
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbAnalyze = new System.Windows.Forms.GroupBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChinese = new System.Windows.Forms.TextBox();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.txtMath = new System.Windows.Forms.TextBox();
            this.plSubject = new System.Windows.Forms.Panel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.plSubject.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbAnalyze
            // 
            this.gbAnalyze.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gbAnalyze.Location = new System.Drawing.Point(258, 12);
            this.gbAnalyze.Name = "gbAnalyze";
            this.gbAnalyze.Size = new System.Drawing.Size(312, 141);
            this.gbAnalyze.TabIndex = 1;
            this.gbAnalyze.TabStop = false;
            this.gbAnalyze.Text = "分析結果";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAnalyze.Location = new System.Drawing.Point(109, 159);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(100, 38);
            this.btnAnalyze.TabIndex = 2;
            this.btnAnalyze.Text = "分析";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "國文：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(16, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 27);
            this.label2.TabIndex = 4;
            this.label2.Text = "英文：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(16, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "數學：";
            // 
            // txtChinese
            // 
            this.txtChinese.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtChinese.Location = new System.Drawing.Point(97, 18);
            this.txtChinese.Name = "txtChinese";
            this.txtChinese.Size = new System.Drawing.Size(100, 35);
            this.txtChinese.TabIndex = 6;
            // 
            // txtEnglish
            // 
            this.txtEnglish.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEnglish.Location = new System.Drawing.Point(97, 59);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(100, 35);
            this.txtEnglish.TabIndex = 7;
            // 
            // txtMath
            // 
            this.txtMath.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMath.Location = new System.Drawing.Point(97, 97);
            this.txtMath.Name = "txtMath";
            this.txtMath.Size = new System.Drawing.Size(100, 35);
            this.txtMath.TabIndex = 8;
            // 
            // plSubject
            // 
            this.plSubject.Controls.Add(this.txtChinese);
            this.plSubject.Controls.Add(this.label3);
            this.plSubject.Controls.Add(this.txtMath);
            this.plSubject.Controls.Add(this.txtEnglish);
            this.plSubject.Controls.Add(this.label1);
            this.plSubject.Controls.Add(this.label2);
            this.plSubject.Location = new System.Drawing.Point(12, 12);
            this.plSubject.Name = "plSubject";
            this.plSubject.Size = new System.Drawing.Size(217, 141);
            this.plSubject.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 233);
            this.Controls.Add(this.plSubject);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.gbAnalyze);
            this.Name = "Form1";
            this.Text = "分數練習";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.plSubject.ResumeLayout(false);
            this.plSubject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private GroupBox gbAnalyze;
        private Button btnAnalyze;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtChinese;
        private TextBox txtEnglish;
        private TextBox txtMath;
        private Panel plSubject;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}