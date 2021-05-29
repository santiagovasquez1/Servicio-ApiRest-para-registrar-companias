using System;

namespace PruebaTeleperformanceApi.Models
{
    public class Company
    {
        public string IdentificationType { get; set; }
        public int IdentificationNumber { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string FirstLastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public Boolean AuthrorizaMessageToCellPhone { get; set; }
        public Boolean AuthrorizaMessageToEmail { get; set; }
        public Company(string identificationType, int IdentificationNumber, string companyName, string firstName, string lastName, string email)
        {
            this.IdentificationType = identificationType;
            this.IdentificationNumber = IdentificationNumber;
            this.CompanyName = companyName;
            this.FirstName = firstName;
            this.FirstLastName = lastName;
            this.Email = email;
        }
    }
}
