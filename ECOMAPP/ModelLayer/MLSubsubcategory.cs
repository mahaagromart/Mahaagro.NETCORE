namespace ECOMAPP.ModelLayer
{
    public class MLSubsubcategory
    {
        public class SubsubCategory
        {
            public int Id { get; set; }
            public string SUBSUBCATEGORY_NAME { get; set; }
            public string CATEGORY_NAME { get; set; }
            public string SUBCATEGORY_NAME { get; set; }
            public int PRIORITY { get; set; }
            public int CATEGORY_ID { get; set; }
            public string CreationDate { get; set; }

         }
        public class MLInsertsubsubcategory
        {
            public string SUBSUBCATEGORY_NAME { get; set; }
            public string CATEGORY_NAME { get; set; }
            public string SUBCATEGORY_NAME { get; set; }
            public int PRIORITY { get; set; }
            public int CATEGORY_ID { get; set; }
        }
        public class MLUpdateSubsubcategory
        {
            public int id { get; set; }
            public string SUBSUBCATEGORY_NAME { get; set; }
            public string CATEGORY_NAME { get; set; }
            public string SUBCATEGORY_NAME { get; set; }
            public int PRIORITY { get; set; }

        }
        public class MLDeletesubsubcategory
        {
            public int id { get; set; }
        }
        public List<MLSubsubcategory.SubsubCategory> SubsubCategoryList { get; set; }
    }
}
