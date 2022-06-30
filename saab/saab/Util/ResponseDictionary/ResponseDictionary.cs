using Newtonsoft.Json;
using saab.Util.Enum;


namespace saab.Util.ResponseDictionary
{
    public class ResponseDictionary
    {
        public static string GetDictionaryError400(MessagesRequest description=default, string error="")
        {
            var resultDictionary = new
            {
                code = 10400,
                mensaje = MessagesRequest.Error.GetString(),
                descripcion = error.Length > 0 ? error :description.GetString()
            };
            return JsonConvert.SerializeObject(resultDictionary);
        }
        
        public string GetDictionaryError401(string description)
        {
            var resultDictionary = new
            {
                code = 10400,
                mensaje = "Something bad happened :(",
                descripcion = description
            };
            return JsonConvert.SerializeObject(resultDictionary);
        }
        
        public static string GetDictionaryError204()
        {
            var resultDictionary = new
            {
                code = 10204,
                mensaje = MessagesRequest.ErrorNoData.GetString(),
                descripcion = MessagesRequest.ErrorNoDataMessage.GetString()
            };
            return JsonConvert.SerializeObject(resultDictionary);
        }

        public static string GetDictionaryTotals(decimal totalValue, string descriptionProyecto)
        {
            var resultDictionary = new
            {
                unidad = descriptionProyecto,
                total = totalValue
            };
            return JsonConvert.SerializeObject(resultDictionary);
        }
    }
}