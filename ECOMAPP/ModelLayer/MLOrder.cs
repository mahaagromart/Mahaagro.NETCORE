namespace ECOMAPP.ModelLayer
{
    public class MLOrder
    {
        public int? OrderID { get; set; }
        public string? ProductID { get; set; }
        public string? VarientID { get; set; }
        public string? CustomerID { get; set; }
        public string? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public string? TotalAmount { get; set; }
        public string? Address {get ; set;}
        public string? pincode { get; set; }
    }

    public class RazorpayPaymentVerification
    {
        public string razorpay_payment_id { get; set; }
        public string razorpay_order_id { get; set; }
        public string razorpay_signature { get; set; }
    }
    public class RazorpayOrder
    {
        public int Amount { get; set; }
        public int Amount_Due { get; set; }
        public int Amount_Paid { get; set; }
        public int Attempts { get; set; }
        public long Created_At { get; set; }
        public string Currency { get; set; }
        public string Entity { get; set; }
        public string Id { get; set; }
        public List<string> Notes { get; set; }
        public string Offer_Id { get; set; }
        public string Receipt { get; set; }
        public string Status { get; set; }
    }

}
