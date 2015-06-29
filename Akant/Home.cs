using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Controls;
using Newtonsoft.Json;
namespace Akant
{
    public partial class Home : MetroForm
    {
        BackgroundWorker worker = new BackgroundWorker();
        BackgroundWorker workerSave = new BackgroundWorker();

        String res;
        Request request;
        public Home()
        {
            request = new Request();
            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteWork);

            workerSave.DoWork += new DoWorkEventHandler(DoSaveWork);
            workerSave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteSaveWorker);
            InitializeComponent();
        }

        private void CompleteSaveWorker(object sender, RunWorkerCompletedEventArgs e)
        {
            String result;
            result = (String) e.Result;
            Response res = new Response();

            try
            {
                res = JsonConvert.DeserializeObject<Response>(result);

                if (res.code == 200)
                {
                    metroLabel4.Text = "Saving to registry";
                    try
                    {
                        saveToRegistry(res);
                    }
                    catch 
                    {
                        metroLabel4.Text = "Error writing to registry";
                        metroProgressBar1.Visible = false;
                    }

                    metroLabel4.Text = "Software Registered succesfully";
                    metroProgressBar1.Visible = false;

                    this.Hide();
                    var postForm = new PostActivate();
                    postForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    metroLabel4.Text = res.msg;
                    metroProgressBar1.Visible = false;
                }

            }
            catch (Exception exc)
            {
                metroLabel4.Text = exc.Message;
                metroProgressBar1.Visible = false;
            }

            

        }

        private void saveToRegistry(Response response)
        {
            Credentials c = new Credentials();
            c.email = request.emailVerifier;
            c.key = request.password;
            c.bios = request.bios;
            if (response.trial == 1)
            {
                c.count = -1;
            }
            else
            {
                c.count = response.count;
            }

            string soft = response.software.ToLower();

            string write = JsonConvert.SerializeObject(c);
            string enc = Encrypt.encrypt(write);
            Registry.createRegistry(enc, response.software.ToLower());
            
        }

        private void DoSaveWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker work = sender as BackgroundWorker;

            try
            {
                e.Result = Network.sendData(request);
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                metroProgressBar1.Visible = false;
            }
        }

        private void CompleteWork(object sender, RunWorkerCompletedEventArgs e)
        {
            res =(String) e.Result;
            metroLabel4.Text = "Received Software Data";

            try
            {
                Response r = new Response();
                r = JsonConvert.DeserializeObject<Response>(res);

                if (r.code == 200)
                {
                    request.software = r.software;
                    metroLabel4.Text = "Validating your key for " + r.software;
                    workerSave.RunWorkerAsync();
                }
                else
                {
                    metroLabel4.Text = r.msg;
                    metroProgressBar1.Visible = false;
                }
            }
            catch 
            {
                metroLabel4.Text = res;
                metroProgressBar1.Visible = false;
            }

            //MessageBox.Show(res);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            
            Request req = (Request)e.Argument;
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                e.Result = Network.sendData(req.emailVerifier, req.password, req.bios);
            }
            catch (Exception e1)
            {
                e.Result = e1.Message;
                metroProgressBar1.Visible = false;
            }



        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            request.bios = Bios.getSerialNumber();
            request.emailVerifier = metroTextBox1.Text;
            request.password = metroTextBox3.Text;
            request.phone = metroTextBox2.Text;


            
            try
            {
                metroLabel4.Text = "Getting Software Data";
                metroProgressBar1.Visible = true;
                worker.RunWorkerAsync(request);
            }
            catch (Exception e1)
            {
                metroLabel4.Text = e1.Message;
                metroProgressBar1.Visible = false;
            }
       
            /**
             * Show progess bar
             * */
            
           

            /**
             * To show second form
             * */
            /**
            this.Hide();
            var postForm = new PostActivate();
            postForm.ShowDialog();
            this.Close();
             * */
        }
    }
}
