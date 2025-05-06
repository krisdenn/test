using System;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class AdminForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnAddEmployee;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Administrator - Employee Management";
            this.Width = 350;
            this.Height = 250;

            GroupBox groupBox = new GroupBox() { Text = "Add New Employee", Dock = DockStyle.Fill };

            Label lblUsername = new Label() { Text = "Username:", Left = 10, Top = 30, AutoSize = true };
            txtUsername = new TextBox() { Left = 120, Top = 25, Width = 180 };

            Label lblPassword = new Label() { Text = "Password:", Left = 10, Top = 70, AutoSize = true };
            txtPassword = new TextBox() { Left = 120, Top = 65, Width = 180, PasswordChar = '*' };

            Label lblRole = new Label() { Text = "Role:", Left = 10, Top = 110, AutoSize = true };
            cmbRole = new ComboBox() { Left = 120, Top = 105, Width = 180 };
            cmbRole.Items.AddRange(new string[] { "Chef", "Waiter" });
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.SelectedIndex = 0;

            btnAddEmployee = new Button() { Text = "Add Employee", Left = 120, Top = 150, Width = 120 };
            btnAddEmployee.Click += BtnAddEmployee_Click;

            groupBox.Controls.Add(lblUsername);
            groupBox.Controls.Add(txtUsername);
            groupBox.Controls.Add(lblPassword);
            groupBox.Controls.Add(txtPassword);
            groupBox.Controls.Add(lblRole);
            groupBox.Controls.Add(cmbRole);
            groupBox.Controls.Add(btnAddEmployee);

            this.Controls.Add(groupBox);
        }

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var exists = SimpleDataStore.Employees.Any(emp => emp.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                MessageBox.Show("Username exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SimpleDataStore.Employees.Add(new Employee()
            {
                Username = username,
                Password = password,
                Role = role
            });

            MessageBox.Show("Employee added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
