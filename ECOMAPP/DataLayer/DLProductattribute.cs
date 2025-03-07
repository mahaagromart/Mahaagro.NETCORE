using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using System.Data;
using static ECOMAPP.ModelLayer.MLProductattribute;

namespace ECOMAPP.DataLayer
{
    public class DLProductattribute
    {
        public List<MLProductattribute.ProductAttribute> GetAllAttribute()
        {
            MLProductattribute _MLProductattribute = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            DataSet _DataSet = new();
            try
            {
                _MLProductattribute.ProductAttributeList = new List<MLProductattribute.ProductAttribute>();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_ATTRIBUTE";
                    _DBAccess.AddParameters("@Action", "SELECTATTRIBUTE");
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]["RETVAL"]?.ToString() ?? "";
                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow Row in _DataTable.Rows)
                        {
                            var Productattribute = new MLProductattribute.ProductAttribute
                            {
                                id = Row["id"] == DBNull.Value ? 0 : Convert.ToInt32(Row["id"]),
                                Attribute_Name = Row["Attribute_Name"]?.ToString() ?? string.Empty,
                                CreationDate = Row["CreationDate"]?.ToString() ?? string.Empty,
                                UpdationDate = Row["UpdationDate"]?.ToString() ?? string.Empty,
                                IsDelete = Row["IsDelete"] == DBNull.Value ? 0 : Convert.ToInt32(Row["IsDelete"])
                            };
                            _MLProductattribute.ProductAttributeList.Add(Productattribute);


                        }


                    }
                    else
                    {
                        _DBReturnData.Code =DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();

                    }
                }
                else
                {
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllAttribute", "DLProductattribute", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();

            }

            return _MLProductattribute.ProductAttributeList;
        }
        public DBReturnData InsertAttribute(MLInsertProductAttribute Data)
        {
            MLProductattribute _MLProductattribute = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            DataSet _DataSet = new();
            try
            {
                _MLProductattribute.ProductAttributeList = new List<MLProductattribute.ProductAttribute>();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_ATTRIBUTE";
                    _DBAccess.AddParameters("@Action", "INSERTATTRIBUTE");
                    _DBAccess.AddParameters("@Attribute_Name", Data.Attribute_Name);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            _DBReturnData.Message = DBEnums.Status.SUCCESS.ToString();
                            _DBReturnData.Code = DBEnums.Codes.SUCCESS;
                        }
                        else
                        {
                            _DBReturnData.Message =DBEnums.Status.FAILURE.ToString();
                            _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                        }
                    }
                }
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertAttribute", "DLProductattribute", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }

        public DBReturnData UpdateAttribute(MLUpdateProductAttribute Data)
        {
            MLProductattribute _MLProductattribute = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            DataSet _DataSet = new();
            try
            {
                _MLProductattribute.ProductAttributeList = new List<MLProductattribute.ProductAttribute>();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_ATTRIBUTE";
                    _DBAccess.AddParameters("@Action", "UPDATEATTRIBUTE");
                    _DBAccess.AddParameters("@id", Data.id);
                    _DBAccess.AddParameters("@Attribute_Name", Data.Attribute_Name);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
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
        
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateAttribute", "DLProductattribute", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }


        public DBReturnData DeleteAttribute(MLDeleteProductAttribute Data)
        {
            MLProductattribute _MLProductattribute = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


            DataSet _DataSet = new();
            try
            {
                _MLProductattribute.ProductAttributeList = new List<MLProductattribute.ProductAttribute>();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_ATTRIBUTE";
                    _DBAccess.AddParameters("@Action", "DELETEATTRIBUTE");
                    _DBAccess.AddParameters("@id", Data.id);
                    _DataSet = _DBAccess.DBExecute();
                    _DBAccess.Dispose();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    foreach (DataRow Row in _DataTable.Rows)
                    {
                        if (Row["RETVAL"]?.ToString() == "SUCCESS")
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
                    
                
                else
                {
                    _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                    _DBReturnData.Code = DBEnums.Codes.INTERNAL_SERVER_ERROR;
                }

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("GetAllTestimonial", "DLProductattribute", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;
        }


    }
}
