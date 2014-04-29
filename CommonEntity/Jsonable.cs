using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cabinet.CommonEntity
{
    public class Jsonable
    {
        protected Jsonable()
        {

        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static T fromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
