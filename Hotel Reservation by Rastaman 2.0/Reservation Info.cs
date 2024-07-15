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
    public partial class Reservation_Info : Form
    {
        public Reservation_Info()
        {
            InitializeComponent();
        }
        //Connects to the database rastatel.db
        MySqlConnection connection = new MySqlConnection("datasource = localhost; port=3306; database=rastatel_db; username=root; password=;");
        
        //Date and time today
        DateTime today;

        //Selects all the data from the database
        public void populate()
        {
            connection.Open();
            string Myquery = "select * from reservation_tbl";
            MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReservationDataGridView.DataSource = ds.Tables[0];
            connection.Close();
        }

        //This method fill the combo box of room number when we base if the room is free or not. The room will pop up in the combo box when its free.
        public void fillRoomCombo() 
        {
            connection.Open();
            string roomstate = "Free";
            MySqlCommand cmd = new MySqlCommand("select room_number from room_tbl where room_status = '" + roomstate + "'", connection);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("room_number", typeof(int));
            dt.Load(rdr);
            cbRoomNumber.ValueMember = "room_number";
            cbRoomNumber.DataSource = dt;
            connection.Close();
        }
        

        public void fillClientNameCombo()
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select client_name from client_tbl", connection);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("client_name", typeof(string));
            dt.Load(rdr);
            cbClientName.ValueMember = "client_name";
            cbClientName.DataSource = dt;
            connection.Close();
        }

        public void fillStaffNameCombo()
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select name from user_tbl", connection);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Load(rdr);
            cbStaffIncharge.ValueMember = "name";
            cbStaffIncharge.DataSource = dt;
            connection.Close();
        }

        private void Reservation_Info_Load(object sender, EventArgs e)
        {
            today = dtpCheckIn.Value;
            fillRoomCombo();
            fillClientNameCombo();
            fillStaffNameCombo();
            lblDate.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
            populate();
        }

        private void dtpDateIn_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(dtpCheckIn.Value,today);
            if (res < 0) 
            {
                MessageBox.Show("Wrong Date For Reservation", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dtpDateOut_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(dtpCheckOut.Value, dtpCheckIn.Value);
            if (res < 0)
            {
                MessageBox.Show("Wrong Dateout, check once more", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void update_room_state() 
        {
            connection.Open();
            string newstate = "Busy";
            string myquery = "UPDATE room_tbl set room_status = '" + newstate + "' where room_number = " + Convert.ToInt32(cbRoomNumber.SelectedValue.ToString()) + ";";
            MySqlCommand cmd = new MySqlCommand(myquery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            fillRoomCombo();
        }
        
        public void update_room_state_deletebutton()
        {
            connection.Open();
            string newstate = "Free"; 
            int roomid = Convert.ToInt32(ReservationDataGridView.SelectedRows[0].Cells[2].Value.ToString());
            string myquery = "UPDATE room_tbl set room_status = '" + newstate + "' where room_number = " +roomid+ ";";
            MySqlCommand cmd = new MySqlCommand(myquery, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            fillRoomCombo();
        }

        private void btnAdd_Reservation_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbClientName.Text == String.Empty)
                {
                    MessageBox.Show("Please input your Client Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbClientName.Focus();
                    return;
                }

                if (cbRoomNumber.Text == String.Empty)
                {
                    MessageBox.Show("Please input your Room Number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbRoomNumber.Focus();
                    return;
                }
                if (txtNoOfDays.Text == String.Empty)
                {
                    MessageBox.Show("Please input Number of Days to stay", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfDays.Focus();
                    return;
                }

                if (txtNoOfAdults.Text == String.Empty)
                {
                    MessageBox.Show("Please input How many Adults are Included", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfAdults.Focus();
                    return;
                }

                if (txtNoOfChildren.Text == String.Empty)
                {
                    MessageBox.Show("Please input How many Children are Included", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfChildren.Focus();
                    return;
                }

                connection.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO reservation_tbl (reservation_id, client_name, room_number, check_in, no_of_days, no_of_adults, no_of_children, staff_incharge) values ('" + txtReservationId.Text + "','" + cbClientName.Text + "', '" + cbRoomNumber.Text + "', '" + dtpCheckIn.Text + " " + lblDate.Text + "','" + txtNoOfDays.Text + "', '" + txtNoOfAdults.Text + "', '" + txtNoOfChildren.Text + "', '" + cbStaffIncharge.Text + "')", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reservation Successfully Added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            update_room_state();
            populate();
        }

        private void ReservationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtReservationId.Text = ReservationDataGridView.CurrentRow.Cells[0].Value.ToString();
            cbClientName.Text = ReservationDataGridView.CurrentRow.Cells[1].Value.ToString();
            cbRoomNumber.Text = ReservationDataGridView.CurrentRow.Cells[2].Value.ToString();
            txtNoOfDays.Text = ReservationDataGridView.CurrentRow.Cells[5].Value.ToString();
            txtNoOfAdults.Text = ReservationDataGridView.CurrentRow.Cells[6].Value.ToString();
            txtNoOfChildren.Text = ReservationDataGridView.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnEdit_Reservation_Click(object sender, EventArgs e)
        {
            if (txtReservationId.Text == "")
            {
                MessageBox.Show("Empty Id, Enter the Reservation Id");
            }
            else
            {
                connection.Open();
                string myquery = "UPDATE reservation_tbl SET client_name = '" + cbClientName.Text + "', room_number = '" + cbRoomNumber.Text + "', no_of_days = '" + txtNoOfDays.Text + "', no_of_adults = '" + txtNoOfAdults.Text + "', no_of_children = '" + txtNoOfChildren.Text + "', staff_incharge = '" + cbStaffIncharge.Text + "' WHERE reservation_id  = '" + txtReservationId.Text + "'";
                MySqlCommand cmd = new MySqlCommand(myquery, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Reservation Successfully Edited", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                populate();
            }
        }

        private void btnDelete_Reservation_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove this Client?", "Remove Client", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                connection.Open();
                string query = "delete from reservation_tbl where reservation_id = " + txtReservationId.Text + "";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                update_room_state_deletebutton();
                populate();

                txtReservationId.Text = "";
                cbClientName.SelectedIndex = -1;
                cbRoomNumber.SelectedIndex = -1;
                txtNoOfDays.Text = "";
                txtNoOfAdults.Text = "";
                txtNoOfChildren.Text = "";
                cbStaffIncharge.SelectedIndex = -1;
            }        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtSearchBox.Text = string.Empty;
            cbFilter.SelectedIndex = -1;
            populate();
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            connection.Open();
            string Myquery = "select * from reservation_tbl where client_name = '" + txtSearchBox.Text + "' ";
            MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
            MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReservationDataGridView.DataSource = ds.Tables[0];
            connection.Close();
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Main_Form main = new Main_Form();
            main.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtReservationId.Text = "";
            cbClientName.SelectedIndex = -1;
            cbRoomNumber.SelectedIndex = -1;
            txtNoOfDays.Text = "";
            txtNoOfAdults.Text = "";
            txtNoOfChildren.Text = "";
            cbStaffIncharge.SelectedIndex = -1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to Checkout this Client?", "Check Out", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.No)
            {
                return;
            }
            else
            {
                connection.Open();
                string myquery = ("UPDATE reservation_tbl SET check_out = '" + dtpCheckOut.Text + " " + lblDate.Text + "' WHERE reservation_id  = '" + txtReservationId.Text + "'");
                MySqlCommand cmd = new MySqlCommand(myquery, connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Client Successfully Check Out", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                populate();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbClientName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtReservationId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRoomPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 250); //SET THE PAPER SIZE

            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Rastatel", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(120, 30));
            e.Graphics.DrawString("1452 J.Roxas St. Caloocan City", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(70, 45));

            e.Graphics.DrawString("______________________________________________________________________", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(0, 50));

            e.Graphics.DrawString("Reservation Id: " + txtReservationId.Text + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 85));
            e.Graphics.DrawString("Name: " + cbClientName.Text + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 105));
            e.Graphics.DrawString("Room Number: " + cbRoomNumber.Text + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 125));
            e.Graphics.DrawString("Check In: " + ReservationDataGridView.CurrentRow.Cells[3].Value.ToString() + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 145));
            e.Graphics.DrawString("Check Out: " + ReservationDataGridView.CurrentRow.Cells[4].Value.ToString() + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 165));
            e.Graphics.DrawString("Staff Incharge: " + cbStaffIncharge.Text + "", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(20, 185));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                connection.Open();
                string Myquery = "select * from reservation_tbl where check_out = '" + " " + "' ";
                MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
                MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ReservationDataGridView.DataSource = ds.Tables[0];
                connection.Close();
            }
            if (cbFilter.SelectedIndex == 1)
            {
                connection.Open();
                string Myquery = "select * from reservation_tbl where check_out is not null";
                MySqlDataAdapter da = new MySqlDataAdapter(Myquery, connection);
                MySqlCommandBuilder cbuilder = new MySqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ReservationDataGridView.DataSource = ds.Tables[0];
                connection.Close();
            }
        }
    }
}