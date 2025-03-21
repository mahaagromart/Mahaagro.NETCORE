using ECOMAPP.CommonRepository;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

namespace ECOMAPP.ModelLayer
{
    public class MLProduct
    {
        public MLProduct()
        {
            ProductList = new List<MlGetProduct>();
            InhouseProductList = new List<MLGetInhouseProduct>();
        }

        public List<MlGetProduct> ProductList { get; set; }
        public List<MLGetInhouseProduct> InhouseProductList { get; set; }

        public class MlGetProduct
        {
            public int? Id { get; set; } = 0;
            public string CATEGORY_ID { get; set; } = string.Empty;
            public string SUB_CATEGORY_ID { get; set; } = string.Empty;
            public string? SUB_SUB_CATEGORY_ID { get; set; }
            public string Product_Name { get; set; } = string.Empty;
            public string Product_Description { get; set; } = string.Empty;
            public string ThumbnailImage { get; set; } = string.Empty;
            public string? BRAND { get; set; }
            public string sku { get; set; } = string.Empty;
            public string UNIT { get; set; } = string.Empty;
            public string[] TAGS_INPUT { get; set; } = Array.Empty<string>();
            public string HSN { get; set; } = string.Empty;

            public string? Added_By { get; set; } = string.Empty;

            // Pricing
            public int PRICING { get; set; }
            public int MAXIMUM_RETAIL_PRICE { get; set; }
            public int SELLING_PRICE { get; set; }
            public int MINIMUM_ORDER_QUANTITY { get; set; }
            public int CURRENT_STOCK_QUANTITY { get; set; }
            public string? CALCULATED_MINIMUM_ORDER_PRICE { get; set; }

            // Logistics
            public int PACKAGE_WEIGHT { get; set; }
            public string PACKAGE_SHAPE { get; set; } = string.Empty;
            public string? PACKAGE_LENGTH { get; set; }
            public string? PACKAGE_WIDTH { get; set; }
            public string? PACKAGE_HEIGHT { get; set; }
            public string? PACKAGE_DIAMETER { get; set; }
            public string? PACKAGE_TOTAL_VOLUME { get; set; }
            public string? DISCOUNT_TYPE { get; set; }
            public string? DISCOUNT_AMOUNT { get; set; }
            public string? TAX_AMOUNT { get; set; }
            public string? TAX_CALCULATION { get; set; }
            public string? CALCULATED_PRICE { get; set; }

            // Varieties
            public string? VAREINTS_NAME { get; set; }
            public string? PROD_ID { get; set; }
            public int? CERTIFICATION { get; set; }
            public List<MLImages>? ImageGallery { get; set; } = new();
            public string? IMAGES { get; set; }
            public string? RATING { get; set; }
            public int? STATUS { get; set; }

            public List<MlProductVariant>? Variants { get; internal set; }
        }

        public class MlGetProducts
        {
            public int? Id { get; set; } = 0;
            public string? CATEGORY_ID { get; set; } = string.Empty;
            public string? SUB_CATEGORY_ID { get; set; } = string.Empty;
            public string? SUB_SUB_CATEGORY_ID { get; set; }
            public string Product_Name { get; set; } = string.Empty;
            public string Product_Description { get; set; } = string.Empty;
            public string ThumbnailImage { get; set; } = string.Empty;
            public string? BRAND { get; set; }
            public string? UNIT { get; set; } = string.Empty;
            public string[]? TAGS_INPUT { get; set; } = Array.Empty<string>();
            public string? PROD_ID { get; set; }
            public string? ADDED_BY { get; set; }

            public string? STATUS { get; set; }
            public string? CERTIFICATION { get; set; }

            public List<MlProductVariant>? Variants { get; internal set; }
        }


        public class MlProductVariant
        {
            public string? VARIENTS_ID { get; set; }
            public string? Product_Name { get; set; }
            public string? Varient_Name { get; set; }
            public string? SKU { get; set; }
            public string? HSN { get; set; }
            public string? isDelete { get; set; }

            public MlProductPricing Pricing { get; set; } = new MlProductPricing();
            public MlProductLogistics Logistics { get; set; } = new MlProductLogistics();
            public List<MLImages> ImageGallery { get; set; } = new List<MLImages>();
        }
        public class MlProductPricing
        {
            public string? PRICING { get; set; }
            public string? MAXIMUM_RETAIL_PRICE { get; set; }
            public string? SELLING_PRICE { get; set; }
            public string? MINIMUM_ORDER_QUANTITY { get; set; }
            public string? CURRENT_STOCK_QUANTITY { get; set; }
            public string? DISCOUNT_TYPE { get; set; }
            public string? DISCOUNT_AMOUNT { get; set; }
            public string? TAX_AMOUNT { get; set; }
            public string? TAX_CALCULATION { get; set; }
            public string? CALCULATED_PRICE { get; set; }
        }

        public class MlProductLogistics
        {
            public string? PACKAGE_SHAPE { get; set; }
            public string? PACKAGE_LENGTH { get; set; }
            public string? PACKAGE_WIDTH { get; set; }
            public string? PACKAGE_HEIGHT { get; set; }
            public string? PACKAGE_WEIGHT { get; set; }
            public string? PACKAGE_DIAMETER { get; set; }
            public string? PACKAGE_TOTAL_VOLUME { get; set; }
        }

        public class MLImages
        {
            public int? ID { get; set; }
            public string? Product_Images { get; set; }
            public string? PROD_ID { get; set; } = string.Empty;
        }


        public class MLVarients
        {

            public int ID { get; set; }
            public string? PROD_ID { get; set; }
            public string? VarientName { get; set; }
        }

        public class MLDeleteProduct
        {
            public string Product_id { get; set; } = string.Empty;
        }

        public class MLToggleCertified
        {
            public string Product_id { get; set; } = string.Empty;
        }

        public class MLToggleStatus
        {
            public string Product_id { get; set; } = string.Empty;
        }

        #region Inhouse Product Management

        public class MLGetInhouseProduct
        {
            public int Id { get; set; }
            public string Product_id { get; set; } = string.Empty;
            public int Quantity { get; set; }
            public string Product_Name { get; set; } = string.Empty;
            public string Image { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Rating { get; set; } = string.Empty;
            public string Category_id { get; set; } = string.Empty;
            public string CreationDate { get; set; } = string.Empty;
            public int SubSubcategory_id { get; set; }
            public string UpdationDate { get; set; } = string.Empty;
            public int Price { get; set; }
        }

        public class MLInsertInhouseProduct : MLGetInhouseProduct { }
        public class MLUpdateInhouseProduct : MLGetInhouseProduct { }

        public class MLDeleteInhouseProduct
        {
            public int Id { get; set; }
        }

        public class MLGetProrductByCategoryId
        {
            public int Id { get; set; }
            public int PaginatioStart { get; set; } = 0;
            public int PaginationEnd { get; set; } = 50;

        }
        
        public class MLGetCompletProductDescription
        {
            public string ProductId { get; set; }


        }

        //------------------------------------------------------------------------------------

        public class VarientList{
            public string ProductName { get; set; }
            public string Price { get; set; }
            public string? Reviews { get; set; }
            public string[]? Images { get; set; }

        }


        public class ProductsList
        {
            
            public string ProductId { get; set; }
            public List<VarientList> VarientList { get; set; }

        }


        public List<ProductsList> productsLists { get; set; }

        //------------------------------------------------------------------------------------

        public class CompleteVarientList
        {
            //TODO: Add the properties AS YOULL NEED TO CREATE AND ADD THEM  
            public string ProductName { get; set; }
            public string Price { get; set; }
            public string? Reviews { get; set; }
            public string[]? Images { get; set; }
            public string? ProductId { get; set; }
            public string? ProductDescription { get; set; }
            public string? Unit { get; set; }
            public string? Brand { get; set; }
            public string? VaientWiseName { get; set; }
            public string? DefaultProductName { get; set; }
            public string? VarientName { get; set; }
            public string? VarientId { get; set; }
            public string? CalculatedPrice { get; set; }
            public string? CurrentStockQuantity { get; set; }
            public string? DiscountAmount { get; set; }
            public string? Pricing { get; set; }
            public string? SellingPrice { get; set; }
            public string? Mrp { get; set; }
            public string? ImageUrl { get; set; }

            public string? PackageShape { get; set; }
            public string? packageLength { get; set; }
            public string? packageWidth { get; set; }
            public string? packageHeight { get; set; }
            public string? packageWeight { get; set; }
            public string? packageDiameter { get; set; }
            public string? packageTotalVolume { get; set; }


        }


        public class ProductDescriptionList
        {

            public string ProductId { get; set; }
            public List<CompleteVarientList> VarientList { get; set; }

        }


        public List<ProductDescriptionList> CompleteProductDescription { get; set; }

        //------------------------------------------------------------------------------------

        #endregion
    }
}
