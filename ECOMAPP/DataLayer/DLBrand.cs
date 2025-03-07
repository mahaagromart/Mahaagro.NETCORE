using System.Data;
using System.Linq.Expressions;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;

namespace ECOMAPP.DataLayer
{
    public class DLBrand
    {
        public List<MLBrand.Brand> GetAllBrand()
        {
           
            MLBrand _MLBrand = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_CATEGORY";
                    _DBAccess.AddParameters("@Action", "SELECTCATEGORY");
                    _DataSet = _DBAccess.DBExecute();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = new DataTable();
                    string Retval = _DataSet.Tables[1].Rows[0]?.ToString() ?? "";
                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow Row in _DataTable.Rows)
                        {
                            var Brand = new MLBrand.Brand
                            {


                            };
                            _MLBrand.BrandList.Add(Brand);
                        }

                    }
                    else
                    {
                        _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();


                    }


                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                }
            } catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllBrand", "DLBrand", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
            }
            return _MLBrand.BrandList;

        }

        public DBReturnData InsertBrand()
        {
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using(DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_CATEGORY";
                    _DBAccess.AddParameters("@Action", "INSERTCATEGORY");
                    _DataSet = _DBAccess.DBExecute();

                }
                if(_DataSet !=null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach(DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

                        }
                    }
                }

            }
            catch(Exception ex)
            {
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }



            return _DBReturnData;
        }

        public DBReturnData UpdateBrand()
        {
            MLBrand _MLBrand = new(); 
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_CATEGORY";
                    _DBAccess.AddParameters("@Action", "UPDATECATEGORY");
                    _DataSet = _DBAccess.DBExecute();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }



            return _DBReturnData;
        }



        public DBReturnData DeleteBrand()
        {
            DBReturnData _DBReturnData = new();

            DataSet _DataSet = new();
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "SP_CATEGORY";
                    _DBAccess.AddParameters("@Action", "DELETECATEGORY");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }



            return _DBReturnData;
        }
    }
}
