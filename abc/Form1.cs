using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(e.GetHashCode() == )
        }


        private void button1_Click(object sender, EventArgs e)
        {

            if (Clipboard.ContainsImage())
            {
                Clipboard.GetImage().Save(@"C:\Users\6emre\Desktop\printScreen.jpg", ImageFormat.Jpeg);

            }
        }
    }
}
