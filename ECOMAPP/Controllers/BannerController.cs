using ECOMAPP.CommonRepository;
using ECOMAPP.DataLayer;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static ECOMAPP.MiddleWare.AppEnums;
using static ECOMAPP.ModelLayer.MLBanner;

namespace ECOMAPP.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BannerController : Controller
    {


        [Route("InsertBanner")]
        [HttpPost]
        [JwtAuthorization(Roles = [Roles.Admin])] 
        public ActionResult<IEnumerable<DBReturnData>> InsertBanner(MlInsertBanner data)
        {
            DBReturnData dBReturnData = new();
            DLBanner dLBanner = new();
            try
            {
                dBReturnData = dLBanner.InsertBanner(data);


            }catch(Exception e)
            {
                dBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                dBReturnData.Retval = DBEnums.Status.FAILURE.ToString() +"due to"+e.ToString();
            }

            return new[] { dBReturnData };                
        }


        [Route("GetAllBanner")]
        [HttpPost]
        public ActionResult<IEnumerable<DBReturnData>> GetAllBanner()
        {
            DBReturnData dBReturnData = new();
            DLBanner dLBanner = new();
            List<MlGetAllBannerEntity> BannerList = [];  
            try
            {
                
                BannerList = dLBanner.GetAllBanner();
                if(BannerList == null && BannerList.Count == 0)
                {
                    dBReturnData.Dataset = null;
                    dBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    dBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                }
                else
                {
                    dBReturnData.Dataset = BannerList;
                    dBReturnData.Code = DBEnums.Codes.SUCCESS;
                    dBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    dBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }

            }
            catch (Exception e)
            {
                dBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                dBReturnData.Retval = DBEnums.Status.FAILURE.ToString() + "due to" + e.ToString();
            }

            return new[] { dBReturnData };


        }

        [Route("GetAllBannerAdmin")]
        [HttpPost] //add admin middleware
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> GetAllBannerAdmin()
        {
            DBReturnData dBReturnData = new();
            DLBanner dLBanner = new();
            List<MlGetAllBannerEntity> BannerList = [];
            try
            {

                BannerList = dLBanner.GetAllBannerAdmin();
                if (BannerList == null && BannerList.Count == 0)
                {
                    dBReturnData.Dataset = null;
                    dBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    dBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                }
                else
                {
                    dBReturnData.Dataset = BannerList;
                    dBReturnData.Code = DBEnums.Codes.SUCCESS;
                    dBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    dBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }

            }
            catch (Exception e)
            {
                dBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                dBReturnData.Retval = DBEnums.Status.FAILURE.ToString() + "due to" + e.ToString();
            }

            return new[] { dBReturnData };


        }


        [Route("ToggleBannerStatus")]
        [HttpPost] //add admin middleware
        [JwtAuthorization(Roles = [Roles.Admin])]
        public ActionResult<IEnumerable<DBReturnData>> ToggleBannerStatus([FromBody] MlToggleBannerStatus mlToggleBannerStatus)
        {
            DBReturnData dBReturnData = new();
            DLBanner dLBanner = new();
            try
            {
                if (dLBanner.ToggleBannerStatus(mlToggleBannerStatus))
                {
                    dBReturnData.Code = DBEnums.Codes.SUCCESS;
                    dBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    dBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                }
                else
                {
                    dBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    dBReturnData.Retval = DBEnums.Status.FAILURE.ToString();

                }


            }
            catch (Exception e)
            {
                dBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                dBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                dBReturnData.Retval = DBEnums.Status.FAILURE.ToString() + "due to" + e.ToString();
            }

            return new[] { dBReturnData };
        }

    }
}