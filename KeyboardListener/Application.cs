using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;
using System.Drawing.Imaging;
using System.IO;

/*
 * Author : Emre Derman
 * Date : 07/29/2020
 * 
 */

namespace KeyboardListener
{
    public class Application : System.Windows.Forms.Form
    {
        public int counter = 1;
        private System.Windows.Forms.Label lblKeyPressed;
        private NotifyIcon notifyIcon1;
        private Button btnClean;
        private IContainer components;
        private Button FileFormat;
        public string CapturePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public Application()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Application));
            this.lblKeyPressed = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnClean = new System.Windows.Forms.Button();
            this.FileFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKeyPressed
            // 
            this.lblKeyPressed.BackColor = System.Drawing.Color.LightYellow;
            this.lblKeyPressed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKeyPressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyPressed.Location = new System.Drawing.Point(0, 0);
            this.lblKeyPressed.Name = "lblKeyPressed";
            this.lblKeyPressed.Size = new System.Drawing.Size(536, 287);
            this.lblKeyPressed.TabIndex = 0;
            this.lblKeyPressed.Text = "Press a key...";
            this.lblKeyPressed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKeyPressed.Click += new System.EventHandler(this.lblKeyPressed_Click);
            this.lblKeyPressed.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblKeyPressed_MouseDoubleClick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ScreenShooter";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(42, 199);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(189, 41);
            this.btnClean.TabIndex = 1;
            this.btnClean.Text = "Temizle";
            this.btnClean.UseVisualStyleBackColor = true;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // FileFormat
            // 
            this.FileFormat.Location = new System.Drawing.Point(279, 199);
            this.FileFormat.Name = "FileFormat";
            this.FileFormat.Size = new System.Drawing.Size(208, 40);
            this.FileFormat.TabIndex = 2;
            this.FileFormat.Text = "Farkli kaydet";
            this.FileFormat.UseVisualStyleBackColor = true;
            this.FileFormat.Click += new System.EventHandler(this.FileFormat_Click);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(536, 287);
            this.Controls.Add(this.FileFormat);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.lblKeyPressed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Keyboard Listener";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new Application());
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            CapturePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\ScreenShots");
            if (!Directory.Exists(CapturePath))
            {
                Directory.CreateDirectory(CapturePath);
            }
            // Watch for keyboard activity
            KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
            this.WindowState = FormWindowState.Minimized;
            ShowInTaskbar = true;
        }

        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
            // 256 : key down    257 : key up
            if (eventArgs.m_Msg == 256 && Clipboard.ContainsImage() && eventArgs.KeyData == Keys.PrintScreen)
            {
                string fileName = string.Format("{0}\\{1}.jpeg", CapturePath, counter);
                Clipboard.GetImage().Save(fileName, ImageFormat.Jpeg);
                counter = counter + 1;
            }
        }

        private void lblKeyPressed_Click(object sender, EventArgs e)
        {
        }
        private void lblKeyPressed_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(CapturePath))
            {
                DirectoryInfo directory = new DirectoryInfo(CapturePath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }
        }

        private void FileFormat_Click(object sender, EventArgs e)
        {
            string dummyFileName = "Save Here";

            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = dummyFileName;

            if (sf.ShowDialog() == DialogResult.OK)
            {            
                CapturePath = Path.GetDirectoryName(sf.FileName);
            }
        }
    }
}
