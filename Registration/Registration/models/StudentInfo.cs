using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration.models
{
    class StudentInfo
    {
        public int Code { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public string Contact { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Resident { get; set; }

        public bool Add(StudentInfo student)
        {
            string connectionstring = "server=DWIP\\SQLEXPRESS; database=Entry;integrated security=true";

            SqlConnection con = new SqlConnection(connectionstring);

            //string query = @"INSERT INTO Students VALUES('" + student.Name + "','" + student.Contact + "','" +
            //               student.Email + "','" + student.Gender + "','" + student.Resident + "')";


            string query = @"INSERT INTO Students VALUES('" + student.Id + "','" + student.Name + "','" + student.Contact + "','" +
                           student.Email + "','" + student.Gender + "','" + student.Resident + "')";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();
            bool isAdded = command.ExecuteNonQuery() > 0;
            con.Close();
            return isAdded;

        }

        public bool Update(StudentInfo userupdate)
        {
            string connectionstring = "server=DWIP\\SQLEXPRESS; database=Entry;integrated security=true";

            SqlConnection connection = new SqlConnection(connectionstring);

            string query = @"UPDATE Students SET Name='" + userupdate.Name + "',Contact= '" + userupdate.Contact + "',Email = '" + userupdate.Email + "',Gender = '" + userupdate.Gender + "',Recident = '" + userupdate.Resident + "' WHERE StudentId='" + userupdate.Id + "' ";


            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            bool isUpdated = cmd.ExecuteNonQuery() > 0;
            connection.Close();
            return isUpdated;
        }
        

    }
}
