using System.Net;
using Emgu.CV.CvEnum;
using System.Linq.Expressions;
using Emgu.CV.Util;
using System.CodeDom;
using System.Net.Configuration;
using Emgu.CV.Structure;
using ZXing.QrCode.Internal;
using ZXing.QrCode;
using ZXing.Rendering;
using ZXing.Datamatrix;
using ZXing.Common;
using ZXing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace zxingApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Image<Gray, byte> image;
            //Image<Gray, byte> circle = new Image<Gray, byte>(@"J:\Code\C#\Practice\QRcircular.png");
            Image<Gray, byte> mainImage;
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            BarcodeWriter writer = new BarcodeWriter();
            QRCodeWriter qR = new QRCodeWriter();

            Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();
            hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M);
            BitMatrix bit = qR.encode(textBox2.Text, BarcodeFormat.QR_CODE, 300, 300, hints);
            Bitmap bitmap = writer.Write(bit);
            image = new Image<Gray, byte>(bitmap);
            mainImage = new Image<Gray, byte>(bitmap);
            pictureBox1.Image = bitmap;
            // find moraba
            //image._ThresholdBinaryInv(new Gray(127), new Gray(255));
            //CvInvoke.FindContours(image, contours,new Mat() , Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);


            //Point[][] points = contours.ToArrayOfArray();
            //for (int i = 0; i < points.Length; i++)
            //{
            //    if (points[i].Length == 4)
            //    {
            //        VectorOfPoint vectorOfPoint = new VectorOfPoint(points[i]);
            //        if (CvInvoke.ContourArea(vectorOfPoint) > 1200)
            //        {
            //            Rectangle rectangle = CvInvoke.BoundingRectangle(vectorOfPoint);
            //            circle = circle.Resize(rectangle.Width, rectangle.Height, Emgu.CV.CvEnum.Inter.Area);
            //            mainImage.ROI = new Rectangle(rectangle.Location, circle.Size);
            //            circle.CopyTo(mainImage);
            //        }
            //    }
            //}

            mainImage.ROI = new Rectangle();
            pictureBox1.Image = mainImage.ToBitmap();
            mainImage.Save(@"J:\Code\C#\Practice\QR.bmp");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BarcodeReader reader = null;
            Image<Bgr, byte> image = null;

            try
            {
                reader = new BarcodeReader();
                image = new Image<Bgr, byte>(@"J:\Code\C#\Practice\QR.bmp");
                //image._ThresholdBinary(new Gray(127), new Gray(255));
                Result result = reader.Decode(image.ToBitmap());
                textBox1.Text = result.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Try again");
            }
            finally
            {
                image.Dispose();
                reader = null;
            }

        }
    }
}
