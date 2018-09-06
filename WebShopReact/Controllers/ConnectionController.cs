using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using WebShopReact.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : Controller
    {
        private string GetCurrent()
        {
            var current = HttpContext.Session.GetString("connection");
            if (current == null)
            {
                current = ConnectionTypes.SqlServer.ToString();
                HttpContext.Session.SetString("connection", current);
            }
            return current;
        }

        //GET: api/connection
        [HttpGet]
        public IEnumerable Index()
        {            
            var connections = (ConnectionTypes[])Enum.GetValues(typeof(ConnectionTypes));
            var connections_strings = new List<String>();
            foreach (var con in connections)
            {
                connections_strings.Add(con.ToString());
            }
            var response = new Dictionary<String, List<String>>
            {
                { "current" , new List<String>{GetCurrent()} },
                { "connection", connections_strings}

            };
            return response;

        }

        [HttpPost]
        public IActionResult SwitchConnection([FromBody] InputConnection input)
        {
            HttpContext.Session.SetString("connection", input.Connection.ToString());
            return Ok();
        }
    }
}