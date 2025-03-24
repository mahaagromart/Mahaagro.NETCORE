using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Google.Api.Gax.ResourceNames;
using static ECOMAPP.ModelLayer.MLCart;

namespace ECOMAPP.DataLayer
{
    public class DLCart
    {
       public List<MLCart.MLCartData> GetCartData()
            {
                MLCart _MLCart = new();
                DBReturnData _DBReturnData = new();
                DataSet _DataSet = new();

            try
            {
                using DBAccess _DBAccess = new DBAccess();
                _DBAccess.DBProcedureName = "";
                _DBAccess.AddParameters("@Action", "GetCartData");
                _DataSet = _DBAccess.DBExecute();
                _DBAccess.Dispose();

                if (_DataSet?.Tables.Count > 0)
                {
                    var DataTable = _DataSet.Tables[0];
                    string RetVal = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString();

                    if (RetVal == "SUCCESS")
                    {
                        foreach (DataRow Row in DataTable.Rows)
                        {
                            MLCart.MLCartData CartData = new()
                            {
                                Id = Row["id"] == DBNull.Value ? 0 : Convert.ToInt32(Row["Id"]),
                                ProductName = Row["ProductName"]?.ToString() ?? string.Empty,
                                Description = Row["Description"]?.ToString() ?? string.Empty,
                            };
                            _MLCart.CartDataList.Add(CartData);

                        }
                    }
                    else
                    {
                        _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                        _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;
                    }
                }
            }


            catch (Exception ex)
            {
                new DALBASE().ErrorLog("GetCartData", "DLCart", ex.ToString());
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
            }

                return _MLCart.CartDataList;
            }



        public DBReturnData InsertCartData()
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using(DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("", "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status=DBEnums.Status.SUCCESS;
                    _DBReturnData.Message=DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }
                


                
            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }



        public DBReturnData UpdateCartData()
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("", "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }




            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }



        public DBReturnData DeleteCartData()
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new DALBASE();


            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("", "");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                DataTable _DataTable = _DataSet.Tables[0];
                string RetVal = _DataTable.Rows[0]["RETVAL"]?.ToString();

                if (RetVal == "SUCCESS")
                {
                    _DBReturnData.Status = DBEnums.Status.SUCCESS;
                    _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Retval = DBEnums.Status.SUCCESS.ToString();
                    _DBReturnData.Code = DBEnums.Codes.SUCCESS;



                }
                else
                {
                    _DBReturnData.Status = DBEnums.Status.FAILURE;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Retval = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.NOT_FOUND;

                }




            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteCartData", "DLCart", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }



            return _DBReturnData;
        }
    }

    }

