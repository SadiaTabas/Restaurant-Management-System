namespace RestaurantManagement.Shared
{
    public class RestaurantResult<T>
    {
        public T? Data { get; set; }

        public bool HasError { get; set; }

        public string? Message { get; set; }
    }
}