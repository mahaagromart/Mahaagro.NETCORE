using ECOMAPP.CommonRepository;
using System.Numerics;

namespace ECOMAPP.ModelLayer
{
    public class MLCrudProduct
    {
        public List<MLProduct> ProductList { get; set; }
        public DBEnums.Modes Mode { get; set; } = DBEnums.Modes.ADD; //Default Mode Add
    }
    public class MLProduct
    {
        public int ProductID { get; set; } = 0; 
        public string? ProductName { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public decimal? Price { get; set; } = decimal.Zero;
    }

    public class MLFilterProduct
    {
        public int PageNumber { get; set;} = 1;
        public int PageSize { get; set;} = 5;
    }
}
