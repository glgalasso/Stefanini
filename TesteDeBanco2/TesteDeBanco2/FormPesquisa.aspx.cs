using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TesteDeBanco2
{
    public partial class FormPesquisa : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cityfill();
                Genderfill();
                Regionfill();
                Classificationfill();
                Sellerfill();

                var IsAdmin = Session["Admin"].ToString();

                if (IsAdmin == "IsAdmin")
                {
                    pnlSeller.Visible = true;
                }
            }
        }

        protected void Cityfill()
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT 0 Id, 'Selecione' Name UNION ALL SELECT Id, Name FROM City";
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            City.DataSource = dataTable;
            City.DataValueField = "Id";
            City.DataTextField = "Name";
            City.DataBind();
        }

        protected void Genderfill()
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT 0 Id, 'Selecione' Name UNION ALL SELECT Id, Name FROM Gender";
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            Gender.DataSource = dataTable;
            Gender.DataValueField = "Id";
            Gender.DataTextField = "Name";
            Gender.DataBind();
        }

        protected void Regionfill()
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT 0 Id, 'Selecione' Name UNION ALL SELECT Id, Name FROM Region";
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            Region.DataSource = dataTable;
            Region.DataValueField = "Id";
            Region.DataTextField = "Name";
            Region.DataBind();
        }

        protected void Classificationfill()
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT 0 Id, 'Selecione' Name UNION ALL SELECT Id, Name FROM Classification";
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            Classification.DataSource = dataTable;
            Classification.DataValueField = "Id";
            Classification.DataTextField = "Name";
            Classification.DataBind();
        }

        protected void Sellerfill()
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = @"SELECT 0 Id, 'Selecione' Name UNION ALL SELECT Id, Name FROM UserRole";
            var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            var dataTable = new DataTable();

            dataAdapter.Fill(dataTable);

            Seller.DataSource = dataTable;
            Seller.DataValueField = "Id";
            Seller.DataTextField = "Name";
            Seller.DataBind();

            //escrever codigo do seller para mostrar o componente somente em caso de administrador
        }


        protected void Clear_Click(object sender, EventArgs e)
        {
            Name.Text = "";
            Gender.SelectedValue = "0";
            City.SelectedValue = "0";
            Region.SelectedValue = "0";
            Lastpurchasefim.Text = "";
            Lastpurchaseini.Text = "";
            Classification.SelectedValue = "0";
            Seller.SelectedValue = "0";
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            var connection = @"Data Source=.\SQLEXPRESS; AttachDbFilename=" + System.Web.Hosting.HostingEnvironment.MapPath("~/bin/Database.mdf") + "; Integrated Security=True; Connect Timeout=30; User Instance=True";
            var command = "";
            var IsAdmin = Session["Admin"].ToString();
            var UserId = Session["UserId"].ToString();

            if (IsAdmin == "IsAdmin")
            {
                //SELECT DO ADMINISTRADOR
                command = @"SELECT DISTINCT Customer.Name Name, Customer.Phone, Gender.Name Gender, City.Name City, Region.Name Region, Customer.LastPurchase, UserRole.Name Role 
                                FROM City, Classification, Customer, Gender, Region, UserRole, UserSys
                                WHERE   Customer.UserId = UserRole.Id AND
                                        Customer.CityId = City.Id AND
                                        Customer.RegionId = Region.Id AND
                                        Customer.GenderId = Gender.Id AND
                                        Customer.ClassificationId = Classification.Id AND

                                        ((Customer.LastPurchase >= '{Dateini}' AND

                                        Customer.LastPurchase <= '{Datefim}') OR '{Dateini}' = '') AND

                                        (Gender.Id = '{Gender}' OR '{Gender}' = '0') AND

                                        (City.Id = '{City}' OR '{City}' = '0') AND

                                        (Region.Id = '{Region}' OR '{Region}' = '0') AND
                                        
                                        (UserRole.Id = '{Seller}' OR '{Seller}' = '0') AND

                                        (Classification.Id = '{Classification}' OR '{Classification}' = '0')"

                                        .Replace("{Gender}", Gender.SelectedValue.ToString())
                                        .Replace("{City}", City.SelectedValue.ToString())
                                        .Replace("{Region}", Region.SelectedValue.ToString())
                                        .Replace("{Seller}", Seller.SelectedValue.ToString())
                                        .Replace("{Classification}", Classification.SelectedValue.ToString())
                                        .Replace("{Dateini}", Lastpurchaseini.Text.ToString())
                                        .Replace("{Datefim}", Lastpurchasefim.Text.ToString());

                var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                Result.DataSource = dataTable;
                Result.DataBind();

            }
            else
            {
                //SELECT DO USUARIO
                command = @"SELECT DISTINCT Customer.Name Name, Customer.Phone, Gender.Name Gender, City.Name City, Region.Name Region, Customer.LastPurchase 
                                FROM City, Classification, Customer, Gender, Region, UserRole, UserSys
                                WHERE   Customer.UserId = '{UserId}' AND
                                        Customer.CityId = City.Id AND
                                        Customer.RegionId = Region.Id AND
                                        Customer.GenderId = Gender.Id AND
                                        Customer.ClassificationId = Classification.Id AND
                                        (Gender.Id = '{Gender}' OR '{Gender}' = '0') AND
                                        (City.Id = '{City}' OR '{City}' = '0') AND
                                        (Region.Id = '{Region}' OR '{Region}' = '0') AND
                                        (Classification.Id = '{Classification}' OR '{Classification}' = '0')"
                                       .Replace("{Gender}", Gender.SelectedValue.ToString())
                                       .Replace("{City}", City.SelectedValue.ToString())
                                       .Replace("{Region}", Region.SelectedValue.ToString())
                                       .Replace("{Classification}", Classification.SelectedValue.ToString())
                                       .Replace("{Dateini}", Lastpurchaseini.Text.ToString())
                                       .Replace("{Datefim}", Lastpurchasefim.Text.ToString())
                                       .Replace("{UserId}", UserId);

                var dataAdapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                Result.DataSource = dataTable;
                Result.DataBind();

            }
        }
    }
}