namespace ECOMAPP.CommonRepository
{
    public class DBReturnData
    {
        public dynamic Dataset { get; set; }
        public DBEnums.Status Status { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
        public string Retval { get; set; }



    }
}