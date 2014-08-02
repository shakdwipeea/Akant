using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using System.Management;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Sicher;
using System.Diagnostics;


namespace WindowsFormsApplication1
{
    public partial class MeenaForm : Form
    {
        static int PICSIZE = 5000;
        String filename, saveFileName, filepath;
        Bitmap b, b1, b2, b3, b4;
        //Bitmap bb, bb1, bb2, bb3;
        //byte[][] datas;
        private System.Windows.Forms.ProgressBar pBar1;
        public Thread ConvThread, SaveThread;
        private int zariCol, meenaCol;
        public delegate void InvokeDelegate();
        System.IO.FileStream fs;
        int global_exit_flag;
        int pic_start, pic_end, pic_cur;
        Color col1, col2,col3,col4;
        StringBuilder saveProfile,loadProfile;// = new StringBuilder();

        public MeenaForm()
        {

            
            InitializeComponent();

            
            




            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;

            //this.WindowState = FormWindowState.Maximized;
            this.Location = new Point(0, 0);
            this.pBar1 = new System.Windows.Forms.ProgressBar();
            //progressbar1 properties... 
            this.pBar1.Location = new System.Drawing.Point(200, 200);
            this.pBar1.Size = new System.Drawing.Size(200, 50);
            this.pBar1.Maximum = 1000;
            this.pBar1.Step = 1;
            float widthRatio = (float)Screen.PrimaryScreen.WorkingArea.Width / (float)this.Width;
            float heightRatio = (float)Screen.PrimaryScreen.WorkingArea.Height / (float)this.Height;
            SizeF scale = new SizeF(widthRatio, heightRatio);

            this.Scale(scale);

            //foreach (Control control in this.Controls)
            //{
            //    control.Font = new Font("Microsoft Sans Serif", control.Font.SizeInPoints * heightRatio * widthRatio);
            //    //control.Size = new Size(Convert.ToInt32(Math.Ceiling(control.Size.Width * widthRatio)), Convert.ToInt32(Math.Ceiling(control.Size.Height * heightRatio)));
            //    //control.Location = new Point(Convert.ToInt32(Math.Ceiling(control.Location.X * widthRatio)), Convert.ToInt32(Math.Ceiling(control.Location.Y * heightRatio)));
            //}

            this.WindowState = FormWindowState.Maximized;
            global_exit_flag = 0;


        }
        public void validateSoftware(String software)
        {

            int quit = 0;
            Validate m = new Validate();
             int code =  m.Start(software);
             if (code == 402)
             {
                 MessageBox.Show("Some error occured.Please try again later");
             }
             else if (code == 20)
             {
                 MessageBox.Show("Trial expired");
             }
             else if (code == 403)
             {
                 MessageBox.Show("Authorization failed");
             }
             else if (code == 404)
             {
                 MessageBox.Show("Please activate the software. Using the Activator 1.9.8 ");
             }
             else if (code == 401)
             {
                 MessageBox.Show("Please run this program as administrator");
             }
             else if (code == 200)
             {
                 int count = m.getCount(software);
                 if (count == -1)
                 {
                     quit = 1;
                 }
                 else if(count > 0)
                 {
                     MessageBox.Show(count.ToString());
                     quit = 1;
                 }
                 else
                 {
                     quit = 1;
                 }
                 //MessageBox.Show("Welcome");
                 
             }



             if (quit == 0)
             {
                 Process.GetCurrentProcess().Kill();
             }
      

            /*
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("DiskSerial");
            string regBios = null;
            try
            {
                regBios = new CryptSerial().decrypt(key.GetValue("DiskSerial").ToString());
            }
            catch
            {
                MessageBox.Show("This is Illegal version of software!!");
                //label1.Text = "Application is Not Activated!";
                Application.Exit();
            }

            DiskUtils du = new DiskUtils();
            string bios = du.getSerialNumber();

            if (key != null)
                key.Close();

            if (bios.Equals(regBios))
            {
                return;
            }
            else
            {
                MessageBox.Show("This is Illegal version of software!!");
                //label1.Text = "Application is Not Activated!";
                Application.Exit();
            }*/

        }
        private void MeenaForm_Load(object sender, EventArgs e)
        {
            try
            {
                validateSoftware("Loom Software");
            }
            catch (Exception)
            {

                //Sicher.dll was missing
                Process.GetCurrentProcess().Kill();
            }
            MessageBox.Show(HDDValidate.GetHDDSerial());
            String s = HDDValidate.GetHDDSerial();
            //HDDValidate.HardDriveHardCode("S1WWJ9FZ800913");//My Laptop 2020202020202020202020205936314454443333 1234
            //HDDValidate.HardDriveHardCode("2020202020202020202020205936314454443333");//ChennaReddy
            //HDDValidate.HardDriveHardCode("563539475156464c202020202020202020202020");//Csb allinone comp
            //HDDValidate.HardDriveHardCode("2020202057202d44585730384141593938363434");//Snvas-hup
            //HDDValidate.HardDriveHardCode("2J6100151D9HDV");// snvas-cubbonpet
            //HDDValidate.HardDriveHardCode("563538474e414730202020202020202020202020");//Yelahanka
            //HDDValidate.HardDriveHardCode("5635384735563132202020202020202020202020");//LS,hindupur
            //HDDValidate.HardDriveHardCode("2020202020202020202020205635385941314731");//Hup,YS
            //HDDValidate.HardDriveHardCode("5635384758425735202020202020202020202020");//YS-2loom-touchpanel
            //HDDValidate.HardDriveHardCode("W -DXW153EL20N35");//YS-Laptop-1008Hooks
            //HDDValidate.HardDriveHardCode("W -DXW09BA492671");//SPS SILKS--SHANKAR
            //HDDValidate.HardDriveHardCode("V6GD6433");//Anil,Hup-Notebook1-No.2389
            //HDDValidate.HardDriveHardCode("2020202020202020202020205636474436343333");//Anil,Hup-Notebook1-No.2389-reformatted
            //HDDValidate.HardDriveHardCode("V6GD88BD");//Anil,Hup-Notebook2-No.2356
            //HDDValidate.HardDriveHardCode("V6GDD3VZ");//Anil,Hup-Notebook3-No.2434
            //HDDValidate.HardDriveHardCode("5635374738534130202020202020202020202020");//HPTouch-shankar
            //HDDValidate.HardDriveHardCode("2J6100154DEWD2");//Anekal-1stcustomer-Lokesh
            //HDDValidate.HardDriveHardCode("9VM8BXGF");//Yelhanka-Lakshminarayan
            //HDDValidate.HardDriveHardCode("20202020202020202020202052393341315a364e");//Hindupur-Obulesh
            //HDDValidate.HardDriveHardCode("V5SEV633");//snvas-notebook1
            //HDDValidate.HardDriveHardCode("J2160051D4WE2D");//snvas-notebook1
            //HDDValidate.HardDriveHardCode("5339335a504a3437202020202020202020202020");//snvas-notebook1
            //HDDValidate.HardDriveHardCode("X M7TF70TC");//snvas-notebook1

            /*if (HDDValidate.Initiate() != 0)
            {
                MessageBox.Show("Not Authorised to use the Software on this Machine...!");
                Application.Exit();// return;
            }*/


           

           

            //buttonSaveFinalDesign.Enabled  = false;

            radioButtonHandloom.Checked = true;
            radioButton1by1.Checked = true;
            radioButtonWhite.Checked = true;
            domainUpDown1.SelectedIndex = 1;
            buttonUp.Enabled = false;
            buttonDown.Enabled = false;
            if(Properties.Settings.Default.KadiyalColumns.Count!=0)
            {
                //if (Properties.Settings.Default.KadiyalColumns.o)
                foreach(object obj in Properties.Settings.Default.KadiyalColumns)
                {
                    if (obj.ToString() != "")
                       comboBoxAddColumn.Items.Add(obj.ToString());
                }
            }
            MessageBox.Show(domainUpDown1.SelectedItem.ToString());
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void buttonDesign_Click(object sender, EventArgs e) //Load Original Image
        {
            b1 = null;
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    //MessageBox.Show(filename);
                    b1 = new Bitmap(filename);
                    if (radioButtonPowerloom.Checked == true)
                        if (b1.Height > 32000 || b1.Width != 896)
                        {
                            MessageBox.Show("Not A Valid Size..\nCheck LoomType Selection"); return;
                        }
                    if (radioButtonHandloom.Checked == true)
                        if (b1.Height > 32000 || !(b1.Width == 504 || b1.Width == 1008 || b1.Width == 672 || b1.Width == 1344))
                        {
                            MessageBox.Show("Not A Valid Size..\nCheck LoomType Selection"); return;
                        }
                    pictureBoxDesign.Image = b1;// new Bitmap(filename);

                    labelOriginalDesign.Text = openFileDialog1.SafeFileName + "(" + b1.Width.ToString() + "," + b1.Height.ToString() + ")";


                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);

            }
        }
        private void buttonZari_Click(object sender, EventArgs e)
        {
            b2 = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                //MessageBox.Show(filename);
                b2 = new Bitmap(filename);
                if (radioButtonPowerloom.Checked == true)
                    if (b2.Height > 32000 || b2.Width != 896)
                    {
                        MessageBox.Show("Not a ValidSize..\nCheck LoomType Selection"); return;
                    }

                if (radioButtonHandloom.Checked == true)
                    if (b2.Height > 32000 || !(b2.Width == 504 || b2.Width == 1008 || b2.Width == 672 || b2.Width == 1344))
                    {
                        MessageBox.Show("Not A Valid Size..\nCheck LoomType Selection"); return;
                    }


                pictureBoxM1.Image = b2;  // new Bitmap(filename);
                labelZariDesign.Text = openFileDialog1.SafeFileName + "(" + b2.Width.ToString() + "," + b2.Height.ToString() + ")";
            }
        }
        private void buttonMeena1_Click(object sender, EventArgs e)
        {
            b3 = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                //MessageBox.Show(filename);
                b3 = new Bitmap(filename);
                if (radioButtonPowerloom.Checked == true)
                    if (b3.Height > 32000 || b3.Width != 896)
                    {
                        MessageBox.Show("Not a ValidSize..\nCheck LoomType Selection"); return;
                    }

                if (radioButtonHandloom.Checked == true)
                    if (b3.Height > 32000 || !(b3.Width == 504 || b3.Width == 1008 || b3.Width == 672 || b3.Width == 1344))
                    {
                        MessageBox.Show("Not A Valid Size..\nCheck LoomType Selection"); return;
                    }

                pictureBoxM2.Image = b3;  // new Bitmap(filename);
                labelMeenaDesign.Text = openFileDialog1.SafeFileName + "(" + b3.Width.ToString() + "," + b3.Height.ToString() + ")";
            }
        }
        private void buttonDesign4_Click(object sender, EventArgs e)
        {
            b4 = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                //MessageBox.Show(filename);
                b4 = new Bitmap(filename);
                if (radioButtonPowerloom.Checked == true)
                    if (b4.Height > 32000 || b4.Width != 896)
                    {
                        MessageBox.Show("Not a ValidSize..\nCheck LoomType Selection"); return;
                    }

                if (radioButtonHandloom.Checked == true)
                    if (b4.Height > 32000 || !(b4.Width == 504 || b4.Width == 1008 || b4.Width == 672 || b4.Width == 1344))
                    {
                        MessageBox.Show("Not A Valid Size..\nCheck LoomType Selection"); return;
                    }

                pictureBoxM3.Image = b4;  // new Bitmap(filename);
                labelDesign4.Text = openFileDialog1.SafeFileName + "(" + b4.Width.ToString() + "," + b4.Height.ToString() + ")";
            }

        }

        private void buttonConv_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConvThread != null && ConvThread.IsAlive == true)
                {
                    //ConvThread.Join();
                    ConvThread.Abort();
                    this.buttonConv.BackColor = System.Drawing.Color.MediumVioletRed;
                    this.buttonConv.Text = "Convert To LoomFormat";
                    toolStripProgressBar1.Value = 0;
                    /*
                    this.buttonConv.BackColor = System.Drawing.Color.LemonChiffon;
                    this.buttonConv.Text = "..";
                    Controls.Remove(pBar1);
                     */
                    return;
                }

                //Mixing Three images (Org Design + Zari Design + Meena Design in to one file)
                //---------------------------------------------------------------------------

                if (b1 == null)
                {
                    MessageBox.Show("Select Design Image......"); return;
                }


                pictureBoxFinal.Image = null;
                b = null;

                MessageBox.Show("DesignSize" + b1.Size.Width.ToString() + "," + b1.Size.Height.ToString() + "," + b1.PixelFormat.ToString());
                if (b2 != null)
                    MessageBox.Show("ZariDesignSize" + b2.Size.Width.ToString() + "," + b2.Size.Height.ToString() + "," + b2.PixelFormat.ToString());

                if (b3 != null)
                    MessageBox.Show("MeenaDesignSize" + b3.Size.Width.ToString() + "," + b3.Size.Height.ToString() + "," + b3.PixelFormat.ToString());
                if (b4 != null)
                    MessageBox.Show("Design4Size" + b4.Size.Width.ToString() + "," + b4.Size.Height.ToString() + "," + b4.PixelFormat.ToString());
                //ConvThread = new Thread(new ThreadStart(convertToLoomformat_fast));
                //ConvThread.Start();


                if (b2 == null && b3 == null && b4 == null)
                {
                    MessageBox.Show("OneColorConversion...");
                    buttonsDisable();
                    convertToLoomformat_fast_Onecolor();
                    buttonsEnable();
                }
                else if (b1 != null && b2 != null && b3 == null && b4 == null)
                {

                    if (b1.PixelFormat != b2.PixelFormat)
                    { MessageBox.Show("Pls make sure image format of both images should be same.."); return; }
                    if (!(b1.Height == b2.Height && b1.Width == b2.Width))
                    { MessageBox.Show("Pls make sure both the images of same size..."); return; }
                    if (radioButton1by1.Checked == true)
                    {
                        MessageBox.Show("TwoColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Twocolor();
                        //convertToLoomformat_fast_Twocolor_kadiyalDesign();
                        buttonsEnable();
                    }
                    if (radioButton1by2.Checked == true)
                    {
                        MessageBox.Show("TwoColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Twocolor_2by2();
                        buttonsEnable();
                    }

                }
                else if (b1 != null && b2 != null && b3 != null && b4 == null)
                {

                    if (b1.PixelFormat != b2.PixelFormat || b2.PixelFormat != b3.PixelFormat)
                    { MessageBox.Show("Pls make sure image format of both images should be same.."); return; }
                    if (!(b1.Height == b2.Height && b1.Width == b2.Width) || !(b2.Height == b3.Height && b2.Width == b3.Width))
                    { MessageBox.Show("Pls make sure both the images of same size..."); return; }
                    if (radioButton1by1.Checked == true)
                    {
                        MessageBox.Show("ThreeColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Threecolor();
                        buttonsEnable();
                    }
                    if (radioButton1by2.Checked == true)
                    {
                        MessageBox.Show("ThreeColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Threecolor_2by2();
                        buttonsEnable();
                    }

                }
                else
                {
                    if (b1.PixelFormat != b2.PixelFormat || b2.PixelFormat != b3.PixelFormat || b3.PixelFormat != b4.PixelFormat)
                    { MessageBox.Show("Pls make sure image format of both images should be same.."); return; }
                    if (!(b1.Height == b2.Height && b1.Width == b2.Width) || !(b2.Height == b3.Height && b2.Width == b3.Width) || !(b3.Height == b4.Height && b3.Width == b4.Width))
                    { MessageBox.Show("Pls make sure both the images of same size..."); return; }




                    //convertToLoomformat_fast_Threecolor();
                    if (radioButton1by1.Checked == true)
                    {
                        MessageBox.Show("FourColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Fourcolor();
                        buttonsEnable();
                    }
                    if (radioButton1by2.Checked == true)
                    {
                        MessageBox.Show("FourColorConversion...");
                        buttonsDisable();
                        convertToLoomformat_fast_Fourcolor_2by2();
                        buttonsEnable();
                    }
                }
                buttonsEnable();
                /*
                 Controls.Add(pBar1);
                 pBar1.BringToFront();
                  */
                //buttonConv.BackColor = System.Drawing.Color.Red;
                //buttonConv.Text = "Cancel   Conversion..";
                //buttonConv.Enabled = false;   

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "LoomFormatConverter Mehod");
            }

        }
        public void updaterProgressBar(int x)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(updaterProgressBar), new object[] { x });
                return;
            }
            try
            {
                toolStripProgressBar1.Value = x;
                //  pBar1.Value = x;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "updaterProgressBar method");
            }

        }
        public void updater(Bitmap b)
        {

            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<Bitmap>(updater), new object[] { b });
                    return;
                }

                this.buttonConv.BackColor = System.Drawing.Color.LemonChiffon;
                this.buttonConv.Text = "Convert To LoomFormat";
                //buttonConv.Enabled = true;
                this.pictureBoxFinal.Image = b;
                //Controls.Remove(pBar1);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void convertToLoomformat_fast_Onecolor()
        {

            MessageBox.Show(b1.Size.Width.ToString() + "," + b1.Size.Height.ToString());
            if (b1.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
            {
                MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
            }
            pictureBoxFinal.Image = b1;

            b = b1;
            labelFinalDesign.Text = "(" + b.Width.ToString() + "," + b.Height.ToString() + ")";
            buttonDown.Enabled = false;
            buttonDown.Enabled = false;

            return;

        }
        public void convertToLoomformat_fast_Twocolor()
        {
            if (checkBoxKadiyalDesign.Checked)
                if (!convertToLoomformat_CheckColor_kadiyalDesign())
                    return;
            try
            {
                unsafe
                {
                    int j = 0, i, k, flag;

                    //create the final bitmap to hold the mixed image
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3, b1.PixelFormat);

                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                    global_exit_flag = 0;
                    for (i = 0, j = 0; i < b1.Height; i++, j++)
                    {
                        //check for cancel operation
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;

                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image

                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        for (k = 0; k < b1data.Stride; k++)
                            nRow[k] = oRow[k];
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                        }
                    }

                    //unlock the bitmaps
                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), PixelFormat.Format32bppRgb);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());

                    if ((!checkBoxShiftBy1.Checked) && checkBoxKadiyalDesign.Checked)
                        finalpic=convertToLoomformat_kadiyalDesign(finalpic);
                    if (checkBoxShiftBy1.Checked && checkBoxKadiyalDesign.Checked)
                        finalpic=convertToLoomformat_shifby1_kadiyalDesign(finalpic);

                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public void convertToLoomformat_fast_Twocolor_2by2()
        {
            if (checkBoxKadiyalDesign.Checked)
                if (!convertToLoomformat_CheckColor_kadiyalDesign())
                    return;
            try
            {
                unsafe
                {
                    int i, j, k, flag;
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3, b1.PixelFormat);

                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                    global_exit_flag = 0;

                    for (i = 0, j = 0; i < b1.Height; i++)
                    {
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;

                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image

                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                        updaterProgressBar((int)(i * 1000L / b1.Height));

                        //check for existence design on b2 bitmap i.e zari
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            //add two lines from design image(b1) to final pic
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow = (byte*)b1data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow[k];
                                j++;
                            }

                            //add two lines from zari image(b2) to final pic
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow2 = (byte*)b2data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow2[k];
                                j++;
                            }
                            i++;
                        }
                        else
                        {
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;

                        }
                    }
                    //unlock the bitmaps
                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), PixelFormat.Format32bppRgb);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());

                    if ((!checkBoxShiftBy1.Checked) && checkBoxKadiyalDesign.Checked)
                        finalpic = convertToLoomformat_kadiyalDesign(finalpic);
                    if (checkBoxShiftBy1.Checked && checkBoxKadiyalDesign.Checked)
                        finalpic = convertToLoomformat_shifby1_kadiyalDesign(finalpic);

                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void convertToLoomformat_fast_Threecolor_2by2()
        {
            try
            {
                unsafe
                {
                    int i, j, k, flag, flag2;
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3, b1.PixelFormat);

                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                    global_exit_flag = 0;

                    for (i = 0, j = 0; i < b1.Height; i++)
                    {
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            b3.UnlockBits(b3data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;

                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image

                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* oRow3 = (byte*)b3data.Scan0 + (i * b3data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                        updaterProgressBar((int)(i * 1000L / b1.Height));

                        //check for existence design on b2 bitmap i.e zari
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            //add two lines from design image(b1) to final pic
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow = (byte*)b1data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow[k];
                                j++;
                            }

                            //add two lines from zari image(b2) to final pic
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow2 = (byte*)b2data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow2[k];
                                j++;
                            }

                            //check for existence design on b3 bitmap i.e meena
                            flag2 = 0;
                            if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                b3.UnlockBits(b3data);
                                for (int ii = 0; ii < b1.Width; ii++)
                                    if (!(b3.GetPixel(ii, i).R == 255 && b3.GetPixel(ii, i).G == 255 && b3.GetPixel(ii, i).B == 255))
                                    { flag2 = 1; break; };
                                b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                            }
                            else
                            {
                                for (int ii = 0; ii < b1data.Stride; ii++)
                                    if (oRow3[ii] != 255)
                                    {
                                        flag2 = 1; break;
                                    }
                            }
                            if (flag2 == 1)
                            {
                                //add two lines from Meena image(b3) to final pic
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow3[k];
                                j++;
                                if (i != b1.Height - 1)
                                {
                                    oRow3 = (byte*)b3data.Scan0 + ((i + 1) * b1data.Stride);
                                    nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                    for (k = 0; k < b1data.Stride; k++)
                                        nRow[k] = oRow3[k];
                                    j++;

                                }
                            }
                            i++;
                        }
                        else
                        {
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;

                        }
                    }
                    //unlock the bitmaps
                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    b3.UnlockBits(b3data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), b1.PixelFormat);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());
                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void convertToLoomformat_fast_Threecolor()
        {

            try
            {
                unsafe
                {

                    int i, k, flag;
                    int j = 0;
                    //create the final bitmap to hold the mixed image
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3, b1.PixelFormat);
                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);

                    global_exit_flag = 0;

                    for (i = 0, j = 0; i < b1.Height; i++, j++)
                    {

                        //check for cancel operation
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            b3.UnlockBits(b3data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;
                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image
                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* oRow3 = (byte*)b3data.Scan0 + (i * b3data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        for (k = 0; k < b1data.Stride; k++)
                            nRow[k] = oRow[k];
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                        }
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b3.UnlockBits(b3data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b3.GetPixel(ii, i).R == 255 && b3.GetPixel(ii, i).G == 255 && b3.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow3[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }
                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow3[k];
                        }

                    }

                    //unlock the bitmaps

                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    b3.UnlockBits(b3data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), b1.PixelFormat);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());
                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void convertToLoomformat_fast_Fourcolor()
        {
            try
            {
                unsafe
                {

                    int i, k, flag;
                    int j = 0;
                    //create the final bitmap to hold the mixed image
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 4, b1.PixelFormat);
                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                    BitmapData b4data = b4.LockBits(new Rectangle(0, 0, b4.Width, b4.Height), ImageLockMode.ReadOnly, b4.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);

                    global_exit_flag = 0;

                    for (i = 0, j = 0; i < b1.Height; i++, j++)
                    {

                        //check for cancel operation
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            b3.UnlockBits(b3data);
                            b4.UnlockBits(b4data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;
                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image
                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* oRow3 = (byte*)b3data.Scan0 + (i * b3data.Stride);
                        byte* oRow4 = (byte*)b4data.Scan0 + (i * b4data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        for (k = 0; k < b1data.Stride; k++)
                            nRow[k] = oRow[k];
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                        }
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b3.UnlockBits(b3data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b3.GetPixel(ii, i).R == 255 && b3.GetPixel(ii, i).G == 255 && b3.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow3[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }
                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow3[k];
                        }
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b4.UnlockBits(b4data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b4.GetPixel(ii, i).R == 255 && b4.GetPixel(ii, i).G == 255 && b4.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b4data = b4.LockBits(new Rectangle(0, 0, b4.Width, b4.Height), ImageLockMode.ReadOnly, b4.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow4[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }
                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow4[k];
                        }

                    }

                    //unlock the bitmaps

                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    b3.UnlockBits(b3data);
                    b4.UnlockBits(b4data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), b1.PixelFormat);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());
                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void convertToLoomformat_fast_Fourcolor_2by2()
        {
            try
            {
                unsafe
                {
                    int i, j, k, flag, flag2;
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 4, b1.PixelFormat);

                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                    BitmapData b4data = b4.LockBits(new Rectangle(0, 0, b4.Width, b4.Height), ImageLockMode.ReadOnly, b4.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                    global_exit_flag = 0;

                    for (i = 0, j = 0; i < b1.Height; i++)
                    {
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            b3.UnlockBits(b3data);
                            b4.UnlockBits(b4data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;

                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image

                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* oRow3 = (byte*)b3data.Scan0 + (i * b3data.Stride);
                        byte* oRow4 = (byte*)b4data.Scan0 + (i * b4data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                        updaterProgressBar((int)(i * 1000L / b1.Height));

                        //check for existence design on b2 bitmap i.e zari
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            //add two lines from design image(b1) to final pic
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow = (byte*)b1data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow[k];
                                j++;
                            }

                            //add two lines from zari image(b2) to final pic
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                            j++;
                            if (i != b1.Height - 1)
                            {
                                oRow2 = (byte*)b2data.Scan0 + ((i + 1) * b1data.Stride);
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow2[k];
                                j++;
                            }

                            //check for existence design on b3 bitmap i.e meena
                            flag2 = 0;
                            if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                b3.UnlockBits(b3data);
                                for (int ii = 0; ii < b1.Width; ii++)
                                    if (!(b3.GetPixel(ii, i).R == 255 && b3.GetPixel(ii, i).G == 255 && b3.GetPixel(ii, i).B == 255))
                                    { flag2 = 1; break; };
                                b3data = b3.LockBits(new Rectangle(0, 0, b3.Width, b3.Height), ImageLockMode.ReadOnly, b3.PixelFormat);
                            }
                            else
                            {
                                for (int ii = 0; ii < b1data.Stride; ii++)
                                    if (oRow3[ii] != 255)
                                    {
                                        flag2 = 1; break;
                                    }
                            }
                            if (flag2 == 1)
                            {
                                //add two lines from Meena image(b3) to final pic
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow3[k];
                                j++;
                                if (i != b1.Height - 1)
                                {
                                    oRow3 = (byte*)b3data.Scan0 + ((i + 1) * b1data.Stride);
                                    nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                    for (k = 0; k < b1data.Stride; k++)
                                        nRow[k] = oRow3[k];
                                    j++;

                                }
                            }
                            //check for existence design on b4 bitmap i.e design 4
                            flag2 = 0;
                            if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                b4.UnlockBits(b4data);
                                for (int ii = 0; ii < b1.Width; ii++)
                                    if (!(b4.GetPixel(ii, i).R == 255 && b4.GetPixel(ii, i).G == 255 && b4.GetPixel(ii, i).B == 255))
                                    { flag2 = 1; break; };
                                b4data = b4.LockBits(new Rectangle(0, 0, b4.Width, b4.Height), ImageLockMode.ReadOnly, b4.PixelFormat);
                            }
                            else
                            {
                                for (int ii = 0; ii < b1data.Stride; ii++)
                                    if (oRow4[ii] != 255)
                                    {
                                        flag2 = 1; break;
                                    }
                            }
                            if (flag2 == 1)
                            {
                                //add two lines from Meena image(b3) to final pic
                                nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                                for (k = 0; k < b1data.Stride; k++)
                                    nRow[k] = oRow4[k];
                                j++;
                                if (i != b1.Height - 1)
                                {
                                    oRow4 = (byte*)b4data.Scan0 + ((i + 1) * b1data.Stride);
                                    nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                                    for (k = 0; k < b1data.Stride; k++)
                                        nRow[k] = oRow4[k];
                                    j++;

                                }
                            }
                            i++;
                        }
                        else
                        {
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow[k];
                            j++;

                        }
                    }
                    //unlock the bitmaps
                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    b3.UnlockBits(b3data);
                    b4.UnlockBits(b4data);
                    finalpic.UnlockBits(finalpicdata);

                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), b1.PixelFormat);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());
                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        public Bitmap ccolour(Bitmap b, ColorDialog color)
        {
            try
            {
                if (b == null)
                {
                    MessageBox.Show("Image not Loaded...");
                    return b;
                }
                unsafe
                {
                    if (b.PixelFormat != PixelFormat.Format24bppRgb)
                    {
                        MessageBox.Show("Supports only for 24bpp .bmp format");
                        return b;
                    }
                    if (color.ShowDialog() == DialogResult.OK)
                    {
                        Color bm;
                        bm = colorDialog1.Color;
                        global_exit_flag = 0;
                        //Bitmap newbitmap = new Bitmap(b.Width, b.Height,b.PixelFormat);
                        BitmapData bdata = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, b.PixelFormat);
                        //BitmapData newbitmapdata = newbitmap.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadOnly, b.PixelFormat);
                        for (int i = 0; i < b.Height; i++)
                        {
                            //check for cancel operation
                            Application.DoEvents();
                            if (global_exit_flag == 1)
                            {
                                b.UnlockBits(bdata);
                                updaterProgressBar(0);
                                global_exit_flag = 0;
                                return b;
                            }

                            updaterProgressBar((int)(i * 1000L / b.Height));

                            byte* oRow = (byte*)bdata.Scan0 + (i * bdata.Stride);
                            //byte* nRow = (byte*)newbitmapdata.Scan0 + (i * newbitmapdata.Stride);
                            for (int k = 0; k < b.Width; k++)
                            {
                                if (oRow[k * 3] != 255 || oRow[k * 3 + 1] != 255 || oRow[k * 3 + 2] != 255)
                                {

                                    oRow[k * 3] = bm.B;
                                    oRow[k * 3 + 1] = bm.G;
                                    oRow[k * 3 + 2] = bm.R;
                                }
                                //int t=(int)oRow[k]-  bm.ToArgb();
                                //nRow[k] =(byte) (oRow[k] +(t));
                            }

                        }
                        //newbitmap.UnlockBits(newbitmapdata);
                        b.UnlockBits(bdata);
                        return b;

                    }
                    return b;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return b;
            }
        }
        public void convertToLoomformat()
        {
            try
            {
                zariCol = 0;

                //create delegate objects to update the main thread with the progress and update the result


                int i, j = 0, k;
                Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3);

                if (radioButton1by2.Checked == true)
                {
                    for (i = 0, j = 0; i < b1.Height; i++)
                    {
                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        if (radioButtonBlock.Checked == true)
                        {
                            if (!((b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255) && (b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255)))
                            {
                                //add two lines from design image(b1) to final pic
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b1.GetPixel(k, i)); j++;
                                if (i != b1.Height - 1)
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b1.GetPixel(k, i + 1)); j++;
                                //add two lines from zari image(b2) to final pic
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b2.GetPixel(k, i)); j++;
                                if (i != b1.Height - 1)
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b2.GetPixel(k, i + 1)); j++;

                                i++;
                            }
                            else
                            {
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b1.GetPixel(k, i)); j++;

                            }
                        }
                        else
                        {
                            if ((b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255) && (b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255))
                            {
                                //add two lines from design image(b1) to final pic
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b1.GetPixel(k, i)); j++;
                                if (i != b1.Height - 1)
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b1.GetPixel(k, i + 1)); j++;
                                //add two lines from zari image(b2) to final pic
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b2.GetPixel(k, i)); j++;
                                if (i != b1.Height - 1)
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b2.GetPixel(k, i + 1)); j++;

                                i++;
                            }
                            else
                            {
                                for (k = 0; k < b1.Width; k++)
                                    finalpic.SetPixel(k, j, b1.GetPixel(k, i)); j++;

                            }

                        }
                    }

                }
                if (radioButton1by1.Checked == true)
                {
                    for (i = 0, j = 0; i < b1.Height; i++, j++)
                    {
                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        for (k = 0; k < b1.Width; k++)
                            finalpic.SetPixel(k, j, b1.GetPixel(k, i));
                        if (b2 != null)//&& i < b2.Height )
                            if (radioButtonBlock.Checked == true)
                            {
                                int flag = 0;
                                for (int ii = 0; ii < b1.Width; ii++)
                                    if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                    { flag = 1; break; };

                                //     if (!((b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255) && (b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255)))
                                if (flag == 1)
                                {
                                    j++;
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b2.GetPixel(k, i));
                                }
                            }
                            else
                            {
                                if ((b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255) && (b2.GetPixel(zariCol, i).R == 255 && b2.GetPixel(zariCol, i).G == 255 && b2.GetPixel(zariCol, i).B == 255))
                                //if ((b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255) && (b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255))
                                {
                                    j++;
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b2.GetPixel(k, i));
                                }
                            }

                        if (b3 != null)//&& i < b2.Height )
                            if (radioButtonBlock.Checked == true)
                            {
                                if (!((b3.GetPixel(3, i).R == 255 && b3.GetPixel(3, i).G == 255 && b3.GetPixel(3, i).B == 255) && (b3.GetPixel(3, i).R == 255 && b3.GetPixel(3, i).G == 255 && b3.GetPixel(3, i).B == 255)))

                                //if ((b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255) && (b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255))
                                {
                                    j++;
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b3.GetPixel(k, i));
                                }
                            }
                            else
                            {
                                if ((b3.GetPixel(3, i).R == 255 && b3.GetPixel(3, i).G == 255 && b3.GetPixel(3, i).B == 255) && (b3.GetPixel(3, i).R == 255 && b3.GetPixel(3, i).G == 255 && b3.GetPixel(3, i).B == 255))

                                //if ((b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255) && (b2.GetPixel(5, i).R == 255 && b2.GetPixel(5, i).G == 255 && b2.GetPixel(5, i).B == 255))
                                {
                                    j++;
                                    for (k = 0; k < b1.Width; k++)
                                        finalpic.SetPixel(k, j, b3.GetPixel(k, i));
                                }
                            }

                    }
                }
                b = new Bitmap(b1.Width, j);
                MessageBox.Show(b.Size.Width.ToString() + "," + b.Size.Height.ToString());
                for (i = 0; i < j; i++)
                {
                    updaterProgressBar((int)(i * 1000L / b.Height));
                    for (k = 0; k < b.Width; k++)
                        b.SetPixel(k, i, finalpic.GetPixel(k, i));

                }
                updater(b);
                MessageBox.Show("Conversion/Mixing Successfully completed...");
            }
            catch (ThreadAbortException e)
            {
                String s = e.Message;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message + "Error in conversionToLoomFormat()-method");
                //MessageBox.Show(e.ToString());
            }
        }
        private void saveToLoomFormat_CardType1()
        {
            try
            {
                //Converting Image to .nlk format.........................
                //--------------------------------------------------------------------------
                int i, j, k;
                int No_of_Weavelines = b.Height;
                int No_of_LoomcontrollerCards = 2;
                int No_of_Legs = 7;
                int Size_of_Leg = 64;
                int Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;
                byte l;

                if (radioButtonHandloom.Checked == true)
                {
                    No_of_Weavelines = b.Height;

                    No_of_Legs = 7;
                    Size_of_Leg = 8 * 9; // 9 boards
                    Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;
                    No_of_LoomcontrollerCards = b.Width / Size_of_LoomcontrollerCard;

                }
                if (radioButtonPowerloom.Checked == true)
                {
                    No_of_Weavelines = b.Height;
                    No_of_LoomcontrollerCards = 2;
                    No_of_Legs = 7;
                    Size_of_Leg = 64;
                    Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;

                }

                byte[] identifier;//2 bytes
                byte[] loomcontrollers;//2 bytes
                byte[] no_of_cards;//2bytes
                byte[] reserved;//12bytes
                byte[] bitvalue;

                identifier = new byte[2];
                loomcontrollers = new byte[2];
                no_of_cards = new byte[2];
                reserved = new byte[12];
                bitvalue = new byte[1];

                identifier[0] = (byte)(Size_of_Leg % 256);
                identifier[1] = (byte)(Size_of_Leg / 256);
                loomcontrollers[0] = (byte)(No_of_LoomcontrollerCards % 256);
                loomcontrollers[1] = (byte)(No_of_LoomcontrollerCards / 256);

                no_of_cards[0] = (byte)(No_of_Weavelines % 256);
                no_of_cards[1] = (byte)(No_of_Weavelines / 256);

                //fs = System.IO.File.Create(saveFileName + ".nlk");//@"d:\test.nlk");
                fs = System.IO.File.Create(saveFileName);
                fs.Write(identifier, 0, 2);
                fs.Write(loomcontrollers, 0, 2);
                fs.Write(no_of_cards, 0, 2);
                fs.Write(reserved, 0, 12);

                global_exit_flag = 0;
                if (radioButtonPowerloom.Checked == true)
                    for (i = 0; i < No_of_Weavelines; i++)
                    {
                        //       toolStripProgressBar1.Value = (i * 1000) / b.Height;
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            return;
                        }

                        updaterProgressBar((int)(i * 1000L / b.Height));
                        for (j = 0; j < No_of_LoomcontrollerCards; j++)//indicates which loomcontroller card
                            for (k = 0; k < Size_of_Leg; k++)      //indicates leg no...
                            {
                                bitvalue[0] = 0x00;
                                for (l = 0; l < No_of_Legs; l++) // indicates which bit/pixel in the leg
                                {
                                    Color c = b.GetPixel(((j * Size_of_LoomcontrollerCard) + (l * Size_of_Leg) + ((Size_of_Leg - 1) - k)), i);
                                    if (((c.R == 255) && (c.G == 255) && (c.B == 255)))
                                        bitvalue[0] |= (byte)(1 << l);
                                }
                                fs.Write(bitvalue, 0, 1);

                            }
                    }

                if (radioButtonHandloom.Checked == true)
                {
                    byte[] dig_data = new byte[No_of_LoomcontrollerCards * 64];
                    for (int lines = 0; lines < No_of_Weavelines; lines++)
                    {

                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            return;
                        }

                        updaterProgressBar((int)(lines * 1000L / b.Height));
                        int v = 0;

                        for (i = 0; i < No_of_LoomcontrollerCards * 64; i++)
                            dig_data[i] = 0x00;

                        for (int LC = 0; LC < No_of_LoomcontrollerCards; LC++)
                        {
                            for (i = 0; i < 7; i++)
                            {
                                byte mask = 0x01;
                                for (l = 0; l < 8; l++)
                                {

                                    for (k = 0; k < 9; k++)
                                    {
                                        Color c = b.GetPixel(v++, lines);
                                        if (((c.R == 255) && (c.G == 255) && (c.B == 255)))
                                            dig_data[LC * 64 + i * 9 + k] |= (byte)(1 << l);
                                    }
                                }
                            }

                        }
                        fs.Write(dig_data, 0, No_of_LoomcontrollerCards * 64);
                    }
                }
                fs.Close();

                MessageBox.Show("File Saved....");
                updaterProgressBar(0);
                //buttonSaveFinalDesign.Enabled = true;
            }
            catch (ThreadAbortException e)
            {
                String s = e.Message;
                fs.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in saveToLoomFormat()-method");
            }
            finally
            {
                //fs.Close(); fs.Dispose();
            }
        }
        private void saveToLoomFormat_CardType2()
        {
            try
            {
                //Converting Image to .nlk format.........................
                //--------------------------------------------------------------------------
                int i, j, k;
                int No_of_Weavelines = b.Height;
                int No_of_LoomcontrollerCards = 2;
                int No_of_Legs = 7;
                int Size_of_Leg = 64;
                int Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;
                byte l;

                if (radioButtonHandloom.Checked == true)
                {
                    No_of_Weavelines = b.Height;
                    No_of_Legs = 7;
                    Size_of_Leg = 8 * 9; // 9 boards
                    //newly added
                    if (b.Width == 672 || b.Width == 1344)
                        Size_of_Leg = 8 * 12; //12 boards
                    Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;
                    No_of_LoomcontrollerCards = b.Width / Size_of_LoomcontrollerCard;

                }
                if (radioButtonPowerloom.Checked == true)
                {
                    No_of_Weavelines = b.Height;
                    No_of_LoomcontrollerCards = 2;
                    No_of_Legs = 7;
                    Size_of_Leg = 64;
                    Size_of_LoomcontrollerCard = No_of_Legs * Size_of_Leg;

                }

                byte[] identifier;//2 bytes
                byte[] loomcontrollers;//2 bytes
                byte[] no_of_cards;//2bytes
                byte[] reserved;//12btes
                byte[] bitvalue;

                identifier = new byte[2];
                loomcontrollers = new byte[2];
                no_of_cards = new byte[2];
                reserved = new byte[12];
                bitvalue = new byte[1];

                identifier[0] = (byte)(Size_of_Leg % 256);
                identifier[1] = (byte)(Size_of_Leg / 256);
                loomcontrollers[0] = (byte)(No_of_LoomcontrollerCards % 256);
                loomcontrollers[1] = (byte)(No_of_LoomcontrollerCards / 256);

                no_of_cards[0] = (byte)(No_of_Weavelines % 256);
                no_of_cards[1] = (byte)(No_of_Weavelines / 256);

                //fs = System.IO.File.Create(saveFileName + ".nlk");//@"d:\test.nlk");
                fs = System.IO.File.Create(saveFileName);
                fs.Write(identifier, 0, 2);
                fs.Write(loomcontrollers, 0, 2);
                fs.Write(no_of_cards, 0, 2);
                fs.Write(reserved, 0, 12);

                global_exit_flag = 0;
                if (radioButtonPowerloom.Checked == true)
                {
                    byte[] dig_data = new byte[No_of_LoomcontrollerCards * 64];
                    for (int lines = 0; lines < No_of_Weavelines; lines++)
                    {

                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            return;
                        }

                        updaterProgressBar((int)(lines * 1000L / b.Height));
                        int v = 0;

                        for (i = 0; i < No_of_LoomcontrollerCards * 64; i++)
                            dig_data[i] = 0x00;

                        for (int LC = 0; LC < No_of_LoomcontrollerCards; LC++)
                        {
                            for (i = 0; i < 7; i++)
                            {
                                byte mask = 0x01;
                                for (l = 0; l < 8; l++)
                                {

                                    for (k = 0; k < 8; k++)
                                    {
                                        Color c = b.GetPixel(v++, lines);
                                        if (((c.R == 255) && (c.G == 255) && (c.B == 255)))
                                            dig_data[LC * 64 + i * 8 + k] |= (byte)(1 << l);
                                    }
                                }
                            }

                        }
                        fs.Write(dig_data, 0, No_of_LoomcontrollerCards * 64);
                    }
                }
                if (radioButtonHandloom.Checked == true)
                {
                    //added...
                    byte[] dig_data;
                    if (b.Width == 672 || b.Width == 1344)
                    {
                        dig_data = new byte[No_of_LoomcontrollerCards * ((Size_of_Leg / 8) * 7)];
                    }
                    else
                    {
                        dig_data = new byte[No_of_LoomcontrollerCards * 64];
                    }
                    for (int lines = 0; lines < No_of_Weavelines; lines++)
                    {

                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            return;
                        }

                        updaterProgressBar((int)(lines * 1000L / b.Height));
                        int v = 0;
                        if (b.Width == 672 || b.Width == 1344)
                            for (i = 0; i < No_of_LoomcontrollerCards * ((Size_of_Leg / 8) * 7); i++)
                                dig_data[i] = 0x00;

                        else
                            for (i = 0; i < No_of_LoomcontrollerCards * 64; i++)
                                dig_data[i] = 0x00;

                        for (int LC = 0; LC < No_of_LoomcontrollerCards; LC++)
                        {
                            for (i = 0; i < 7; i++)
                            {
                                for (l = 0; l < 8; l++)
                                {
                                    if (b.Width == 672 || b.Width == 1344)
                                        for (k = 0; k < (Size_of_Leg / 8); k++)
                                        {
                                            Color c = b.GetPixel(v++, lines);
                                            if (((c.R == 255) && (c.G == 255) && (c.B == 255)))
                                                dig_data[LC * ((Size_of_Leg / 8) * 7) + i * (Size_of_Leg / 8) + k] |= (byte)(1 << l);
                                        }
                                    else
                                        for (k = 0; k < 9; k++)
                                        {
                                            Color c = b.GetPixel(v++, lines);
                                            if (((c.R == 255) && (c.G == 255) && (c.B == 255)))
                                                dig_data[LC * 64 + i * 9 + k] |= (byte)(1 << l);
                                        }

                                }
                            }

                        }
                        if (b.Width == 672 || b.Width == 1344)
                            fs.Write(dig_data, 0, No_of_LoomcontrollerCards * ((Size_of_Leg / 8) * 7));
                        else
                            fs.Write(dig_data, 0, No_of_LoomcontrollerCards * 64);
                    }
                }
                fs.Close();

                MessageBox.Show("File Saved....");
                updaterProgressBar(0);
                //buttonSaveFinalDesign.Enabled = true;
            }
            catch (ThreadAbortException e)
            {
                String s = e.Message;
                fs.Close();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error in saveToLoomFormat()-method");
            }
            finally
            {
                //fs.Close(); fs.Dispose();
            }


        }

        private void buttonSaveFinalDesign_Click(object sender, EventArgs e)
        {
            // Saving the Image in the file
            //-----------------------------------------------------------------------------


            try
            {
                if (pictureBoxFinal.Image == null)
                {
                    MessageBox.Show("No image to save..", ""); return;
                }

                saveFileDialog1.Filter = "BitmapImage|*.bmp|JPeg Image|*.jpg|Gif Image|*.gif";

                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    buttonsDisable();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 2:
                            pictureBoxFinal.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            MessageBox.Show(saveFileDialog1.FileName + " is saved...", "");
                            break;

                        case 1:
                            //MessageBox.Show((b.PixelFormat).ToString());
                            b.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            MessageBox.Show(saveFileDialog1.FileName + " is saved...", "");
                            //pictureBoxFinal.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBoxFinal.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            MessageBox.Show(saveFileDialog1.FileName + " is saved...", "");
                            break;

                    }
                    fs.Close();
                    buttonsEnable();

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                fs.Close();
            }
        }
        private void buttonEditFinalDesign_Click(object sender, EventArgs e)
        {
            //string filename;
            filepath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images\\" + DateTime.Now.Minute + ".bmp");
            pictureBoxFinal.Image.Save(filepath);
            buttonRefresh.Visible = true;

            //try
            //{
            //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        filename = openFileDialog1.FileName;
            //        filepath = filename;

            //    }
            //}
            //catch(Exception eee)
            //{
            //    MessageBox.Show(eee.ToString());
            //}
            System.Diagnostics.Process.Start("rundll32.exe", string.Format("shell32.dll,OpenAs_RunDLL \"{0}\"", filepath.ToString()));
        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Bitmap editbm = new Bitmap(filepath);
            pictureBoxFinal.Image = editbm;
            b = editbm;
            buttonRefresh.Visible = false;
        }
        private void buttonAutoSize_Click(object sender, EventArgs e)
        {

            pictureBoxDesign.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxM1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxFinal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxDesign.Refresh();
            pictureBoxM1.Refresh();
            pictureBoxFinal.Refresh();

        }

        private void buttonStretch_Click(object sender, EventArgs e)
        {
            pictureBoxDesign.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxM1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxFinal.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxDesign.Refresh();
            pictureBoxM1.Refresh();
            pictureBoxFinal.Refresh();

        }


        private void toolStripSplitButtonExit_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        public void buttonSaveLoomFormat_Click(object sender, EventArgs e)
        {
            try
            {
                //ConvThread.Join();
                if (b == null)
                {
                    MessageBox.Show("Conversion not done!!!...", ""); return;
                }
                if (SaveThread != null)//&& SaveThread.IsAlive == true)
                {
                    //ConvThread.Join();
                    MessageBox.Show("aborting save thread..." + SaveThread.ThreadState.ToString());
                    SaveThread.Abort();
                    SaveThread.Join();


                    toolStripProgressBar1.Value = 0;
                    //        return;
                }

                if (pictureBoxFinal.Image == null)
                {
                    //MessageBox.Show("Conversion not done...", ""); return;
                }
                if (fs != null)
                    fs.Close();
                if (domainUpDown1.SelectedIndex == 0) // old card            
                    saveFileDialog1.Filter = "LoomImage(*NLK)|*.nlk";
                if (domainUpDown1.SelectedIndex == 1)  // new card
                    saveFileDialog1.Filter = "LoomImage(*LMK)|*.lmk";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                if (saveFileDialog1.FileName == "")
                { MessageBox.Show("File Not Selected..."); return; }
                saveFileName = saveFileDialog1.FileName;

                //SaveThread = new Thread(new ThreadStart(saveToLoomFormat));

                //SaveThread.Start();
                buttonsDisable();
                if (domainUpDown1.SelectedIndex == 0) // old card
                    saveToLoomFormat_CardType1();
                if (domainUpDown1.SelectedIndex == 1)  // new card
                    saveToLoomFormat_CardType2();

                buttonsEnable();
                //  buttonConv.BackColor = System.Drawing.Color.Red;
                //  buttonConv.Text = "Cancel   Conversion..";


            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "saveToLoomFormat");
            }

        }





        private void buttonsEnable()
        {
            buttonDesign.Enabled = true;
            buttonZari.Enabled = true;
            buttonMeena1.Enabled = true;
            buttonDesign4.Enabled = true;
            buttonSaveFinalDesign.Enabled = true;
            buttonSaveLoomFormat.Enabled = true;
            buttonDesClr.Enabled = true;
            buttonZariClr.Enabled = true;
            buttonMeenaClr.Enabled = true;
            buttonDesign4Clr.Enabled = true;
            buttonApplyColOrgDes.Enabled = true;
            buttonAppColZariDes.Enabled = true;
            buttonAppColMeena1.Enabled = true;
            buttonAppColDesign4.Enabled = true;
            buttonUp.Enabled = true;
            buttonDown.Enabled = true;

        }
        private void buttonsDisable()
        {
            buttonDesign.Enabled = false;
            buttonZari.Enabled = false;
            buttonMeena1.Enabled = false;
            buttonDesign4.Enabled = false;
            buttonSaveFinalDesign.Enabled = false;
            buttonSaveLoomFormat.Enabled = false;
            buttonDesClr.Enabled = false;
            buttonZariClr.Enabled = false;
            buttonMeenaClr.Enabled = false;
            buttonDesign4Clr.Enabled = false;
            buttonApplyColOrgDes.Enabled = false;
            buttonAppColZariDes.Enabled = false;
            buttonAppColMeena1.Enabled = false;
            buttonAppColDesign4.Enabled = false;
            buttonUp.Enabled = false;
            buttonDown.Enabled = false;
        }




        private void buttonDesClr_Click(object sender, EventArgs e)
        {
            pictureBoxDesign.Image = null;
            b1 = null;

        }
        private void buttonZariClr_Click(object sender, EventArgs e)
        {
            pictureBoxM1.Image = null;
            b2 = null;

        }
        private void buttonMeenaClr_Click(object sender, EventArgs e)
        {
            pictureBoxM2.Image = null;
            b3 = null;

        }
        private void buttonDesign4Clr_Click(object sender, EventArgs e)
        {
            pictureBoxM3.Image = null;
            b4 = null;

        }





        private void buttonCancel_Click(object sender, EventArgs e)
        {
            global_exit_flag = 1;
            buttonsEnable();
        }





        private void buttonApplyColOrgDes_Click(object sender, EventArgs e)
        {
            buttonsDisable();
            b1 = ccolour(b1, colorDialog1);
            pictureBoxDesign.Image = b1;
            buttonsEnable();
        }
        private void buttonAppColZariDes_Click(object sender, EventArgs e)
        {
            buttonsDisable();
            b2 = ccolour(b2, colorDialog1);
            pictureBoxM1.Image = b2;
            buttonsEnable();
        }
        private void buttonAppColMeena1_Click(object sender, EventArgs e)
        {
            buttonsDisable();
            b3 = ccolour(b3, colorDialog1);
            pictureBoxM2.Image = b3;
            buttonsEnable();
        }
        private void buttonAppColDesign4_Click(object sender, EventArgs e)
        {
            buttonsDisable();
            b4 = ccolour(b4, colorDialog1);
            pictureBoxM3.Image = b4;
            buttonsEnable();
        }


        private void exToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void buttonUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxDesign.Image == null)
                {
                    MessageBox.Show("Image is not Loaded..."); return;
                }
                if (pic_cur == pic_start) return;
                pic_cur--;
                if (pic_cur == pic_start)
                    buttonUp.Enabled = false;
                else
                    buttonUp.Enabled = true;
                if (pic_cur != pic_end) buttonDown.Enabled = true;
                int finalhight = b.Height - pic_cur * PICSIZE;
                if (finalhight > PICSIZE)
                    finalhight = PICSIZE;
                int initlhight = pic_cur * PICSIZE;
                pictureBoxFinal.Image = b.Clone(new Rectangle(0, initlhight, b.Width, finalhight), b.PixelFormat);
                /*switch (pic_cur)
                {
                    case 0:
                        pictureBoxFinal.Image = b.Clone(new Rectangle(0, 0, b.Width, finalhight ), b.PixelFormat);
                        break;
                    case 1:
                        pictureBoxFinal.Image = b.Clone(new Rectangle(0, 30000, b.Width, finalhight ), b.PixelFormat);
                        break;
                    case 2:
                        pictureBoxFinal.Image = b.Clone(new Rectangle(0, 60000, b.Width, finalhight ), b.PixelFormat);
                        break;
                    case 3:
                        pictureBoxFinal.Image = b.Clone(new Rectangle(0, 90000, b.Width, finalhight ), b.PixelFormat);
                        break;
                
                }*/

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void buttonDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxDesign.Image == null)
                {
                    MessageBox.Show("Image is not Loaded..."); return;
                }
                if (pic_cur == pic_end) return;
                pic_cur++;
                if (pic_cur == pic_end)
                    buttonDown.Enabled = false;
                else
                    buttonDown.Enabled = true;
                if (pic_cur != pic_start) buttonUp.Enabled = true;
                //if (pic_cur >= pic_end)
                //    pic_cur = pic_end ;
                int finalhight = b.Height - pic_cur * PICSIZE;
                if (finalhight > PICSIZE) finalhight = PICSIZE;
                int initlhight = pic_cur * PICSIZE;
                pictureBoxFinal.Image = b.Clone(new Rectangle(0, initlhight, b.Width, finalhight), b.PixelFormat);

            }

            // finalpic.Clone(new Rectangle(0, 0, b1.Width, j), b1.PixelFormat);

            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {

            try
            {
                //System.IO.Directory.Delete((Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images\\")));
                Array.ForEach(Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images\\")), File.Delete);
                RepeatImage obj = new RepeatImage();
                obj.Show();
                obj.passValues(radioButtonHandloom,radioButtonPowerloom,domainUpDown1);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);

            }
        }

        private void pictureBoxFinal_Click(object sender, EventArgs e)
        {
            //    if (zoom)
            //    {
            //        Graphics g = Graphics.FromImage(b);
            //        Rectangle rect = new Rectangle(0, 0, 3*b.Width, 3*b.Height);

            //    }
        }

        private void MeenaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Array.ForEach(Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "images\\")), File.Delete);
            System.Environment.Exit(System.Environment.ExitCode);
        }




        private void comboBoxAddColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {

                MessageBox.Show("Enter digit Only...");

                e.Handled = true;
                return;

            }

        }
        private void comboBoxAddColumn_KeyDown(object sender, KeyEventArgs e)
        {
            comboBoxAddColumns(b1,comboBoxAddColumn, e);
        }
        private void comboBoxAddColumns(Bitmap bb, ComboBox comboBoxAddColumn,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (bb == null)
                {
                    MessageBox.Show("Please Load the Image First...");
                    return;
                }
                if (comboBoxAddColumn.Text == "")
                {
                    return;
                }
                if (comboBoxAddColumn.Items.Contains(comboBoxAddColumn.Text))
                {
                    MessageBox.Show("Item already exits...");
                    e.Handled = true;
                    return;
                }

                if (int.Parse(comboBoxAddColumn.Text) > bb.Width)
                {
                    MessageBox.Show("Please enter the column value within the image width...");
                    return;
                }
                comboBoxAddColumn.Items.Add(comboBoxAddColumn.Text);
                comboBoxAddColumn.Text = "";
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (comboBoxAddColumn.SelectedItem != null)
                {
                    comboBoxAddColumn.Items.Remove(comboBoxAddColumn.SelectedItem);
                    e.Handled = true;
                    if (comboBoxAddColumn.Items.Count == 0)
                        comboBoxAddColumn.Text = "Add Columns";
                    comboBoxAddColumn.Focus();
                    return;
                }
            }


        }
        private Color checkColor(Bitmap bbb)
        {
            Color colr = Color.White;
            int counter = 0;
            try
            {
                unsafe
                {

                    BitmapData bdata = bbb.LockBits(new Rectangle(0, 0, bbb.Width, bbb.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    for (int i = 0; i < bbb.Height; i++)
                    {


                        byte* oRow = (byte*)bdata.Scan0 + (i * bdata.Stride);

                        for (int k = 0; k < bbb.Width; k++)
                        {
                            if (oRow[k * 3] != 255 || oRow[k * 3 + 1] != 255 || oRow[k * 3 + 2] != 255)
                            {
                                Color kkk = Color.FromArgb(oRow[k * 3 + 2], oRow[k * 3 + 1], oRow[k * 3]);



                                if (kkk != colr)
                                {
                                    colr = kkk;
                                    counter++;
                                }

                            }
                            if (counter > 2)
                            {
                                bbb.UnlockBits(bdata);
                                return Color.Black;
                            }

                        }

                    }

                    bbb.UnlockBits(bdata);

                    return colr;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Color.White;
            }

        }
        private Boolean convertToLoomformat_CheckColor_kadiyalDesign()
        {
            
            col1 = checkColor(new Bitmap(b1));
            col2 = checkColor(new Bitmap(b2));
            if (col1 == Color.Black && col2 == Color.Black)
            {
                MessageBox.Show("please apply single and diffrent color to both the design...");
                return false;
            }
            if (col1 == Color.Black && col2 != Color.Black)
            {
                MessageBox.Show("please apply single color to orizinal design..");
                return false;
            }
            if (col1 != Color.Black && col2 == Color.Black)
            {
                MessageBox.Show("please apply single color to zari design..");
                return false;
            }
            if (col1 != Color.Black && col2 != Color.Black && col1 == col2)
            {
                MessageBox.Show("Both the design have same color...\nplease apply diffrent color to both the design design..");
                return false;

            }
            return true;
        }
        private void convertToLoomformat_fast_Twocolor_kadiyalDesign()
        {
            Color col1, col2;
            col1 = checkColor(new Bitmap(b1));
            col2 = checkColor(new Bitmap(b2));
            if (col1 == Color.Black && col2 == Color.Black)
            {
                MessageBox.Show("please apply single and diffrent color to both the design...");
                return;
            }
            if (col1 == Color.Black && col2 != Color.Black)
            {
                MessageBox.Show("please apply single color to orizinal design..");
                return;
            }
            if (col1 != Color.Black && col2 == Color.Black)
            {
                MessageBox.Show("please apply single color to zari design..");
                return;
            }
            if (col1 != Color.Black && col2 != Color.Black && col1 == col2)
            {
                MessageBox.Show("Both the design have same color...\nplease apply diffrent color to both the design design..");
                return;

            }





            try
            {
                unsafe
                {
                    int j = 0, i, k, flag;

                    //create the final bitmap to hold the mixed image
                    Bitmap finalpic = new Bitmap(b1.Width, b1.Height * 3, b1.PixelFormat);

                    //Lock the bitmaps, so to Map into memory and access the memory directly
                    BitmapData b1data = b1.LockBits(new Rectangle(0, 0, b1.Width, b1.Height), ImageLockMode.ReadOnly, b1.PixelFormat);
                    BitmapData b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                    BitmapData finalpicdata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                    global_exit_flag = 0;
                    for (i = 0, j = 0; i < b1.Height; i++, j++)
                    {
                        //check for cancel operation
                        Application.DoEvents();
                        if (global_exit_flag == 1)
                        {
                            b1.UnlockBits(b1data);
                            b2.UnlockBits(b2data);
                            finalpic.UnlockBits(finalpicdata);
                            updaterProgressBar(0);
                            global_exit_flag = 0;
                            labelFinalDesign.Text = "                  ";
                            buttonDown.Enabled = false;
                            buttonDown.Enabled = false;

                            return;
                        }
                        //set the byte pointers to video memory at proper lines of image

                        byte* oRow = (byte*)b1data.Scan0 + (i * b1data.Stride);
                        byte* oRow2 = (byte*)b2data.Scan0 + (i * b2data.Stride);
                        byte* nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);

                        updaterProgressBar((int)(i * 1000L / b1.Height));
                        for (k = 0; k < b1data.Stride; k++)
                            nRow[k] = oRow[k];
                        flag = 0;
                        if (b1.PixelFormat != PixelFormat.Format24bppRgb)
                        {
                            b2.UnlockBits(b2data);
                            for (int ii = 0; ii < b1.Width; ii++)
                                if (!(b2.GetPixel(ii, i).R == 255 && b2.GetPixel(ii, i).G == 255 && b2.GetPixel(ii, i).B == 255))
                                { flag = 1; break; };
                            b2data = b2.LockBits(new Rectangle(0, 0, b2.Width, b2.Height), ImageLockMode.ReadOnly, b2.PixelFormat);
                        }
                        else
                        {
                            for (int ii = 0; ii < b1data.Stride; ii++)
                                if (oRow2[ii] != 255)
                                {
                                    flag = 1; break;
                                }
                        }

                        if (flag == 1)
                        {
                            j++;
                            nRow = (byte*)finalpicdata.Scan0 + (j * finalpicdata.Stride);
                            for (k = 0; k < b1data.Stride; k++)
                                nRow[k] = oRow2[k];
                        }
                    }

                    //unlock the bitmaps
                    b1.UnlockBits(b1data);
                    b2.UnlockBits(b2data);
                    finalpic.UnlockBits(finalpicdata);


                    //trim the image
                    finalpic = finalpic.Clone(new Rectangle(0, 0, b1.Width, j), PixelFormat.Format32bppRgb);
                    MessageBox.Show(finalpic.Size.Width.ToString() + "," + finalpic.Size.Height.ToString());

                    if (!checkBoxShiftBy1.Checked)
                    {


                        BitmapData finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);



                        for (i = 0; i < finalpic.Height; i++)
                        {
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat == PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);
                                
                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if ((finalpic.GetPixel(ii, i).R == col2.R && finalpic.GetPixel(ii, i).G == col2.G && finalpic.GetPixel(ii, i).B == col2.B))
                                    { flag = 1; break; };
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                        finalpic.SetPixel(int.Parse(obj.ToString()), i, Color.Black);
                                    }
                                }
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
                            }
                            else
                            {
                                flag = 0;
                                for (int ii = 0; ii < b1data.Stride; ii++)
                                    if (oRow[ii * 3] == col2.B && oRow[ii * 3 + 1] == col2.G && oRow[ii * 3 + 2] == col2.R)
                                    {flag = 1; break; }
                                if(flag==1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                            oRow[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                    }

                                }
                            

                            }
                        
                        }
                        finalpic.UnlockBits(finaldata);
                    }
                    else
                    {
                        BitmapData finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);



                        for (i = 0; i < finalpic.Height; i++)
                        {
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);
                                Boolean iscolr = false;
                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if ((finalpic.GetPixel(ii, i).R == col2.R && finalpic.GetPixel(ii, i).G == col2.G && finalpic.GetPixel(ii, i).B == col2.B))
                                    { flag = 1; break; };
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {



                                        if (i == 0)
                                        {

                                            iscolr = true;
                                        }
                                        else
                                            finalpic.SetPixel(int.Parse(obj.ToString()), i - 1, Color.Black);

                                        if (i == finalpic.Height - 1 && iscolr)
                                        {

                                            finalpic.SetPixel(int.Parse(obj.ToString()), i, Color.Black);
                                        }

                                    }
                                }
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadOnly, finalpic.PixelFormat);
                            }

                            else
                            {
                                Boolean iscolr = false;
                                flag = 0;
                                for (int ii = 0; ii < b1data.Stride; ii++)
                                    if (oRow[ii * 3] == col2.B && oRow[ii * 3 + 1] == col2.G && oRow[ii * 3 + 2] == col2.R)
                                    { flag = 1; break; }
                                if (flag == 1)
                                {

                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {

                                        if (i == 0)
                                        {

                                            iscolr = true;
                                        }
                                        else
                                        {
                                            byte* oRow1 = (byte*)finaldata.Scan0 + ((i - 1) * finaldata.Stride);

                                            oRow1[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                        }

                                        if (i == finalpic.Height - 1 && iscolr)
                                        {

                                            oRow[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                        }

                                    }
                                }

                            }
                        }
                        finalpic.UnlockBits(finaldata);
                    }

                    if (finalpic.Height >= 30000 && (domainUpDown1.SelectedIndex == 0))
                    {
                        MessageBox.Show("Converted image size > 30000..It should be less(In Ver1 Cards)"); return;
                    }
                    labelFinalDesign.Text = "(" + b1.Width.ToString() + "," + finalpic.Height.ToString() + ")";
                    b = finalpic;
                    pic_start = 0;
                    pic_end = b.Size.Height / PICSIZE;
                    pic_cur = pic_start;

                    MessageBox.Show("Conversion/Mixing Successfully completed...");
                    int finalhight = b.Height - pic_cur * PICSIZE;
                    if (finalhight > PICSIZE)
                        finalhight = PICSIZE;
                    pictureBoxFinal.Image = finalpic.Clone(new Rectangle(0, 0, b1.Width, finalhight), b1.PixelFormat);
                    buttonUp.Enabled = false;
                    if (b.Height > PICSIZE)
                        buttonDown.Enabled = true;
                    else
                        buttonDown.Enabled = false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        private Bitmap convertToLoomformat_kadiyalDesign(Bitmap finalpic)
        {
            try
            {
                unsafe
                {
                    


                        BitmapData finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                        int i, flag = 0;


                        for (i = 0; i < finalpic.Height; i++)
                        {
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);

                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if ((finalpic.GetPixel(ii, i).R == col2.R && finalpic.GetPixel(ii, i).G == col2.G && finalpic.GetPixel(ii, i).B == col2.B))
                                    { flag = 1; break; };
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                        finalpic.SetPixel(int.Parse(obj.ToString()), i, Color.Black);
                                    }
                                }
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                            }
                            else
                            {
                                flag = 0;
                                for (int ii = 0; ii < finaldata.Stride; ii++)
                                    if (oRow[ii * 3] == col2.B && oRow[ii * 3 + 1] == col2.G && oRow[ii * 3 + 2] == col2.R)
                                    { flag = 1; break; }
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                        oRow[int.Parse(obj.ToString()) * 3] = 0;
                                        oRow[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                        oRow[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                    }

                                }


                            }

                        }
                        


                        for (i = 0; i < finalpic.Height; i++)
                        {
                            if (i == finalpic.Height - 2)
                                break;
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);

                                flag = 0;
                                
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                        
                                        col3= finalpic.GetPixel(int.Parse(obj.ToString()), i);

                                        col4 = finalpic.GetPixel(int.Parse(obj.ToString()), i + 2);
                                        if (col3 == col4 && col3.R == Color.Black.R && col3.B == Color.Black.B && col3.G == Color.Black.G)
                                        {
                                            
                                              flag = 1;
                                              finalpic.SetPixel(int.Parse(obj.ToString()), i + 1, Color.Black);
                                           
                                        }
                                    }
                                    if (flag == 1)
                                        i++;
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                            }
                            else
                            {
                                flag = 0;
                                
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {
                                        byte* oRow2 = (byte*)finaldata.Scan0 + ((i+2) * finaldata.Stride);
                                        if ((oRow[int.Parse(obj.ToString()) * 3] == col2.B && oRow[int.Parse(obj.ToString()) * 3 + 1] == col2.G && oRow[int.Parse(obj.ToString()) * 3 + 2] == col2.R) &&
                                            (oRow2[int.Parse(obj.ToString()) * 3] == col2.B && oRow2[int.Parse(obj.ToString()) * 3 + 1] == col2.G && oRow2[int.Parse(obj.ToString()) * 3 + 2] == col2.R))
                                        {
                                            byte* oRow1 = (byte*)finaldata.Scan0 + ((i+1) * finaldata.Stride);
                                            oRow1[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                            flag=1;
                                        }
                                    }
                                    if(flag==1)
                                        i++;

                              }


                            }
                       

                        
                        finalpic.UnlockBits(finaldata);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            return finalpic;
        }
        private Bitmap convertToLoomformat_shifby1_kadiyalDesign(Bitmap finalpic)
        {
            try
            {
                unsafe
                {
                    

                        BitmapData finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                        int i,flag=0;


                        for (i = 0; i < finalpic.Height; i++)
                        {
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);
                                Boolean iscolr = false;
                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if ((finalpic.GetPixel(ii, i).R == col2.R && finalpic.GetPixel(ii, i).G == col2.G && finalpic.GetPixel(ii, i).B == col2.B))
                                    { flag = 1; break; };
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {



                                        if (i == 0)
                                        {

                                            iscolr = true;
                                        }
                                        else
                                            finalpic.SetPixel(int.Parse(obj.ToString()), i - 1, Color.Black);

                                        if (i == finalpic.Height - 1 && iscolr)
                                        {

                                            finalpic.SetPixel(int.Parse(obj.ToString()), i, Color.Black);
                                        }

                                    }
                                }
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                            }

                            else
                            {
                                Boolean iscolr = false;
                                flag = 0;
                                for (int ii = 0; ii < finaldata.Stride; ii++)
                                    if (oRow[ii * 3] == col2.B && oRow[ii * 3 + 1] == col2.G && oRow[ii * 3 + 2] == col2.R)
                                    { flag = 1; break; }
                                if (flag == 1)
                                {

                                    foreach (Object obj in comboBoxAddColumn.Items)
                                    {

                                        if (i == 0)
                                        {

                                            iscolr = true;
                                        }
                                        else
                                        {
                                            byte* oRow1 = (byte*)finaldata.Scan0 + ((i - 1) * finaldata.Stride);

                                            oRow1[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow1[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                        }

                                        if (i == finalpic.Height - 1 && iscolr)
                                        {

                                            oRow[int.Parse(obj.ToString()) * 3] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                            oRow[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                        }

                                    }
                                }

                            }
                        }
                        for (i = 0; i < finalpic.Height; i++)
                        {
                            if (i == finalpic.Height - 2)
                            {
                                break;
                            }
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);

                                flag = 0;
                                
                                foreach (Object obj in comboBoxAddColumn.Items)
                                {
                                    
                                    col3 = finalpic.GetPixel(int.Parse(obj.ToString()), i);
                                    col4 = finalpic.GetPixel(int.Parse(obj.ToString()), i + 2);
                                    if (col3 == col4 && col3.R == Color.Black.R && col3.B == Color.Black.B && col3.G == Color.Black.G)
                                    {

                                        flag = 1;
                                        finalpic.SetPixel(int.Parse(obj.ToString()), i + 1, Color.Black);

                                    }
                                }
                                if (flag == 1)
                                    i++;
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                            }
                            else
                            {
                                flag = 0;
                                
                                foreach (Object obj in comboBoxAddColumn.Items)
                                {
                                    byte* oRow2 = (byte*)finaldata.Scan0 + ((i + 2) * finaldata.Stride);
                                    if ((oRow[int.Parse(obj.ToString()) * 3] == col2.B && oRow[int.Parse(obj.ToString()) * 3 + 1] == col2.G && oRow[int.Parse(obj.ToString()) * 3 + 2] == col2.R) &&
                                        (oRow2[int.Parse(obj.ToString()) * 3] == col2.B && oRow2[int.Parse(obj.ToString()) * 3 + 1] == col2.G && oRow2[int.Parse(obj.ToString()) * 3 + 2] == col2.R))
                                    {
                                        byte* oRow1 = (byte*)finaldata.Scan0 + ((i + 1) * finaldata.Stride);
                                        oRow1[int.Parse(obj.ToString()) * 3] = 0;
                                        oRow1[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                        oRow1[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                        flag = 1;
                                    }
                                }
                                if (flag == 1)
                                    i++;

                            }


                        }


                        
                        finalpic.UnlockBits(finaldata);

                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            return finalpic;
        }

        private void buttonKadiyalDesign_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.KadiyalColumns.Clear();
            foreach (object obj in comboBoxAddColumn.Items)
            {
                Properties.Settings.Default.KadiyalColumns.Add(obj.ToString());
                
            }
            Properties.Settings.Default.Save();
                
        }

        private void comboBoxAddColumn1_KeyDown(object sender, KeyEventArgs e)
        {
            comboBoxAddColumns(b1,comboBoxAddColumn1, e);
        }

        private void comboBoxAddColumn2_KeyDown(object sender, KeyEventArgs e)
        {
            comboBoxAddColumns(b2,comboBoxAddColumn2, e);
        }

        private void comboBoxAddColumn3_KeyDown(object sender, KeyEventArgs e)
        {
            comboBoxAddColumns(b3,comboBoxAddColumn3, e);
        }

        private void comboBoxAddColumn4_KeyDown(object sender, KeyEventArgs e)
        {
            comboBoxAddColumns(b4,comboBoxAddColumn4, e);
        }

        private void buttonApply1_Click(object sender, EventArgs e)
        {
            if (b1 == null)
            {
                MessageBox.Show("Please Load the Image First...");
                return;
            }
            
            pictureBoxDesign.Image = addColumns(b1,comboBoxAddColumn1);
        }
        private void buttonApply2_Click(object sender, EventArgs e)
        {
            if (b2 == null)
            {
                MessageBox.Show("Please Load the Image First...");
                return;
            }

            pictureBoxM1.Image = addColumns(b2,comboBoxAddColumn2);
        }

        private void buttonApply3_Click(object sender, EventArgs e)
        {
            if (b3 == null)
            {
                MessageBox.Show("Please Load the Image First...");
                return;
            }

            pictureBoxM2.Image = addColumns(b3,comboBoxAddColumn3);
        }

        private void buttonApply4_Click(object sender, EventArgs e)
        {
            if (b4 == null)
            {
                MessageBox.Show("Please Load the Image First...");
                return;
            }

            pictureBoxM3.Image = addColumns(b4,comboBoxAddColumn4);
        }
        private Bitmap addColumns(Bitmap finalpic,ComboBox comboBoxAddCol)
        {
            
            try
            {
                unsafe
                {
                    


                        BitmapData finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                        int i, flag = 0;
                        Color coll = Color.White;
                            

                        for (i = 0; i < finalpic.Height; i++)
                        {
                            byte* oRow = (byte*)finaldata.Scan0 + (i * finaldata.Stride);

                            if (finalpic.PixelFormat != PixelFormat.Format24bppRgb)
                            {
                                finalpic.UnlockBits(finaldata);

                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if ((finalpic.GetPixel(ii, i).R != coll.R && finalpic.GetPixel(ii, i).G != coll.G && finalpic.GetPixel(ii, i).B != coll.B))
                                    { flag = 1; break; };
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddCol.Items)
                                    {
                                        finalpic.SetPixel(int.Parse(obj.ToString()), i, Color.Black);
                                    }
                                }
                                finaldata = finalpic.LockBits(new Rectangle(0, 0, finalpic.Width, finalpic.Height), ImageLockMode.ReadWrite, finalpic.PixelFormat);
                            }
                            else
                            {
                                flag = 0;
                                for (int ii = 0; ii < finalpic.Width; ii++)
                                    if (oRow[ii * 3] != 255 && oRow[ii * 3 + 1] !=255 && oRow[ii * 3 + 2] !=255)
                                    { flag = 1; break; }
                                if (flag == 1)
                                {
                                    foreach (Object obj in comboBoxAddCol.Items)
                                    {
                                        oRow[int.Parse(obj.ToString()) * 3] = 0;
                                        oRow[int.Parse(obj.ToString()) * 3 + 1] = 0;
                                        oRow[int.Parse(obj.ToString()) * 3 + 2] = 0;
                                    }

                                }


                            }

                        }


                                           
                        finalpic.UnlockBits(finaldata);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            return finalpic;
        
        }

        private void buttonSaveProfile_Click(object sender, EventArgs e)
        {
             SaveFileDialog saveProfileFileDialog = new SaveFileDialog();
                saveProfileFileDialog.Filter = "CSV File|*.csv";
                saveProfileFileDialog.OverwritePrompt = false;

                saveProfile = new StringBuilder();
                saveProfileFileDialog.ShowDialog();
                if (saveProfileFileDialog.FileName != "")
                {
                    if (comboBoxAddColumn1.Items.Count != 0)
                    {
                        
                        string one = "1";
                        foreach (object obj in comboBoxAddColumn1.Items)
                        {
                            one = one +","+ obj.ToString();
                        }
                        var newLine = string.Format("{0},{1}",one,Environment.NewLine);
                        saveProfile.Append(newLine);
                    }
                    if (comboBoxAddColumn2.Items.Count != 0)
                    {

                        string one = "2";
                        foreach (object obj in comboBoxAddColumn2.Items)
                        {
                            one = one + "," + obj.ToString();
                        }
                        var newLine = string.Format("{0},{1}", one, Environment.NewLine);
                        saveProfile.Append(newLine);
                    }
                    if (comboBoxAddColumn3.Items.Count != 0)
                    {

                        string one = "3";
                        foreach (object obj in comboBoxAddColumn3.Items)
                        {
                            one = one + "," + obj.ToString();
                        }
                        var newLine = string.Format("{0},{1}", one, Environment.NewLine);
                        saveProfile.Append(newLine);
                    }
                    if (comboBoxAddColumn4.Items.Count != 0)
                    {

                        string one = "4";
                        foreach (object obj in comboBoxAddColumn4.Items)
                        {
                            one = one + "," + obj.ToString();
                        }
                        var newLine = string.Format("{0},{1}", one, Environment.NewLine);
                        saveProfile.Append(newLine);
                    }
                    
                    File.WriteAllText(@saveProfileFileDialog.FileName, saveProfile.ToString());
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            OpenFileDialog openProfileDialog= new OpenFileDialog();
            openProfileDialog.Filter= "CSV File|*.csv";

            int nom = 0;
            if (openProfileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = openProfileDialog.FileName;
                comboBoxAddColumn1.Items.Clear();
                comboBoxAddColumn2.Items.Clear();
                comboBoxAddColumn3.Items.Clear();
                comboBoxAddColumn4.Items.Clear();
                using (var rd = new StreamReader(filename))
                    {
                        while (!rd.EndOfStream)
                        {
                            var splits = rd.ReadLine().Split(',');
                            if (int.Parse(splits[0]) == 1)
                            {
                                
                                comboBoxAddColumn1.Items.AddRange(splits);
                                comboBoxAddColumn1.Items.RemoveAt(0);
                                comboBoxAddColumn1.Items.RemoveAt(comboBoxAddColumn1.Items.Count - 1);
                                buttonApply1_Click(sender, e);

                            }
                            else if (int.Parse(splits[0]) == 2)
                            {
                                
                                comboBoxAddColumn2.Items.AddRange(splits);
                                comboBoxAddColumn2.Items.RemoveAt(0);
                                comboBoxAddColumn2.Items.RemoveAt(comboBoxAddColumn2.Items.Count - 1);
                                buttonApply2_Click(sender, e);
                            }
                            else if (int.Parse(splits[0]) == 3)
                            {
                                
                                comboBoxAddColumn3.Items.AddRange(splits);
                                comboBoxAddColumn3.Items.RemoveAt(0);
                                comboBoxAddColumn3.Items.RemoveAt(comboBoxAddColumn3.Items.Count - 1);
                                buttonApply3_Click(sender, e);
                            }
                            else if (int.Parse(splits[0]) == 4)
                            {
                                comboBoxAddColumn4.Items.AddRange(splits);
                                comboBoxAddColumn4.Items.RemoveAt(0);
                                comboBoxAddColumn4.Items.RemoveAt(comboBoxAddColumn4.Items.Count - 1);
                                buttonApply4_Click(sender, e);
                            }

                            //column2.Add(splits[1]);
                        }
                    }
            }
        }

        

        

       










    }


    class HDDValidate
    {
        static private string HDDToActivate;


        /* NOTE THAT THE STATIC STRINGS CAN BE ASKED FOR FROM THE USER DURING THE FIRST RUNTIME, AND CAN BE ASKED IF THIS DIALOG BOX IS TO BE SHOWN AGAIN.
        AS SOON AS HE SELECTS THE PATH OF THE WORKSPACE AND EXECUTABLE, THE STRINGS WILL BE STORED AND CAN BE USED IN Runner()    THIS CAN BE ACHIEVED USING A SIMPLE 
        FORMS APPLICATION	. NOTE THAT IT CAN ALSO BE OBTAINED BY ASKING THE USER WHERE TO INSTALL THE SOFTWARE DURING THE INSTALL PROCESS. */


        public static void HardDriveHardCode(string ToActivate)   // just pass the hard drive serial number on which the application should be running
        {

            HDDToActivate = ToActivate;
        }


        public static int Initiate()                                   // Initiate() to be used to 
        {

            string DeviceProspectiveStr = GetHDDSerial();
            //DeviceProspectiveStr.Replace(" ", String.Empty);
            DeviceProspectiveStr = DeviceProspectiveStr.Trim();
            int ret = -99;                          // SENTINEL returns 0 if the two strings are equal

            if (String.Equals(DeviceProspectiveStr, HDDToActivate) == true)
            {

                ret = 0;
                return ret;
            }
            else
                return ret;
        }

        public static string GetHDDSerial()  // reads the HDD  serial, returns it in the form of a string
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] != null)
                    return wmi_HD["SerialNumber"].ToString();
            }

            return string.Empty;
        }

    }
    //To get the Bios Number.
    class DiskUtils
    {

        public String getSerialNumber()
        {
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c wmic bios get serialnumber");
            procInfo.RedirectStandardOutput = true;
            procInfo.UseShellExecute = false;
            procInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procInfo;
            proc.Start();

            string result = proc.StandardOutput.ReadToEnd();
            result = result.Substring(13, result.Length - 13);
            result = result.Trim();
            return result;
        }
    }
    //encryption classes
    public class CryptSerial
    {
        /**
        * This function will encrypt the serial
        * @param diskSerial is the string needs to be encrypted
        * @return encrypted string
        */
        public String encrypt(String diskSerial)
        {
            try
            {
                return new Encryption().encrypt(diskSerial);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /**
         * This function will decrypt the serial bases on the logic of encryption
         * @param diskSerial is string which needs to be decrypted
         * @return decrypted String
         */
        public String decrypt(String encryptedSerial)
        {
            try
            {
                return new Encryption().decrypt(encryptedSerial);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    class Encryption
    {
        //add using System.Security.Cryptography;
        //add using System.IO
        static readonly string PasswordHash = "P@@Sw0rdRock";
        static readonly string SaltKey = "S@LT&KEYRock";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public string encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }



}
