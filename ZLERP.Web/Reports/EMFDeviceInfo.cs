using System;
using System.Collections.Generic;
using System.Text;

namespace ZLERP.Web.Reports
{
    public class EMFDeviceInfo
    {
        private bool m_Landscape = false;

        public bool Landscape
        {
            get
            {
                return this.m_Landscape;
            }
            set
            {
                this.m_Landscape = value;
            }
        }

        /*
         * The pixel depth of the color range supported by the image output. 
         * Valid values are 1, 4, 8, 24, and 32. 
         * The default value is 24. 
         * ColorDepth is only supported for TIFF rendering and is otherwise ignored by the report server for other image output formats. 
         * Note: 
         * For this release of SQL Server, the value of this setting is ignored, and the TIFF image is always rendered as 24-bit.
        */
        private int m_ColorDepth = 24;

        public int ColorDepth
        {
            get
            {
                return this.m_ColorDepth;
            }
        }


        /*
         * The number of columns to set for the report. This value overrides the report's original settings.
         * 
         * not used
         * 
        */
        private int m_Columns = 0;

        public int Columns
        {
            get
            {
                return this.m_Columns;
            }
            set
            {
                this.m_Columns = value;
            }
        }


        /*
         * The column spacing to set for the report. This value overrides the report's original settings.
         * 
         * not used
         * 
        */
        private int m_ColumnSpacing = 0;

        public int ColumnSpacing
        {
            get
            {
                return this.m_ColumnSpacing;
            }
            set
            {
                this.m_ColumnSpacing = value;
            }
        }

        /*
         * The resolution of the output device in x-direction. The default value is 96.
         * 
        */
        private int m_DpiX = 96;

        public int DpiX
        {
            get
            {
                return this.m_DpiX;
            }
            set
            {
                this.m_DpiX = value;
            }
        }


        /*
         * The resolution of the output device in y-direction. The default value is 96.
         * 
        */
        private int m_DpiY = 96;

        public int DpiY
        {
            get
            {
                return this.m_DpiY;
            }
            set
            {
                this.m_DpiY = value;
            }
        }


        /*
         * The last page of the report to render. The default value is the value for StartPage.
         * 
         */
        private int m_EndPage = 0;

        public int EndPage
        {
            get
            {
                return this.m_EndPage;
            }
            set
            {
                this.m_EndPage = value;
            }
        }


        /*
         * The first page of the report to render. A value of 0 indicates that all pages are rendered. The default value is 1.
         * 
         */
        private int m_StartPage = 1;

        public int StartPage
        {
            get
            {
                return this.m_StartPage;
            }
            set
            {
                this.m_StartPage = value;
            }
        }

        /*
         * The bottom margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.
         * 
         */
        private decimal m_MarginBottom = 0;

        public decimal MarginBottom
        {
            get
            {
                return this.m_MarginBottom;
            }
            set
            {
                this.m_MarginBottom = value;
            }
        }


        /*
         * The top margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.
         * 
         */
        private decimal m_MarginTop = 0;

        public decimal MarginTop
        {
            get
            {
                return this.m_MarginTop;
            }
            set
            {
                this.m_MarginTop = value;
            }
        }


        /*
         * The left margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.
         * 
         */
        private decimal m_MarginLeft = 0;

        public decimal MarginLeft
        {
            get
            {
                return this.m_MarginLeft;
            }
            set
            {
                this.m_MarginLeft = value;
            }
        }


        /*
         * The right margin value, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 1in). This value overrides the report's original settings.
         *  
         */
        private decimal m_MarginRight = 0;

        public decimal MarginRight
        {
            get
            {
                return this.m_MarginRight;
            }
            set
            {
                this.m_MarginRight = value;
            }
        }

        /*
         * One of the Graphics Device Interface (GDI) supported output formats: BMP, EMF, GIF, JPEG, PNG, or TIFF.
         * 
         */
        private string m_OutputFormat = "EMF";

        public string OutputFormat
        {
            get
            {
                return this.m_OutputFormat;
            }
            set
            {
                this.m_OutputFormat = value;
            }
        }

        /*
         * The page height, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 11in). This value overrides the report's original settings.
         * 
         */
        private decimal m_PageHeight = 0;

        public decimal PageHeight
        {
            get
            {
                return this.m_PageHeight;
            }
            set
            {
                this.m_PageHeight = value;
            }
        }


        /*
         * The page width, in inches, to set for the report. You must include an integer or decimal value followed by "in" (for example, 8.5in). This value overrides the report's original settings.
         * 
         * 
        */
        private decimal m_PageWidth = 0;

        public decimal PageWidth
        {
            get
            {
                return this.m_PageWidth;
            }
            set
            {
                this.m_PageWidth = value;
            }
        }

        public string DeviceInfoString
        {
            get
            {
                string strRet = string.Empty;

                strRet += "<DeviceInfo>" +
                    "  <OutputFormat>" + this.m_OutputFormat + "</OutputFormat>";

                if (this.m_Landscape)
                    strRet +=
                        "  <PageWidth>" + this.m_PageHeight.ToString() + "cm</PageWidth>" +
                        "  <PageHeight>" + this.m_PageWidth.ToString() + "cm</PageHeight>";
                else
                    strRet +=
                        "  <PageWidth>" + this.m_PageWidth.ToString() + "cm</PageWidth>" +
                        "  <PageHeight>" + this.m_PageHeight.ToString() + "cm</PageHeight>";

                strRet +=
                        "  <MarginTop>" + this.m_MarginTop.ToString() + "cm</MarginTop>" +
                        "  <MarginLeft>" + this.m_MarginLeft.ToString() + "cm</MarginLeft>" +
                        "  <MarginRight>" + this.m_MarginRight.ToString() + "cm</MarginRight>" +
                        "  <MarginBottom>" + this.m_MarginBottom.ToString() + "cm</MarginBottom>";

                strRet += "</DeviceInfo>";

                return strRet;

            }
        }
   
    }
}
