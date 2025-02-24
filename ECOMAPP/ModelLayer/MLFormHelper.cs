namespace ECOMAPP.ModelLayer
{
    public class MLFormHelper
    {
        public class MlFormHelper
        {
            public int Code { get; set; } = 400;
            public string Message { get; set; } = string.Empty;
            public string Retval { get; set; } = string.Empty;



            public class CountryEntity
            {
                public string CountryId { get; set; } = string.Empty;
                public string CountryName { get; set; } = string.Empty;

                public string CountryEmoji { get; set; } = string.Empty;
            }
            public class StateEntity
            {
                public string StateId { get; set; } = string.Empty;
                public string StateName { get; set; } = string.Empty;
            }
            public class CityEntity
            {
                public string CityId { get; set; } = string.Empty;
                public string CityName { get; set; } = string.Empty;
            }

            public class MlFormData
            {
                public string CountryId { get; set; } = string.Empty;
                public string StateId { get; set; } = string.Empty;
                public string CityId { get; set; } = string.Empty;
            }

            public class FaqEntities
            {
                public int id { get; set; } = 0;
                public string question { get; set; } = string.Empty;
                public string answer { get; set; } = string.Empty;
                public string updation_date { get; set; } = string.Empty;
                public string status { get; set; } = string.Empty;
            }
            public List<MlFormHelper.FaqEntities> FaqEntitiesList { get; set; } = [];

            public List<MlFormHelper.CountryEntity> CountryEntities { get; set; } = [];
            public List<MlFormHelper.StateEntity> StateEntities { get; set; } = [];
            public List<MlFormHelper.CityEntity> CityEntities { get; set; } = [];

        }
    }
}
