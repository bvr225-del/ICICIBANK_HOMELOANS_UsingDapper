using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantLocation { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
