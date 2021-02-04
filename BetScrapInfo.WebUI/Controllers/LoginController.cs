using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using BetScrapInfo.WebUI.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetScrapInfo.WebUI.Models;
using Entities.Concrete;

namespace BetScrapInfo.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private IUserService _userService;

      
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult signIn(SignInViewModel model)
        {

            var data=_userService.GetUser(model.Username, model.Password);
            if (data==null)
            {
                return Ok(false);
            }
            else
            {
                HttpContext.Session.SetObject("username", data.Username);
                HttpContext.Session.SetObject("id", data.Id);

                var us = HttpContext.Session.GetObject<String>("username");

                return Ok(data.Username);
            }

        }
    }
}


