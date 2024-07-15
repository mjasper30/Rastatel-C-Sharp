using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hotel_Reservation_by_Rastaman_2._0
{
    public partial class Users : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource = localhost; port=3306; database=rastatel_db; username=root; password=;");
        public void populate()
        {
            connection.Open();
            string Mysquery = "select * from user_tbl";
            MySqlDataAdapter da = new MySqlDataAdapter(Mysquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StaffDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        public Users()
        { 
            InitializeComponent();
        }

        private void ClientDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtStaffID.Text = StaffDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtName.Text = StaffDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            cbGender.Text = StaffDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtContactNumber.Text = StaffDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtEmail.Text = StaffDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtUsername.Text = StaffDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            txtPassword.Text = StaffDataGridView.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtStaffID.Text == String.Empty)
            {
                MessageBox.Show("Please input your Staff ID");
                txtStaffID.Focus();
                return;
            }

            if (txtName.Text == String.Empty)
            {
                MessageBox.Show("Please input your Name");
                txtName.Focus();
                return;
            }

            if (cbGender.Text == String.Empty)
            {
                MessageBox.Show("Please input your Gender");
                cbGender.Focus();
                return;
            }

            if (txtContactNumber.Text == String.Empty)
            {
                MessageBox.Show("Please input your Contact Number");
                txtContactNumber.Focus();
                return;
            }

            if (txtEmail.Text == String.Empty)
            {
                MessageBox.Show("Please input your Email");
                txtEmail.Focus();
                return;
            }

            if (txtUsername.Text == String.Empty)
            {
                MessageBox.Show("Please input your Username");
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text == String.Empty)
            {
                MessageBox.Show("Please input your Password");
                txtPassword.Focus();
                return;
            }

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into user_tbl values(" + txtStaffID.Text + ",'" + txtName.Text + "','" + cbGender.Text + "','" + txtContactNumber.Text + "','" + txtEmail.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "')", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Staff Successfully Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove this Staff?", "Remove Staff", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                return;
            }
            else 
            {
                connection.Open();
                string query = "delete from user_tbl where staff_id = " + txtStaffID.Text + "";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            populate();

            txtStaffID.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            cbGender.SelectedIndex = -1;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            connection.Open();
            string myquery = "UPDATE user_tbl set name = '" + txtName.Text + "', gender = '" + cbGender.Text + "', contact_number = '" + txtContactNumber.Text + "', email = '" + txtEmail.Text + "', username = '" + txtUsername.Text + "', password = '" + txtPassword.Text + "' where staff_id = " + txtStaffID.Text + ";";
            MySqlCommand cmd = new MySqlCommand(myquery, connection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Staff Successfully Edited", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            populate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            string Myquery = "select * from user_tbl where name = '" + txtSearchBox.Text + "' ";
            MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StaffDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = string.Empty;
            populate();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            populate();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Main_Form main = new Main_Form();
            main.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtStaffID.Text = string.Empty;
            txtContactNumber.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Text = string.Empty;
            cbGender.SelectedIndex = -1;
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
