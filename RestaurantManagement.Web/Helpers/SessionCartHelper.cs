using System.Text.Json;
using RestaurantManagement.Entities;

namespace RestaurantManagement.Helpers
{
    public static class SessionCart
    {
        private const string KEY = "CART";

        public static List<CartItem> Get(ISession session)
        {
            var data = session.GetString(KEY);
            return data == null ? new List<CartItem>()
                : JsonSerializer.Deserialize<List<CartItem>>(data)!;
        }

        public static void Save(ISession session, List<CartItem> cart)
        {
            session.SetString(KEY, JsonSerializer.Serialize(cart));
        }

        public static void Clear(ISession session)
        {
            session.Remove(KEY);
        }
    }
}