using System;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace RestaurantSystem
{
    public partial class MDIParent : MetroForm
    {
        private ToolStripStatusLabel statusLabel;

        public MDIParent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Set up the MDI container, menu, etc.
            this.IsMdiContainer = true;
            this.Text = "Restaurant System - MDI Parent";
            this.Width = 800;
            this.Height = 600;

            // Other control initialization code...

            // For example, add a StatusStrip.
            StatusStrip statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel("Current User: None") { Name = "StatusLabel" };
            statusStrip.Items.Add(statusLabel);
            statusStrip.Dock = DockStyle.Bottom;
            this.Controls.Add(statusStrip);
        }

        public void OpenForm(Form childForm)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == childForm.GetType())
                {
                    frm.BringToFront();
                    return;
                }
            }
            childForm.MdiParent = this;
            childForm.Show();
        }

        public void UpdateStatus(string text)
        {
            if (statusLabel != null)
                statusLabel.Text = text;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Debug message (optional)
            // MessageBox.Show("Logged in user role: " + Session.CurrentUserRole);

            if (!string.IsNullOrEmpty(Session.CurrentUserRole))
            {
                if (Session.CurrentUserRole.Equals("Chef", StringComparison.OrdinalIgnoreCase))
                {
                    OpenForm(new ChefForm());
                }
                else if (Session.CurrentUserRole.Equals("Waiter", StringComparison.OrdinalIgnoreCase))
                {
                    OpenForm(new WaiterForm());
                }
                else if (Session.CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    OpenForm(new AdminForm());
                }
                else if (Session.CurrentUserRole.Equals("Customer", StringComparison.OrdinalIgnoreCase))
                {
                    OpenForm(new CustomerForm());
                }
            }
        }
    }
}
