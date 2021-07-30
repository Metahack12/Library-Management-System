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
    public partial class ViewStudentInformation : Form
    {
        public ViewStudentInformation()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewStudentInformation_Load(this, null);
        }

        private void txtSearchEnrollment_TextChanged(object sender, EventArgs e)
        {
            if(txtSearchEnrollment.Text!="")
            {
                label1.Visible = false;
                Image image = Image.FromFile("D:/Sem5/Liberay Management System/search1.gif");
                pictureBox1.Image=image;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from NewStudent where enroll LIKE '" + txtSearchEnrollment.Text+ "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            else
            {
                label1.Visible = true;
                Image image = Image.FromFile("D:/Sem5/Liberay Management System/search.gif");
                pictureBox1.Image = image;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from NewStudent where enroll LIKE '" + txtSearchEnrollment.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void ViewStudentInformation_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * from NewStudent";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        int stuid;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                stuid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                panel2.Visible = true;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from NewStudent where stuid=" + stuid + "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                txtSName.Text = ds.Tables[0].Rows[0][1].ToString();
                txtEnrollmentNo.Text = ds.Tables[0].Rows[0][2].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0][3].ToString();
                txtSemester.Text = ds.Tables[0].Rows[0][4].ToString();
                txtContact.Text = ds.Tables[0].Rows[0][5].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0][6].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data Will Be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                String sname = txtSName.Text;
                String senroll = txtEnrollmentNo.Text;
                String dept = txtDepartment.Text;
                String sem = txtSemester.Text;
                Int64 contact = Int64.Parse(txtContact.Text);
                String semail = txtEmail.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "UPDATE NewStudent set sname= '" + sname + "',enroll='" + senroll + "',dep='" + dept + "',sem='" + sem + "',contact='" + contact + "',email='" + semail + "' where stuid=" + rowid + "";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);
                ViewStudentInformation_Load(this, null);
                
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data Will Be Deleted. Confirm?", "Confirmation Dialog", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "DELETE from NewStudent where stuid=" + rowid + "";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ViewStudentInformation_Load(this, null);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unsaved Data will  be lost.", "Are you Sure ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK); 
            { 
                panel2.Visible = false;
            }        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
