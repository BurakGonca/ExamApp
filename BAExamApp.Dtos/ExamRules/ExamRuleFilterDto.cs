using BAExamApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ExamRules;
public class ExamRuleFilterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
}
