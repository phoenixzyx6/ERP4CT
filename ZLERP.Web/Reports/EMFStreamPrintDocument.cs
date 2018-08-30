using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Microsoft.Reporting.WebForms;
using System.Web;

namespace ZLERP.Web.Reports
{
    public partial class EMFStreamPrintDocument : PrintDocument
    {
        private int m_CurrentPageIndex;

        private IList<Stream> m_EMFStreams;
        private LocalReport m_LocalReport = null;
        private System.Drawing.Imaging.Metafile m_PageImage = null;

        public EMFStreamPrintDocument(LocalReport localReport, string ReportName)
            : base()
        {
            this.m_ReportName = ReportName;
            string strMessage = string.Empty;
            if (this.ReadSetting(out strMessage) == null)
                this.m_ErrorMessage = strMessage;
            if (this.m_ErrorMessage == string.Empty)
            {
                this.m_LocalReport = localReport;
            }
        }

        private string m_ErrorMessage = string.Empty;

        public string ErrorMessage
        {
            get
            {
                return this.m_ErrorMessage;
            }
            set
            {
                this.m_ErrorMessage = value;
            }
        }

        private string m_ReportName = string.Empty;
        
        private EMFDeviceInfo ReadSetting(out string WarningMessage)
        {

            WarningMessage = string.Empty;

            System.Data.DataSet dsXML = new DataSet();

            EMFDeviceInfo emfdi = new EMFDeviceInfo();

            dsXML.ReadXml(HttpRuntime.BinDirectory + @"\ReportSettings.xml");
            
            System.Data.DataTable dtXML = new DataTable();

            foreach (System.Data.DataTable dt in dsXML.Tables)
            {
                if (dt.TableName == this.m_ReportName)
                {
                    dtXML = dt;
                    break;
                }
            }

            if (dtXML.Rows.Count != 0)
            {
                System.Data.DataRow dr = dtXML.Rows[0];
                if (Printer.PrinterInList(dr["PrinterName"].ToString()))
                {
                    base.PrinterSettings.PrinterName = dr["PrinterName"].ToString();
                    if (Printer.FormInPrinter(dr["PrinterName"].ToString(), dr["PaperName"].ToString()))
                    {
                        if (Printer.FormSameSize(dr["PrinterName"].ToString(), dr["PaperName"].ToString(), System.Convert.ToDecimal(dr["PageWidth"]), System.Convert.ToDecimal(dr["PageHeight"])))
                        {

                            bool bolExist = false;

                            foreach (System.Drawing.Printing.PaperSize ps in base.PrinterSettings.PaperSizes)
                            {
                                if (ps.PaperName == dr["PaperName"].ToString())
                                {
                                    base.PrinterSettings.DefaultPageSettings.PaperSize = ps;
                                    base.DefaultPageSettings.PaperSize = ps;
                                    bolExist = true;
                                    break;
                                }
                            }

                            if (!bolExist)
                            {
                                WarningMessage += "\r\n Can not use the customized paper, because the printer selected does not the customized papersize!";

                                if (dtXML != null)
                                    dtXML.Dispose();
                                if (dsXML != null)
                                {
                                    dsXML.Clear();
                                    dsXML = null;
                                }

                                return null;
                            }

                            if (dr["Orientation"].ToString() == "Z")
                            {
                                base.DefaultPageSettings.Landscape = false;
                                base.PrinterSettings.DefaultPageSettings.Landscape = false;
                                emfdi.Landscape = false;
                            }
                            else
                            {
                                base.DefaultPageSettings.Landscape = true;
                                base.PrinterSettings.DefaultPageSettings.Landscape = true;
                                emfdi.Landscape = true;
                            }

                            this.bolOrientation = emfdi.Landscape;
                            
                            emfdi.PageWidth = System.Convert.ToDecimal(dr["PageWidth"].ToString());
                            emfdi.PageHeight = System.Convert.ToDecimal(dr["PageHeight"].ToString());
                            emfdi.MarginTop = System.Convert.ToDecimal(dr["MarginTop"].ToString());
                            emfdi.MarginBottom = System.Convert.ToDecimal(dr["MarginBottom"].ToString());
                            emfdi.MarginLeft = System.Convert.ToDecimal(dr["MarginLeft"].ToString());
                            emfdi.MarginRight = System.Convert.ToDecimal(dr["MarginRight"].ToString());

                            base.PrinterSettings.PrinterName = dr["PrinterName"].ToString();

                            if (dtXML != null)
                                dtXML.Dispose();
                            if (dsXML != null)
                            {
                                dsXML.Clear();
                                dsXML = null;
                            }
                            WarningMessage = string.Empty;
                            return emfdi;
                        }
                        else
                            WarningMessage += "\r\n Papersize defined in the config file is not the same with the one in the system!";
                    }
                    else
                        WarningMessage += "\r\n Printer defined in the config file does not support the customized papersize!";
                }
                else
                    WarningMessage += "\r\n Printer defined in the config file is not in the available printer list!";
            }
            else
                WarningMessage += "\r\n Can not get any information about the report in the config file!";

            if (dtXML != null)
                dtXML.Dispose();
            if (dsXML != null)
            {
                dsXML.Clear();
                dsXML = null;
            }
            return null;
        }

        private bool bolOrientation = false;
        
        private Stream CreateStream(string Name, string FileNameExtension, Encoding Encoding, string MimeType, bool WillSeek)
        {
            System.IO.Stream streamRet = new System.IO.MemoryStream();
            this.m_EMFStreams.Add(streamRet);
            return streamRet;
        }

        private void Export(LocalReport Report)
        {
            string strMessage = string.Empty;

            EMFDeviceInfo emfdi = this.ReadSetting(out strMessage);

            string strDeviceInfo = emfdi.DeviceInfoString;

            emfdi = null;

            Warning[] Warnings;

            this.m_EMFStreams = new System.Collections.Generic.List<System.IO.Stream>();

            Report.Render("Image", strDeviceInfo, this.CreateStream, out Warnings);

            foreach (System.IO.Stream s in this.m_EMFStreams)
                s.Position = 0;


        }

        protected override void OnBeginPrint(PrintEventArgs ev)
        {
            base.OnBeginPrint(ev);
        }
        
        protected override void OnPrintPage(PrintPageEventArgs ev)
        {
            base.OnPrintPage(ev);

            if (this.m_EMFStreams == null || this.m_EMFStreams.Count == 0)
            {
                this.Export(this.m_LocalReport);
                this.m_CurrentPageIndex = 0;
                if (this.m_EMFStreams.Count == 0 || this.m_EMFStreams == null)
                    return;
            }
            
            this.m_PageImage = new Metafile(this.m_EMFStreams[this.m_CurrentPageIndex]);

            //ev.Graphics.DrawImageUnscaledAndClipped(this.m_PageImage, ev.PageBounds);
            ev.Graphics.DrawImage(this.m_PageImage, ev.PageBounds);
            this.m_CurrentPageIndex++;
            ev.HasMorePages = (this.m_CurrentPageIndex < this.m_EMFStreams.Count);
            if (this.m_CurrentPageIndex == this.m_EMFStreams.Count)
            {
                this.m_CurrentPageIndex = 0;
                this.m_EMFStreams.Clear();
                this.m_EMFStreams = null;
                this.m_PageImage.Dispose();
                base.Dispose();
            }

        }

    }
}