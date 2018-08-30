<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.Page" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="ZLERP.Web.Helpers" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        string strXML = Request.Form["xml"];
        UTF8Encoding encoding = new UTF8Encoding();
        if (!string.IsNullOrEmpty(strXML))
        {
            strXML = encoding.GetString(Convert.FromBase64String(strXML));
        }
        System.IO.StringReader textReader = new System.IO.StringReader(strXML);
        dataSet.ReadXml(textReader);
        ExcelExportHelper.ExportExcel(dataSet);
    }
</script>