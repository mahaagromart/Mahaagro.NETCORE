using System.Data;
using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using static ECOMAPP.ModelLayer.MLTestimonial;

namespace ECOMAPP.DataLayer
{
    public class DLTestimonial
    {
        public List<MLTestimonial.Testimonial> GetAllTestimonial()
        {
            MLTestimonial _MLTestimonial = new();
            DALBASE _DALBASE = new();
            DBReturnData _DBReturnData = new();


                DataSet _DataSet = new();
            try
            {
                _MLTestimonial.TestimonialList = new List<MLTestimonial.Testimonial>();
                using (DBAccess _DBAccess = new DBAccess())
                {
                    _DBAccess.DBProcedureName = "SP_CATEGORY";
                    _DBAccess.AddParameters("@Action", "SELECTCATEGORY");
                    _DataSet = _DBAccess.DBExecute();
                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable = _DataSet.Tables[0];
                    string Retval = _DataSet.Tables[1].Rows[0]?.ToString() ?? "";
                    if (Retval == "SUCCESS")
                    {
                        foreach (DataRow Row in _DataTable.Rows)
                        {
                            var Testimonial = new MLTestimonial.Testimonial
                            {
                                //id = row["id"] == DBNull.Value ? 0 : Convert.ToInt32(row["id"]),
                                //Category_Name = row["Category_Name"]?.ToString() ?? string.Empty,
                                //Subcategory_Name = row["Subcategory_Name"]?.ToString() ?? string.Empty,
                                //CreationDate = row["CreationDate"]?.ToString() ?? string.Empty,
                                //Priority = row["priority"] == DBNull.Value ? 0 : Convert.ToInt32(row["priority"])
                            };
                            _MLTestimonial.TestimonialList.Add(Testimonial);


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

            }
            catch (Exception ex) 
            {
                _DALBASE.ErrorLog("GetAllTestimonial", "DLTestimonial", ex.Message);
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();

            }

            return _MLTestimonial.TestimonialList;
        }

        public DBReturnData InsertEcommerceTestimonial(MLInsertTestimonial Data)
        {
      
            DBReturnData _DBReturnData = new();
            DALBASE _DALBASE = new();

            DataSet _DataSet = new();
           
            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");

                    _DataSet = _DBAccess.DBExecute();

                }
                if (_DataSet != null && _DataSet.Tables.Count > 0)
                {
                    DataTable _DataTable=_DataSet.Tables[0];
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
              

            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("InsertEcommerceTestimonial", "DLTestimonial", ex.Message);
                _DBReturnData.Message =DBEnums.Status.FAILURE.ToString() + ex.Message.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_GATEWAY;

            }

            return _DBReturnData;

        }

        public DBReturnData UpdateEcommerceTestimonial(MLUpdateTestimonial Data)
        {
            DBReturnData _DBReturnData = new();
            DataSet _DataSet = new();
            DALBASE _DALBASE = new();

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");

                    _DataSet = _DBAccess.DBExecute();

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


            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("UpdateEcommerceTestimonial", "DLTestimonial", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() + ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;

        }


        

       public DBReturnData DeleteEcommerceTestimonial(MLDeleteTestimonial Data)
       {
        
            DBReturnData _DBReturnData = new();
            DALBASE _DALBASE = new();

            DataSet _DataSet = new();

            try
            {
                using (DBAccess _DBAccess = new())
                {
                    _DBAccess.DBProcedureName = "";
                    _DBAccess.AddParameters("@Action", "");

                    _DataSet = _DBAccess.DBExecute();

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


            }
            catch (Exception ex)
            {
                _DALBASE.ErrorLog("DeleteEcommerceTestimonial", "DLTestimonial", ex.Message);
                _DBReturnData.Message = DBEnums.Status.FAILURE.ToString() +ex.ToString();
                _DBReturnData.Code = DBEnums.Codes.BAD_REQUEST;

            }

            return _DBReturnData;

        }
    }
}
