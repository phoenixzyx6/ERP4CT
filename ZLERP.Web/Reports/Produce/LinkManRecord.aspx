<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkManRecord.aspx.cs"
    Inherits="ZLERP.Web.Reports.Produce.LinkManRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width,user-scalable=yes" />
    <title>前场工长记录</title>
    <script src="<%=ResolveUrl("~/Scripts/jquery-1.4.4.min.js")%>" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function cleancontent() {
            $('#Content').val('');
        }
        function contentisnotnull() {
            if ($.trim($('#Content').val()) == '') {
                alert('内容不能为空');
                return false;
            } else {
                return true;
            }
        }
        function savesuccess() {
            alert('保存成功');
            $('#Content').val('');
            window.close();
        }
        function logonvalid() {
            alert('您没有权限访问');
            $('#Content').val('');
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <table style="margin: auto">
            <tr>
                <td>
                    <label for="Content">
                        前场工长记录</label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="Content" runat="server" Rows="5" Columns="20" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return contentisnotnull();" />
                    <input type="button" id="btnCancel" name="btnCancel" value="取消" onclick="cleancontent();" />
                </td>
            </tr>
        </table>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table border="1" cellspacing="0" cellpadding="0" style="margin: 10px auto;">
                    <tr>
                        <th>
                            内容
                        </th>
                        <th>
                            时间
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("Content") %>
                    </td>
                    <td>
                        <%#Eval("BuildTime","{0:yyyy-MM-dd HH:mm:ss}")%>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
