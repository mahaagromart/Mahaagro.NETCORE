namespace ECOMAPP.CommonRepository
{
    public class DBEnums
    {
        public enum Status { SUCCESS, FAILURE }
        public enum Modes
        {
            ADD = 0, UPDATE = 1, DELETE = 2, UNDELETE = 3, SEARCH = 4, VIEW = 5, COPY = 6
        }

         public enum Codes
        {
            SUCCESS = 200,
            CREATED = 201,
            ACCEPTED = 202,
            NO_CONTENT = 204,
            BAD_REQUEST = 400,
            UNAUTHORIZED = 401,
            FORBIDDEN = 403,
            NOT_FOUND = 404,
            CONFLICT = 409,
            INTERNAL_SERVER_ERROR = 500,
            NOT_IMPLEMENTED = 501,
            BAD_GATEWAY = 502,
            SERVICE_UNAVAILABLE = 503,
            ORDER_NOT_CREATED = 1001,
            ORDER_CREATED = 1002,
            ORDER_NOT_VERIFIED = 1003,
            ORDER_VERIFIED = 1004,
        }
        //public enum Code { s, d }



    }
}