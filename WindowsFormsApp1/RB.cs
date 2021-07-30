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
    public partial class RB : Form
    {
        public RB()
        {
            InitializeComponent();
        }

        private void txtSearchEnrollment_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchEnrollment.Text != "")
            {
                panel2.Visible = false;
                dataGridView1.DataSource = null;
            }
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            if (txtSearchEnrollment.Text != "")
            {
                String eid = txtSearchEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select * from IRBook where std_enroll = '" + eid + "'and book_return_date is null";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("Invalid Enrollment No or No Book Issued", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBook_Issue_Date.Clear();
            txtSearchEnrollment.Clear();
            panel2.Visible = false;

        }

    private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Exit ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        String bname;
        String bdate;
        Int64 rowid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {

                rowid = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                bname = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                bdate = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            txtBName.Text = bname;
            txtBook_Issue_Date.Text = bdate;

        }

        private void RB_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            txtSearchEnrollment.Clear();
        }

        private void btnReturn_Book_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source =USER\\SQLEXPRESS;database =library;integrated security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "update IRBook set book_return_date= '" + dateTimePicker1.Text + "' where std_enroll='" + txtSearchEnrollment.Text + "'and id=" + rowid + "";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Book Returned Succesfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RB_Load(this, null);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtBook_Issue_Date.Clear();
            panel2.Visible = false;
        }
    }
}
