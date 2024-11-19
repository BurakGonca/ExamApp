using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Candidate.CandidateExams
{
	public class CandidateExamQuestionsByCandidateListDto
	{
        public Guid Id { get; set; }
        public string?  Content { get; set; }
        public byte[]? Image { get; set; }
        public int? Order { get; set; }
        public int MaxScore { get; set; }
        public int? Score { get; set; }
        public CandidateQuestionType CandidateQuestionType { get; set; }


        public List<CandidateExamQuestionsByCandidateAnswersListDto> Answers { get; set; }
    }
}
