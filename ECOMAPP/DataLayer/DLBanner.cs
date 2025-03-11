using System.Data;
using ECOMAPP.CommonRepository;
using static ECOMAPP.ModelLayer.MLBanner;
using static ECOMAPP.ModelLayer.MLProduct;

namespace ECOMAPP.DataLayer
{
    public class DLBanner:DALBASE
    {
        public DBReturnData InsertBanner(MlInsertBanner data)
        {
            DBReturnData dBReturnData = new();
            try
            {
                List<DataSet> Results = new();
                foreach (MlInsertBannerList? item in data.Data)
                {
                    if (item != null)
                    {
                        using (DBAccess dBAccess = new())
                        {
                            dBAccess.DBProcedureName = "SP_BANNERSERVICES";
                            dBAccess.AddParameters("@Action", "InsertBanner");
                            dBAccess.AddParameters("@Path", string.Join(",", item.BannerList ?? []));
                            dBAccess.AddParameters("@BlockName",item.BannerType);
                            dBAccess.AddParameters("@DirectionLinkWeb",item.BannerUrl??"");
                            dBAccess.AddParameters("@DirectionLinkMobile","");
                            dBAccess.AddParameters("@CampaingStart",item.StartDate ?? DateTime.UtcNow.ToString());
                            dBAccess.AddParameters("@CampaingEnd",item.EndDate ?? DateTime.UtcNow.ToString());
                            Results.Add(dBAccess.DBExecute());
                            dBAccess.Dispose();
                        }
                    }
                    // Process item

                }

                foreach(DataSet da in Results)
                {
                    if(da.Tables[0].Rows[0]["RETVAL"].ToString() == "Success")
                    {
                        dBReturnData.Code = DBEnums.Codes.SUCCESS;
                        dBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                        dBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                        dBReturnData.Status = DBEnums.Status.SUCCESS;
                    }
                    else
                    {
                        dBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                        dBReturnData.Status = DBEnums.Status.SUCCESS;
                        dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                        dBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                        break;
                    }
               
                }
                


            }
            catch (Exception c)
            {
                ErrorLog("Insert Bammer", "DLBanner", c.ToString());
                dBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                dBReturnData.Retval = DBEnums.Status.FAILURE.ToString() + "due to" + c.ToString();
            }
            return dBReturnData;

        }

        public List<MlGetAllBannerEntity> GetAllBanner()
        {
            List<MlGetAllBannerEntity> rdata = [];
            DataSet dataSet = new();
            try
            {
                using (DBAccess dBAccess = new())
                {
                    dBAccess.DBProcedureName = "SP_BANNERSERVICES";
                    dBAccess.AddParameters("@Action", "GetAllBanner");
                    dataSet = dBAccess.DBExecute();
                    dBAccess.Dispose();
                };
                if(dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[1].Rows[0]["Retval"].ToString() == "Success")
                    {
                        foreach(DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            string? bannerUrlList = dataRow["path"]?.ToString();
                            List<string> banners = !string.IsNullOrEmpty(bannerUrlList)
                                ? new List<string>(bannerUrlList.Split(',')) : [];
                            foreach (string bannerUrl in banners)
                            {
                                rdata.Add(new MlGetAllBannerEntity
                                {
                                    Id = Convert.ToInt32(dataRow["id"] ?? 0),
                                    BannerType = dataRow["BlockName"].ToString()??"Null", 
                                    Description = "Banner Image", //TODO add column and retrive 3-10-2025 pavan
                                    Url = bannerUrl,
                                    isdelete = false
                                });

                            }
                        }
                    }
                }
            }
            catch(Exception c)
            {
                rdata = null;
                ErrorLog("Insert Bammer", "DLBanner", c.ToString());
            }
            return rdata ?? [];
        }
        
        
        public List<MlGetAllBannerEntity> GetAllBannerAdmin()
        {
            List<MlGetAllBannerEntity> rdata = [];
            DataSet dataSet = new();
            try
            {
                using (DBAccess dBAccess = new())
                {
                    dBAccess.DBProcedureName = "SP_BANNERSERVICES";
                    dBAccess.AddParameters("@Action", "GetAllBannerAdmin");
                    dataSet = dBAccess.DBExecute();
                    dBAccess.Dispose();
                };
                if(dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[1].Rows[0]["Retval"].ToString() == "Success")
                    {
                        foreach(DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            string? bannerUrlList = dataRow["path"]?.ToString();
                            List<string> banners = !string.IsNullOrEmpty(bannerUrlList)
                                ? new List<string>(bannerUrlList.Split(',')) : [];
                            foreach (string bannerUrl in banners)
                            {
                                rdata.Add(new MlGetAllBannerEntity
                                {
                                    Id = Convert.ToInt32(dataRow["id"] ?? 0),
                                    BannerType = dataRow["BlockName"].ToString()??"Null", 
                                    Description = "Banner Image", //TODO add column and retrive 3-10-2025 pavan
                                    Url = bannerUrl,
                                    isdelete = Convert.ToSByte(dataRow["isdelete"]) == 1 ? true : false
                                });

                            }
                        }
                    }
                }
            }
            catch(Exception c)
            {
                rdata = null;
                ErrorLog("GetAllBannerAdmin", "DLBanner", c.ToString());
            }
            return rdata ?? [];
        }


        public bool ToggleBannerStatus(MlToggleBannerStatus mlToggleBannerStatus)
        {
            bool rdata = false;
            try
            {
                DataSet dataSet = new();
                using(DBAccess dBAccess = new())
                {

                    dBAccess.DBProcedureName = "SP_BANNERSERVICES";
                    dBAccess.AddParameters("@Action", "ToggleStatus");
                    dBAccess.AddParameters("@Id", mlToggleBannerStatus.Id);
                    dataSet = dBAccess.DBExecute();
                    dBAccess.Dispose();
                }
                if(dataSet != null && dataSet.Tables.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["Retval"].ToString() == "Suceess")
                    {
                        return true;
                    }
                }

            }catch(Exception e)
            {
                ErrorLog("ToggleBannerStatus", "DLBanner", e.ToString());
            }

            return rdata;
        }
    }
}
