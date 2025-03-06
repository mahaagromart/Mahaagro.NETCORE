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
            Success = 200,
            Created = 201,
            Accepted = 202,
            NoContent = 204,
            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            Conflict = 409,
            InternalServerError = 500,
            NotImplemented = 501,
            BadGateway = 502,
            ServiceUnavailable = 503
        }
        //public enum Code { s, d }


    
    }
}
