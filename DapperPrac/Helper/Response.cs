namespace DapperPrac.Helper
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = null!;
        public object? Result { get; set; }

        public static Response ReturnResponse(bool isSucess, string msg, object? obj = null)
        {
            return new Response
            {
                IsSuccess = isSucess,
                Message = msg,
                Result = obj
            };
        }
    }

    public static class ResponseMessages
    {
        public static string AddedSuccessfully = "{0} Added successfully";
        public static string DeletedSuccessfully = "{0} with Id : {1}  Deleted successfully";
        public static string FoundSuccessfully = "{0} Found successfully";
        public static string UpdatedSuccessfully = "{0} Updated with Id : {1} successfully";
        public static string ModelNotFound = "{0} for Id : {1} Not Found";
    }

}
