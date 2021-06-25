using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FancyFormsApplication
{
    public class AsposeUtils
    {
        static AsposeUtils()
        {
            new Aspose.Cells.License().SetLicense(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/license", "Aspose.Total.lic"));
            new Aspose.Pdf.License().SetLicense(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/license", "Aspose.Total.lic"));
            new Aspose.Words.License().SetLicense(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/license", "Aspose.Total.lic"));
        }

        public static string ExcelToPdf(string excelFile, string pdfFile)
        {
            Aspose.Cells.Workbook xls = new Aspose.Cells.Workbook(excelFile);
            Aspose.Cells.PdfSaveOptions pdfSaveOptions = new Aspose.Cells.PdfSaveOptions(Aspose.Cells.SaveFormat.Pdf);
            pdfSaveOptions.OnePagePerSheet = true;
            //pdfSaveOptions.
            xls.Save(pdfFile, pdfSaveOptions);
            return pdfFile;
        }


        public static string WordToPdf(string wordFile, string pdfFile)
        {
            Aspose.Words.Document doc = new Aspose.Words.Document(wordFile);
            doc.Save(pdfFile, Aspose.Words.SaveFormat.Pdf);
            return pdfFile;
        }

        public static string ImgToPdf(string file, string pdfFile)
        {
            Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            image.File = file;
            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            Aspose.Pdf.Page page = doc.Pages.Add();
            // 添加图片到页面段落
            page.Paragraphs.Add(image);
            System.Drawing.Image image2 = System.Drawing.Image.FromFile(file);
            page.Paragraphs.Add(image);
            page.PageInfo.Height = image2.Height;
            page.PageInfo.Width = image2.Width;
            doc.Save(pdfFile);
            return pdfFile;
        }

        public static string ImgToPdf(List<string> imgList, string pdfFile)
        {
            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            Aspose.Pdf.Page page = doc.Pages.Add();
            foreach (string imgPath in imgList)
            {
                Aspose.Pdf.Image image = new Aspose.Pdf.Image();
                image.File = imgPath;
                page.Paragraphs.Add(image);
                System.Drawing.Image image2 = System.Drawing.Image.FromFile(imgPath);
                page.Paragraphs.Add(image);
                page.PageInfo.Height = image2.Height;
                page.PageInfo.Width = image2.Width;
            }
            doc.Save(pdfFile);
            return pdfFile;
        }


        public static string PdfToWord(string pdfFile, string wordFile)
        {
            Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(pdfFile);
            pdf.Save(wordFile, Aspose.Pdf.SaveFormat.DocX);
            return wordFile;
        }
        public static string PdfToImg(string pdfFile, string imgDirectory)
        {

            //定义Jpeg转换设备
            Aspose.Pdf.Document document = new Aspose.Pdf.Document(pdfFile);
            var device = new Aspose.Pdf.Devices.JpegDevice();
            //int quality = int.Parse(100);
            //directoryPath += quality;
            //Directory.CreateDirectory(directoryPath);
            //默认质量为100，设置质量的好坏与处理速度不成正比，甚至是设置的质量越低反而花的时间越长，怀疑处理过程是先生成高质量的再压缩
            device = new Aspose.Pdf.Devices.JpegDevice(100);
            //遍历每一页转为jpg
            for (var i = 1; i <= document.Pages.Count; i++)
            {
                string filePathOutPut = Path.Combine(imgDirectory, string.Format("{0}.jpg", i));// Path.Combine(directoryPath, string.Format("{0}.jpg", i));
                FileStream fs = new FileStream(filePathOutPut, FileMode.OpenOrCreate);
                try
                {
                    device.Process(document.Pages[i], fs);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    fs.Close();
                    File.Delete(filePathOutPut);
                }
            }
            return imgDirectory;

        }

        public static string PdfMergn(List<string> pdfFiles, string newPdfFIle)
        {
            Aspose.Pdf.Document pdf1 = new Aspose.Pdf.Document(pdfFiles[0]);
            for (var i = 1; i < pdfFiles.Count; i++)
            {
                Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(pdfFiles[i]);
                foreach (Aspose.Pdf.Page page in pdf.Pages)
                {
                    pdf1.Pages.Add(page);
                }
            }
            pdf1.Save(newPdfFIle);
            return newPdfFIle;

        }

        public static string PdfWatermark(List<string> pdfFiles, string pdfDirectory, string watermark)        {   
            Aspose.Pdf.Facades.FormattedText formattedText = new Aspose.Pdf.Facades.FormattedText
                (watermark, System.Drawing.Color.LightGray, 
                Aspose.Pdf.Facades.FontStyle.TimesItalic, 
                Aspose.Pdf.Facades.EncodingType.Identity_h,
                true, 
                70F);            
            Aspose.Pdf.TextStamp textStamp = new Aspose.Pdf.TextStamp(formattedText);
            textStamp.Opacity = 0.7;//透明度
            textStamp.VerticalAlignment = Aspose.Pdf.VerticalAlignment.Center;
            textStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;            
            foreach (string pdfFile in pdfFiles)            {                Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(pdfFile);
                foreach (Aspose.Pdf.Page page in pdf.Pages)                {                    page.AddStamp(textStamp);                }                pdf.Save(Path.Combine(pdfDirectory, Path.GetFileName(pdfFile)));            }
            return pdfDirectory;        }
    }
}
