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

namespace ASM_Database_Final
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            InitializeCombobox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cbRole.SelectedIndex = 0;
            txtUsername.Focus();
        }

        private void ClearData()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            if (cbRole.Items.Count > 0)
            {
                cbRole.SelectedIndex = -1; // Đưa combobox về trạng thái chưa chọn
            }
            txtUsername.Focus(); // Đưa con trỏ chuột về ô tài khoản
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {


            // Get data from user input
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string role = cbRole.SelectedItem.ToString();

            // Valid data
            bool isValid = ValidateData(username, password, role);
            if (isValid)
            {
                
                SqlConnection connection = DatabaseConnection.GetConnection();
                // Check the connection
                if (connection != null)
                {                 
                    string query = "SELECT EmployeeId, PasswordChanged FROM Employee WHERE Username = @username" +
                                   " AND Password = @password AND AuthorityLevel = @role";
                    connection.Open();  
                    SqlCommand command = new SqlCommand(query, connection);
                    // Add params to query
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("password", password);
                    command.Parameters.AddWithValue("role", role);

                    // Retrieve data from database
                    SqlDataReader reader = command.ExecuteReader();
                    int employeeId = 0;
                    bool passwordChanged = false;
                    while (reader.Read())
                    {
                        employeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
                        passwordChanged = reader.GetBoolean(reader.GetOrdinal("PasswordChanged"));
                    }

                    if (employeeId > 0)
                    {
                        MessageBox.Show(
                            "Login success",
                            "Information",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        RedirectPage(role, employeeId, passwordChanged);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Invalid login credential",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        ClearData();
                    }

                    // Close the connection
                    connection.Close();
                }
            }
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      public void InitializeCombobox()
        {
            cbRole.Items.Add("Admin");
            cbRole.Items.Add("Warehouse Manager");
            cbRole.Items.Add("Sale");
            cbRole.SelectedIndex = 0;
        }

        private bool ValidateData(string username, string password, string role)
        {
            bool isValid = true;
            if (username == null || username == string.Empty)
            {
                MessageBox.Show(
                    "Username cannot be blank",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                isValid = false;
                txtUsername.Focus();
            }
            else if (password == null || password == string.Empty)
            {
                MessageBox.Show(
                    "Password cannot be blank",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                isValid = false;
                txtPassword.Focus();
            }
            else if (role == null || role == string.Empty)
            {
                MessageBox.Show(
                    "No role selected",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                isValid = false;
                cbRole.Focus();
            }
            return isValid;
        }

        private void RedirectPage(string selectedRole, int employeeId, bool isChangePassword)
        {
            // Check if isChangePassword = true (1 in database)
            // so that application do not need to redirect user to ChangePassword Form
            if (isChangePassword)
            {
                if (selectedRole != null)
                {
                    // Redirect user based on role
                    if (selectedRole == "Admin")
                    {
                        AdminForm adminForm = new AdminForm(selectedRole, employeeId);
                        this.Hide();
                        adminForm.Show();
                    }
                    else if (selectedRole == "Warehouse Manager")
                    {
                        WarehouseManagerForm warehouseManagerForm = new WarehouseManagerForm(selectedRole, employeeId);
                        this.Hide();
                        warehouseManagerForm.Show();
                    }
                    else if (selectedRole == "Sale")
                    {
                        SaleForm saleForm = new SaleForm(selectedRole, employeeId);
                        this.Hide();
                        saleForm.Show();
                    }
                }
            }
            // If it is false (0 in database) the app will redirect to ChangePassword Form
            // so that user have to change password in the first time login
            else
            {
                ChangePassword changePassword = new ChangePassword(employeeId, selectedRole);
                changePassword.Show();
                this.Hide();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Thêm danh sách quyền vào ComboBox
            cbRole.Items.Add("Admin");
            cbRole.Items.Add("Warehouse Manager");
            cbRole.Items.Add("Sale");

            // Chọn sẵn dòng đầu tiên để người dùng đỡ phải click
            cbRole.SelectedIndex = 0;
        }
    }
}
