using System;
using System.Web.UI;

public partial class About : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*XSRF  
         * Cross Site Request Forgery:
         * 
         */
        AntiForgeryTokenHelper.Check(this, HiddenField1);
    }
}