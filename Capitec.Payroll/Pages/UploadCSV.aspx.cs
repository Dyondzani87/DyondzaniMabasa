using Capitec.Payroll.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Capitec.Payroll.DataLayer;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;

namespace Capitec.Payroll.Pages
{
    public partial class UploadCSV : System.Web.UI.Page
    {
        public string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SqlDataLayer _dataLayer = new SqlDataLayer(connectionString);

            string fileExtension = Path.GetExtension(FileUpload.FileName); // Get File Extension

            TimeKeeping timeKeepings = new TimeKeeping();

            if (FileUpload.HasFile) // Check if the file is Uploaded
            {
                if (fileExtension == ".csv") //Check the file Extension
                {
                    string filePath = Server.MapPath(FileUpload.FileName);
                    var reader = new StreamReader(File.OpenRead(filePath));

                    string reportString = File.ReadLines(filePath).Last(); //Footer
                    int ReportId = Convert.ToInt32(reportString.Split(',')[1]);

                    bool ReportExists = _dataLayer.CheckIfReportIdExists(ReportId);

                    if (!ReportExists)
                    {
                        while (!reader.EndOfStream)
                        {
                            var fileData = reader.ReadLine();
                            if (fileData.IndexOf("date") != 0 && fileData.IndexOf("report") != 0) //Excluding Header and Footer when processing CSV file
                            {
                                var fileRow = fileData.Split(',');

                                CultureInfo ci = CultureInfo.CreateSpecificCulture("en-GB");
                                var Date = Convert.ToDateTime(fileRow[0], ci.DateTimeFormat).ToString("d");

                                timeKeepings.EmployeeId = Convert.ToInt32(fileRow[2]);
                                timeKeepings.HoursWorked = Convert.ToDouble(fileRow[1].ToString());
                                timeKeepings.JobGroupId = _dataLayer.GetJobGroupIdByCode(fileRow[3].ToString());
                                timeKeepings.Date = Convert.ToDateTime(Date);
                                timeKeepings.ReportId = ReportId;

                                //Insert Into Database Table
                                _dataLayer.CreateTimeKeeping(timeKeepings);
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage.Text = "Report has already been uploaded.";
                    }
                    
                }
                else
                {
                    ErrorMessage.Text = "Please Upload .CSV file";
                }
            }
            else
                ErrorMessage.Text = "No File Uploaded.";
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string ReportSql = "SELECT		EmployeeId [Employee ID], SUM(HoursWorked * Rate) [Amount Paid]";
                ReportSql += " from TimeKeeping A inner join JobGroup B";
                ReportSql += " on			a.JobGroup = b.JobGroupId";
                ReportSql += " group by	EmployeeId order by	[Employee ID]";

                SqlCommand command = new SqlCommand(ReportSql, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();

                dataAdapter.Fill(dataSet);
                GridView1.DataSource = dataSet;
                GridView1.DataBind();
            }
        }
    }
}