namespace ECOMAPP.ModelLayer
{
    public class MLBrand
    {
        public class Brand
        {

            public int Category_id { get; set; }
            public string Category_Name { get; set; }
            public string CreationDate { get; set; }
            public string Image { get; set; }
            public int Priority { get; set; }
            public string Status { get; set; }

        }

        public List<MLBrand.Brand> BrandList { get; set; }

    }
}
