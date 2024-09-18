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
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||  // Firstname
        string.IsNullOrWhiteSpace(textBox2.Text) ||  // Lastname
        string.IsNullOrWhiteSpace(textBox3.Text) ||  // Mail
        string.IsNullOrWhiteSpace(textBox4.Text) ||  // Subject
        string.IsNullOrWhiteSpace(textBox5.Text) ||  // Password
        string.IsNullOrWhiteSpace(textBox6.Text) ||  // Address
        string.IsNullOrWhiteSpace(textBox7.Text) ||  // Branch
        string.IsNullOrWhiteSpace(textBox8.Text))    // Contact
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // Check if user with the same email already exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM faculty WHERE mail = @mail";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        int emailCount = (int)checkCmd.ExecuteScalar();

                        if (emailCount > 0)
                        {
                            MessageBox.Show("User is already exists.", "User Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert new record into faculty table
                    string insertQuery = "INSERT INTO faculty (firstname, lastname, mail, address, branch, contact, subject, password) " +
                                         "VALUES (@firstname, @lastname, @mail, @address, @branch, @contact, @subject, @password)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        insertCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        insertCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        insertCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        insertCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        insertCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        insertCmd.Parameters.AddWithValue("@subject", textBox4.Text);
                        insertCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Faculty registered successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form20 form20 = new Form20();
                    form20.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form20 form20 = new Form20();
            form20.ShowDialog();
        }
    }
}
