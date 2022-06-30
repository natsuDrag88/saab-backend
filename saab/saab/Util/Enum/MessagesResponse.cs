namespace saab.Util.Enum
{
    public enum MessagesRequest
    {
        Error,
        ErrorPeriod,
        ErrorCredentials,
        ErrorUser,
        ErrorTypeUser,
        ErrorTokenRequired,
        ErrorTokenValid,
        ErrorNoData,
        ErrorNoDataMessage
    }
    
    public static class MessagesRequestExtensions
    {
        public static string GetString(this MessagesRequest me)
        {
            return me switch
            {
                MessagesRequest.Error => "Something bad happened :(",
                MessagesRequest.ErrorPeriod => "The format of the period is not as expected.",
                MessagesRequest.ErrorCredentials => "The credentials entered are incorrect.",
                MessagesRequest.ErrorUser => "Invalid user.",
                MessagesRequest.ErrorTypeUser => "The user type is not allowed access to the application.",
                MessagesRequest.ErrorTokenRequired => "The token is required.",
                MessagesRequest.ErrorTokenValid => "The token provided is not valid.",
                MessagesRequest.ErrorNoData => "No data.",
                MessagesRequest.ErrorNoDataMessage => "No information found to display.",
                _ => "No value given"
            };
        }
    }
}