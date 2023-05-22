using System;
using System.Web.UI;

public partial class Contact : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
         */
    }

    protected void ButtonComment_Click(object sender, EventArgs e)
    {
        LabelComment.Text = TextComment.Text;

    }
}