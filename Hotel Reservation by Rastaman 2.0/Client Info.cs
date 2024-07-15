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
    public partial class Client_Info : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource = localhost; port=3306; database=rastatel_db; username=root; password=;");
   
        public void populate()
        {
            connection.Open();
            string Mysquery = "select * from client_tbl";
            MySqlDataAdapter da = new MySqlDataAdapter(Mysquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ClientDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }
       

        public Client_Info()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
        }

        private void Client_Info_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            populate();        
        }

        private void btnAdd_Client_Click(object sender, EventArgs e)
        {
            try
            {

                if (cbIdType.Text == String.Empty)
                {
                    MessageBox.Show("Please select ID Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClientName.Focus();
                    return;
                }


                if (txtClientName.Text == String.Empty)
                {
                    MessageBox.Show("Please input Client Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClientName.Focus();
                    return;
                }

                if (cbGender.Text == String.Empty)
                {
                    MessageBox.Show("Please select Gender", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbGender.Focus();
                    return;
                }

                if (txtContactNumber.Text == String.Empty)
                {
                    MessageBox.Show("Please input the Contact Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtContactNumber.Focus();
                    return;
                }

                if (txtAddress.Text == String.Empty)
                {
                    MessageBox.Show("Please input the Address", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddress.Focus();
                    return;
                }

                if (cbCountry.Text == String.Empty)
                {
                    MessageBox.Show("Please select a Country", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbCountry.Focus();
                    return;
                }

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO client_tbl (client_id ,client_name , gender, id_type, contact_number, address, country) values ('" + txtClientID.Text + "', '" + txtClientName.Text + "', '" + cbGender.Text + "', '" + cbIdType.Text + "', '" + txtContactNumber.Text + "' , '" + txtAddress.Text + "','" + cbCountry.Text + "')", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Client Successfully Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e){
            connection.Open();
            string Myquery = "select * from client_tbl where client_Name = '"+txtSearchBox.Text+"' ";
            MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ClientDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClientDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ClientDataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);

            txtClientID.Text = ClientDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtClientName.Text = ClientDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            cbGender.Text = ClientDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            cbIdType.Text = ClientDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtContactNumber.Text = ClientDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtAddress.Text = ClientDataGridView.SelectedRows[0].Cells[5].Value.ToString();
            cbCountry.Text = ClientDataGridView.SelectedRows[0].Cells[6].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            connection.Open();
            string myquery = "UPDATE client_tbl SET client_name = '" + txtClientName.Text + "', gender = '" + cbGender.Text + "', id_type = '" + cbIdType.Text + "', contact_number = '" + txtContactNumber.Text + "' , address = '" + txtAddress.Text + "', country = '" + cbCountry.Text + "' WHERE client_id  = '" + txtClientID.Text + "'";
            MySqlCommand cmd = new MySqlCommand(myquery, connection);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Client Successfully Edited", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            connection.Close();
            populate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove this Client?", "Remove Client", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                connection.Open();
                string query = "DELETE FROM client_tbl WHERE client_id  = '" + txtClientID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                populate();
                
                txtClientID.Text = "";
                txtClientName.Text = "";
                cbGender.SelectedIndex = -1;
                txtContactNumber.Text = "";
                txtAddress.Text = "";
                cbCountry.SelectedIndex = -1;
                cbIdType.SelectedIndex = -1;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = string.Empty;
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
            txtClientID.Text = "";
            txtClientName.Text = "";
            cbGender.SelectedIndex = -1;
            txtContactNumber.Text = "";
            txtAddress.Text = "";
            cbCountry.SelectedIndex = -1;
            cbIdType.SelectedIndex = -1;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
