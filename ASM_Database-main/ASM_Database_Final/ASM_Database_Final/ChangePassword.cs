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
    public partial class ChangePassword : Form
    {
         int employeeId;
         string role;

        // Sửa hoặc thêm constructor này
        public ChangePassword(int employeeId, string role)
        {
            InitializeComponent(); // Dòng này rất quan trọng để hiển thị giao diện
            this.employeeId = employeeId;
            this.role = role ?? "Default"; // Xử lý nếu role bị null
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void ClearData()
        {
            txtNewPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtNewPassword.Focus();
        }

        private void RedirectPage(int employeeId, string authorityLevel)
        {
            if (authorityLevel == "Admin")
            {
                AdminForm adminForm = new AdminForm(authorityLevel, employeeId);
                this.Hide();
                adminForm.Show();
            }
            else if (authorityLevel == "Warehouse Manager")
            {
                WarehouseManagerForm warehouseManagerForm = new WarehouseManagerForm(authorityLevel, employeeId);
                this.Hide();
                warehouseManagerForm.Show();
            }
            else if (authorityLevel == "Sale")
            {
                SaleForm saleForm = new SaleForm(authorityLevel, employeeId);
                this.Hide();
                saleForm.Show();
            }
        }

        private void UpdateEmployee(int employeeId, string newPassword)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "UPDATE Employee SET Password = @newPassword, " +
                             "PasswordChanged = 1 " +
                             "WHERE EmployeeId = @employeeId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("newPassword", newPassword);
                command.Parameters.AddWithValue("employeeId", employeeId);
                int isChanged = command.ExecuteNonQuery();
                if (isChanged > 0)
                {
                    MessageBox.Show(
                        "Successfully change password",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    RedirectPage(employeeId, role);
                }
                else
                {
                    MessageBox.Show(
                        "An error occur while change password",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    ClearData();
                }
                connection.Close();
            }
        }

        private bool ValidateData(string newPassword, string confirmPassword)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show(
                    "New password cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtNewPassword.Focus();
                isValid = false;
            }
            else if (string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show(
                    "Confirm password cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                isValid = false;
            }
            else if (newPassword != confirmPassword)
            {
                MessageBox.Show(
                    "New & confirm passwords are not similar",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ClearData();
                isValid = false;
            }

            return isValid;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            bool isVaild = ValidateData(newPassword, confirmPassword);
            if (isVaild)
            {
                UpdateEmployee(employeeId, newPassword);
            }
        }
    }
}
