using Newtonsoft.Json;
using PruebaTeleperformanceApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTeleperformanceApi.Helpers
{
    public static class SaveCompanyHelper
    {
        private static readonly string filePath = @"Resources/Companies.json";
        public static List<Company> Companies { get; set; }

        public static async Task GetCompanies()
        {
            string jsonFile = File.ReadAllText(filePath);
            Companies = JsonConvert.DeserializeObject<List<Company>>(jsonFile);
        }

        public static async Task SaveProject(Company newCompany)
        {
            Company company;
            if (Companies.Exists(x => x.IdentificationNumber == newCompany.IdentificationNumber))
            {
                company = Companies.Find(x => x.IdentificationNumber == newCompany.IdentificationNumber);
                company.IdentificationType = newCompany.IdentificationType;
                company.CompanyName = newCompany.CompanyName;
                company.FirstName = newCompany.FirstName;
                company.SecondName = newCompany.SecondName;
                company.FirstLastName = newCompany.FirstLastName;
                company.SecondLastName = newCompany.SecondLastName;
                company.Email = newCompany.Email;
                company.AuthrorizaMessageToCellPhone = newCompany.AuthrorizaMessageToCellPhone;
                company.AuthrorizaMessageToEmail = newCompany.AuthrorizaMessageToEmail;
            }
            else
            {
                Companies.Add(newCompany);
            }

            await using (StreamWriter file = File.CreateText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, Companies);
            }
        }
    }
}
