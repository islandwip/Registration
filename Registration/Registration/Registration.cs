using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Registration.models;

namespace Registration
{
    public partial class Registration : Form
    {
        public string Id="";
        public Registration()
        {
            InitializeComponent();
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            int flag = 1;

            //validation
            string email = textBox3.Text;
            
            if (textBox1.Text.Length < 3)
            {
                errorProvider1.SetError(textBox1, "Please enter a valid Name");
                flag = 0;

            }
            else
            {
                errorProvider1.Clear();
            }
            if (textBox2.Text.Length < 11 || textBox2.Text.Length > 11)
            {
                errorProvider1.SetError(textBox2,"Please Enter a Valid Phone Number");
                flag = 0;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (!IsValidEmail(email))
            {
                errorProvider1.SetError(textBox3,"Email No must have @ and it`s must be Valid");
                flag = 0;
            }
            else
            {
                errorProvider1.Clear();
            }

            if (flag == 0)
            {
                return;
            }


            


            //**************


            try
            {
                StudentInfo student = new StudentInfo();

                DateTime date = DateTime.Now;
                DateTime idate = Convert.ToDateTime(date);
                int datey = Convert.ToInt32(idate.Year) / 1000;
                int datem = Convert.ToInt32(idate.Month);

                //student.Id = datev.ToString()+"-000-"+"7"+student.Code;



                student.Name = textBox1.Text;
                student.Contact = textBox2.Text;
                student.Email = textBox3.Text;
                student.Code = Convert.ToInt32(codeTextBox.Text);

                student.Id = datey.ToString() +"-"+ student.Code +"-"+ datem;

                if (maleRadioButton.Checked == true)
                {
                    student.Gender = "Male";
                }
                else if (femaleRadioButton.Checked == true)
                {
                    student.Gender = "Female";
                }
                else if (othersRadioButton.Checked == true)
                {
                    student.Gender = "Others";
                }
                else
                {
                    genderLabel.Text = "*Please Select Gender First!";
                }

                //select recedient
                if (recidentCheckBox.Checked == true)
                {
                    student.Resident = "Yes";
                }
                else
                {
                    student.Resident = "No";
                }

                bool isStudentAdded = student.Add(student);


                
                if (isStudentAdded)
                {
                    MessageBox.Show("Done!");
                    //return;
                }
                else
                {
                    MessageBox.Show("Failed");
                }
                string connectionstring = "server=DWIP\\SQLEXPRESS; database=Entry;integrated security=true";

                SqlConnection con = new SqlConnection(connectionstring);
                string query = @"SELECT StudentId,Name,Contact,Email,Gender,Recident FROM Students";

                SqlCommand command = new SqlCommand(query, con);

                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                studentsDataGridView.DataSource = dt;

                con.Close();
                
            }
            
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            string connectionstring = "server=DWIP\\SQLEXPRESS; database=Entry;integrated security=true";

            SqlConnection con = new SqlConnection(connectionstring);
            string query = @"SELECT StudentId,Name,Contact,Email,Gender,Recident FROM Students";

            SqlCommand command = new SqlCommand(query, con);

            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            studentsDataGridView.DataSource = dt;

            con.Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            codeTextBox.Text = studentsDataGridView.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = studentsDataGridView.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = studentsDataGridView.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = studentsDataGridView.CurrentRow.Cells[3].Value.ToString();
            updateButton.Visible = true;
            Id = studentsDataGridView.CurrentRow.Cells[0].Value.ToString();

        }

        private void updateButton_Click(object sender, EventArgs e)
        {

            if (Id == "")
            {
                MessageBox.Show("Please Search code first!");
                return;
            }

            if (Id != codeTextBox.Text)
            {
                MessageBox.Show("Sorry, you can't update Id!");
                return;
            }
            try
            {
                StudentInfo userupdate = new StudentInfo();

                userupdate.Id = codeTextBox.Text;

                userupdate.Name = textBox1.Text;
                userupdate.Contact = textBox2.Text;
                userupdate.Email = textBox3.Text;
                

                
                if (maleRadioButton.Checked == true)
                {
                    userupdate.Gender = "Male";
                }
                else if (femaleRadioButton.Checked == true)
                {
                    userupdate.Gender = "Female";
                }
                else if (othersRadioButton.Checked == true)
                {
                    userupdate.Gender = "Others";
                }
                else
                {
                    genderLabel.Text = "*Please Select Gender First!";
                }

                //select recedient
                if (recidentCheckBox.Checked == true)
                {
                    userupdate.Resident = "Yes";
                }
                else
                {
                    userupdate.Resident = "No";
                }

                bool isStudentupdate = userupdate.Update(userupdate);



                if (isStudentupdate)
                {
                    MessageBox.Show("Done!");
                    return;
                }
                else
                {
                    MessageBox.Show("Failed");
                }
                

            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }

        }


        
    }
}
