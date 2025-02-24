namespace ECOMAPP.ModelLayer
{
    public class MLProductattribute
    {
        public class ProductAttribute
        {

            public int id { get; set; }
            public string Attribute_Name { get; set; }
            public string CreationDate { get; set; }
            public string UpdationDate { get; set; }
            public int IsDelete { get; set; }


        }
        public class MLInsertProductAttribute
        {

   
            public string Attribute_Name { get; set; }
  
  


        }
        public class MLUpdateProductAttribute
        {

            public int id { get; set; }
            public string Attribute_Name { get; set; }



        }

        public class MLDeleteProductAttribute
        {

            public int id { get; set; }
         


        }
        public List<MLProductattribute.ProductAttribute> ProductAttributeList { get; set; }
    }
}
