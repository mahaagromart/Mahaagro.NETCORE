using static ECOMAPP.ModelLayer.MLFormHelper;
using System.Data;
using ECOMAPP.CommonRepository;

namespace ECOMAPP.DataLayer
{
    public class DLFormHelper:DALBASE
    {
        public MlFormHelper GetAllCountry()
        {
            MlFormHelper objFormHelper = new();
            try
            {
                DataSet dataSet = new();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_TblCountriesCityState";
                    Db.AddParameters("@Action", "SelectCountries");
                    Db.DBExecute();
                    dataSet = Db.DBExecute();
                    Db.Dispose();
                }

                if (dataSet != null && dataSet.Tables.Count > 0)
                {

                    objFormHelper.CountryEntities = new List<MlFormHelper.CountryEntity>();
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        objFormHelper.CountryEntities.Add(new MlFormHelper.CountryEntity()
                        {
                            CountryId = row["Id"].ToString()??"",
                            CountryName = row["CountrieName"].ToString() ?? "",
                            CountryEmoji = row["Emoji"].ToString() ?? ""
                        });
                    }
                    objFormHelper.Code = 200;
                    objFormHelper.Message = "OK";
                    objFormHelper.Retval = "Success";
                }

            }
            catch (Exception ex)
            {
                ErrorLog("GetAllcountry", "formhelper", ex.ToString());
                objFormHelper.Code = 400;
                objFormHelper.Message = ex.ToString();
                objFormHelper.Retval = "Failed";

            }

            return objFormHelper;
        }


        public MlFormHelper GetStatesByCountry(string countryId)
        {

            MlFormHelper objFormHelper = new();
            try
            {
                DataSet dataset = new();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_TblCountriesCityState";
                    Db.AddParameters("@Action", "SelectStates");
                    Db.AddParameters("@CountrieId", countryId);
                    dataset = Db.DBExecute();
                    Db.Dispose();
                }
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    objFormHelper.StateEntities = new List<MlFormHelper.StateEntity>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        objFormHelper.StateEntities.Add(new MlFormHelper.StateEntity()
                        {
                            StateId = row["StateId"].ToString() ?? "",
                            StateName = row["StateName"].ToString() ?? ""
                        });
                    }
                    objFormHelper.Code = 200;
                    objFormHelper.Message = "OK";
                    objFormHelper.Retval = "Success";
                }



            }
            catch (Exception ex)
            {
                ErrorLog("GetAllcountry", "formhelper", ex.ToString());
                objFormHelper.Code = 400;
                objFormHelper.Message = ex.ToString();
                objFormHelper.Retval = "Failed";

            }

            return objFormHelper;

        }





        public MlFormHelper GetCityByState(string StateId)
        {

            MlFormHelper objFormHelper = new();
            try
            {
                DataSet dataset = new();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_TblCountriesCityState";
                    Db.AddParameters("@Action", "SelectCities");
                    Db.AddParameters("@StateId", StateId);
                    dataset = Db.DBExecute();
                    Db.Dispose();
                }
                if (dataset != null && dataset.Tables.Count > 0)
                {
                    objFormHelper.CityEntities = new List<MlFormHelper.CityEntity>();
                    foreach (DataRow row in dataset.Tables[0].Rows)
                    {
                        objFormHelper.CityEntities.Add(new MlFormHelper.CityEntity()
                        {
                            CityId = row["StateId"].ToString() ?? "",
                            CityName = row["StateName"].ToString() ?? ""
                        });
                    }
                    objFormHelper.Code = 200;
                    objFormHelper.Message = "OK";
                    objFormHelper.Retval = "Success";
                }
            }
            catch (Exception ex)
            {
                ErrorLog("GetCityBystate", "formhelper", ex.ToString());
                objFormHelper.Code = 400;
                objFormHelper.Message = ex.ToString();
                objFormHelper.Retval = "Failed";

            }

            return objFormHelper;

        }



        public MlFormHelper GetFaq()
        {
            MlFormHelper faqdto = new MlFormHelper();
            faqdto.FaqEntitiesList = new List<MlFormHelper.FaqEntities>();
            try
            {
                DataSet dataSet = new DataSet();
                using(DBAccess Db = new DBAccess())
                {
                    Db.DBProcedureName = "SP_FAQ";
                    Db.AddParameters("@Action", "SELECT");
                    dataSet = Db.DBExecute();
                    Db.Dispose();
                }
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    faqdto.FaqEntitiesList = new List<MlFormHelper.FaqEntities>();
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        faqdto.FaqEntitiesList.Add(new MlFormHelper.FaqEntities()
                        {
                            id = Convert.ToInt32(row["id"]),
                            question = row["question"].ToString() ?? "",
                            answer = row["answer"].ToString() ?? "",
                            
                        });
                    }
                    faqdto.Code = 200;
                    faqdto.Message = "OK";
                    faqdto.Retval = "Success";
                }
            }
            catch (Exception ex)
            {
                ErrorLog("GET FAQ", "FAQ", ex.ToString());
                faqdto.Code = 500;
                faqdto.Message = "Internal Server Error";
                faqdto.Retval = "Failed";

            }
            return faqdto;
        }

    }
}
