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
            public int? Id { get; set; }
            public string? PROD_ID { get; set; }
            public string CATEGORY_ID { get; set; }
            public string SUB_CATEGORY_ID { get; set; }
            public string Product_Name { get; set; }
            public string Product_Description { get; set; }
            public string IMAGES { get; set; }
            public string? BRAND { get; set; }
            public string sku { get; set; }
            public string UNIT { get; set; }
            public string[] TAGS_INPUT { get; set; }
            public string? HSN { get; set; }
            public string VAREINTS_NAME { get; set; }

            public int? CERTIFICATION { get; set; }
            public int? STATUS { get; set; }

            public string? RATING { get; set; }

            // Pricing
            public int PRICING { get; set; }
            public int MAXIMUM_RETAIL_PRICE { get; set; }
            public int SELLING_PRICE { get; set; }
            public int MINIMUM_ORDER_QUANTITY { get; set; }
            public int CURRENT_STOCK_QUANTITY { get; set; }

            // Logistics
            public int PACKAGE_WEIGHT { get; set; }
            public string PACKAGE_SHAPE { get; set; }
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

            public string? MINIMUM_CALCULATED_PRICE { get; set; } 
            public List<MLImages>? IMAGESLIST { get; set; }

            public string? VARIENTNAME { get; set; }
            public string MINIMUMCALCULATEDPRICE { get; set; }


        }



        public class MLImages
        {
            public int ID { get; set; }
            public string Product_Images { get; set; }
            public string PROD_ID { get; set; }
        }

        public class MLDeleteProduct
        {
            public string Product_id { get; set; }
        }

        public class MLToggleCertified
        {
            public string Product_id { get; set; }
        }

        public class MLToggleStatus
        {
            public string Product_id { get; set; }
        }

        #region Inhouse Product Management

        public class MLGetInhouseProduct
        {
            public int Id { get; set; }
            public string Product_id { get; set; }
            public int Quantity { get; set; }
            public string Product_Name { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }
            public string Rating { get; set; }
            public string Category_id { get; set; }
            public string CreationDate { get; set; }
            public int SubSubcategory_id { get; set; }
            public string UpdationDate { get; set; }
            public int Price { get; set; }
        }

        public class MLInsertInhouseProduct : MLGetInhouseProduct { }

        public class MLUpdateInhouseProduct : MLGetInhouseProduct { }

        public class MLDeleteInhouseProduct
        {
            public int Id { get; set; }
        }

        #endregion
    }


}