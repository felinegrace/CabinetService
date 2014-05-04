﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cabinet.Utility;

namespace Cabinet.Framework.CommonEntity
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
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (System.Exception ex)
            {
                Logger.error("Jsonable: error: {0} | json: {1}", ex.Message, json);
                return default(T);
            }
            
        }
    }
}
