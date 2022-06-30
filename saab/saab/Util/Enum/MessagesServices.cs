namespace saab.Util.Enum
{
    public enum MessageServices
    {
        MessageAlertGeneral
    }

    public static class MessageServicesExtensions
    {
        public static string GetString(this MessageServices me)
        {
            return me switch
            {
                MessageServices.MessageAlertGeneral => "Ahorros menores a esperados",
                _ => "No value given"
            };
        }
    }
}