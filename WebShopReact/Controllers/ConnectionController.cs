using Microsoft.AspNetCore.Mvc;
using WebShop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : Controller
    {
        // GET: api/connection/getCurrent
        [HttpGet("/[action]")]
        public IActionResult GetCurrent()
        {
            var current = HttpContext.Session.GetString("connection");
            if (current == null)
            {
                current = ConnectionTypes.SqlServer.ToString();
                HttpContext.Session.SetString("connection", current);
            }
            return Ok(current);
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
            return connections_strings;

        }

        [HttpPost]
        public IActionResult SwitchConnection(ConnectionTypes connection)
        {
            HttpContext.Session.SetString("connection", connection.ToString());
            ViewData["current"] = connection.ToString();
            ViewData["success"] = true;
            return View();
        }
    }
}