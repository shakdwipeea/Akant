using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Akant
{
    public partial class PostActivate : MetroForm
    {
        public PostActivate(Response res)
        {
           
            InitializeComponent();
            String license = res.trial == 1 ? "Trial" : "Full";

            metroLabel1.Text = "Congrats!! \n  " + res.software.ToUpper() +
                " Software has been registered \n on this computer. \n " + "This is a " + license.ToUpper() + " licensed software. \n" +
                "All the best. \n";
        }

        private void PostActivate_Load(object sender, EventArgs e)
        {

        }
    }
}
