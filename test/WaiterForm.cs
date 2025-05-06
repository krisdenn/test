using System;
using System.Linq;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class WaiterForm : Form
    {
        private DataGridView dgvOrders;
        private Button btnMarkServed;

        public WaiterForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.Text = "Waiter - Order Management";
            this.Width = 600;
            this.Height = 400;

            GroupBox groupBoxOrders = new GroupBox()
            {
                Text = "Completed Orders (Ready to Serve)",
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

            btnMarkServed = new Button() { Text = "Mark as Served", Left = 250, Top = 280, Width = 120 };
            btnMarkServed.Click += BtnMarkServed_Click;
            this.Controls.Add(btnMarkServed);
        }

        private void LoadOrders()
        {
            // Load orders with status "Completed"
            var completedOrders = SimpleDataStore.Orders
                                    .Where(o => o.Status.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                                    .ToList();
            dgvOrders.DataSource = null;
            dgvOrders.DataSource = completedOrders;
        }

        private void BtnMarkServed_Click(object sender, EventArgs e)
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
                order.Status = "Served";
                MessageBox.Show($"Order {order.OrderId} marked as Served.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadOrders();
            }
        }
    }
}
