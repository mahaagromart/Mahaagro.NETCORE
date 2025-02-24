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
                        _DBReturnData.Code = 400;
                        _DBReturnData.Message = "FAILED";


                    }


                }
                else
                {
                    _DBReturnData.Code = 404;
                    _DBReturnData.Message = "No Testimonial found.";
                }
            } catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllBrand", "DLBrand", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";
            }
            return _MLBrand.BrandList;

        }

        public MLBrand InsertBrand()
        {
            MLBrand _MLBrand = new();
            DALBASE _DALBASE = new();
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
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;
                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;

                        }
                    }
                }

            }
            catch(Exception ex)
            {
                _DBReturnData.Message = "NOT EXISTS";
                _DBReturnData.Code = 400;

            }



            return _MLBrand;
        }

        public MLBrand UpdateBrand()
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
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;
                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DBReturnData.Message = "NOT EXISTS";
                _DBReturnData.Code = 400;

            }



            return _MLBrand;
        }



        public MLBrand DeleteBrand()
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
                    _DBAccess.AddParameters("@Action", "DELETECATEGORY");
                    _DataSet = _DBAccess.DBExecute();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow _DataRow in _DataTable.Rows)
                    {
                        if (_DataRow["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = "SUCCESS";
                            _DBReturnData.Code = 200;
                        }
                        else
                        {
                            _DBReturnData.Message = "FAILED";
                            _DBReturnData.Code = 401;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _DBReturnData.Message = "NOT EXISTS";
                _DBReturnData.Code = 400;

            }



            return _MLBrand;
        }
    }
}
