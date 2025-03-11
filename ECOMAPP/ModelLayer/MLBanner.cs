using System.Security.Cryptography.Pkcs;

namespace ECOMAPP.ModelLayer
{
    public class MLBanner
    {
        public class MlInsertBannerList
        {
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public required string BannerType { get; set; }
            public string? BannerUrl { get; set; }
            public required List<string?> BannerList { get; set; }
        }
        public class MlInsertBanner
        {
            public required List<MlInsertBannerList?> Data {get;set;}
        }

        public class MlToggleBannerStatus
        {
            public string Id { get; set; }
        }
        

        public class MlGetAllBannerEntity
        {
            public int? Id { get; set; }
            public string BannerType { get; set; }
            public string Url { get; set; }
            public string? Description { get; set; } 
            
            public bool isdelete { get; set; }

        }
        


    }
}
