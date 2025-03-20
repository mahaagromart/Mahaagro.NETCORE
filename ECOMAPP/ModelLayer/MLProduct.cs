﻿using ECOMAPP.CommonRepository;
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
        }

        public class MLImages
        {
            public int ID { get; set; }
            public string Product_Images { get; set; } = string.Empty;
            public string PROD_ID { get; set; } = string.Empty;
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


        public class VarientList
        {
            public string ProductName { get; set; }
            public string Price { get; set; }
            public string? Reviews { get; set; }
            public string[]? Images { get; set; }
            public string varientName { get; set; }

        }


        public class ProductsList
        {

            public string ProductId { get; set; }
            public List<VarientList> VarientList { get; set; }

        }


        public List<ProductsList> productsLists { get; set; }




        #endregion
    }
}