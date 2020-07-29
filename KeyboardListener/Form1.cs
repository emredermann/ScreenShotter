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
namespace Example
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		public int counter = 1;
		private System.Windows.Forms.Label lblKeyPressed;
        private NotifyIcon notifyIcon1;
        private IContainer components;

        public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			
			InitializeComponent();
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblKeyPressed = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // lblKeyPressed
            // 
            this.lblKeyPressed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKeyPressed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyPressed.Location = new System.Drawing.Point(0, 0);
            this.lblKeyPressed.Name = "lblKeyPressed";
            this.lblKeyPressed.Size = new System.Drawing.Size(298, 64);
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
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(298, 64);
            this.Controls.Add(this.lblKeyPressed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Keyboard Listener";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Resizes);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			// how to ignore "" in c#

			string root = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\ScreenShots");
			Console.WriteLine(root);
			
				// If directory does not exist, create it. 

			if (!Directory.Exists(root))
			{
				Directory.CreateDirectory(root);
			}		
			Application.Run(new Form1());
			
		}


		private void Form1_Load(object sender, System.EventArgs e)
		{
			// Watch for keyboard activity
			KeyboardListener.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
			this.WindowState = FormWindowState.Minimized;
			ShowInTaskbar = true;
		}
		 
		private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
		{
			KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;
			lblKeyPressed.Text = string.Format("Key = {0}  Msg = {1}  Text = {2}", eventArgs.m_Key, eventArgs.m_Msg, eventArgs.KeyData);


			// 256 : key down    257 : key up
			if(eventArgs.m_Msg == 256)
			{
				this.BackColor = Color.Red;
				if (Clipboard.ContainsImage() && eventArgs.KeyData == Keys.PrintScreen)
				{

					// how to ignore "" in c#
					string fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\ScreenShots")+
						"\\"+ counter.ToString()+".jpeg";				
					Clipboard.GetImage().Save(fileName, ImageFormat.Jpeg);
					counter = counter + 1;
				}
				
			}
			else
			{
				this.BackColor = Color.Green;				
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
			notifyIcon1.Visible = false;
			WindowState = FormWindowState.Normal;
		}

        
        private void Resizes(object sender, EventArgs e)
        {			
			ShowInTaskbar = true;
			
		}
    }
}
