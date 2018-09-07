using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : Controller
    {
        IConnectionManager _connectionManager;

        public ConnectionController(IConnectionManager connectionManager)
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