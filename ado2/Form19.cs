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
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||  // Firstname
       string.IsNullOrWhiteSpace(textBox2.Text) ||  // Lastname
       string.IsNullOrWhiteSpace(textBox3.Text) ||  // Mail
       string.IsNullOrWhiteSpace(textBox6.Text) ||  // Address
       string.IsNullOrWhiteSpace(textBox7.Text) ||  // Branch
       string.IsNullOrWhiteSpace(textBox8.Text) ||  // Contact
       string.IsNullOrWhiteSpace(textBox4.Text) ||  // Roll Number
       string.IsNullOrWhiteSpace(textBox5.Text))    // Password
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // Check if the roll number exists
                    string checkRollQuery = "SELECT COUNT(*) FROM student WHERE roll = @roll";
                    using (SqlCommand checkCmd = new SqlCommand(checkRollQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@roll", textBox4.Text);
                        int rollCount = (int)checkCmd.ExecuteScalar();

                        if (rollCount == 0)
                        {
                            MessageBox.Show("Roll number cannot be changed", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Check if data has changed
                    string checkDataQuery = "SELECT COUNT(*) FROM student WHERE firstname = @firstname AND lastname = @lastname AND mail = @mail AND address = @address AND branch = @branch AND contact = @contact AND password = @password AND roll = @roll";
                    using (SqlCommand checkCmd = new SqlCommand(checkDataQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        checkCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        checkCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        checkCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        checkCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        checkCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        checkCmd.Parameters.AddWithValue("@roll", textBox4.Text);
                        checkCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        int unchangedCount = (int)checkCmd.ExecuteScalar();

                        if (unchangedCount > 0)
                        {
                            MessageBox.Show("No changes were made. Please verify the details and try again.", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // Update the student record
                    string updateQuery = "UPDATE student SET firstname = @firstname, lastname = @lastname, mail = @mail, address = @address, branch = @branch, contact = @contact, password = @password WHERE roll = @roll";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        updateCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        updateCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        updateCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        updateCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        updateCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        updateCmd.Parameters.AddWithValue("@roll", textBox4.Text);
                        updateCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student details updated successfully!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form18 form18 = new Form18();
                            form18.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while updating. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
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
            Form18 form18 = new Form18();
            form18.ShowDialog();
        }
    }
    }
