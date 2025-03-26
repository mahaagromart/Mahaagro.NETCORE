namespace ECOMAPP.ModelLayer
{
    public class MLCart
    {

        public class MLInsertCart
        {
            public string PROD_ID { get; set; }
            public string VARIENTS_ID { get; set; }
            public int Quantity { get; set; }
        }
        public class MLCartData
        {
            public int Id { get; set; }
            public string ProductName { get; set; } = string.Empty; 
            public string Description { get; set; } = string.Empty; 
        }
        public List<MLCartData> CartDataList { get; set; } = new();

        public class ProductCartList
        {
            public string? Id {get;set;}
            public string? PROD_ID {get;set;}
            public string? VARIENTS_ID {get;set;}
            public string? Quantity { get; set; }

        }
        public List<ProductCartList> productCartLists { get; set; }


        public class ProductDtoCart
        {
            public string? ProductId { get; set; }
            public int? Quantity { get; set; }
            public string? VarientsId { get; set; }
            public List<string>? ProductImages { get; set; }
            public string? PackageDiameter { get; set; }
            public string? PackageHeight { get; set; }
            public string? PackageLength { get; set; }
            public string? PackageShape { get; set; }
            public string? PackageTotalVolume { get; set; }
            public string? PackageWeight { get; set; }
            public string? PackageWidth { get; set; }
            public string? CalculatedPrice { get; set; }
            public int? CurrentStockQuantity { get; set; }
            public string? DiscountAmount { get; set; }
            public string? DiscountType { get; set; }
            public int? MaximumRetailPrice { get; set; }
            public int? MinimumOrderQuantity { get; set; }
            public int? Pricing { get; set; }
            public int? SellingPrice { get; set; }
            public string? TaxAmount { get; set; }
            public string? TaxCalculation { get; set; }
            public string? Brand { get; set; }
            public int? CategoryId { get; set; }
            public string? ProductDescription { get; set; }
            public string? ProductName { get; set; }
            public int? SubCategoryId { get; set; }
            public int? SubSubCategoryId { get; set; }
            public string? TagsInput { get; set; }
            public string? Unit { get; set; }
            public string? Hsn { get; set; }
            public string? Sku { get; set; }
            public string? VarientsName { get; set; }

        }

       
     public List<ProductDtoCart> ProductsCart { get; set; }
        




    }


}