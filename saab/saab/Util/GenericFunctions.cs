using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace saab.Util
{
    public static class GenericFunctions
    {
        public static Dictionary<string, int> CountKeyValue(Dictionary<string, int> objectDictionary, string key)
        {
            if (objectDictionary.ContainsKey(key))
            {
                var total = objectDictionary[key]+1;
                objectDictionary[key] = total;
            }
            else  objectDictionary.Add(key,1);

            return objectDictionary;
        }
    }
}