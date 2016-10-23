namespace FacultyApp
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
            this.scrapeButton = new System.Windows.Forms.Button();
            this.resultText = new System.Windows.Forms.TextBox();
            this.facultyCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // scrapeButton
            // 
            this.scrapeButton.Location = new System.Drawing.Point(245, 119);
            this.scrapeButton.Name = "scrapeButton";
            this.scrapeButton.Size = new System.Drawing.Size(322, 112);
            this.scrapeButton.TabIndex = 0;
            this.scrapeButton.Text = "Scrape!";
            this.scrapeButton.UseVisualStyleBackColor = true;
            this.scrapeButton.Click += new System.EventHandler(this.scrapeButton_Click);
            // 
            // resultText
            // 
            this.resultText.Location = new System.Drawing.Point(684, 160);
            this.resultText.Name = "resultText";
            this.resultText.Size = new System.Drawing.Size(100, 31);
            this.resultText.TabIndex = 1;
            // 
            // facultyCombo
            // 
            this.facultyCombo.FormattingEnabled = true;
            this.facultyCombo.Items.AddRange(new object[] {
            "Ira Woodring",
            "Ana Posada"});
            this.facultyCombo.Location = new System.Drawing.Point(46, 144);
            this.facultyCombo.Name = "facultyCombo";
            this.facultyCombo.Size = new System.Drawing.Size(161, 33);
            this.facultyCombo.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 646);
            this.Controls.Add(this.facultyCombo);
            this.Controls.Add(this.resultText);
            this.Controls.Add(this.scrapeButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scrapeButton;
        private System.Windows.Forms.TextBox resultText;
        private System.Windows.Forms.ComboBox facultyCombo;
    }
}

