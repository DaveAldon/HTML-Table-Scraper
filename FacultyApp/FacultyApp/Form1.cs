using System;
using System.Windows.Forms;

namespace FacultyApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void scrapeButton_Click(object sender, EventArgs e)
        {
            string facName;
            Run runInit = new Run();
            facName = facultyCombo.SelectedText;

            facName.Replace(" ", string.Empty);
            runInit.name = facName;
            resultText.Text = runInit.start().ToString();
        }
    }
}
