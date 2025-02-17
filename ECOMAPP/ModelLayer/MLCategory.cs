using ECOMAPP.CommonRepository;

namespace ECOMAPP.ModelLayer
{
    public class MLCrudCategory
    {
        public List<MLCategory> CategoryList { get; set; }
        public DBEnums.Modes Mode { get; set; } = DBEnums.Modes.ADD;
    }
    public class MLCategory
    {
        public int? Category_id { get; set; } = 0;
        public string?  Category_Name { get; set; } = string.Empty;
        public string? CreationDate { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public int? Priority { get; set; }
        public Boolean? Status { get; set; }

     }

    public class MLFilterCategory
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public class MlInsertProductCategoryData
    {
        public string? Category_id { get; set; }
        public string? Category_Name {get;set;} 
        public string? Image {get;set;} 
    }

    public class EcommerceCategoryDTO
    {

        public int Code { get; set; }
        public string Message { get; set; }
        public string Retval { get; set; }

        public class Category
        {

            public int Category_id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public string Image { get; set; }
            public int Priority { get; set; }
            public string Status { get; set; }

        }

        public List<EcommerceCategoryDTO.Category> CategoryList { get; set; }

    }








}



