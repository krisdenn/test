using System;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class UnifiedLoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;

        public UnifiedLoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Login";
            this.Width = 300;
            this.Height = 220;

            GroupBox groupBox = new GroupBox
            {
                Text = "Login",
                Dock = DockStyle.Fill
            };

            // Username label and textbox
            Label lblUsername = new Label
            {
                Text = "Username:",
                Left = 10,
                Top = 30,
                AutoSize = true
            };
            txtUsername = new TextBox
            {
                Left = 100,
                Top = 25,
                Width = 150
            };

            // Password label and textbox
            Label lblPassword = new Label
            {
                Text = "Password:",
                Left = 10,
                Top = 70,
                AutoSize = true
            };
            txtPassword = new TextBox
            {
                Left = 100,
                Top = 65,
                Width = 150,
                PasswordChar = '*'
            };

            // Login button
            btnLogin = new Button
            {
                Text = "Login",
                Left = 100,
                Top = 110,
                Width = 80
            };
            btnLogin.Click += BtnLogin_Click;

            // Registration button (for customers only)
            btnRegister = new Button
            {
                Text = "Register",
                Left = 190,
                Top = 110,
                Width = 80
            };
            btnRegister.Click += BtnRegister_Click;

            // Add controls to group box
            groupBox.Controls.Add(lblUsername);
            groupBox.Controls.Add(txtUsername);
            groupBox.Controls.Add(lblPassword);
            groupBox.Controls.Add(txtPassword);
            groupBox.Controls.Add(btnLogin);
            groupBox.Controls.Add(btnRegister);

            // Add the group box to the form.
            this.Controls.Add(groupBox);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // First, check employee credentials.
            var employee = SimpleDataStore.Employees
                .FirstOrDefault(emp => emp.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                     && emp.Password == password);

            if (employee != null)
            {
                Session.CurrentUserName = employee.Username;
                Session.CurrentUserRole = employee.Role;

                MessageBox.Show("Employee login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // If the UnifiedLoginForm is opened as an MDI child, update status and open the corresponding employee form.
                if (this.MdiParent is MDIParent mdi)
                {
                    mdi.UpdateStatus($"Current User: {Session.CurrentUserName} ({Session.CurrentUserRole})");

                    if (employee.Role.Equals("Chef", StringComparison.OrdinalIgnoreCase))
                    {
                        mdi.OpenForm(new ChefForm());
                    }
                    else if (employee.Role.Equals("Waiter", StringComparison.OrdinalIgnoreCase))
                    {
                        mdi.OpenForm(new WaiterForm());
                    }
                    else if (employee.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        mdi.OpenForm(new AdminForm());
                    }
                }
                this.Close();
                return;
            }
            else
            {
                // Next, check if the credentials belong to a customer.
                var customer = SimpleDataStore.Customers
                    .FirstOrDefault(cust => cust.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                          && cust.Password == password);

                if (customer != null)
                {
                    Session.CurrentUserName = customer.Username;
                    Session.CurrentUserRole = "Customer";

                    MessageBox.Show("Customer login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (this.MdiParent is MDIParent mdi)
                    {
                        mdi.UpdateStatus($"Current User: {Session.CurrentUserName} ({Session.CurrentUserRole})");
                        mdi.OpenForm(new CustomerForm());
                    }
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Login failed. Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Customer registration only
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }
    }
}
