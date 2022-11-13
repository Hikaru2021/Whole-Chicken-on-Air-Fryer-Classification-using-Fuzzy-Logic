using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_Hinolan_FuzzySystem
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!richTextBox1.ClientRectangle.Contains(e.Location))
            {
                richTextBox1.Capture = false;
            }
            else if (!richTextBox1.Capture)
            {
                richTextBox1.Capture = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
