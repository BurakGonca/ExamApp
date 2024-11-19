using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Enums;
public enum ExamType
{
    [Display(Name = "Zahoot_Exam")]
    Zahoot = 1,
    [Display(Name = "Standard_Exam")]
    Standard = 2
}

