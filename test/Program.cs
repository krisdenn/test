using System;
using System.Windows.Forms;

namespace RestaurantSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (UnifiedLoginForm loginForm = new UnifiedLoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // The MDI parent starts **ONLY IF** login is successful
                    Application.Run(new MDIParent());
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
