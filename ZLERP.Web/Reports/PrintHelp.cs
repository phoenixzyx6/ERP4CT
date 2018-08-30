using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text;
using System.Data;
using System.Drawing;

namespace ZLERP.Web.Reports
{
    /// <summary>
    /// PrintHelp 的摘要描述
    /// </summary>
    public class PrintHelp
    {


        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="printerName"></param>
        public void Run(LocalReport report,string printerName, bool landscape)
        {
            //LocalReport report = new LocalReport();
            //report.ReportPath = reportPath;//加上报表的路径
            //report.DataSources.Add(new ReportDataSource(dt1SourceName, dt1));
            //report.DataSources.Add(new ReportDataSource(dt2SourceName, dt2));
            //report.EnableExternalImages = true;
            //ReportParameter rp = new ReportParameter("isHindeLogoImg", isHindeLogo.ToString());//这里我在报表里弄的参数
            //report.SetParameters(rp);
            //report.GetDefaultPageSettings().PaperSize.h
            
            Export(report);
            m_currentPageIndex = 0;
            landscape = report.GetDefaultPageSettings().IsLandscape;
            Print(printerName, landscape);
        }

        private void Export(LocalReport report)
        {
            
            //这里是设置打印的格式 边距什么的
            double width = Convert.ToDouble(report.GetDefaultPageSettings().PaperSize.Width)/25.4;
            double height = Convert.ToDouble(report.GetDefaultPageSettings().PaperSize.Height) / 25.4;
            //string deviceInfo =
            //  "<DeviceInfo>" +
            //  "  <OutputFormat>EMF</OutputFormat>" +
            //  "  <PageWidth>"+width+"mm</PageWidth>" +
            //  "  <PageHeight>"+height+"mm</PageHeight>" +
            //  "  <MarginTop>5mm</MarginTop>" +
            //  "  <MarginLeft>2mm</MarginLeft>" +
            //  "  <MarginRight>2mm</MarginRight>" +
            //  "  <MarginBottom>5mm</MarginBottom>" +
            //  "</DeviceInfo>";
            string deviceInfo =
             "<DeviceInfo>" +
             "  <OutputFormat>EMF</OutputFormat>" +
             "</DeviceInfo>";        
            Warning[] warnings;
            m_streams = new List<Stream>();
            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);//一般情况这里会出错的  使用catch得到错误原因  一般都是简单错误
            }
            catch (Exception ex)
            {
                Exception innerEx = ex.InnerException;//取内异常。因为内异常的信息才有用，才能排除问题。
                while (innerEx != null)
                {
                    //MessageBox.Show(innerEx.Message);
                    string errmessage = innerEx.Message;
                    innerEx = innerEx.InnerException;
                }
            }
            foreach (Stream stream in m_streams)
            {
                stream.Position = 0;
            }
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //name 需要进一步处理
            Stream stream = new FileStream(name + DateTime.Now.Millisecond + "." + fileNameExtension, FileMode.Create);//为文件名加上时间
            m_streams.Add(stream);
            return stream;
        }

        public void Print(string printerName, bool landscape)
        {
            //string printerName = this.TextBox1.Text.Trim();// "傳送至 OneNote 2007";
            if (m_streams == null || m_streams.Count == 0)
                return;
            PrintDocument printDoc = new PrintDocument();

            //printDoc.PrinterSettings.PrintToFile = true;//获取或设置一个值，该值指示是否发送到文件或端口 

            //设置打印时横向还是纵向 
            printDoc.DefaultPageSettings.Landscape = landscape; 
         
            if (printerName.Length > 0)
            {
                printDoc.PrinterSettings.PrinterName = printerName;
            }
            else
            {
                foreach (string fPrinterName in PrinterSettings.InstalledPrinters)
                {
                    printDoc.PrinterSettings.PrinterName = fPrinterName;
                    break;
                }
                
            }           
            //foreach (PaperSize ps in printDoc.PrinterSettings.PaperSizes)
            //{
            //    if (ps.PaperName == "A4")
            //    {
            //        printDoc.PrinterSettings.DefaultPageSettings.PaperSize = ps;
            //        printDoc.DefaultPageSettings.PaperSize = ps;
            //        //printDoc.PrinterSettings.IsDefaultPrinter;//知道是否是预设定的打印机
            //    }
            //}

            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format("Can't find printer " + printerName);
                System.Diagnostics.Debug.WriteLine(msg);
                return;
            }
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            ////初始化打印对话框对象       
            //PrintDialog printDialog1 = new PrintDialog();       
            ////将PrintDialog.UseEXDialog属性设置为True，才可显示出打印对话框       
            //printDialog1.UseEXDialog = true;       
            ////将printDocument1对象赋值给打印对话框的Document属性       
            //printDialog1.Document = printDocument1;       
            ////打开打印对话框       
            //DialogResult result = printDialog1.ShowDialog();       
            //if (result == DialogResult.OK)                  
            //    printDocument1.Print();//开始打印

            printDoc.Print();
        }


        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            //ev.Graphics.DrawImage(pageImage, 0, 0,1169, 827);//設置打印尺寸 单位是像素

            //调整打印机区域的边距
            Rectangle adjustedRect;
            adjustedRect = new Rectangle(
                    ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                    ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                    ev.PageBounds.Width,
                    ev.PageBounds.Height);

            //绘制一个白色背景的报告
            //ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            //这里的Graphics对象实际指向了打印机
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            //ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
    }
}