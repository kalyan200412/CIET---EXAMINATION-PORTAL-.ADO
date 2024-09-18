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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Database connection and updating details
            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // Check if User ID exists
                    string checkUserIdQuery = "SELECT COUNT(*) FROM principal WHERE userid = @userid";
                    using (SqlCommand checkCmd = new SqlCommand(checkUserIdQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@userid", textBox3.Text);
                        int userIdCount = (int)checkCmd.ExecuteScalar();

                        if (userIdCount == 0)
                        {
                            MessageBox.Show("User ID cannot be changed.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Update user details
                    string updateQuery = "UPDATE principal SET firstname = @firstname, lastname = @lastname, password = @password WHERE userid = @userid";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        updateCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        updateCmd.Parameters.AddWithValue("@userid", textBox3.Text);
                        updateCmd.Parameters.AddWithValue("@password", textBox4.Text);

                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("User details updated successfully!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form5 form5 = new Form5();
                    form5.ShowDialog();
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
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }
    }
}
