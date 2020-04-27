using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConAppUploadDocumentO365
{
    class Login
    {
        public static SecureString GetPassword()
        {
            string password = "passwordHere";
            SecureString securePassword = new SecureString();
            
            foreach(char c in password)
            {
                securePassword.AppendChar(c);
            }
            return securePassword;
        }
    }
}
