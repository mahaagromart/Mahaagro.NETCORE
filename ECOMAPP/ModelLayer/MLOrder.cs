namespace ECOMAPP.ModelLayer
{
    public class MLOrder
    {
        public int? OrderID { get; set; }
        public string? ProductID { get; set; }
        public string?[] VarientID { get; set; }
        public string? CustomerID { get; set; }
        public string? OrderDate { get; set; }
        public int? Quantity { get; set; }

        public string? TotalAmount { get; set; }
        public string? DeliveryAddress { get ; set;}
        public string? pincode { get; set; }
    }


    public class AddressInfo
    {
        public string SellerPinCode { get; set; }
        public string SellerAddress { get; set; }
        public string UserPinCode { get; set; }
        public string UserAddress { get; set; }
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


    public class Credentials
    {
        public string username { get; set; }
        public string request_type { get; set; }
        public string request_action { get; set; }
        public string pickup_address_id { get; set; }
        public string eeApiToken { get; set; }
        public string business_type { get; set; }
    }



    public class EkartServiceResponse
    {
        public bool status { get; set; }
        public int pincode { get; set; }
        public string remark { get; set; }
        public EkartDetails details { get; set; }
    }

    public class EkartDetails
    {
        public bool cod { get; set; }
        public int max_cod_amount { get; set; }
        public bool forward_pickup { get; set; }
        public bool forward_drop { get; set; }
        public bool reverse_pickup { get; set; }
        public bool reverse_drop { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }
    public class EkartTokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
    public class TokenCacheEntry
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
    public class ServiceabilityDetails
    {
        public bool cod { get; set; }
        public int max_cod_amount { get; set; }
        public bool forward_pickup { get; set; }
        public bool forward_drop { get; set; }
        public bool reverse_pickup { get; set; }
        public bool reverse_drop { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    }

    public class ServiceabilityResponse
    {
        public bool status { get; set; }
        public int pincode { get; set; }
        public string remark { get; set; }
        public ServiceabilityDetails details { get; set; }
    }

}
