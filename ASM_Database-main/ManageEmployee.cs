using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_Database_Final
{
    public partial class ManageEmployee : Form
    {
        int employeeId;
        string employeePosition;
        public ManageEmployee(string employeePosition)
        {
            employeeId = 0;
            InitializeComponent();
            this.employeePosition = employeePosition;

            cbAuthorityLevel.Items.Clear(); // Xóa các item cũ nếu có
            cbAuthorityLevel.Items.Add("Admin");
            cbAuthorityLevel.Items.Add("Warehouse Manager");
            cbAuthorityLevel.Items.Add("Sale");

            // Chọn mặc định dòng đầu tiên
            cbAuthorityLevel.SelectedIndex = 0;
        }
        public ManageEmployee()
        {

        }

        private bool ValidateData(string employeeCode,
                        string employeeName,
                        string employeePosition,
                        string authorityLevel,
                        string username,
                        string password)
        {
            bool isValid = true;

            if (employeeCode == null || employeeCode == string.Empty)
            {
                MessageBox.Show(
                    "Employee Code cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPhone.Focus();
                isValid = false;
            }
            else if (employeeName == null || employeeName == string.Empty)
            {
                MessageBox.Show(
                    "Employee Name cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtEmployeeName.Focus();
                isValid = false;
            }
            else if (employeePosition == null || employeePosition == string.Empty)
            {
                MessageBox.Show(
                    "Employee Position cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtEmployeePosition.Focus();
                isValid = false;
            }
            else if (authorityLevel == null || authorityLevel == string.Empty)
            {
                MessageBox.Show(
                    "Employee Code cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                cbAuthorityLevel.Focus();
                isValid = false;
            }
            else if (username == null || username == string.Empty)
            {
                MessageBox.Show(
                    "Username cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtUsername.Focus();
                isValid = false;
            }
            else if (password == null || password == string.Empty)
            {
                MessageBox.Show(
                    "Password cannot be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtPassword.Focus();
                isValid = false;
            }

            return isValid;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {

        }

        private void ChangeButtonStatus(bool buttonStatus)
        {
            btnUpdate.Enabled = buttonStatus;
            btnDelete.Enabled = buttonStatus;
            btnClear.Enabled = buttonStatus;
            btnAdd.Enabled = buttonStatus;
        }

        private void FlushEmployeeId()
        {
            this.employeeId = 0;
            ChangeButtonStatus(false);
        }

        private void LoadEmployeeData()
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "SELECT * FROM Employee";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dtgEmployee.DataSource = table;
                connection.Close();
            }
        }

        private void ClearData()
        {
            FlushEmployeeId();
            txtPhone.Text = string.Empty;
            txtEmployeeName.Text = string.Empty;
            txtEmployeePosition.Text = string.Empty;
            cbAuthorityLevel.SelectedIndex = 0;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPhone.Focus();
        }

        public void InitialiezCombobox()
        {
            cbAuthorityLevel.Items.Add("Admin");
            cbAuthorityLevel.Items.Add("Warehouse Manager");
            cbAuthorityLevel.Items.Add("Sale");
            cbAuthorityLevel.SelectedIndex = 0;
        }

        private bool CheckUserExistence(int employeeId)
        {
            bool isExist = false;
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string checkCustomerQuerry = "SELECT * FROM Employee WHERE EmployeeId = @employeeId";
                SqlCommand command = new SqlCommand(checkCustomerQuerry, connection);
                command.Parameters.AddWithValue("employeeId", employeeId);
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
                connection.Close();
            }
            return isExist;
        }

        private void AddUser(int manualId, string employeeCode, string employeeName,
                      string employeePosition, string authorityLevel,
                      string username, string password)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                // Câu lệnh SQL phải bao gồm cột EmployeeID
                string sql = "INSERT INTO Employee (EmployeeID, EmployeeName, Role, Phone, Username, Password, AuthorityLevel, PasswordChanged) " +
                             "VALUES (@ID, @name, @role, @phone, @user, @pass, @auth, 0)";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", manualId); // Gán ID từ người dùng nhập
                command.Parameters.AddWithValue("@name", employeeName);
                command.Parameters.AddWithValue("@role", employeePosition);
                command.Parameters.AddWithValue("@phone", employeeCode);
                command.Parameters.AddWithValue("@user", username);
                command.Parameters.AddWithValue("@pass", password);
                command.Parameters.AddWithValue("@auth", authorityLevel);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully added ID: " + manualId);
                    LoadEmployeeData();
                    ClearData();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: This ID already exists! Please use a different number.");
                }
                connection.Close();
            }
        
        }

        private void UpdateUser(int employeeId, string employeeName, string employeeCode,
                        string employeePosition, string authorityLevel, string username, string password)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
               
                string sql = "UPDATE Employee SET " +
                             "EmployeeName = @name, " +
                             "Role = @role, " +      
                             "Phone = @phone, " +   
                             "AuthorityLevel = @auth, " +
                             "Username = @user, " +
                             "Password = @pass " +
                             "WHERE EmployeeID = @id";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", employeeId);
                command.Parameters.AddWithValue("@name", employeeName);
                command.Parameters.AddWithValue("@role", employeePosition);
                command.Parameters.AddWithValue("@phone", employeeCode);
                command.Parameters.AddWithValue("@auth", authorityLevel);
                command.Parameters.AddWithValue("@user", username);
                command.Parameters.AddWithValue("@pass", password);

                try
                {
                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Employee updated successfully!", "Success");
                        LoadEmployeeData(); 
                        ClearData();        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update failed: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        

        private void DeleteUser(int employeeId)
        {
            SqlConnection connection = DatabaseConnection.GetConnection();
            if (connection != null)
            {
                connection.Open();
                string sql = "DELETE Employee WHERE EmployeeID = @employeeId";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("employeeId", employeeId);
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show(
                        "Successfully delete user",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearData();
                    LoadEmployeeData();
                }
                else
                {
                    MessageBox.Show(
                        "Cannot delete user",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                connection.Close();
            }
        }

        private void SearchUser(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                LoadEmployeeData();
            }
            else
            {
                SqlConnection connection = DatabaseConnection.GetConnection();
                if (connection != null)
                {
                    connection.Open();
                    // Chỉ tìm kiếm trên những cột thực tế có trong bảng
                    string query = "SELECT * FROM Employee WHERE EmployeeName LIKE @search " +
                                   "OR Role LIKE @search " +
                                   "OR Phone LIKE @search " +
                                   "OR AuthorityLevel LIKE @search " +
                                   "OR Username LIKE @search";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("search", "%" + search + "%");

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dtgEmployee.DataSource = table;
                    connection.Close();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Chuyển đổi ID từ ô TextBox sang kiểu số (int)
            int inputId;
            if (!int.TryParse(txtEmployeeID.Text, out inputId))
            {
                MessageBox.Show("Please enter a numeric Employee ID.");
                return;
            }

            // 2. Lấy các thông tin khác
            string employeeCode = txtPhone.Text;
            string employeeName = txtEmployeeName.Text;
            string employeePosition = txtEmployeePosition.Text;
            string authorityLevel = cbAuthorityLevel.SelectedItem.ToString();
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (ValidateData(employeeCode, employeeName, employeePosition, authorityLevel, username, password))
            {
                // 3. Truyền inputId vào hàm AddUser
                AddUser(inputId, employeeCode, employeeName, employeePosition, authorityLevel, username, password);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get user input data
            string employeeCode = txtPhone.Text;
            string employeeName = txtEmployeeName.Text;
            string employeePosition = txtEmployeePosition.Text;
            string authorityLevel = cbAuthorityLevel.SelectedItem.ToString();
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate data
            bool isValid = ValidateData(employeeCode,
                                        employeeName,
                                        employeePosition,
                                        authorityLevel,
                                        username,
                                        password);

            if (isValid)
            {
                UpdateUser(employeeId, employeeCode, employeeName, employeePosition, authorityLevel, username, password);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete this user", "Warning",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DeleteUser(employeeId);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            switch (employeePosition)
            {
                case "Admin":
                    {
                        AdminForm adminForm = new AdminForm(employeePosition, employeeId);
                        this.Hide();
                        adminForm.Show();
                        break;
                    }
                case "Warehouse Manager":
                    {
                        WarehouseManagerForm warehouseManagerForm = new WarehouseManagerForm(employeePosition, employeeId);
                        this.Hide();
                        warehouseManagerForm.Show();
                        break;
                    }
                case "Sale":
                    {
                        SaleForm saleForm = new SaleForm(employeePosition, employeeId);
                        this.Hide(); saleForm.Show();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ManagerEmployee_Load(object sender, EventArgs e)
        {
            LoadEmployeeData();
            ChangeButtonStatus(false);
            InitialiezCombobox();
        }

        private void txtSearch_KeyUp(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            SearchUser(search);
        }

        private void dtgEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Get row index based on current cell (cell clicked)
            int index = dtgEmployee.CurrentCell.RowIndex;

            // Check index
            if (index != -1)
            {
                // get value of each cell based on row index
                // you have to check the sql query which is used to load data from database (LoadEmployeeData function)
                // use this query and execute it in SSMS to imagine the datagridview
                // the order of the column header is follow: ID | Code | Name | Position | AuthorityLevel | Username | Password | PasswordChanged
                // and the index is from 0 to 7

                // Get the employeeID (index is 0)
                employeeId = Convert.ToInt32(dtgEmployee.Rows[index].Cells[0].Value);

                // Change the button status by true (update, delete, clear is enable when employeeId is assigned with value > 0)
                ChangeButtonStatus(true);

                // Get the employeeCode (index is 1)
                txtPhone.Text = dtgEmployee.Rows[index].Cells[1].Value.ToString();

                // Get the employeeName (index is 2)
                txtEmployeeName.Text = dtgEmployee.Rows[index].Cells[2].Value.ToString();

                // Get the employeePosition (index is 3)
                txtEmployeePosition.Text = dtgEmployee.Rows[index].Cells[3].Value.ToString();

                // Get the authorityLevel (index is 4)
                string authorityLevel = dtgEmployee.Rows[index].Cells[4].Value.ToString();

                // Change the combobox selected index base ont the value of authority level
                if (authorityLevel == "Admin")
                {
                    cbAuthorityLevel.SelectedIndex = 0;
                }
                else if (authorityLevel == "Warehouse Manager")
                {
                    cbAuthorityLevel.SelectedIndex = 1;
                }
                else if (authorityLevel == "Sale")
                {
                    cbAuthorityLevel.SelectedIndex = 2;
                }

                // Get the Username (index is 5)
                txtUsername.Text = dtgEmployee.Rows[index].Cells[5].Value.ToString();

                // Get the Password (index is 6)
                txtPassword.Text = dtgEmployee.Rows[index].Cells[6].Value.ToString();
            }
        }

        private void cbAuthorityLevel_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            // 1. Lấy nội dung người dùng nhập vào ô tìm kiếm (txtSearch)
            // Trim() giúp loại bỏ các dấu cách thừa ở đầu và cuối chuỗi
            string searchKeyword = txtSearch.Text.Trim();

            // 2. Gọi hàm SearchUser đã có sẵn trong class ManageEmployee
            // Hàm này sẽ thực thi lệnh SELECT với toán tử LIKE trên database gốm sứ
            SearchUser(searchKeyword);

            // 3. Kiểm tra nếu không có kết quả thì thông báo (tùy chọn)
            if (dtgEmployee.Rows.Count == 0 && !string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("No staff found matching the keyword: " + searchKeyword, "Search Results");
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {

            // 1. Lấy dữ liệu từ các ô nhập liệu trên giao diện
            
            string employeeCode = txtPhone.Text.Trim();
            string employeeName = txtEmployeeName.Text.Trim();
            string employeePosition = txtEmployeePosition.Text.Trim();
            string authorityLevel = cbAuthorityLevel.SelectedItem != null ? cbAuthorityLevel.SelectedItem.ToString() : "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 2. Kiểm tra xem người dùng đã chọn nhân viên nào từ bảng chưa (ID phải > 0)
           

            // 3. Kiểm tra tính hợp lệ của dữ liệu đầu vào
            bool isValid = ValidateData(employeeCode, employeeName, employeePosition, authorityLevel, username, password);

            if (isValid)
            {
                // 4. Gọi hàm UpdateUser với thứ tự tham số khớp với định nghĩa hàm
          if (employeeId <= 0)
            {
                MessageBox.Show("Please select an employee from the list to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }       // Thứ tự: ID -> Name -> Code -> Position -> Auth -> User -> Pass
                UpdateUser(employeeId, employeeName, employeeCode, employeePosition, authorityLevel, username, password);
            }
        }
    }
}    

