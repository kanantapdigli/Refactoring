using System;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public bool HasCreditLimit { get; set; }
        public decimal? CreditLimit { get; set; }
    }
}