using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class RegistrationForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtName;
        private MaskedTextBox mtbPhone;
        private TextBox txtAddress;
        private DateTimePicker dtpDateOfBirth;
        private Button btnRegister;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer Registration";
            this.Width = 350;
            this.Height = 400;

            GroupBox groupBox = new GroupBox()
            {
                Text = "Register New Customer",
                Dock = DockStyle.Fill
            };

            Label lblUsername = new Label() { Text = "Username:", Left = 10, Top = 30, AutoSize = true };
            txtUsername = new TextBox() { Left = 120, Top = 25, Width = 180 };

            Label lblPassword = new Label() { Text = "Password:", Left = 10, Top = 70, AutoSize = true };
            txtPassword = new TextBox() { Left = 120, Top = 65, Width = 180, PasswordChar = '*' };

            Label lblName = new Label() { Text = "Full Name:", Left = 10, Top = 110, AutoSize = true };
            txtName = new TextBox() { Left = 120, Top = 105, Width = 180 };

            Label lblPhone = new Label() { Text = "Phone:", Left = 10, Top = 150, AutoSize = true };
            mtbPhone = new MaskedTextBox() { Left = 120, Top = 145, Width = 180, Mask = "000-000-0000" };

            Label lblAddress = new Label() { Text = "Address:", Left = 10, Top = 190, AutoSize = true };
            txtAddress = new TextBox() { Left = 120, Top = 185, Width = 180, Height = 50, Multiline = true };

            Label lblDOB = new Label() { Text = "Date of Birth:", Left = 10, Top = 250, AutoSize = true };
            dtpDateOfBirth = new DateTimePicker() { Left = 120, Top = 245, Width = 180 };

            btnRegister = new Button() { Text = "Register", Left = 120, Top = 290, Width = 80 };
            btnRegister.Click += BtnRegister_Click;

            groupBox.Controls.Add(lblUsername);
            groupBox.Controls.Add(txtUsername);
            groupBox.Controls.Add(lblPassword);
            groupBox.Controls.Add(txtPassword);
            groupBox.Controls.Add(lblName);
            groupBox.Controls.Add(txtName);
            groupBox.Controls.Add(lblPhone);
            groupBox.Controls.Add(mtbPhone);
            groupBox.Controls.Add(lblAddress);
            groupBox.Controls.Add(txtAddress);
            groupBox.Controls.Add(lblDOB);
            groupBox.Controls.Add(dtpDateOfBirth);
            groupBox.Controls.Add(btnRegister);

            this.Controls.Add(groupBox);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string name = txtName.Text.Trim();
            string phone = mtbPhone.Text;
            string address = txtAddress.Text;
            DateTime dob = dtpDateOfBirth.Value;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please fill the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if username already exists.
            var exists = SimpleDataStore.Customers.Exists(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                MessageBox.Show("Username already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var customer = new Customer()
            {
                Username = username,
                Password = password,
                Name = name,
                Phone = phone,
                Address = address,
                DateOfBirth = dob
            };

            SimpleDataStore.Customers.Add(customer);
            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
