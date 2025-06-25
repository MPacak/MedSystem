using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IDoctorAuthService
    {
        bool ValidateSignupToken(string email, string signupToken);
        void CompleteSignup(string email, string signupToken, string newPassword);
        string Login(string email, string password);
    }
}
