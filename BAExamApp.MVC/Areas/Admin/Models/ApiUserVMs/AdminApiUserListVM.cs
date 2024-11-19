using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BAExamApp.MVC.Areas.Admin.Models.ApiUserVMs
{
    public class AdminApiUserListVM
    {
        public Guid Id { get; set; }

        [Display(Name = "First_Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last_Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Profile_Image")]
        public byte[]? NewImage { get; set; }
    }
}