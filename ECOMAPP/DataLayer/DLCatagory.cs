using ECOMAPP.ModelLayer;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ECOMAPP.CommonRepository;

namespace ECOMAPP.DataLayer
{
    public class DLCatagory 
    {

        #region Commented section

        //public DataSet InsertProductCategory(MlInsertProductCategoryData rdata)
        //{
        //    string ProcedureName = "SP_CATEGORY";
        //    DataSet _dataSet = new DataSet();

        //    // Create the DataTable with the necessary columns
        //    DataTable dataTable = new DataTable();
        //    dataTable.Columns.Add("CATEGORY_ID", typeof(int)); // Required for UPDATE/DELETE operations
        //    dataTable.Columns.Add("Category_Name", typeof(string));
        //    dataTable.Columns.Add("Image", typeof(string));
        //    dataTable.Columns.Add("Priority", typeof(Int32));

        //    // If the CategoryList is not null, process it
        //    if (_mlCrudCategory.CategoryList != null)
        //    {
        //        foreach (var category in _mlCrudCategory.CategoryList)
        //        {
        //            DataRow row = dataTable.NewRow();
        //            row["Category_Name"] = category.Category_Name;
        //            row["Image"] = category.Image;
        //            row["Priority"] = category.Priority;

        //            // Add the row to the DataTable
        //            dataTable.Rows.Add(row);
        //        }

        //        using (DBAccess dbAccess = new DBAccess())
        //        {
        //            dbAccess.DBProcedureName = ProcedureName;

        //            // Add parameters for the mode and category list
        //            dbAccess.AddParameters("Mode", _mlCrudCategory.Mode);
        //            dbAccess.AddParameters("@CategoryList", dataTable);

        //            // Execute the stored procedure and retrieve the result set
        //            _dataSet = dbAccess.DBExecute();
        //        }
        //    }

        //    return _dataSet;
        //}

        #endregion

        public EcommerceCategoryDTO InsertProductCategory(MlInsertProductCategoryData data)
        {
            EcommerceCategoryDTO category = new EcommerceCategoryDTO();
            try
            {
                DataSet ds = new DataSet();
                using (DBAccess Db = new())
                {
                    Db.DBProcedureName = "SP_CATEGORY";
                    Db.AddParameters("@Action", "INSERTCATEGORY");
                    Db.AddParameters("@Category_Name",data.Category_Name??"");
                    Db.AddParameters("@Image",data.Image??"");
                    ds = Db.DBExecute();
                    Db.Dispose();
                }
                if(ds != null && ds.Tables.Count > 0)
                {
                    DataTable Dtable = ds.Tables[0];
                    foreach (DataRow dr in Dtable.Rows) 
                    {
                        if (dr["RETVAL"]?.ToString() == "SUCCESS")
                        {
                            category.Message = "SUCCESS";
                            category.Code = 200; 
                        }
                        else
                        {
                            category.Message = "failed to insert";
                            category.Code = 401;
                        }
                    }


                }
                else
                {
                    category.Message = "NOT EXISTS";
                    category.Code = 400;
                }



            }
            catch (Exception ex) 
            {
            
            }
            return category;


        }





    }
}
