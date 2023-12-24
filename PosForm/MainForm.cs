using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PosForm
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            RegisterForm bfm1 = new RegisterForm();
            bfm1.ShowDialog();
        }

        private void btn_Cashup_Click(object sender, EventArgs e)
        {
            CashupForm bcm1 = new CashupForm();
            bcm1.ShowDialog();
            
        }

        private void btn_Business_Click(object sender, EventArgs e)
        {
            BusinessForm bbm1 = new BusinessForm();
            bbm1 .ShowDialog();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
