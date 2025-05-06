using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    public partial class MDIParent : Form
    {
        private ToolStripStatusLabel statusLabel;

        public MDIParent()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Set the form as an MDI container.
            this.IsMdiContainer = true;
            this.Text = "Restaurant System - MDI Parent";
            this.Width = 800;
            this.Height = 600;

            // Create the MenuStrip.
            MenuStrip menuStrip = new MenuStrip();

            // ----- File Menu -----
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");
            ToolStripMenuItem logoutMenuItem = new ToolStripMenuItem("&Logout");
            logoutMenuItem.Click += LogoutMenuItem_Click;
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("E&xit");
            exitMenuItem.Click += ExitMenuItem_Click;
            fileMenu.DropDownItems.Add(logoutMenuItem);
            fileMenu.DropDownItems.Add(exitMenuItem);

            // ----- Forms Menu -----
            ToolStripMenuItem formsMenu = new ToolStripMenuItem("&Forms");
            ToolStripMenuItem chefFormMenuItem = new ToolStripMenuItem("&Chef Form");
            chefFormMenuItem.Click += (s, e) => { OpenForm(new ChefForm()); };
            ToolStripMenuItem waiterFormMenuItem = new ToolStripMenuItem("&Waiter Form");
            waiterFormMenuItem.Click += (s, e) => { OpenForm(new WaiterForm()); };
            ToolStripMenuItem adminFormMenuItem = new ToolStripMenuItem("&Admin Form");
            adminFormMenuItem.Click += (s, e) => { OpenForm(new AdminForm()); };
            ToolStripMenuItem customerFormMenuItem = new ToolStripMenuItem("&Customer Form");
            customerFormMenuItem.Click += (s, e) => { OpenForm(new CustomerForm()); };
            formsMenu.DropDownItems.Add(chefFormMenuItem);
            formsMenu.DropDownItems.Add(waiterFormMenuItem);
            formsMenu.DropDownItems.Add(adminFormMenuItem);
            formsMenu.DropDownItems.Add(customerFormMenuItem);

            // ----- Login Menu -----
            ToolStripMenuItem loginMenu = new ToolStripMenuItem("&Login");
            ToolStripMenuItem employeeLoginMenuItem = new ToolStripMenuItem("Employee &Login");
            employeeLoginMenuItem.Click += (s, e) => { OpenForm(new EmployeeLoginForm()); };
            ToolStripMenuItem customerLoginMenuItem = new ToolStripMenuItem("Customer Lo&gin");
            customerLoginMenuItem.Click += (s, e) => { OpenForm(new CustomerLoginForm()); };
            loginMenu.DropDownItems.Add(employeeLoginMenuItem);
            loginMenu.DropDownItems.Add(customerLoginMenuItem);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(formsMenu);
            menuStrip.Items.Add(loginMenu);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            // Create the StatusStrip.
            StatusStrip statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel("Current User: None") { Name = "StatusLabel" };
            statusStrip.Items.Add(statusLabel);
            statusStrip.Dock = DockStyle.Bottom;
            this.Controls.Add(statusStrip);

            // Create the NotifyIcon.
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = System.Drawing.SystemIcons.Information;
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = "Restaurant System";
            notifyIcon.BalloonTipText = "Welcome to the Restaurant System!";
            notifyIcon.ShowBalloonTip(3000);
        }

        public void UpdateStatus(string text)
        {
            if (statusLabel != null)
                statusLabel.Text = text;
        }

        private void OpenForm(Form childForm)
        {
            // Open only one instance per form type.
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

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            // Clear session info.
            Session.CurrentUserName = null;
            Session.CurrentUserRole = null;
            UpdateStatus("Current User: None");
            MessageBox.Show("Logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
