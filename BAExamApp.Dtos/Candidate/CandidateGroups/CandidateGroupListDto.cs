﻿using BAExamApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateGroups;
public class CandidateGroupListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CandidateBranchId { get; set; }
    public string BranchName { get; set; }
    public Status Status { get; set; }

}