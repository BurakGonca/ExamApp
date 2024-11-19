using BAExamApp.Dtos.Students;
using Microsoft.AspNetCore.Http;

namespace BAExamApp.Business.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.OriginalImage, opt => opt.MapFrom(src => src.NewImage));
        CreateMap<StudentDto, Student>()
            .ForMember(dest => dest.StudentClassrooms, opt => opt.MapFrom(src =>
                src.ClassroomIds.Select(classroomId => new StudentClassroom
                {
                    ClassroomId = classroomId
                }).ToList()))

            .ReverseMap()
            .ForMember(dest => dest.ClassroomIds, opt => opt.MapFrom(src =>
                src.StudentClassrooms.Select(sc => sc.ClassroomId).ToList()));
        CreateMap<Student, StudentListDto>();
        CreateMap<Student, StudentDetailsDto>(); /*.ForMember(dest => dest.CityName, config => config.MapFrom(src => src.City.Name));*/
        CreateMap<Student, StudentDetailsForTrainerDto>()
            .ForMember(dest => dest.StudentExams, config => config.MapFrom(src => src.StudentExams.Where(x => x.IsFinished)))
            .ForMember(dest => dest.StudentName, config => config.MapFrom(src => src.FullName));
        CreateMap<StudentCreateDto, Student>()
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => src.NewImage != null ? ConvertIFormFileToByteArray(src.NewImage) : null))
            .ForMember(dest => dest.StudentClassrooms, opt => opt.MapFrom(src =>
                src.ClassroomIds.Select(classroomId => new StudentClassroom
                {
                    ClassroomId = classroomId
                }).ToList())) 

            .ReverseMap()
            .ForMember(dest => dest.ClassroomIds, opt => opt.MapFrom(src =>
                src.StudentClassrooms.Select(sc => sc.ClassroomId).ToList())); ;
        CreateMap<StudentUpdateDto, Student>()
            .ForMember(dest => dest.NewImage, opt => opt.Condition(src => src.RemoveImage == true || (src.NewImage != null && src.RemoveImage == false)))
            .ForMember(dest => dest.NewImage, opt => opt.MapFrom(src => ConvertIFormFileToByteArray(src.NewImage)))
            .ForMember(dest => dest.StudentClassrooms, opt => opt.MapFrom(src =>
                src.ClassroomIds.Select(classroomId => new StudentClassroom
                {
                    ClassroomId = classroomId
                }).ToList())) 

            .ReverseMap()
            .ForMember(dest => dest.ClassroomIds, opt => opt.MapFrom(src =>
                src.StudentClassrooms.Select(sc => sc.ClassroomId).ToList()));

    }

    /// <summary>
    /// Verilen IFormFile dosyasını byte dizisine dönüştürür.
    /// </summary>
    /// <param name="formFile">Dönüştürülecek IFormFile dosyası.</param>
    /// <returns>Byte dizisi olarak dönüştürülmüş dosya. Eğer dosya null veya boş ise null döner.</returns>
    private byte[] ConvertIFormFileToByteArray(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            formFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
