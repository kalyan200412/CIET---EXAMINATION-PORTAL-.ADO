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
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||  
          string.IsNullOrWhiteSpace(textBox2.Text) ||  
          string.IsNullOrWhiteSpace(textBox3.Text) || 
          string.IsNullOrWhiteSpace(textBox4.Text) ||  
          string.IsNullOrWhiteSpace(textBox5.Text) ||  
          string.IsNullOrWhiteSpace(textBox6.Text) ||  
          string.IsNullOrWhiteSpace(textBox7.Text) ||  
          string.IsNullOrWhiteSpace(textBox8.Text))  
            {
                MessageBox.Show("Please fill all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // Check if the email exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM faculty WHERE mail = @mail";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        int emailCount = (int)checkCmd.ExecuteScalar();

                        if (emailCount == 0)
                        {
                            MessageBox.Show("Mail ID cannot be changed. Please use the old mail to update.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Check if the existing data matches the new input
                    string checkIfDataChangedQuery = "SELECT COUNT(*) FROM faculty WHERE mail = @mail AND firstname = @firstname AND lastname = @lastname AND address = @address AND branch = @branch AND contact = @contact AND subject = @subject AND password = @password";
                    using (SqlCommand checkDataCmd = new SqlCommand(checkIfDataChangedQuery, conn))
                    {
                        checkDataCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        checkDataCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        checkDataCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        checkDataCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        checkDataCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        checkDataCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        checkDataCmd.Parameters.AddWithValue("@subject", textBox4.Text);
                        checkDataCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        int dataMatchCount = (int)checkDataCmd.ExecuteScalar();

                        if (dataMatchCount > 0)
                        {
                            MessageBox.Show("No changes detected. Please check the data and try again.", "No Changes Detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // If data has changed, update the record
                    string updateQuery = "UPDATE faculty SET firstname = @firstname, lastname = @lastname, address = @address, branch = @branch, contact = @contact, subject = @subject, password = @password " +
                                         "WHERE mail = @mail";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        updateCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        updateCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        updateCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        updateCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        updateCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        updateCmd.Parameters.AddWithValue("@subject", textBox4.Text);
                        updateCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Faculty details updated successfully!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form20 form20 = new Form20();
                            form20.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made. Please verify the details and try again.", "Update Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Form20 form20 = new Form20();
            form20.ShowDialog();
        }
    }
}
