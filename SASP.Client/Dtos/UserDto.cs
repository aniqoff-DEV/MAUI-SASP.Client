using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASP.Client.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string NumberPhone { get; set; }

        public string Email { get; set; }
    }
}
