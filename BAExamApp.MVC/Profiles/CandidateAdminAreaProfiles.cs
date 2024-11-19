using AutoMapper;
using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.MVC.Areas.CandidateAdmin.Models.CandidateAdminVMs;


namespace BAExamApp.MVC.Profiles;

public class CandidateAdminAreaProfiles : Profile
{
    public CandidateAdminAreaProfiles()
    {      
        //CandidateAdminController
        CreateMap<CandidateAdminCreateVM, CandidateAdminCreateDto>();
        CreateMap<CandidateAdminListDto, CandidateAdminListVM>();
        CreateMap<CandidateAdminDetailsDto, CandidateAdminDetailsVM>();
        CreateMap<CandidateAdminUpdateDto, CandidateAdminUpdateVM>();
        CreateMap<CandidateAdminUpdateVM, CandidateAdminUpdateDto>();
        CreateMap<CandidateAdminDto, CandidateAdminUpdateDto>();
        CreateMap<CandidateAdminUpdateVM, CandidateAdminDto>();
        CreateMap<CandidateAdminDto, CandidateAdminUpdateVM>();

    }
}