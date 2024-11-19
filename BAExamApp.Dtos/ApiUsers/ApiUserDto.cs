using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiUsers
{
    public class ApiUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string? Image { get; set; }
        public byte[]? OriginalImage { get; set; }
    }
}