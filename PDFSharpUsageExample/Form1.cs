using PdfiumViewer;
using System;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;


namespace PDFSharpUsageExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
        }


        private void ProcessPDFFileToImage(string fileName)
        {
            #region spire.pdf lib
            //PdfDocument doc = new PdfDocument();
            //doc.LoadFromFile(fileName);
            //for (int i = 0; i < doc.Pages.Count; i++)
            //{
            //    Image bmp = doc.SaveAsImage(i);
            //    Image emf = doc.SaveAsImage(i, Spire.Pdf.Graphics.PdfImageType.Metafile);
            //    Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
            //    using (Graphics g = Graphics.FromImage(zoomImg))
            //    {
            //        g.ScaleTransform(2.0f, 2.0f);
            //        g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
            //    }
            //    var bmpFileName = string.Format("convertToBmp{0}.bmp", i);
            //    bmp.Save(bmpFileName, ImageFormat.Bmp);
            //    System.Diagnostics.Process.Start(bmpFileName);
            //    var emfFileName = string.Format("convertToEmf{0}.bmp", i);
            //    emf.Save(emfFileName, ImageFormat.Png);
            //    System.Diagnostics.Process.Start(emfFileName);
            //    var zoomImgFileName = string.Format("convertToZoom{0}.bmp", i);
            //    zoomImg.Save(zoomImgFileName, ImageFormat.Png);
            //    System.Diagnostics.Process.Start(zoomImgFileName);
            //}
            #endregion

            #region use pdfiumviewer
            using (var doc = PdfDocument.Load(fileName)) // C# Read PDF File
            {
                for (int i = 0; i < doc.PageCount; i++)
                {
                    var dpi = 300;

                    using (var image = doc.Render(i, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
                    {
                        var encoder = ImageCodecInfo.GetImageEncoders()
                            .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        var encParams = new EncoderParameters(1);
                        encParams.Param[0] = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, 100L);

                        image.Save(@"output_withEncoder_" + i + ".jpg", encoder, encParams);
                        image.Save(@"output_" + i + ".jpg", ImageFormat.Jpeg);
                        //image.Save(@"output_" + i + ".jpg", encoder, encParams);
                        //image.Save(@"output_" + i + ".jpg", encoder, encParams);
                    }
                }
            }
            #endregion
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_selectFile_Click(object sender, EventArgs e)
        {
            // Show the dialog that allows user to select a file, the 
            // call will result a value from the DialogResult enum
            // when the dialog is dismissed.
            DialogResult result = this.openFileDialog1.ShowDialog();
            // if a file is selected
            if (result == DialogResult.OK)
            {
                // Set the selected file URL to the textbox
                this.textBox1.Text = this.openFileDialog1.FileName;
                ProcessPDFFileToImage(this.openFileDialog1.FileName);
            }
        }
    }
}
