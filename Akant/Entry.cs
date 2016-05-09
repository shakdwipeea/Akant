

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Akant
{
    public partial class Entry : Form
    {
        Form f;
        public Entry()
        {
             f = this;
            InitializeComponent();
            button1.Enabled = false;
        }

        private void Entry_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string uname = textBox1.Text;
            string key = textBox2.Text;
            string bios = Bios.getSerialNumber();
            var res = "";
            try
            {
                res = Network.sendData(uname, key, bios);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                Process.GetCurrentProcess().Kill();
            }
            finally
            {
                Response r = new Response();
                r = JsonConvert.DeserializeObject<Response>(res);

                MessageBox.Show(r.msg);
                if (r.code == 200)
                {
                    MessageBox.Show("Activating the license for " + r.software);
                    UserDetails u = new UserDetails(key, r.software);
                    u.Text = r.software;
                    u.ShowDialog();
                }

            }
           
   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Boolean textChecker () {
            if(string.IsNullOrEmpty(textBox1.Text)||string.IsNullOrEmpty(textBox2.Text) ){
                return false;
            } 
            return true;
    }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textChecker()) {
                button1.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textChecker())
            {
                button1.Enabled = true;
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
         
            
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.LightBlue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.Gray;
        }
      }

}
