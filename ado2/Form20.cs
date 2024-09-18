using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ado2
{
    public partial class Form20 : Form
    {
        public Form20()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form21 form21 = new Form21();
            form21.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter both mail ID and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sivac\\OneDrive\\Documents\\adodatabase.mdf;Integrated Security=True;Connect Timeout=30";

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string loginQuery = "SELECT COUNT(*) FROM faculty WHERE mail = @mail AND password = @password";
                    using (var loginCmd = new SqlCommand(loginQuery, conn))
                    {
                        loginCmd.Parameters.AddWithValue("@mail", textBox1.Text);
                        loginCmd.Parameters.AddWithValue("@password", textBox2.Text);
                        int userCount = (int)loginCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Login successful!", "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form23 form23 = new Form23();
                            form23.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Invalid mail ID or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
