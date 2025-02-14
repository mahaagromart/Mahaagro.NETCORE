namespace ECOMAPP.ModelLayer
{
    public class MLOrder
    {
        public int? OrderID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public string OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
