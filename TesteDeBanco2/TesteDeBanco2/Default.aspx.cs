using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TesteDeBanco2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public bool Logar(string login, string senha)
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT UserRole.Id, UserRole.IsAdmin FROM UserSys, UserRole 
                            WHERE CONVERT(VARCHAR(32), HashBytes('MD5', Password), 2) = '{Senha}' 
                            AND Email = '{Email}' 
                            AND UserSys.UserRoleId = UserRole.Id"
                .Replace("{Senha}", GerarHashMd5(senha))
                .Replace("{Email}", login);
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dataTable.Rows[0]["IsAdmin"]) == true)
                {
                    Session["Admin"] = "IsAdmin";
                    Session["UserId"] = dataTable.Rows[0]["Id"].ToString();
                }
                else
                {
                    Session["Admin"] = "NAdmin";
                    Session["UserId"] = dataTable.Rows[0]["Id"].ToString();
                }

                Response.Redirect("FormPesquisa.aspx");

            }
            else
            {
                Email.BorderColor = System.Drawing.Color.Red;
                LabelEmail.ForeColor = System.Drawing.Color.Red;
                Password.BorderColor = System.Drawing.Color.Red;
                LabelPassword.ForeColor = System.Drawing.Color.Red;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "erro", "alert('The email and/or password entered is invalid. Please try again.');", true);
            }
            return false;

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            // select* from Tabela WHERE CONVERT(VARCHAR(32), HashBytes('MD5', senha), 2) = '{Senha}'
            bool retorno = Logar(Email.Text.Trim(), Password.Text.Trim());

        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }
}