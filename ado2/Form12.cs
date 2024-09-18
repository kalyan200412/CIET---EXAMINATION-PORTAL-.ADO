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

namespace ado2
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    string checkUserIdQuery = "SELECT COUNT(*) FROM coe WHERE userid = @userid";
                    using (SqlCommand checkCmd = new SqlCommand(checkUserIdQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@userid", textBox3.Text);
                        int userIdCount = (int)checkCmd.ExecuteScalar();

                        if (userIdCount > 0)
                        {
                            MessageBox.Show("User ID already exists. Please choose a different User ID.", "Duplicate User ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string checkPasswordQuery = "SELECT COUNT(*) FROM coe WHERE password = @password";
                    using (SqlCommand checkCmd = new SqlCommand(checkPasswordQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@password", textBox4.Text);
                        int passwordCount = (int)checkCmd.ExecuteScalar();

                        if (passwordCount > 0)
                        {
                            MessageBox.Show("Password already exists. Please choose a different Password.", "Duplicate Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO coe (firstname, lastname, userid, password) VALUES (@firstname, @lastname, @userid, @password)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        insertCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        insertCmd.Parameters.AddWithValue("@userid", textBox3.Text);
                        insertCmd.Parameters.AddWithValue("@password", textBox4.Text);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("COE Registered successfully!", "Registered Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form13 form13 = new Form13();
                    form13.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form13 form13 = new Form13();
            form13.ShowDialog();
        }
    }
}
    

