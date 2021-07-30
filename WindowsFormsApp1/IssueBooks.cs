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

namespace WindowsFormsApp1
{
    public partial class IssueBooks : Form
    {
        public IssueBooks()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1(object sender, EventArgs e)
        {

        }

        private void IssueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd = new SqlCommand("Select bName from NewBook", con);      
            SqlDataReader Sdr = cmd.ExecuteReader();
            
            while(Sdr.Read())
            {
                for(int i=0;i<Sdr.FieldCount;i++)
                {
                    comboBoxBooks.Items.Add(Sdr.GetString(i));
                }
                
            }
            Sdr.Close();
            con.Close();
        }

        private void txtEnterEnrollmentNo_TextChanged(object sender, EventArgs e)
        {
            if (txtEnterEnrollmentNo.Text != "")
            {
                txtSName.Clear();
                txtDepartment.Clear();
                txtSemester.Clear();
                txtContact.Clear();
                txtEmail.Clear();
            }
        }
        int count;
        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            if (txtEnterEnrollmentNo.Text != "")
            {
                String eid = txtEnterEnrollmentNo.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from NewStudent where enroll = '" +eid + "'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
       
                //-----------------------------------------------------------------------------------------------
                //Code to count how many book have been issued on this enrollment no.
                cmd.CommandText = "Select count(std_enroll) from IRBook where std_enroll = '" + eid + "' and book_return_date is null";
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                DataSet ds1 = new DataSet();
                da.Fill(ds1);

                count = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
                //-----------------------------------------------------------------------------------------------

                if (ds.Tables[0].Rows.Count!=0)
                {
                    txtSName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
                    
                }
                else
                {
                    txtSName.Clear();
                    txtDepartment.Clear();
                    txtSemester.Clear();
                    txtContact.Clear();
                    txtEmail.Clear(); 
                    MessageBox.Show("Invalid Enrollment No","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            if (txtSName.Text!="")
            {
                if(comboBoxBooks.SelectedIndex!=-1&&count<=2)
                {
                    // MessageBox.Show("Hello i am in");
                    String enroll = txtEnterEnrollmentNo.Text;
                    String sname = txtSName.Text;
                    string sdep = txtDepartment.Text;
                    string sem = txtSemester.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookname = comboBoxBooks.Text;
                    String bookIssueDate = dateTimePicker1.Text;

                    String eid = txtEnterEnrollmentNo.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "insert into IRBook(std_enroll,std_name,std_dep,std_sem,std_contact,std_email,book_name,book_issue_date)values('"+enroll+"','"+sname+"','"+sdep+"','"+sem+"',"+contact+",'"+email+"','"+bookname+"','"+bookIssueDate+"')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Book Issued.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                else 
                {
                    MessageBox.Show("Select Book. OR Maximum number of Book has been ISSUED", "No Book Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                MessageBox.Show("Enter Valid Enrollment No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnterEnrollmentNo.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }
    }
}
