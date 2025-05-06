namespace RestaurantSystem
{
    public static class Session
    {
        public static string CurrentUserName { get; set; }
        public static string CurrentUserRole { get; set; } // "Admin", "Chef", "Waiter", or "Customer"
    }
}
