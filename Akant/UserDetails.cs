

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace Akant
{
    public partial class UserDetails : Form
    {
        string software,k;
        public UserDetails(string key,string Software)
        {
            k = key;
            software = Software;
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        Boolean textChecker()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(richTextBox1.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                return false;
            }
            return true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textChecker())
            {
                Request req = new Request();
                req.username = textBox1.Text;
                req.email = textBox2.Text;
                req.phone = textBox3.Text;
                req.address = richTextBox1.Text;
                req.city = textBox4.Text;
                req.country = textBox5.Text;
                req.emailVerifier = textBox6.Text;
                req.software = software;
                req.password = k;
                req.bios = Bios.getSerialNumber();
                String res = "";
                try
                {
                    res = Network.sendData(req);
                }
                catch (Exception e5)
                {

                    MessageBox.Show(e5.Message);
                    Process.GetCurrentProcess().Kill();
                }
                Response r = new Response();
                r = JsonConvert.DeserializeObject<Response>(res);

                MessageBox.Show(r.msg);
                if (r.code == 200)
                {
                    Credentials c = new Credentials();
                    c.email = req.emailVerifier;
                    c.key = k;
                    c.bios = req.bios;
                    if (r.trial == 1)
                    {
                        c.count = -1;
                    }
                    else
                    {
                        c.count = r.count;
                    }

                    string soft = r.software.ToLower();

                    string write = JsonConvert.SerializeObject(c);
                    string enc = Encrypt.encrypt(write);
                    Registry.createRegistry(enc, software.ToLower());
                    MessageBox.Show("System Updated");
                    Application.Exit();
                } 
            }
            else
            {
                MessageBox.Show("Pls fill all the details before proceeding further");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
        }
    }
}
