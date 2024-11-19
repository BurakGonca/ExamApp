using BAExamApp.Core.Enums;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.TrainerClassrooms;
using BAExamApp.Entities.DbSets;
using System.Linq.Expressions;

namespace BAExamApp.Business.Services;
public class TrainerClassroomService : ITrainerClassroomService
{
    private readonly ITrainerClassroomRepository _trainerClassroomRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IMapper _mapper;
    public TrainerClassroomService(IMapper mapper, ITrainerClassroomRepository trainerClassroomRepository, IClassroomRepository classroomRepository, ITrainerRepository trainerRepository)
    {
        _trainerClassroomRepository = trainerClassroomRepository;
        _mapper = mapper;
        _classroomRepository = classroomRepository;
        _trainerRepository = trainerRepository;
    }

    public async Task<string> GetClassroomNameByClassroomId(Guid Id)
    {
        var classroom = await _classroomRepository.GetAsync(x => x.Id == Id);
        return classroom.Name;
    }

    public async Task<IDataResult<List<TrainerClassroomExamListDto>>> GetClassroomsExamsByTrainerId(Guid id)
    {
        var trainerClassroomList = await _trainerClassroomRepository.GetAllAsync(x => x.TrainerId == id);

        var classroomWithExamList = new List<TrainerClassroomExamListDto>();

        foreach (var trainerClassroom in trainerClassroomList)
        {
            var examClassrooms = trainerClassroom.Classroom.ExamClassrooms.Where(ec => ec.Exam.Status != Status.Deleted);

            if (examClassrooms.Any())
            {
                var classroomWithExam = new TrainerClassroomExamListDto
                {
                    Name = trainerClassroom.Classroom.Name,
                    Exams = _mapper.Map<List<ExamListDto>>(examClassrooms.Select(ec => ec.Exam))
                };
                classroomWithExamList.Add(classroomWithExam);
            }
        }
        return new SuccessDataResult<List<TrainerClassroomExamListDto>>(classroomWithExamList, Messages.ListedSuccess);
    }

    public async Task<IDataResult<List<TrainerClassromDto>>> GetTrainersWithSpesificClassroomIdAsync(Guid classroomId)
    {

        var trainers = await _trainerRepository.GetAllAsync(x => x.TrainerClassrooms.Any(x => x.ClassroomId == classroomId && string.IsNullOrEmpty(x.DeletedBy)));

        return trainers.Any()
            ? new SuccessDataResult<List<TrainerClassromDto>>(_mapper.Map<List<TrainerClassromDto>>(trainers), Messages.ListedSuccess)
            : new ErrorDataResult<List<TrainerClassromDto>>(Messages.NoTrainerFoundInClassroom);
    }

    /// <summary>
    /// Seçilen trainerların update edilmesini sağlar
    /// </summary>
    /// <param name="classroomId"></param>
    /// <param name="trainersIds"></param>
    /// <returns>ıResult</returns>
    public async Task<IResult> UpdateTrainersIdsInClassroom(Guid classroomId, List<Guid> trainersIds)
    {
        var existingTrainers = await _trainerClassroomRepository.GetAllAsync(x => x.ClassroomId == classroomId);


        var trainersToRemove = existingTrainers.Where(trainer => !trainersIds.Contains(trainer.TrainerId)).ToList();
        foreach (var trainerToRemove in trainersToRemove)
        {
            await _trainerClassroomRepository.DeleteAsync(trainerToRemove);
        }
        List<Guid> trainersToAdd = new List<Guid>();
        if (trainersIds != null)
        {
            trainersToAdd = trainersIds.Where(trainerId => !existingTrainers.Any(s => s.TrainerId == trainerId)).ToList();

        }
        foreach (var trainerIdToAdd in trainersToAdd)
        {
            var newTrainer = new TrainerClassroom
            {
                ClassroomId = classroomId,
                TrainerId = trainerIdToAdd
            };
            await _trainerClassroomRepository.AddAsync(newTrainer);
        }
        await _trainerClassroomRepository.SaveChangesAsync();

        return new SuccessResult("Trainer güncelleme işlemi başarıyla tamamlandı.");
    }

    public async Task<IResult> AddTrainersToClassroomAsync(TraninerAddClassroomDto classroomAddTrainerDto)
    {
        var trainerClassRoom = await _trainerClassroomRepository.GetAllAsync(x => x.ClassroomId == classroomAddTrainerDto.ClassroomId);

        var toBeDeletedTrainers = trainerClassRoom.Where(x => !classroomAddTrainerDto.SelectedTrainersIds.Contains(x.TrainerId)).ToList();

        var newTrainers = classroomAddTrainerDto.SelectedTrainersIds
    .Where(trainerId => !trainerClassRoom.Any(tc => tc.TrainerId == trainerId))
    .Select(trainerId => new TrainerClassroom
    {
        ClassroomId = classroomAddTrainerDto.ClassroomId,
        TrainerId = trainerId,
        AssignedDate = DateTime.Now
    })
    .ToList();

        if (toBeDeletedTrainers != null)
        {
            foreach (var t in toBeDeletedTrainers)
            {
                t.ResignedDate = DateTime.Now;
            }
            await _trainerClassroomRepository.DeleteRangeAsync(toBeDeletedTrainers);

        }
        if (trainerClassRoom != null)

        {
            await _trainerClassroomRepository.AddRangeAsync(newTrainers);
        }
        await _trainerClassroomRepository.SaveChangesAsync();
        return new SuccessResult(Messages.UpdateSuccess);
    }

    public async Task<IDataResult<List<TrainerClassromDto>>> GetAllByExpression(Expression<Func<TrainerClassroom, bool>> expression, bool tracking = true)
    {


        var trainers = await _trainerClassroomRepository.GetAllAsync(expression, tracking);

        return trainers.Any()
     ? new SuccessDataResult<List<TrainerClassromDto>>(_mapper.Map<List<TrainerClassromDto>>(trainers), Messages.ListedSuccess)
     : new ErrorDataResult<List<TrainerClassromDto>>(Messages.NoTrainerFoundInClassroom);



    }
}
