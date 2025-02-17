using ECOMAPP.CommonRepository;
using ECOMAPP.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ECOMAPP.DataLayer
{
    public class DLProduct
    {
        public DataSet DBPostProduct([FromBody] MLCrudProduct _mlCrudProduct)
        {
            string ProcedureName = "Proc_Post_Product";
            DataSet dsProduct = new DataSet();


            #region You can pass multiple records at a time in database as a table
            DataTable dtProduct = new DataTable();
            dtProduct.Columns.Add("ProductID", typeof(int));
            dtProduct.Columns.Add("ProductName", typeof(string));
            dtProduct.Columns.Add("Category", typeof(string));
            dtProduct.Columns.Add("Price", typeof(decimal));

            if (_mlCrudProduct.ProductList != null)
            {
                foreach (var item in _mlCrudProduct.ProductList)
                {
                    DataRow row = dtProduct.NewRow();
                    row["ProductID"] = item.ProductID;
                    row["ProductName"] = item.ProductName;
                    row["Category"] = item.Category;
                    row["Price"] = item.Price ?? 0;

                    dtProduct.Rows.Add(row);
                    
                }
            }
            #endregion

            using (DBAccess Db = new DBAccess())
            {
                Db.DBProcedureName = ProcedureName;
                Db.AddParameters("@Mode", _mlCrudProduct.Mode.ToString());
                Db.AddParameters("@ProductList", dtProduct);
                dsProduct = Db.DBExecute();
                Db.Dispose();
            }
            return dsProduct;
        }

        public List<MLProduct> DBGetProduct([FromBody] MLFilterProduct _mlFilterProduct)
        {
            string ProcedureName = "Proc_Get_Products";
            DataSet dsProduct = new DataSet();
            List<MLProduct> _objProductList = new List<MLProduct>();


            using (DBAccess Db = new DBAccess())
            {
                Db.DBProcedureName = ProcedureName;
                Db.AddParameters("@PageNumber", _mlFilterProduct.PageNumber);
                Db.AddParameters("@PageSize", _mlFilterProduct.PageSize);
                dsProduct = Db.DBExecute();
                Db.Dispose();
            }
            foreach (DataRow item in dsProduct.Tables[0].Rows)
            {
                _objProductList.Add(new MLProduct
                {
                    ProductID = Convert.ToInt32(item["ProductID"]),
                    ProductName = item["ProductName"].ToString(),
                    Category = item["Category"].ToString(),
                    Price = Convert.ToDecimal(item["Price"]),
                });
            }
            return _objProductList;
        }
        public List<MLProduct> DBUpdateProduct([FromBody] MLFilterProduct _mlFilterProduct)
        {
            string ProcedureName = "Proc_Get_Products";
            DataSet dsProduct = new DataSet();
            List<MLProduct> _objProductList = new List<MLProduct>();


            using (DBAccess Db = new DBAccess())
            {
                Db.DBProcedureName = ProcedureName;
                Db.AddParameters("@PageNumber", _mlFilterProduct.PageNumber);
                Db.AddParameters("@PageSize", _mlFilterProduct.PageSize);
                dsProduct = Db.DBExecute();
                Db.Dispose();
            }
            foreach (DataRow item in dsProduct.Tables[0].Rows)
            {
                _objProductList.Add(new MLProduct
                {
                    ProductID = Convert.ToInt32(item["ProductID"]),
                    ProductName = item["ProductName"].ToString(),
                    Category = item["Category"].ToString(),
                    Price = Convert.ToDecimal(item["Price"]),
                });
            }
            return _objProductList;
        }
    }
}
