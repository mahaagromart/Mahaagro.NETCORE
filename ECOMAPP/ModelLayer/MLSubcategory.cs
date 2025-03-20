namespace ECOMAPP.ModelLayer
{
    public class MLSubcategory
    {
        public class Subcategory
        {
            public int id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public int ? Category_id { get; set; }
            public int Priority { get; set; }
            public string Subcategory_Name { get; set; }

        }
        public class MLInsertSubcategory
        {

            public string Category_Name { get; set; }

            public int Category_id { get; set; }
            public int Priority { get; set; }
            public string Subcategory_Name { get; set; }

        }
        public class MLDeleteSubCateggory
        {
            public int id { get; set; }
        }
        public class MLGetThroughId
        {
            public int id { get; set; }
        }

        public class MLGetThroughCategoryId
        {
            public int Category_id { get; set; }
        }

        public class MLUpdateSubcategory
        {
            public int id { get; set; }
            public string Category_Name { get; set; }

            public int Category_id { get; set; }
            public int Priority { get; set; }
            public string Subcategory_Name { get; set; }


        }
        public class MLGetbySubCategory
        {
            public int id { get; set; }
            public string Category_Name { get; set; }

            public int Category_id { get; set; }

            public string Subcategory_Name { get; set; }

        }
        public List<MLSubcategory.MLGetbySubCategory> SubCategoryListByCategory { get; set; }

        public List<MLSubcategory.Subcategory> SubcategoryList { get; set; }
    }
}