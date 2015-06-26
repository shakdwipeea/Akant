using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
namespace Akant
{
    public partial class Home : MetroForm
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroProgressBar1.Visible = true;
            this.Hide();
            var postForm = new PostActivate();
            postForm.ShowDialog();
            this.Close();
        }
    }
}
