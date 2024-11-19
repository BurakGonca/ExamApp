using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAExamApp.MVC.Areas.Admin.Models.TrainerVMs
{
    public class AdminTrainerCreateVM
    {
        [Display(Name = "First_Name")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string FirstName { get; set; }

        [Display(Name = "Last_Name")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        public bool Gender { get; set; }

        [Display(Name = "Profile_Image")]
        public IFormFile? NewImage { get; set; }

        //[Display(Name = "TechnicalUnit_Name")]
        //[Required(ErrorMessage = "{0} alanı boş bırakılamaz.")]
        //public Guid? TechnicalUnitId { get; set; }
        public SelectList? TechnicalUnitList { get; set; }

        public List<Guid> ProductIds { get; set; }
        public SelectList? ProductList { get; set; }

        [Display(Name = "OtherEmails")]
        public List<string>? OtherEmails { get; set; }

        //[Display(Name = "Talent")]
       // public List<Guid>? TrainerTalentIds { get; set; }

       // [Display(Name = "Talent")]
       // public Guid? TalentId { get; set; } // Yetenek alanı null kabul edilebilir.
       // public SelectList? TalentList { get; set; }
    }
}
