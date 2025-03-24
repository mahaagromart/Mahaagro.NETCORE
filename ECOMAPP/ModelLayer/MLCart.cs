namespace ECOMAPP.ModelLayer
{
    public class MLCart
    {
        public class MLCartData
        {
            public int Id { get; set; }
            public string ProductName { get; set; } = string.Empty; 
            public string Description { get; set; } = string.Empty; 
        }
        public List<MLCartData> CartDataList { get; set; } = new();
    }
}