using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_Database_Final
{
    public partial class AdminForm : Form
    {
        int employeeId;
        string authorityLevel;
        public AdminForm(string authorityLevel, int employeeId)
        {
            InitializeComponent();
            this.authorityLevel = authorityLevel;
            this.employeeId = employeeId;
        }

        private void btnManageEmployee_Click(object sender, EventArgs e)
        {   
            // Giả sử Form ManageEmployee nhận vào vai trò (authorityLevel)
            ManageEmployee manageEmployee = new ManageEmployee(this.authorityLevel);
            this.Hide();
            manageEmployee.Show();
        }
    }
    
}
