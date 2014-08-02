
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Akant
{
    public partial class Form1 : Form
    {
        Thread t;
        Form f;
        int flag = 0;
        String software, k;
        public Form1()
        {
             f = this;

            InitializeComponent();
            textBox1.Focus();
            label9.Hide();
            label5.Hide();
            label6.Hide();
            label7.Hide();
            label10.Hide();
                label8.Hide();
            textBox3.Hide();
            textBox8.Hide();
            textBox9.Hide();
            textBox10.Hide();
            textBox11.Hide();
            richTextBox1.Hide();
            //btn1.Enabled = false;   
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {

        }
        
        private void btn1_Click(object sender, EventArgs e)
        {
            if (flag == 0) 
            {
                String key = "";
                key = textBox2.Text + "-" + textBox4.Text + "-" + textBox6.Text + "-" + textBox7.Text + "-" + textBox5.Text;
                k = key;
                if (textBox1.Text != "" && key != "")
                {
                    label4.Text = "";

                    t = new Thread(this.loading);
                    t.Start();

                    int try1 = 0;

                    string uname = textBox1.Text;

                    string bios = Bios.getSerialNumber();
                    var res = "";
                    try
                    {
                        //MessageBox.Show(key);
                        res = Network.sendData(uname, key, bios);
                        //MessageBox.Show(res);
                        t.Abort();
                        try1 = 1;
                    }
                    catch (Exception e1)
                    {
                        t.Abort();
                        label4.Text = "NETWORK CONNECTION UNAVAILABLE";
                        //MessageBox.Show(e1.Message);
                        //Process.GetCurrentProcess().Kill();
                    }
                    finally
                    {
                       // try1 = 1;
                        if (try1 == 1)
                        {
                            Response r = new Response();
                            r = JsonConvert.DeserializeObject<Response>(res);

                            //MessageBox.Show(r.msg);
                            label4.Text = r.msg.ToUpper();
                             //r.code = 200;
                            if (r.code == 200)
                            {
                                label1.Text = r.software.ToUpper();
                                //label4.Text = "Activating the license for " + r.software;
                                //MessageBox.Show("Activating the license for " + r.software);

                                //Initializing the new form

                                software = r.software;
                                label2.Hide();
                                label3.Hide();
                                label11.Hide();
                                textBox1.Hide();
                                textBox2.Hide();
                                textBox4.Hide();
                                textBox5.Hide();
                                textBox6.Hide();
                                textBox7.Hide();

                                label9.Show();
                                label5.Show();
                                label6.Show();
                                label7.Show();
                                label8.Show();
                                label10.Show();
                                textBox11.Show();
                                textBox3.Show();
                                textBox8.Show();
                                textBox9.Show();
                                textBox10.Show();
                                richTextBox1.Show();

                                Point y = new Point(37, 540);
                                label4.Location = y;

                                Point p = new Point(178, 580);
                                btn1.Location = p;

                                Size s = new Size(585, 680);
                                f.Size = s;
                                textBox11.Focus();
                                btn1.Enabled = false;
                                //initialized new form
                                flag = 1;



                                label4.Text = "Ensure proper connection before proceeding".ToUpper();
                                //  UserDetails u = new UserDetails(key, r.software);
                                // u.Text = r.software;
                                // u.ShowDialog();
                            }
                        
            
                        }
                    }

                }
                else
                {
                    label4.Text = "PLEASE FILL IN THE DETAILS BEFORE PROCEEDING";
                }

            }
            else if (flag == 1)
            {
                if (textChecker())
                {
                    label4.Text = "";
                    t = new Thread(this.loading);
                    t.Start();
                    Request req = new Request();
                    int try2 = 0;

                    /////////////////////////////////
                    req.username = textBox1.Text;
                    req.email = textBox3.Text;
                    req.phone = textBox8.Text;
                    req.address = richTextBox1.Text;
                    req.city = textBox9.Text;
                    req.country = textBox10.Text;
                    req.emailVerifier = textBox11.Text;
                    req.software = software;
                    req.password = k;
                    //////////////////////////////////////
                    req.bios = Bios.getSerialNumber();
                    String res = "";
                    try
                    {
                        
                        res = Network.sendData(req);
                        try2 = 1;
                        t.Abort();
                    }
                    catch (Exception e5)
                    {
                        t.Abort();
                        label4.Text = "NETWORK CONNECTION UNAVAILABLE";
                       // MessageBox.Show("NETWORK CONNECTION UNAVAILABLE");
                       // Process.GetCurrentProcess().Kill();
                    }
                    finally
                    {
                        if (try2 == 1)
                        {
                            Response r = new Response();
                            r = JsonConvert.DeserializeObject<Response>(res);

                            label4.Text = r.msg.ToUpper();

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
                                MessageBox.Show("Thank You");
                                Application.Exit();
                            }
                        }
                    }
                    
                }
                else
                {
                    label4.Text = "Pls fill all the details before proceeding further";
                }
            }

            
            
          
            //new Entry().ShowDialog();
            //loading();
        }

      

      

        void enterInRegistry() {
            Encrypt en = new Encrypt();
           // string b = Encrypt.Encrypt_Decrypt("Hello");
           // MessageBox.Show(b);
            
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            btn1.BackColor = Color.Gray;
            btn1.ForeColor = Color.White;
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            btn1.BackColor = Color.LightBlue;
            btn1.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                t.Abort();
            }
            catch (Exception)
            {
                
                
            }
            Application.Exit();

        }



        //int count1 = 1;
        string key = "";
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
               count1 =  textBox2.Text.Length;
            if (count1 >= 5)
            {
                textBox4.Focus();
          
            }
            
        }

    
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
            count1 = textBox4.Text.Length;
            if (count1 == 5)
            {
                textBox6.Focus();

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
            count1 = textBox6.Text.Length;
            if (count1 == 5)
            {
                textBox7.Focus();

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
            count1 = textBox7.Text.Length;
            if (count1 == 5)
            {
                textBox5.Focus();

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int count1 = 0;
            count1 = textBox5.Text.Length;
            if (count1 == 5)
            {
                btn1.Focus();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        public void loading()
        {
            
            Graphics myGraphics = base.CreateGraphics();
            Pen myPen = new Pen(Color.Gray);
            SolidBrush mySolidBrush = new SolidBrush(Color.Gray);
            for (int i = 0; ; i += 10)
            {

                if (i == 500)
                {
                    i = 0;
                }
                myGraphics.FillEllipse(mySolidBrush, i , 500, 10, 10);
                myGraphics.FillEllipse(mySolidBrush, 30 + i, 500, 10, 10);
                myGraphics.FillEllipse(mySolidBrush, 60 + i, 500, 10, 10);
                myGraphics.FillEllipse(mySolidBrush, 90 + i, 500, 10, 10);
                Thread.Sleep(50);
                this.Invalidate();
                myGraphics.Clear(Color.FromArgb(0,64,64));
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                btn1.Enabled = true;
            }
        }

        Boolean textChecker()
        {
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox8.Text) || string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(richTextBox1.Text) )
            {
                return false;
            }
            return true;
        }
     
    }
}



/* 
 * This was in btn1.click
 * 
 * if (checkFirstTime())
           {

               if (checkRegistry())
               {
                   regular();
               }
               else
               {
                   new Entry().ShowDialog();
               }
           }
           else
           {
               //MessageBox.Show("Well This has been used earlier");
               //System.Environment.Exit(0);
               if (checkRegistry())
               {
                   regular();
               }
               else
               {
                   MessageBox.Show("Authentication failed", "error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                   new Entry().ShowDialog();
               }

           }*/