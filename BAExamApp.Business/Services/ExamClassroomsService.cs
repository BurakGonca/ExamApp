using BAExamApp.Dtos.ExamClassrooms;
using BAExamApp.Entities.DbSets;
using System;


namespace BAExamApp.Business.Services;
public class ExamClassroomsService : IExamClassroomsService
{
    private readonly IExamClassroomsRepository _examClassroomsRepository;
    private readonly IMapper _mapper;

    public ExamClassroomsService(IExamClassroomsRepository examClassroomsRepository, IMapper mapper)
    {
        _examClassroomsRepository = examClassroomsRepository;
        _mapper = mapper;
    }

    public async Task<IDataResult<ExamClassroomsDto>> AddAsync(ExamClassroomsAddDto examClassroomsAddDto)
    {
        var examClassroom = _mapper.Map<ExamClassrooms>(examClassroomsAddDto);
        await _examClassroomsRepository.AddAsync(examClassroom);
        await _examClassroomsRepository.SaveChangesAsync();

        var dto = _mapper.Map<ExamClassroomsDto>(examClassroom);
        return new SuccessDataResult<ExamClassroomsDto>(dto, Messages.AddSuccess);
    }

    public async Task<IDataResult<ExamClassroomsDto>> DeleteAsync(Guid id)
    {
        var examClassroom = await _examClassroomsRepository.GetByIdAsync(id);
        if (examClassroom == null)
        {
            return new ErrorDataResult<ExamClassroomsDto>(Messages.ExamClassroomNotFound);
        }

        await _examClassroomsRepository.DeleteAsync(examClassroom);
        await _examClassroomsRepository.SaveChangesAsync();

        return new SuccessDataResult<ExamClassroomsDto>(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<ExamClassroomsListDto>>> GetAllAsync()
    {
        var examClassrooms = await _examClassroomsRepository.GetAllAsync();
        var dtoList = _mapper.Map<List<ExamClassroomsListDto>>(examClassrooms);

        return new SuccessDataResult<List<ExamClassroomsListDto>>(dtoList, Messages.ExamClassroomsRetrievedSuccessfully);
    }

    public async Task<IDataResult<ExamClassroomsDto>> GetByIdAsync(Guid id)
    {
        var examClassroom = await _examClassroomsRepository.GetByIdAsync(id);
        if (examClassroom == null)
        {
            return new ErrorDataResult<ExamClassroomsDto>(Messages.ExamClassroomNotFound);
        }

        var dto = _mapper.Map<ExamClassroomsDto>(examClassroom);
        return new SuccessDataResult<ExamClassroomsDto>(dto, Messages.ExamClassroomRetrievedSuccessfully);
    }

    public async Task<IDataResult<ExamClassroomsDto>> UpdateAsync(ExamClassroomsUpdateDto examClassroomsUpdateDto)
    {
        var examClassroom = await _examClassroomsRepository.GetByIdAsync(examClassroomsUpdateDto.Id);
        if (examClassroom == null)
        {
            return new ErrorDataResult<ExamClassroomsDto>(Messages.ExamClassroomNotFound);
        }

        _mapper.Map(examClassroomsUpdateDto, examClassroom);
        await _examClassroomsRepository.UpdateAsync(examClassroom);
        await _examClassroomsRepository.SaveChangesAsync();

        var dto = _mapper.Map<ExamClassroomsDto>(examClassroom);
        return new SuccessDataResult<ExamClassroomsDto>(dto, Messages.ExamClassroomUpdatedSuccessfully);
    }
}
