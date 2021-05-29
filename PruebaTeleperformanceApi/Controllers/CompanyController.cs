using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PruebaTeleperformanceApi.Helpers;
using PruebaTeleperformanceApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTeleperformanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController: ControllerBase
    {
        [HttpGet("Home")]
        public  IActionResult Home()
        {
            var dict= new Dictionary<string, object>();
            dict.Add("Message", "Bienvenido a Company ApiRest");
            dict.Add("Companies", SaveCompanyHelper.Companies);
            return Ok(dict);
        }

        [HttpGet("CanRegister/{id}")]
        public IActionResult CanRegister(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if(id== 900674336||id== 811033098)
            {
                dict.Add("Message", "Puede hacer registro de la compañia");
                dict.Add("CanRegister", true);
                return Ok(dict);
            }
            else
            {
                dict.Add("Message", "No puede hacer registro de la compañia");
                dict.Add("CanRegister", false);
                return BadRequest(dict);
            }
        }

        [HttpPost("RegisterCompany/{id}")]
        public async Task<IActionResult>RegisterCompany([FromBody] JObject body,int id)
        {
            try
            {
                int identificationNumber = id;
                string identificationType = body["identificationType"].Value<string>();
                string companyName = body["companyName"].Value<string>();
                string firstName = body["firstName"].Value<string>();
                string firstLastName = body["firstLastName"].Value<string>();
                string email = body["email"].Value<string>();

                Company newCompany = new Company(identificationType, identificationNumber, companyName, firstName, firstLastName, email);                
                await SaveCompanyHelper.SaveProject(newCompany);

                return Ok(newCompany);
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, object>();
                dict.Add("Message",ex);
                return BadRequest(dict);
            }

        }
    }
}
