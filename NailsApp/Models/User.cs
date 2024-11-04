using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NailsApp.Models
{
    public class User
    {
        public int UserId { get; set; }


        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }


        public string Email { get; set; } = null!;


        public string PhoneNumber { get; set; } = null!;


        public string UserAddress { get; set; } = null!;

        public char? Gender { get; set; } = null!;


        public string Pass { get; set; } = null!;

        public bool IsManicurist { get; set; }

        public bool IsBlocked { get; set; }


        public string? ProfilePic { get; set; }

        public bool IsManager { get; set; }
    }
}
