using System;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class CustomerLoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;

        public CustomerLoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer Login";
            this.Width = 300;
            this.Height = 220;

            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Customer Login";
            groupBox.Dock = DockStyle.Fill;

            Label lblUsername = new Label() { Text = "Username:", Left = 10, Top = 30, AutoSize = true };
            txtUsername = new TextBox() { Left = 100, Top = 25, Width = 150 };

            Label lblPassword = new Label() { Text = "Password:", Left = 10, Top = 70, AutoSize = true };
            txtPassword = new TextBox() { Left = 100, Top = 65, Width = 150, PasswordChar = '*' };

            btnLogin = new Button() { Text = "Login", Left = 100, Top = 110, Width = 80 };
            btnLogin.Click += BtnLogin_Click;

            btnRegister = new Button() { Text = "Register", Left = 190, Top = 110, Width = 80 };
            btnRegister.Click += BtnRegister_Click;

            groupBox.Controls.Add(lblUsername);
            groupBox.Controls.Add(txtUsername);
            groupBox.Controls.Add(lblPassword);
            groupBox.Controls.Add(txtPassword);
            groupBox.Controls.Add(btnLogin);
            groupBox.Controls.Add(btnRegister);
            this.Controls.Add(groupBox);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            var customer = SimpleDataStore.Customers
                .FirstOrDefault(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                  && c.Password == password);

            if (customer != null)
            {
                Session.CurrentUserName = customer.Username;
                Session.CurrentUserRole = "Customer";
                MessageBox.Show("Customer login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update status and open CustomerForm.
                if (this.MdiParent is MDIParent mdi)
                {
                    mdi.UpdateStatus($"Current User: {Session.CurrentUserName} ({Session.CurrentUserRole})");
                    mdi.OpenForm(new CustomerForm());
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Login unsuccessful. Check credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }
    }
}
