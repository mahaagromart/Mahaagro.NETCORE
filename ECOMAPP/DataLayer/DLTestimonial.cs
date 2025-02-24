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
                        _DBReturnData.Code = 400;
                        _DBReturnData.Message = "FAILED";

                    }
                }
                else
                {
                    _DBReturnData.Code = 404;
                    _DBReturnData.Message = "No Testimonial found.";
                }

            }
            catch (Exception ex) 
            {
                _DALBASE.ErrorLog("GetAllTestimonial", "DLTestimonial", ex.Message);
                _DBReturnData.Code = 500;
                _DBReturnData.Message = "Internal Server Error";

            }

            return _MLTestimonial.TestimonialList;
        }

        public MLTestimonial InsertEcommerceTestimonial(MLInsertTestimonial Data)
        {
            MLTestimonial _MLTestimonial = new();
            DLTestimonial _DLTestimonial = new();
            DBReturnData _DBReturnData = new();

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

            return  _MLTestimonial ;

        }

        public MLTestimonial UpdateEcommerceTestimonial(MLUpdateTestimonial Data)
        {
            MLTestimonial _MLTestimonial = new();
            DLTestimonial _DLTestimonial = new();
            DBReturnData _DBReturnData = new();

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

            return _MLTestimonial;

        }


        

       public MLTestimonial DeleteEcommerceTestimonial(MLDeleteTestimonial Data)
        {
            MLTestimonial _MLTestimonial = new();
            DLTestimonial _DLTestimonial = new();
            DBReturnData _DBReturnData = new();

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

            return _MLTestimonial;

        }
    }
}
