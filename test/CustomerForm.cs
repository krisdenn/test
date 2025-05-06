using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace RestaurantSystem
{
    public partial class CustomerForm : MetroForm
    {
        private ComboBox cmbMenu;
        private Button btnPlaceOrder;
        private Button btnRegister;
        private Label lblWaitTime;

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer - Order Placement";
            this.Width = 400;
            this.Height = 300;

            GroupBox groupBox = new GroupBox()
            {
                Text = "Place an Order",
                Left = 10,
                Top = 10,
                Width = 360,
                Height = 150
            };

            Label lblMenu = new Label() { Text = "Menu Item:", Left = 10, Top = 30, AutoSize = true };
            cmbMenu = new ComboBox() { Left = 100, Top = 25, Width = 240 };
            cmbMenu.Items.AddRange(new string[] { "Burger", "Pizza", "Pasta", "Salad" });
            cmbMenu.SelectedIndex = 0;

            btnPlaceOrder = new Button() { Text = "Place Order", Left = 100, Top = 70, Width = 100 };
            btnPlaceOrder.Click += BtnPlaceOrder_Click;
            
            // This label shows the estimated wait time (demo: pending orders count * 5 minutes).
            lblWaitTime = new Label() { Text = "Estimated Wait Time: N/A", Left = 10, Top = 110, AutoSize = true };

            groupBox.Controls.Add(lblMenu);
            groupBox.Controls.Add(cmbMenu);
            groupBox.Controls.Add(btnPlaceOrder);
            groupBox.Controls.Add(lblWaitTime);
            this.Controls.Add(groupBox);

            // Register button for new customer registration.
            btnRegister = new Button() { Text = "Register", Left = 10, Top = 180, Width = 80 };
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            string menuItem = cmbMenu.SelectedItem.ToString();
            int newOrderId = SimpleDataStore.Orders.Any() ? SimpleDataStore.Orders.Max(o => o.OrderId) + 1 : 1;
            var order = new Order()
            {
                OrderId = newOrderId,
                CustomerName = Session.CurrentUserName ?? "Guest",
                MenuItem = menuItem,
                Status = "Pending",
                PrepTime = 0
            };
            SimpleDataStore.Orders.Add(order);

            // Simple wait time calculation: pending orders count * 5 minutes
            int pendingCount = SimpleDataStore.Orders.Count(o => o.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase));
            int estimatedWait = pendingCount * 5;
            lblWaitTime.Text = $"Estimated Wait Time: {estimatedWait} mins";

            MessageBox.Show("Order placed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }
    }
}
