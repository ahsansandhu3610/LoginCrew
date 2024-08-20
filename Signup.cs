using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace LoginAndRegistrationForm
{
    public partial class Signup : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=loginData;Integrated Security=True;");
        public Signup()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signup_loginhere_Click(object sender, EventArgs e)
        {
            Form1 form11 = new Form1();
            form11.Show();
            this.Hide();

        }

        private void signup_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_btn_Click(object sender, EventArgs e)
        {
            if (signup_email.Text == "" || signup_username.Text == "" || signup_password.Text == "")
            {
                MessageBox.Show("Please fill all fields", "Error Box", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        String CheckUsername = "SELECT *FROM admin WHERE username = '"
                            + signup_username.Text.Trim() + "'";

                        using (SqlCommand checkUser = new SqlCommand(CheckUsername, connect))
                        {
                            
                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(signup_username.Text + " is already exist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                            else
                            {
                                string insertData = "INSERT INTO admin(email,username,passowrd,date_created) VALUES(@email,@username,@pass,@date)";
                                DateTime date = DateTime.Today;
                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {
                                    cmd.Parameters.AddWithValue("@email", signup_email.Text.Trim());
                                    cmd.Parameters.AddWithValue("@username", signup_username.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", signup_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", date);

                                    cmd.ExecuteNonQuery();
                                    MessageBox.Show("Registeration successfull ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Form1 form1 = new Form1();
                                    form1.Show();
                                    this.Hide();
                                }

                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Connecting Database:" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void signup_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (signup_checkbox.Checked)
            {
                signup_password.PasswordChar = '\0';
            }
            else
            {
                signup_password.PasswordChar = '*';    
            }

        }

        private void signup_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
