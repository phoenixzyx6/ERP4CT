<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="ZLERP.Web.help.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <iframe id="pdf" style="width: 80%; height: 100%; position: absolute; float: right"></iframe>
    <div style='width: 20%; height: 100%; float: left'>
        <form id="form1" runat="server">
        <script type="text/javascript">
            function changeURL(a) {
                var url = "doc/ERP3.5用户手册/" + a + ".pdf";
                this.window.top.document.getElementById("pdf").setAttribute("src", url);
            }
        </script>
        <asp:TreeView ID="TreeView1" runat="server" ImageSet="Faq" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
            <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
            <Nodes>
                <asp:TreeNode Text="ERP帮助" Value="ERP帮助">
                    <asp:TreeNode Text="原材料管理" Value="原材料管理">
                        <asp:TreeNode Text="材料消耗估算" Value="材料消耗估算"></asp:TreeNode>
                        <asp:TreeNode Text="厂商信息管理" Value="厂商信息管理"></asp:TreeNode>
                    </asp:TreeNode>
                    <asp:TreeNode Text="销售管理" Value="销售管理"></asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
        </form>
    </div>
</body>
</html>
