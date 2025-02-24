using ECOMAPP.CommonRepository;
using System.Numerics;

namespace ECOMAPP.ModelLayer
{
    public class MLProduct
    {
        public class MlGetProduct
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


        #region   INHOUSEPRODUCT STARTS

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
        public class MLInsertInhouseProduct
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
        public class MLUpdateInhouseProduct
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
        public class MLDeleteInhouseProduct
        {
            public int Id { get; set; }

        }



        #endregion  INHOUSE  ENDS
        public List<MLProduct.MlGetProduct> ProductList { get; set; }
        public List<MLProduct.MLGetInhouseProduct> InhouseProductList { get; set; }

    }

 }
