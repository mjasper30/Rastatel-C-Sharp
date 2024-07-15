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
    public partial class RoomInfo : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource = localhost; port=3306; database=rastatel_db; username=root; password=;");
        public void populate()
        {
            connection.Open();
            string Mysquery = "select * from rastatel_db.room_tbl";
            MySqlDataAdapter da = new MySqlDataAdapter(Mysquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }
        public RoomInfo()
        {
            InitializeComponent();
        }

        private void btnAdd_RoomInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRoomNumber.Text == String.Empty)
                {
                    MessageBox.Show("Please Input Room Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRoomNumber.Focus();
                    return;
                }

                if (txtRoomPhone.Text == String.Empty)
                {
                    MessageBox.Show("Please Input Room Phone", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRoomPhone.Focus();
                    return;
                }

                if (cbRoomStatus.Text == String.Empty)
                {
                    MessageBox.Show("Please Input Room Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRoomPhone.Focus();
                    return;
                }

                if (cbRoomType.Text == String.Empty)
                {
                    MessageBox.Show("Please Input your Room type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbRoomType.Focus();
                    return;
                }

                if (cbBedType.Text == String.Empty)
                {
                    MessageBox.Show("Please Input your Bed Type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbBedType.Focus();
                    return;
                }

                if (txtPrice.Text == String.Empty)
                {
                    MessageBox.Show("Please Input the Price", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrice.Focus();
                    return;
                }

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("insert into room_tbl values(" + txtRoomNumber.Text + ",'" + txtRoomPhone.Text + "','" + cbRoomStatus.Text + "', '" + cbRoomType.Text + "', '" + cbBedType.Text + "', '" + txtPrice.Text + "')", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Successfully Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            string Myquery = "select * from room_tbl where room_number = '" + txtSearchBox.Text + "' or room_phone = '" + txtSearchBox.Text + "' or room_status = '" + txtSearchBox.Text + "' ";
            MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        private void RoomDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow Myrow in RoomDataGridView.Rows)
            {
                if (Convert.ToString(Myrow.Cells[2].Value) == "Busy")
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Salmon;
                    Myrow.DefaultCellStyle.ForeColor = Color.Black;
                }
                if (Convert.ToString(Myrow.Cells[2].Value) == "Free")
                {
                    Myrow.DefaultCellStyle.BackColor = Color.Lime;
                    Myrow.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            txtRoomNumber.Text = RoomDataGridView.SelectedRows[0].Cells[0].Value.ToString();
            txtRoomPhone.Text = RoomDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            cbRoomStatus.Text = RoomDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            cbRoomType.Text = RoomDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            cbBedType.Text = RoomDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtPrice.Text = RoomDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = string.Empty;
            cbFilter.SelectedIndex = -1;
            populate();
        }

        private void btnEdit_RoomInfo_Click(object sender, EventArgs e)
        {
            if (txtRoomPhone.Text == "" || cbRoomStatus.Text == "" || cbRoomType.Text == "" || cbBedType.Text == "" || txtPrice.Text == "" || txtRoomNumber.Text == "")
            {
                MessageBox.Show("Please fill all the information to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                connection.Open();
                string myquery = "UPDATE room_tbl SET room_phone = '" + txtRoomPhone.Text + "', room_status = '" + cbRoomStatus.Text + "',  room_type = '" + cbRoomType.Text + "',  bed_type = '" + cbBedType.Text + "',  price = '" + txtPrice.Text + "' WHERE room_number = " + txtRoomNumber.Text + ";";
                MySqlCommand cmd = new MySqlCommand(myquery, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Successfully Edited", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                populate();
            }   
        }

        private void btnDelete_RoomInfo_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove this Room?", "Remove Romove", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                connection.Open();
                string query = "delete from room_tbl where room_number = " + txtRoomNumber.Text + "";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Successfully Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                populate();

                txtRoomNumber.Text = string.Empty;
                txtRoomPhone.Text = string.Empty;
                cbRoomStatus.SelectedIndex = -1;
                cbBedType.SelectedIndex = -1;
                cbRoomType.SelectedIndex = -1;
                txtPrice.Text = string.Empty;
            } 
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
        }

        private void RoomInfo_Load(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            populate();
        }

        private void txtRoomNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Main_Form main = new Main_Form();
            main.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRoomNumber.Text = string.Empty;
            txtRoomPhone.Text = string.Empty;
            cbRoomStatus.SelectedIndex = -1;
            cbBedType.SelectedIndex = -1;
            cbRoomType.SelectedIndex = -1;
            txtPrice.Text = string.Empty;
        }

        private void RoomDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRoomType.Text == "AC" || cbRoomType.Text == "Non-AC")
            {
                cbBedType.Focus();
            }
        }

        private void cbBedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int package1 = 300;
            int package2 = 450;
            int package3 = 600;
            int package4 = 200;
            int package5 = 350;
            int package6 = 500;

            if (cbRoomType.Text == "AC" && cbBedType.Text == "Single")
            {
                txtPrice.Text = "₱" + Convert.ToString(package1);
            }
            if (cbRoomType.Text == "AC" && cbBedType.Text == "Double")
            {
                txtPrice.Text = "₱" + Convert.ToString(package2);
            }
            if (cbRoomType.Text == "AC" && cbBedType.Text == "Triple")
            {
                txtPrice.Text = "₱" + Convert.ToString(package3);
            }
            if (cbRoomType.Text == "Non-AC" && cbBedType.Text == "Single")
            {
                txtPrice.Text = "₱" + Convert.ToString(package4);
            }
            if (cbRoomType.Text == "Non-AC" && cbBedType.Text == "Double")
            {
                txtPrice.Text = "₱" + Convert.ToString(package5);
            }
            if (cbRoomType.Text == "Non-AC" && cbBedType.Text == "Triple")
            {
                txtPrice.Text = "₱" + Convert.ToString(package6);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                connection.Open();
                string Myquery = "select * from room_tbl where room_status = '" + "Free" + "' ";
                MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
                MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                RoomDataGridView.DataSource = ds.Tables[0];
                connection.Close();
            }
            else if (cbFilter.SelectedIndex == 1)
            {
                connection.Open();
                string Myquery = "select * from room_tbl where room_status = '" + "Busy" + "' ";
                MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
                MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                RoomDataGridView.DataSource = ds.Tables[0];
                connection.Close();
            }
        }
    }
}
