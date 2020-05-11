using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using MastiTimes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MastiTimes.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public BaseController()
        {
            //User u = await CurrentUser();
            //ViewBag.CurrentUser = u;
        }

        public string Get(string key)
        {
            string sObject = HttpContext.Session.GetString(key);
            return sObject;
        }
        public T Get<T>(string key)
        {
            //_context = context;
            if (HttpContext.Session.Keys.Contains(key))
            {
                string sObject = HttpContext.Session.GetString(key);
                return JsonConvert.DeserializeObject<T>(sObject);
            }
            else
            {
                return default(T);
            }
        }
        public void Set(string key, object obj)
        {
            String jsonString = JsonConvert.SerializeObject(obj);
            HttpContext.Session.SetString(key, jsonString);
        }

        internal User CurrentUser
        {
            get
            {
                User u = Get<User>("CurrentUser");
                if (u == null) u = new User()
                {
                    FirstName = "Anony",
                    //Role = new Role()
                    //{
                    //    Name = "Anonymous",
                    //}
                };
                return u;
            }
            set
            {
                Set("CurrentUser", value);
            }
        }
        
        //Same method as above. Created because we used loginmodel
        internal User LoggedInUser
        {
            get
            {
                User u = Get<User>("LoggedInUser");
                if (u == null) u = new User() { FirstName = "Anony" };
                return u;
            }
            set
            {
                Set("LoggedInUser", value);
            }
        }

        

    }
}