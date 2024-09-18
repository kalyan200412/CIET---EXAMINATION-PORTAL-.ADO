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
    public partial class Form15 : Form
    {
        public Form15()
        {
            InitializeComponent();
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

                        if (userIdCount == 0)
                        {
                            MessageBox.Show("User ID can't be Updated. Please enter old User Id.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateQuery = "UPDATE coe SET firstname = @firstname, lastname = @lastname, password = @password WHERE userid = @userid";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                        updateCmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                        updateCmd.Parameters.AddWithValue("@userid", textBox3.Text); // User ID remains the same
                        updateCmd.Parameters.AddWithValue("@password", textBox4.Text);

                        updateCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("COE details updated successfully!", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form14 form14 = new Form14();
                    form14.ShowDialog();
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
            Form14 form14 = new Form14();
            form14.ShowDialog();
        }
    }
}
