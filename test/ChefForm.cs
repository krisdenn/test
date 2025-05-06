using System;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace RestaurantSystem
{
    public partial class ChefForm : MetroForm
    {
        private DataGridView dgvOrders;
        private NumericUpDown nudPrepTime;
        private Button btnMarkCompleted;

        public ChefForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.Text = "Chef - Order Management";
            this.Width = 600;
            this.Height = 400;

            GroupBox groupBoxOrders = new GroupBox()
            {
                Text = "Pending Orders",
                Left = 10,
                Top = 10,
                Width = 560,
                Height = 250
            };

            dgvOrders = new DataGridView()
            {
                Left = 10,
                Top = 20,
                Width = 540,
                Height = 220,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            groupBoxOrders.Controls.Add(dgvOrders);
            this.Controls.Add(groupBoxOrders);

            GroupBox groupBoxUpdate = new GroupBox()
            {
                Text = "Update Order",
                Left = 10,
                Top = 270,
                Width = 560,
                Height = 80
            };

            Label lblPrepTime = new Label() { Text = "Prep Time (min):", Left = 10, Top = 35, AutoSize = true };
            nudPrepTime = new NumericUpDown() { Left = 120, Top = 30, Width = 100, Minimum = 1, Maximum = 120 };
            btnMarkCompleted = new Button() { Text = "Mark as Completed", Left = 250, Top = 28, Width = 150 };
            btnMarkCompleted.Click += BtnMarkCompleted_Click;

            groupBoxUpdate.Controls.Add(lblPrepTime);
            groupBoxUpdate.Controls.Add(nudPrepTime);
            groupBoxUpdate.Controls.Add(btnMarkCompleted);

            this.Controls.Add(groupBoxUpdate);
        }

        private void LoadOrders()
        {
            // Only load orders that are "Pending".
            var pendingOrders = SimpleDataStore.Orders
                                    .Where(o => o.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                                    .ToList();
            dgvOrders.DataSource = null;
            dgvOrders.DataSource = pendingOrders;
        }

        private void BtnMarkCompleted_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderId"].Value);
            var order = SimpleDataStore.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.PrepTime = (int)nudPrepTime.Value;
                order.Status = "Completed";
                MessageBox.Show($"Order {order.OrderId} marked as Completed with prep time {order.PrepTime} mins.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders();
            }
        }
    }
}
