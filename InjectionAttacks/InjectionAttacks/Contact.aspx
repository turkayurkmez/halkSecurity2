<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" 
    Inherits="Contact" ValidateRequest="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your contact page.</h3>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>
    <h3>XSS attack!</h3>
    <asp:TextBox ID="TextComment" TextMode="MultiLine" runat="server" Height="177px" Width="370px"></asp:TextBox><br />
    <asp:Label ID="LabelComment" runat="server" Text="Label"></asp:Label><br />
    <asp:Button ID="ButtonComment" runat="server" Text="Yorum Yap" OnClick="ButtonComment_Click" />
</asp:Content>
