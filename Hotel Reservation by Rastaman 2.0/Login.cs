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
    public partial class Login : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource = localhost; port=3306; database=rastatel_db; username=root; password=;");
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter("select COUNT(*) from user_tbl where username='" + txtUsername.Text + "' and password='" + txtPassword.Text + "' ", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Main_Form mf = new Main_Form();
                mf.Show();
                this.Hide();
            }
            if (txtUsername.Text == string.Empty && txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Please input your username and password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else if (txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Please input your username", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please input your password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else 
            {
                MessageBox.Show("Wrong Username or Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            connection.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            panel5.BackColor = Color.White;
            txtUsername.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel5.BackColor = SystemColors.Control;
            txtPassword.BackColor = SystemColors.Control;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogin_Enter(object sender, EventArgs e)
        {
            connection.Open();
            MySqlDataAdapter sda = new MySqlDataAdapter("select COUNT(*) from user_tbl where username='" + txtUsername.Text + "' and password='" + txtPassword.Text + "' ", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Main_Form mf = new Main_Form();
                mf.Show();
                this.Hide();
            }
            else if (txtUsername.Text == string.Empty && txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Please input your username and password");
                txtUsername.Focus();
            }
            else if (txtUsername.Text == string.Empty)
            {
                MessageBox.Show("Please input your username");
                txtUsername.Focus();
            }
            else if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please input your password");
                txtPassword.Focus();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
                txtUsername.Focus();
            }
            connection.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
