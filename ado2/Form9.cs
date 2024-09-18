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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||  
     string.IsNullOrWhiteSpace(textBox2.Text) || 
     string.IsNullOrWhiteSpace(textBox3.Text) ||  
     string.IsNullOrWhiteSpace(textBox4.Text) ||  
     string.IsNullOrWhiteSpace(textBox5.Text))             {
                MessageBox.Show("Please fill all fields, including branch.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

         
            using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();

                string checkUserIdQuery = "SELECT COUNT(*) FROM hod WHERE userid = @userid";
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

                // Check if Password already exists in the `hod` table
                string checkPasswordQuery = "SELECT COUNT(*) FROM hod WHERE password = @password";
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

                // Insert new record into the `hod` table with branch
                string insertQuery = "INSERT INTO hod (firstname, lastname, userid, password, branch) VALUES (@firstname, @lastname, @userid, @password, @branch)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@userid", textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@branch", textBox5.Text);  // Inserting branch value

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("HOD registered successfully!", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form8 form8 = new Form8();
            form8.ShowDialog();
        }
    }
}






