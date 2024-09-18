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
    public partial class Form16 : Form
    {
        public Form16()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form17 form17 = new Form17();
            form17.ShowDialog();
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

                    string checkRollQuery = "SELECT COUNT(*) FROM student WHERE roll = @roll";
                    using (SqlCommand checkCmd = new SqlCommand(checkRollQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@roll", textBox7.Text);
                        int rollCount = (int)checkCmd.ExecuteScalar();

                        if (rollCount > 0)
                        {
                            MessageBox.Show("Roll number already exists. Please choose a different Roll number.", "Duplicate Roll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO student (firstname, lastname, mail, address, branch, contact, roll, password) " +
                                         "VALUES (@firstname, @lastname, @mail, @address, @branch, @contact, @roll, @password)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        insertCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        insertCmd.Parameters.AddWithValue("@mail", textBox3.Text);
                        insertCmd.Parameters.AddWithValue("@address", textBox6.Text);
                        insertCmd.Parameters.AddWithValue("@branch", textBox7.Text);
                        insertCmd.Parameters.AddWithValue("@contact", textBox8.Text);
                        insertCmd.Parameters.AddWithValue("@roll", textBox4.Text);
                        insertCmd.Parameters.AddWithValue("@password", textBox5.Text);

                        insertCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student registered successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form17 form17 = new Form17();
                    form17.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form17 form17 = new Form17();
            form17.ShowDialog();
        }
    }
}
