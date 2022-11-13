using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Lab3_Hinolan_FuzzySystem
{
    public partial class MainProgram : Form
    {
        private FuzzySet chariz;

        public MainProgram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int timeValue = Convert.ToInt32(timeInput.Text);
            int tempValue = Convert.ToInt32(tempInput.Text);
            int determine = 1;
            bool checker = false;

            chariz = new FuzzySet();

            if (timeValue >= 1 && timeValue <= 60)
            {
                checker = true;
                if (tempValue >= 350 && tempValue <= 450)
                {
                    checker = true;
                    if (checker == true)
                    {
                        determine = 1;
                    }
                }
                else
                    determine = 0;
            }
            else
                determine = 0;


            switch (determine)
            {
                case 0:
                    classificationOutput.Text = "Invalid Time or Temperature Input";
                    break;   
                case 1:
                    classificationOutput.Text = chariz.defuzzify(timeValue, tempValue);
                    centroidOutput.Text = chariz.computeCentroid(timeValue, tempValue).ToString();
                    break;
            }
       
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        } 
    }
}
