using System;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class EmployeeLoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;

        public EmployeeLoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Employee Login";
            this.Width = 300;
            this.Height = 200;

            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Employee Login";
            groupBox.Dock = DockStyle.Fill;

            Label lblUsername = new Label() { Text = "Username:", Left = 10, Top = 30, AutoSize = true };
            txtUsername = new TextBox() { Left = 100, Top = 25, Width = 150 };

            Label lblPassword = new Label() { Text = "Password:", Left = 10, Top = 70, AutoSize = true };
            txtPassword = new TextBox() { Left = 100, Top = 65, Width = 150, PasswordChar = '*' };

            btnLogin = new Button() { Text = "Login", Left = 100, Top = 110, Width = 80 };
            btnLogin.Click += BtnLogin_Click;

            groupBox.Controls.Add(lblUsername);
            groupBox.Controls.Add(txtUsername);
            groupBox.Controls.Add(lblPassword);
            groupBox.Controls.Add(txtPassword);
            groupBox.Controls.Add(btnLogin);
            this.Controls.Add(groupBox);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var employee = SimpleDataStore.Employees
                .FirstOrDefault(emp => emp.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                    && emp.Password == password);

            if (employee != null)
            {
                Session.CurrentUserName = employee.Username;
                Session.CurrentUserRole = employee.Role;
                MessageBox.Show("Employee login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update status in the MDI parent if available.
                if (this.MdiParent is MDIParent mdi)
                    mdi.UpdateStatus($"Current User: {Session.CurrentUserName} ({Session.CurrentUserRole})");

                this.Close();
            }
            else
            {
                MessageBox.Show("Login unsuccessful. Check credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
