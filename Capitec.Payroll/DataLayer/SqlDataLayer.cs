using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capitec.Payroll.Models;


namespace Capitec.Payroll.DataLayer
{
    public class SqlDataLayer
    {
        private string ConnectionString;
        public SqlDataLayer(string ConString)
        {
            ConnectionString = ConString;
        }
        public string CreateTimeKeeping(TimeKeeping timeKeeping)
        {
            string Results = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string InsertSQL = "insert into TimeKeeping(EmployeeId, [Date], HoursWorked, JobGroup, ReportId) ";
                    InsertSQL += "values(" + timeKeeping.EmployeeId + ", '" + timeKeeping.Date + "', " + timeKeeping.HoursWorked + ", " + timeKeeping.JobGroupId + ", " + timeKeeping.ReportId + ")";

                    SqlCommand sqlCommand = new SqlCommand(InsertSQL, connection);

                    sqlCommand.ExecuteNonQuery();

                    Results = "SUCCESS";
                }
            }
            catch (Exception exception)
            {
                Results = exception.Message;
            }
            return Results;
        }
        public int GetJobGroupIdByCode(string Code)
        {
            int Id = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string SelectSQL = "SELECT JobGroupId from JobGroup WHERE code = '" + Code + "'";
                SqlCommand command = new SqlCommand(SelectSQL, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Id = Convert.ToInt32(reader["JobGroupId"].ToString());
                }
            }

            return Id;

        }
        public bool CheckIfReportIdExists(int reportId)
        {
            int Count = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string SelectSQL = "select count(*) ReportCount from TimeKeeping where reportId = " + reportId;
                SqlCommand command = new SqlCommand(SelectSQL, connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Count = Convert.ToInt32(reader["ReportCount"].ToString());
                }
            }

            if (Count > 0)
                return true;
            else
                return false;
        }
    }
}