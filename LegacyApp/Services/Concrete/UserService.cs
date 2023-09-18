using LegacyApp.DAL.Repositories.Abstract;
using LegacyApp.DAL.Repositories.Concrete;
using LegacyApp.Services.Abstract;
using System;
using System.Text.RegularExpressions;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly ClientRepository _clientRepository;
        private readonly UserCreditServiceClient _userCreditService;

        //We can implement DI here with the help of interfaces if they they didn't want changed Program.cs
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditServiceClient();
        }

        //We can implement DTO here if they they didn't want changed Program.cs
        public bool AddUser(string firstname, string surname, string email, DateTime birthDate, int clientId)
        {
            if (!IsValidFullname(firstname, surname)) return false;
            if (IsValidEmail(email)) return false;

            var age = GetAge(birthDate);
            if (!IsValidAge(age)) return false;

            var client = _clientRepository.GetById(clientId);

            //If we use DTO, we can implement here third-party mapper library or custom extension methods for mapping
            var user = new User
            {
                Client = client,
                BirthDate = birthDate,
                Email = email,
                Firstname = firstname,
                Surname = surname,
                HasCreditLimit = client.Status == ClientStatus.VeryImportant ? false : true
            };
          
            CalculateCreditLimitForUser(user);

            if (HasExceededCreditLimit(user)) return false;

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool IsValidFullname(string firstName, string surname)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname))
                return false;

            return true;
        }

        private bool IsValidEmail(string email)
        {
            const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = DateTime.Now.Year - birthDate.Year;

            if (birthDate.Date > currentDate.Date.AddYears(-age))
                age--;

            return age;
        }

        private bool IsValidAge(int age)
        {
            if (age < 21)
                return false;

            return true;
        }

        private void CalculateCreditLimitForUser(User user)
        {
            user.CreditLimit = user.Client.Status switch
            {
                ClientStatus.Standart => CalculateCreditLimitForStandartUser(user),
                ClientStatus.Important => CalculateCreditLimitForImportantUser(user),
                ClientStatus.VeryImportant => null,
                _ => throw new NotImplementedException(),
            };
        }

        private decimal CalculateCreditLimitForStandartUser(User user)
        {
            return _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.BirthDate);
        }

        private decimal CalculateCreditLimitForImportantUser(User user)
        {
            var creditLimit = _userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.BirthDate);
            return creditLimit * 2;
        }

        private bool HasExceededCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
                return true;

            return false;
        }
    }
}