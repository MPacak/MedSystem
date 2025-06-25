using BL.AuthUtils;
using BL.IServices;
using DAL.IRepositories;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DoctorAuthService : IDoctorAuthService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IConfiguration _config;

        public DoctorAuthService(IUnitofWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public void CompleteSignup(string email, string signupToken, string newPassword)
        {
            if (!ValidateSignupToken(email, signupToken))
                throw new InvalidOperationException("Invalid or expired signup token.");
            var doctor = _unitOfWork.GetRepository<Doctor>()
                    .FindOne(d => d.Email == email)!;

            var salt = GenerateSalt();
            var hash = ComputeHash(newPassword, salt);

            doctor.PwdSalt = salt;
            doctor.PwdHash = hash;
            doctor.SecurityToken = null; 
            _unitOfWork.Save();
        }

        private static string GenerateSalt() =>
           PasswordHashProvider.GetSalt();

        private static string ComputeHash(string password, string salt) =>
            PasswordHashProvider.GetHash(password, salt);
        public bool ValidateSignupToken(string email, string signupToken)
        {
            var repo = _unitOfWork.GetRepository<Doctor>();
            var dr = repo.FindOne(d =>
                d.Email == email &&
                d.SecurityToken == signupToken &&
                d.PwdHash == null   
            );
            return dr != null;
        }

        public string Login(string email, string password)
        {
            var repo = _unitOfWork.GetRepository<Doctor>();
           

            var doctor = repo.FindOne(d => d.Email == email)
                         ?? throw new InvalidOperationException("Invalid email ");
            if (doctor.PwdHash == null || doctor.PwdSalt == null)
                throw new InvalidOperationException(
                    "Your account is not activated. Please complete signup first.");
            var computed = ComputeHash(password, doctor.PwdSalt);
            if (computed != doctor.PwdHash)
                throw new InvalidOperationException("Invalid password");

            var jwt = TokenProvider.CreateToken(
                secureKey: _config["Jwt:Key"],
                expiration: 60 * 24 * 7,           
                subject: doctor.Email,
                additionalClaims: new[] { new Claim("doctorId", doctor.Id.ToString()) }
            );

            return jwt;
        }

    }
}
