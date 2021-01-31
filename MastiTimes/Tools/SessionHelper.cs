using MastiTimes.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Tools
{
    public static class SessionHelper
    {
        private static HttpContext _context;
        public static object Get(HttpContext context, string key)
        {
            _context = context;
            string sObject = context.Session.GetString(key);
            return JsonConvert.DeserializeObject(sObject);
        }
        public static void Set(HttpContext context, string key, object obj)
        {
            //String jsonString = Microsoft.AspNetCore.Mvc.Formatters.Json;
            //String jsonString = System.Runtime.Serialization.Json;
            //String jsonString = Microsoft.Extensions.Configuration.Json ;
            String jsonString = JsonConvert.SerializeObject(obj);
            context.Session.SetString(key, jsonString);
        }

        internal static bool UserLoggedIn
        {
            get
            {
                return Get(_context, "CurrentUser") != null;
            }

        }

        internal static User CurrentUser
        {
            get
            {
                return (User)Get(_context, "CurrentUser");
            }
            set
            {
                Set(_context, "CurrentUser", value);
            }

        }

    }
}

