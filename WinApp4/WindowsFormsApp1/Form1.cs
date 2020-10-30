using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormClient : System.Windows.Forms.Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
           // timer1.Interval = int.Parse();
            //timer1.Enabled;
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void mnuBye_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string s1 = textBox16.Text;
            string s2 = textBox9.Text;
            string s3 = textBox10.Text;
            string s4 = textBox4.Text;
            string s5 = textBox17.Text;
            string s6 = DateTimePicker1.Text;
        }
    }
}
