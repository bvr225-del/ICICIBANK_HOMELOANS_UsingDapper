using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Dtos
{
    public class UserRolesInformationResponse
    {
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }

    }
}
