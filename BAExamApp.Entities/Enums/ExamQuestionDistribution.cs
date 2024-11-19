using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Entities.Enums;
public enum ExamQuestionDistribution
{
    [Display(Name = "Same_Questions")]
    SameQuestions = 1,
    [Display(Name = "Different_Questions")]
    DifferentQuestions = 2
}
