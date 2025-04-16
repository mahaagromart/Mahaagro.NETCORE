namespace ECOMAPP.CommonRepository
{
    public class DBReturnData
    {
        public dynamic? Dataset { get; set; }
        public DBEnums.Status Status { get; set; }
        public string Message { get; set; }
        public DBEnums.Codes Code { get; set; }
        public string? Retval { get; set; }

        public string? OrderId { get; set; }

    }
}
