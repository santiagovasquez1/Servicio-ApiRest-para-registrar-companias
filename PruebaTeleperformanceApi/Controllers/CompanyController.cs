using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PruebaTeleperformanceApi.Helpers;
using PruebaTeleperformanceApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTeleperformanceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        [HttpGet("Home")]
        public IActionResult Home()
        {
            var dict = new Dictionary<string, object>();
            dict.Add("Message", "Bienvenido a Company ApiRest");
            dict.Add("Companies", SaveCompanyHelper.Companies);
            return Ok(dict);
        }

        [HttpGet("CanRegister/{id}")]
        public IActionResult CanRegister(int id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (id == 900674336 || id == 811033098)
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

        [HttpGet("GetCompany/{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            Company tempCompany = SaveCompanyHelper.Companies.FirstOrDefault(x => x.IdentificationNumber == id);
            if (tempCompany != null)
            {
                return Ok(tempCompany);
            }
            else
            {
                var dict = new Dictionary<string, object>();
                dict.Add("Message", "La compañia no se encuentra registrada");
                return BadRequest(dict);
            }
        }

        [HttpPost("RegisterCompany/{id}")]
        public async Task<IActionResult> RegisterCompany([FromBody] JObject body, int id)
        {
            try
            {
                int identificationNumber = id;
                string identificationType = body["identificationType"].Value<string>();
                string email = body["email"].Value<string>();

                Company newCompany = new Company(identificationType, identificationNumber, email);
                newCompany.CompanyName = body["companyName"].Value<string>();
                newCompany.FirstName= body["firstName"].Value<string>();
                newCompany.FirstLastName = body["firstLastName"].Value<string>();
                newCompany.SecondName = body["secondName"].Value<string>();
                newCompany.SecondLastName = body["secondLastName"].Value<string>();
                newCompany.AuthrorizaMessageToCellPhone = body["authrorizaMessageToCellPhone"].Value<Boolean>();
                newCompany.AuthrorizaMessageToEmail = body["authrorizaMessageToEmail"].Value<Boolean>();

                await SaveCompanyHelper.SaveProject(newCompany);

                return Ok(newCompany);
            }
            catch (Exception ex)
            {
                var dict = new Dictionary<string, object>();
                dict.Add("Message", ex);
                return BadRequest(dict);
            }

        }
    }
}
