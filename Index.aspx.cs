using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CountryCityApp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Use pk, ksa, ir or aus COUNTRY CODE to view city table

        protected void FindButton_Click(object sender, EventArgs e)
        {

            var countryCode = CountryCodeTextBox.Text;

            if (string.IsNullOrWhiteSpace(countryCode))
            {
                ErrorLabel1.Text = "Invalid Country Code!";
                CountryNameTextBox.Text = "";

            }
            else
            {
                ErrorLabel1.Text = "";
                var countryName = GetCountryName(countryCode);
                if (string.IsNullOrWhiteSpace(countryName))
                {
                    ErrorLabel1.Text = "No Country Found With Code " + countryCode;
                    CountryNameTextBox.Text = "";

                }
                else
                {
                    ErrorLabel1.Text = "";
                    CountryNameTextBox.Text = countryName;

                    DataTable cities =  GetCities(countryCode);
                    CityGridView.DataSource = cities;
                    CityGridView.DataBind();
                }


            }

            CountryNameLabel.Text = "";
            CityCodeLabel.Text = "";
            CityNameLabel.Text = "";
            CityCodeTextBox.Text = "";
            CityNameTextBox.Text = "";
        }

        private DataTable GetCities(string countryCode)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


            //declare @CountryCode varchar(10) = 'PK';

            var query = "select CityCode as [City Code], CityName as[City Name] from City where CountryCode = @CountryCode";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@CountryCode", countryCode);

                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                    }
                }
            }
            return new DataTable();
        }

        private string GetCountryName(string countryCode)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;


            //declare @CountryCode varchar(10) = 'PK';

            var query = "select CountryName from Country where CountryCode = @CountryCode";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@CountryCode", countryCode);

                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var countryName = dt.Rows[0]["CountryName"].ToString();
                            return countryName;
                        }
                    }
                }
            }
            return null;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CityCodeLabel.Text = "";
            bool check = IsAllRequiredDataGiven();

            if (check)
            {
                (string countryCode, string countryName, string cityCode, string cityName) formData = GetFormData();

                bool exist = IsCountryExist(formData.countryCode);

                if (exist)
                {
                    SaveCity(formData);
                }
                else
                {
                    SaveCountry(formData);
                    SaveCity(formData);

                }

            }
        }

        private void SaveCountry((string countryCode, string countryName, string cityCode, string cityName) formData)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            var query = "insert into Country values (@CountryCode, @CountryName);";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlcom = new SqlCommand(query, con))
                {
                    con.Open();
                    sqlcom.Parameters.AddWithValue("@CountryCode", formData.countryCode);
                    sqlcom.Parameters.AddWithValue("@CountryName", formData.countryName);


                    sqlcom.ExecuteNonQuery();

                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('Country Registered Successfully')", true);
        }

        private void SaveCity((string countryCode, string countryName, string cityCode, string cityName) formData)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            var query = "insert into City values(@CityCode, @CityName, @CountryCode)";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlcom = new SqlCommand(query, con))
                {
                    con.Open();
                    sqlcom.Parameters.AddWithValue("@CityCode", formData.cityCode);
                    sqlcom.Parameters.AddWithValue("@CityName", formData.cityName);
                    sqlcom.Parameters.AddWithValue("@CountryCode", formData.countryCode);

                    sqlcom.ExecuteNonQuery();
                    
                }
            }
            CityCodeLabel.Text = "";
            CityNameLabel.Text = "";
            CityCodeTextBox.Text = "";
            CityNameTextBox.Text = "";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('City Registered Successfully')", true);

            DataTable cities = GetCities(formData.countryCode);
            CityGridView.DataSource = cities;
            CityGridView.DataBind();
        }

        private bool IsCountryExist(string countryCode)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            var query = "select Top 1 Cast(1 as bit) from Country where CountryCode = @CountryCode";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@CountryCode", countryCode);

                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var exist = Convert.ToBoolean(dt.Rows[0][0]);
                            return exist;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsAllRequiredDataGiven()
        {

            (string countryCode, string countryName, string cityCode, string cityName ) formData = GetFormData();

            bool check = true;
            if (string.IsNullOrWhiteSpace(formData.countryCode))
            {
                ErrorLabel1.Text = "* Required";
                check = false;
            }
            if (string.IsNullOrWhiteSpace(formData.countryName))
            {
                CountryNameLabel.Text = "* Required";
                check = false;
            }
            if (string.IsNullOrWhiteSpace(formData.cityCode))
            {
                CityCodeLabel.Text = "* Required";
                check = false;
            }
            if (string.IsNullOrWhiteSpace(formData.cityName))
            {
                CityNameLabel.Text = "* Required";
                check = false;
            }

            return check;
        }

        private (string, string, string, string) GetFormData()
        {
            //var countryCode = CountryCodeTextBox.Text;
            //var countryName = CountryNameTextBox.Text;
            //var cityCode = CityCodeTextBox.Text;
            //var cityName = CityNameTextBox.Text;

            var formData = (CountryCodeTextBox.Text, CountryNameTextBox.Text,
                CityCodeTextBox.Text, CityNameTextBox.Text);

            return formData;
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            (string countryCode, string countryName, string cityCode, string cityName) formData = GetFormData();

            bool exist = IsCountryExist(formData.countryCode);

            if (exist)
            {
                CountryNameLabel.Text = "";
                CityCodeLabel.Text = "";

                bool cityExist = IsCityExist(formData.cityCode);


                if (cityExist)
                {
                    CityNameLabel.Text = "";

                    DeleteCity(formData);
                    CityCodeTextBox.Text = "";
                    CityNameTextBox.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertMessage", "alert('City Deleted Successfully')", true);
                    DataTable cities = GetCities(formData.countryCode);
                    CityGridView.DataSource = cities;
                    CityGridView.DataBind();

                }
                else
                {
                    CityNameLabel.Text = "City doesn't exist of code " + formData.cityCode;
                }
            }
            else
            {
                CountryNameLabel.Text = "Country doesn't exist ";
            }
        }

        private void DeleteCity((string countryCode, string countryName, string cityCode, string cityName) formData)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            var query = "delete from City where CountryCode = @CountryCode and CityCode = @CityCode";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand sqlcom = new SqlCommand(query, con))
                {
                    con.Open();
                    sqlcom.Parameters.AddWithValue("@CountryCode", formData.countryCode);
                    sqlcom.Parameters.AddWithValue("@CityCode", formData.cityCode);
                    
                    sqlcom.ExecuteNonQuery();

                }
            }
        }

        private bool IsCityExist(string cityCode)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            var query = "select Top 1 Cast(1 as bit) from City where CityCode = @CityCode";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@CityCode", cityCode);

                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var exist = Convert.ToBoolean(dt.Rows[0][0]);
                            return exist;
                        }
                    }
                }
            }
            return false;
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            CountryCodeTextBox.Text = "";
            CountryNameTextBox.Text = "";
            CityCodeTextBox.Text = "";
            CityNameTextBox.Text = "";

            ErrorLabel1.Text = "";
            CountryNameLabel.Text = "";
            CityCodeLabel.Text = "";
            CityNameLabel.Text = "";

            CityGridView.DataSource = "";
            CityGridView.DataBind();
        }

        
    }
}