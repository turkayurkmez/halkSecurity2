using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //SELECT * FROM Employees WHERE FirstName ='' AND LastName =''

        SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=Northwind;Integrated Security=True");

        SqlCommand command = new SqlCommand($"SELECT * FROM Employees WHERE FirstName =@name AND LastName =@password", sqlConnection);
        command.Parameters.AddWithValue("@name", TextBoxUserName.Text);
        command.Parameters.AddWithValue("@password", TextBoxPassword.Text);


        sqlConnection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            LabelResult.Text = "Giriş Başarılı";
        }
        else
        {
            LabelResult.Text = "Giriş Başarısız....";
        }
        sqlConnection.Close();

    }
}