using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Utils
{
    public static class ValidationMessages
    {
        public static StringBuilder UserSignIn(LoginDTO loginDTOObj)
        {
            StringBuilder validationMessages = new StringBuilder();//here it will create only one object

            // Validate input fields
            //if the empname is empty or whitespace we are showing validation messages
            if (string.IsNullOrWhiteSpace(loginDTOObj.UserName) || string.IsNullOrEmpty(loginDTOObj.UserName))
            {
                validationMessages.AppendLine("UserName is required,please enter correct username.");//in that object we are appending the data
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(loginDTOObj.Password)) || string.IsNullOrEmpty(Convert.ToString(loginDTOObj.Password)))
            {
                validationMessages.AppendLine("Password is required,please enter correct password.");//in that object we are appending the data
            }
            return validationMessages;
        }
    }
}
