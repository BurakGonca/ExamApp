using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.Entities.DbSets.Candidates;


namespace BAExamApp.Business.Profiles;
public class CandidateAdminProfile:Profile
{
    public CandidateAdminProfile()
    {
        CreateMap<CandidateCandidateAdmin, CandidateAdminDto>();
        CreateMap<CandidateCandidateAdmin, CandidateAdminListDto>();
        CreateMap<CandidateAdminCreateDto, CandidateCandidateAdmin>();
        CreateMap<CandidateAdminUpdateDto, CandidateCandidateAdmin>();
        CreateMap<CandidateCandidateAdmin, CandidateAdminCreateDto>();
        CreateMap<CandidateCandidateAdmin, CandidateAdminDetailsDto>();
        CreateMap<CandidateCandidateAdmin, CandidateAdminUpdateDto>();
        CreateMap<CandidateAdminDto, CandidateAdminUpdateDto>();






    }
}
