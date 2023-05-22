using System;
using System.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public static class AntiForgeryTokenHelper
{
    /*
 * 1. Request gönderen istemciye bir komplex değer ver.
 * 2. Bu değeri aynı zamanda sunucu da sakla.
 * 3. Istemci bir talep gönderdiğinde; gelen komplex değer ile sunucuda sakladığın değeri karşılaştır.
 * 4. Eşit değilse; bir başkası request göndermiştir!!!!!
 */
    public static void Check(Page page, HiddenField hiddenField)
    {
        if (!page.IsPostBack)
        {
            //ilk request:
            Guid token = Guid.NewGuid();
            hiddenField.Value = token.ToString();
            page.Session["token"] = token;

        }
        else
        {
            Guid client = new Guid(hiddenField.Value);
            Guid server = (Guid)page.Session["token"];

            if (client != server)
            {
                throw new SecurityException("XSRF-> CSRF Atağı saptandı");
            }
        }
    }
}