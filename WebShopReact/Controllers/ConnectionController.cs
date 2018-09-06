using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using WebShopReact.Models;
using WebShopReact.Managers;
using Microsoft.AspNetCore.Authorization;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : Controller
    {
        ConnectionManager _connectionManager;

        public ConnectionController(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        private string GetCurrent()
        {
            return User.Claims.Where(c => c.Type == "Database").FirstOrDefault().Value;
        }

        //GET: api/connection
        [HttpGet]
        [Authorize]
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
        [Authorize]
        public IActionResult SwitchConnection([FromBody] InputConnection input)
        {
            var token = _connectionManager.SwitchConnection(input.Connection);
            return Ok(new { token = token });
        }
    }
}