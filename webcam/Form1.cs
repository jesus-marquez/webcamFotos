using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace webcam
{
    public partial class Form1 : Form
    {
        Emgu.CV.Capture captura;
        SaveFileDialog guardado = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            if (captura==null)
            {
                captura = new Emgu.CV.Capture(0);
            }
            captura.ImageGrabbed += activarWebCam;
            captura.Start();
        }

        private void activarWebCam(object sender, EventArgs e)
        {
            try
            {
                Mat img = new Mat();
                captura.Retrieve(img);
                pictureBox1.Image = img.ToImage<Bgr, byte>().Bitmap;
            }catch(Exception )
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            if (captura != null)
            {
                captura = null;
                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            guardado.Filter = "Format jpg|.jpg";
            guardado.Title = "Guardar como:";
            var hora = DateTime.Now;
           // string result=Regex.Replace(hora,@"[^\w\d]", "-");
            guardado.FileName = hora.ToString("dd-mm-yyyy_hh-mm");

            if (guardado.ShowDialog() == DialogResult.OK)
            {
                Mat img = new Mat();
                captura.Retrieve(img);
                pictureBox1.Image = img.ToImage<Bgr, byte>().Bitmap;
                
                pictureBox1.Image.Save(guardado.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            }
               
        }
    }
}
