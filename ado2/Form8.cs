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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
              if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Please enter both User ID and Password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    try
                    {
                        conn.Open();

                        string loginQuery = "SELECT COUNT(*) FROM hod WHERE userid = @userid AND password = @password";
                        using (SqlCommand cmd = new SqlCommand(loginQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@userid", textBox1.Text);
                            cmd.Parameters.AddWithValue("@password", textBox2.Text);

                            int loginCount = (int)cmd.ExecuteScalar();

                            if (loginCount > 0)
                            {
                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                Form11 form11 = new Form11();
                                form11.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Invalid User ID or Password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
}

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form9 form9 = new Form9();
            form9.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
