namespace ECOMAPP.ModelLayer
{
    public class MLVarients
    {
        public class Varients
        {
            public int Id { get; set; }
            public string Product_Id { get; set; }
            public int Quantity { get; set; }
            public string ProductName { get; set; }
            public string Images { get; set; }
            public string Description { get; set; }
            public string Ratings { get; set; }
            public string Category_id { get; set; }
            public string CreationDate { get; set; }

        }
        public class MLInsertVarients
        {
            public int Id { get; set; }
            public string Product_Id { get; set; }
            public int Quantity { get; set; }
            public string ProductName { get; set; }
            public string Images { get; set; }
            public string Description { get; set; }
            public string Ratings { get; set; }
            public string Category_id { get; set; }
            public string CreationDate { get; set; }

        }
        public class MLUpdateVarients
        {
            public int Id { get; set; }
            public string Product_Id { get; set; }
            public int Quantity { get; set; }
            public string ProductName { get; set; }
            public string Images { get; set; }
            public string Description { get; set; }
            public string Ratings { get; set; }
            public string Category_id { get; set; }
            public string CreationDate { get; set; }

        }
        public class MLDeleteVarients
        {
            public int Id { get; set; }
          

        }
        public List<MLVarients.Varients> VarientsList { get; set; }
    }
}
