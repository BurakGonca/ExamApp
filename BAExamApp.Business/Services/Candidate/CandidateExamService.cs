using BAExamApp.Business.Interfaces.Services;
using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.Dtos.Exams;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using Hangfire;
using IResult = BAExamApp.Core.Utilities.Results.IResult;
using BAExamApp.Dtos.Candidate.CandidateQuestionRule;
using BAExamApp.Dtos.Candidate.CandidateExamRule;
using Microsoft.Identity.Client;
using BAExamApp.Dtos.Candidate.CandidateAdmins;
using BAExamApp.Dtos.Emails;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateExamService : ICandidateExamService
{
    private readonly ICandidateExamRepository _candidateExamRepository;
    private readonly ICandidateCandidatesExamsRepository _candidateCandidatesExamsRepository;
    private readonly ICandidateCandidatesGroupsRepository _candidateCandidatesGroupsRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICandidateCandidateQuestionRepository _candidateCandidateQuestionRepository;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICandidateCandidateAnswerRepository _candidateCandidateAnswerRepository;
    private readonly ICandidateAnswerRepository _candidateAnswerRepository;
    private readonly ISendMailService _sendMailService;
    private readonly ICandidateAdminRepository _candidateAdminRepository;


    public CandidateExamService(ICandidateExamRepository candidateExamRepository, ICandidateCandidatesExamsRepository candidateCandidatesExamsRepository, ICandidateCandidatesGroupsRepository candidateCandidatesGroupsRepository, ICandidateRepository candidateRepository, ICandidateCandidateQuestionRepository candidateCandidateQuestionRepository, IUserService userService, IHttpContextAccessor contextAccessor, ICandidateCandidateAnswerRepository candidateCandidateAnswerRepository, ICandidateAnswerRepository candidateAnswerRepository, ISendMailService sendMailService, ICandidateAdminRepository candidateAdminRepository)
    {
        _candidateExamRepository = candidateExamRepository;
        _candidateCandidatesExamsRepository = candidateCandidatesExamsRepository;
        _candidateCandidatesGroupsRepository = candidateCandidatesGroupsRepository;
        _candidateRepository = candidateRepository;
        _candidateCandidateQuestionRepository = candidateCandidateQuestionRepository;
        _userService = userService;
        _contextAccessor = contextAccessor;
        _candidateCandidateAnswerRepository = candidateCandidateAnswerRepository;
        _candidateAnswerRepository = candidateAnswerRepository;
        _sendMailService = sendMailService;
        _candidateAdminRepository = candidateAdminRepository;
    }

    /// <summary>
    /// Sınavı id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    public async Task<IDataResult<CandidateExamDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var exam = await _candidateExamRepository.GetByIdAsync(id);
            if (exam is null)
            {
                return new ErrorDataResult<CandidateExamDto>(Messages.ExamNotFound);
            }

            return new SuccessDataResult<CandidateExamDto>(exam.Adapt<CandidateExamDto>(), Messages.ExamFoundSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateExamDto>(Messages.ExamNotFound);
        }
    }

    /// <summary>
    /// Tüm sınavları çağırma işlemi
    /// </summary>
    /// <returns></returns>

    public async Task<IDataResult<List<CandidateExamListDto>>> GetAllAsync()
    {
        try
        {
            var examList = await _candidateExamRepository.GetAllAsync();
            if (examList is null || !examList.Any())
            {
                return new ErrorDataResult<List<CandidateExamListDto>>(new List<CandidateExamListDto>(), Messages.ListNotFound);
            }

            return new SuccessDataResult<List<CandidateExamListDto>>(examList.Adapt<List<CandidateExamListDto>>(), Messages.ListedSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<CandidateExamListDto>>(new List<CandidateExamListDto>(), Messages.ListNotFound);
        }
    }


    /// <summary>
    /// Sınav ekleme ve verilen sorulari atama işlemi.
    /// </summary>
    /// <param name="candidateExamCreateDto"></param>
    /// <returns></returns>

    public async Task<IDataResult<CandidateExamDto>> AddAsync(CandidateExamCreateDto candidateExamCreateDto, List<CandidateQuestionListDto> questions)
    {
        try
        {
            List<Guid> idList;
            if (candidateExamCreateDto.forGroup)
            {
                var candidatesGroups = await _candidateCandidatesGroupsRepository.GetAllAsync(x => candidateExamCreateDto.GroupIds.Contains(x.CandidateGroupId) && x.Candidate.Status == Core.Enums.Status.Active);
                idList = candidatesGroups.Select(x => x.CandidateId).Distinct().ToList();
            }
            else
            {
                idList = candidateExamCreateDto.CandidateIds;
            }

            var newExam = candidateExamCreateDto.Adapt<CandidateExam>();
            var newCandidateExam = await _candidateExamRepository.AddAsync(newExam);

            // sınav puanlama bölümü

            int totalCoefficient = candidateExamCreateDto.TestQuestionsCoefficient + candidateExamCreateDto.ClassicQuestionsCoefficient + candidateExamCreateDto.AlgorithmQuestionsCoefficient;
            int unitPoints = candidateExamCreateDto.MaxScore / totalCoefficient;

            int sectionCount = new List<int>() { candidateExamCreateDto.TestQuestionCount, candidateExamCreateDto.ClassicQuestionCount, candidateExamCreateDto.AlgorithmQuestionCount }.Count(x => x != 0);

            int totalPointsOfTestQuestions = candidateExamCreateDto.TestQuestionsCoefficient * unitPoints;
            int totalPointsOfClassicQuestions = candidateExamCreateDto.ClassicQuestionsCoefficient * unitPoints;
            int totalPointsOfAlgorithmQuestions = candidateExamCreateDto.AlgorithmQuestionsCoefficient * unitPoints;

            int remainder = candidateExamCreateDto.MaxScore - (totalCoefficient * unitPoints);
            int extra = remainder % sectionCount;

            int testBaseScore = 0;
            int classicBaseScore = 0;
            int algorithmBaseScore = 0;

            int testRemainder = 0;
            int classicRemainder = 0;
            int algorithmRemainder = 0;

            if (candidateExamCreateDto.TestQuestionCount > 0)
            {
                totalPointsOfTestQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Test bölümü toplam puan

                testBaseScore = totalPointsOfTestQuestions / candidateExamCreateDto.TestQuestionCount;
                testRemainder = totalPointsOfTestQuestions % candidateExamCreateDto.TestQuestionCount;
            }


            if (candidateExamCreateDto.ClassicQuestionCount > 0)
            {
                totalPointsOfClassicQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Klasik bölümü toplam puan

                classicBaseScore = totalPointsOfClassicQuestions / candidateExamCreateDto.ClassicQuestionCount;
                classicRemainder = totalPointsOfClassicQuestions % candidateExamCreateDto.ClassicQuestionCount;
            }

            if (candidateExamCreateDto.AlgorithmQuestionCount > 0)
            {
                totalPointsOfAlgorithmQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Algoritma bölümü toplam puan

                algorithmBaseScore = totalPointsOfAlgorithmQuestions / candidateExamCreateDto.AlgorithmQuestionCount;
                algorithmRemainder = totalPointsOfAlgorithmQuestions % candidateExamCreateDto.AlgorithmQuestionCount;
            }

            foreach (var id in idList)
            {

                var candidatesExams = await _candidateCandidatesExamsRepository.AddAsync(new CandidatesExams() { CandidateId = id, CandidateExamId = newCandidateExam.Id });

                var testQuestions = questions.Where(x => x.CandidateQuestionType == 1).ToList();
                var classicQuestions = questions.Where(x => x.CandidateQuestionType == 3).ToList();
                var algorithmQuestions = questions.Where(x => x.CandidateQuestionType == 2).ToList();

                int copyTestRemainder = testRemainder;
                int copyClassicRemainder = classicRemainder;
                int copyAlgorithmRemainder = algorithmRemainder;

                if (testQuestions.Count > 0)
                {
                    var candidateExamTestQuestions = questions.Where(x => x.CandidateQuestionType == 1);

                    foreach (var item in candidateExamTestQuestions)
                    {
                        var selectedTestQuestion = item;
                        var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                            .AddAsync(new CandidateCandidateQuestion()
                            {
                                QuestionId = selectedTestQuestion.Id,
                                CandidatesExamsId = candidatesExams.Id,
                                MaxScore = copyTestRemainder-- > 0 ? testBaseScore + 1 : testBaseScore
                            });
                    }

                }
                if (classicQuestions.Count > 0)
                {
                    var candidateExamClassicQuestions = questions.Where(x => x.CandidateQuestionType == 3);
                    foreach (var item in candidateExamClassicQuestions)
                    {
                        var selectedClassicQuestion = item;
                        var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                            .AddAsync(new CandidateCandidateQuestion()
                            {
                                QuestionId = selectedClassicQuestion.Id,
                                CandidatesExamsId = candidatesExams.Id,
                                MaxScore = copyClassicRemainder-- > 0 ? classicBaseScore + 1 : classicBaseScore
                            });
                    }

                }
                if (algorithmQuestions.Count > 0)
                {
                    var candidateAlgorithmQuestions = questions.Where(x => x.CandidateQuestionType == 2);
                    foreach (var item in candidateAlgorithmQuestions)
                    {
                        var selectedAlgorithmQuestion = item;
                        var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                            .AddAsync(new CandidateCandidateQuestion()
                            {
                                QuestionId = selectedAlgorithmQuestion.Id,
                                CandidatesExamsId = candidatesExams.Id,
                                MaxScore = copyAlgorithmRemainder-- > 0 ? algorithmBaseScore + 1 : algorithmBaseScore
                            });
                    }
                }



            }
            await _candidateExamRepository.SaveChangesAsync();
            return new SuccessDataResult<CandidateExamDto>(newExam.Adapt<CandidateExamDto>(), Messages.AddSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateExamDto>(Messages.AddError);
        }
    }

    public async Task AddQuestionsToExam(CandidateExamDto exam, List<CandidateQuestionListDto> questions, Guid candidatesExamsId, CandidateExamRuleDto examRule)
    {
        int totalCoefficient = examRule.TestQuestionsCoefficient + examRule.ClassicQuestionsCoefficient + examRule.AlgorithmQuestionsCoefficient;
        int unitPoints = exam.MaxScore / totalCoefficient;

        int sectionCount = new List<int>() { exam.TestQuestionCount, exam.ClassicQuestionCount, exam.AlgorithmQuestionCount }.Count(x => x != 0);

        int totalPointsOfTestQuestions = examRule.TestQuestionsCoefficient * unitPoints;
        int totalPointsOfClassicQuestions = examRule.ClassicQuestionsCoefficient * unitPoints;
        int totalPointsOfAlgorithmQuestions = examRule.AlgorithmQuestionsCoefficient * unitPoints;

        int remainder = exam.MaxScore - (totalCoefficient * unitPoints);
        int extra = remainder % sectionCount;

        int testBaseScore = 0;
        int classicBaseScore = 0;
        int algorithmBaseScore = 0;

        int testRemainder = 0;
        int classicRemainder = 0;
        int algorithmRemainder = 0;

        if (exam.TestQuestionCount > 0)
        {
            totalPointsOfTestQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Test bölümü toplam puan

            testBaseScore = totalPointsOfTestQuestions / exam.TestQuestionCount;
            testRemainder = totalPointsOfTestQuestions % exam.TestQuestionCount;
        }


        if (exam.ClassicQuestionCount > 0)
        {
            totalPointsOfClassicQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Klasik bölümü toplam puan

            classicBaseScore = totalPointsOfClassicQuestions / exam.ClassicQuestionCount;
            classicRemainder = totalPointsOfClassicQuestions % exam.ClassicQuestionCount;
        }

        if (exam.AlgorithmQuestionCount > 0)
        {
            totalPointsOfAlgorithmQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Algoritma bölümü toplam puan

            algorithmBaseScore = totalPointsOfAlgorithmQuestions / exam.AlgorithmQuestionCount;
            algorithmRemainder = totalPointsOfAlgorithmQuestions % exam.AlgorithmQuestionCount;
        }




        var testQuestions = questions.Where(x => x.CandidateQuestionType == 1).ToList();
        var classicQuestions = questions.Where(x => x.CandidateQuestionType == 3).ToList();
        var algorithmQuestions = questions.Where(x => x.CandidateQuestionType == 2).ToList();

        int copyTestRemainder = testRemainder;
        int copyClassicRemainder = classicRemainder;
        int copyAlgorithmRemainder = algorithmRemainder;


        if (testQuestions.Count > 0)
        {
            for (int i = 0; i < testQuestions.Count; i++)
            {
                var selectedTestQuestion = testQuestions[i];

                var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                       .AddAsync(new CandidateCandidateQuestion()
                       {
                           QuestionId = selectedTestQuestion.Id,
                           CandidatesExamsId = candidatesExamsId,
                           MaxScore = copyTestRemainder-- > 0 ? testBaseScore + 1 : testBaseScore
                       });


            }


        }

        if (classicQuestions.Count > 0)
        {
            for (int i = 0; i < classicQuestions.Count; i++)
            {
                var selectedClassicQuestion = classicQuestions[i];
                var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                       .AddAsync(new CandidateCandidateQuestion()
                       {
                           QuestionId = selectedClassicQuestion.Id,
                           CandidatesExamsId = candidatesExamsId,
                           MaxScore = copyClassicRemainder-- > 0 ? classicBaseScore + 1 : classicBaseScore
                       });

            }
        }

        if (algorithmQuestions.Count > 0)
        {
            for (int i = 0; i < algorithmQuestions.Count; i++)
            {
                var selectedAlgorithmQuestion = algorithmQuestions[i];

                var candidateCandidateQuestion = await _candidateCandidateQuestionRepository
                       .AddAsync(new CandidateCandidateQuestion()
                       {
                           QuestionId = selectedAlgorithmQuestion.Id,
                           CandidatesExamsId = candidatesExamsId,
                           MaxScore = copyAlgorithmRemainder-- > 0 ? algorithmBaseScore + 1 : algorithmBaseScore
                       });


            }
        }


        await _candidateCandidateQuestionRepository.SaveChangesAsync();

        await _candidateExamRepository.SaveChangesAsync();

    }



    public async Task<IDataResult<CandidateExamDto>> CreateExamForLinkAsync(CandidateExamLinkCreateDto candidateExamCreateDto)
    {
        try
        {

            var newExam = candidateExamCreateDto.Adapt<CandidateExam>();
            newExam.ExamDateTime = DateTime.Now;
            var newCandidateExam = await _candidateExamRepository.AddAsync(newExam);

            // sınav puanlama bölümü

            int totalCoefficient = candidateExamCreateDto.TestQuestionsCoefficient + candidateExamCreateDto.ClassicQuestionsCoefficient + candidateExamCreateDto.AlgorithmQuestionsCoefficient;
            int unitPoints = candidateExamCreateDto.MaxScore / totalCoefficient;

            int sectionCount = new List<int>() { candidateExamCreateDto.TestQuestionCount, candidateExamCreateDto.ClassicQuestionCount, candidateExamCreateDto.AlgorithmQuestionCount }.Count(x => x != 0);

            int totalPointsOfTestQuestions = candidateExamCreateDto.TestQuestionsCoefficient * unitPoints;
            int totalPointsOfClassicQuestions = candidateExamCreateDto.ClassicQuestionsCoefficient * unitPoints;
            int totalPointsOfAlgorithmQuestions = candidateExamCreateDto.AlgorithmQuestionsCoefficient * unitPoints;

            int remainder = candidateExamCreateDto.MaxScore - (totalCoefficient * unitPoints);
            int extra = remainder % sectionCount;

            int testBaseScore = 0;
            int classicBaseScore = 0;
            int algorithmBaseScore = 0;

            int testRemainder = 0;
            int classicRemainder = 0;
            int algorithmRemainder = 0;

            if (candidateExamCreateDto.TestQuestionCount > 0)
            {
                totalPointsOfTestQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Test bölümü toplam puan

                testBaseScore = totalPointsOfTestQuestions / candidateExamCreateDto.TestQuestionCount;
                testRemainder = totalPointsOfTestQuestions % candidateExamCreateDto.TestQuestionCount;
            }


            if (candidateExamCreateDto.ClassicQuestionCount > 0)
            {
                totalPointsOfClassicQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Klasik bölümü toplam puan

                classicBaseScore = totalPointsOfClassicQuestions / candidateExamCreateDto.ClassicQuestionCount;
                classicRemainder = totalPointsOfClassicQuestions % candidateExamCreateDto.ClassicQuestionCount;
            }

            if (candidateExamCreateDto.AlgorithmQuestionCount > 0)
            {
                totalPointsOfAlgorithmQuestions += (remainder / sectionCount) + (extra-- > 0 ? 1 : 0); //Algoritma bölümü toplam puan

                algorithmBaseScore = totalPointsOfAlgorithmQuestions / candidateExamCreateDto.AlgorithmQuestionCount;
                algorithmRemainder = totalPointsOfAlgorithmQuestions % candidateExamCreateDto.AlgorithmQuestionCount;
            }

            await _candidateExamRepository.SaveChangesAsync();
            return new SuccessDataResult<CandidateExamDto>(newExam.Adapt<CandidateExamDto>(), Messages.AddSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateExamDto>(Messages.AddError);
        }
    }




    /// <summary>
    /// Sınav silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(Guid id)
    {
        try
        {
            var candidate = await _candidateExamRepository.GetByIdAsync(id);
            if (candidate is null)
            {
                return new ErrorResult(Messages.ExamNotFound);
            }

            await _candidateExamRepository.DeleteAsync(candidate);
            await _candidateExamRepository.SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.DeleteFail);
        }
    }

    public async Task<IDataResult<CandidateExamDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        try
        {
            var result = await _candidateExamRepository.GetByIdAsync(id);

            if (result is null)
            {
                return new ErrorDataResult<CandidateExamDetailsDto>(Messages.ExamNotFound);
            }

            var candidateResult = await _candidateRepository.GetAllAsync(x => x.Exams.Any(y => y.CandidateExamId == id));
            if (candidateResult is null)
            {
                return new ErrorDataResult<CandidateExamDetailsDto>(Messages.CandidateNotFound);
            }

            var candidateExamDetailDto = result.Adapt<CandidateExamDetailsDto>();

            candidateExamDetailDto.CandidateList = candidateResult.Select(x => x.Adapt<CandidateExamDetailsListDto>()).ToList();

            var questions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExams.CandidateExamId == id);

            foreach (var item in candidateExamDetailDto.CandidateList)
            {
                item.Score = questions.Where(x => x.CandidatesExams.CandidateId == item.Id).Sum(x => x.Score ?? 0);
            }


            return new SuccessDataResult<CandidateExamDetailsDto>(candidateExamDetailDto, Messages.ExamFoundSuccess);
        }
        catch (Exception)
        {

            return new ErrorDataResult<CandidateExamDetailsDto>(Messages.ExamNotFound);
        }
    }

    public async Task<IDataResult<CandidateExamQuestionsByCandidateDto>> GetExamQuestionsByCandidateIdAsync(Guid examId, Guid candidateId)
    {

        var candidateExamList = await _candidateCandidatesExamsRepository.GetAllAsync(x => x.CandidateId == candidateId && x.CandidateExamId == examId);
        var candidateExam = candidateExamList.FirstOrDefault();

        var examQuestions = (await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExams.CandidateId == candidateId && x.CandidatesExams.CandidateExamId == examId)).OrderBy(x => x.Question.CandidateQuestionType == CandidateQuestionType.Test ? 0 : x.Question.CandidateQuestionType == CandidateQuestionType.Classic ? 1 : 2).ThenBy(x => x.Question.CandidateQuestionType);

        CandidateExamQuestionsByCandidateDto dto = new CandidateExamQuestionsByCandidateDto()
        {
            CandidateId = candidateId,
            ExamId = examId,
            FullName = candidateExam.Candidate.FirstName + " " + candidateExam.Candidate.LastName,
            ExamName = candidateExam.CandidateExam.Name
        };

        int i = 1;
        foreach (var question in examQuestions)
        {
            CandidateExamQuestionsByCandidateListDto candidateExamQuestionsByCandidateListDto;

            var answers = question.Question.QuestionAnswers.Where(x => x.Status != Status.Deleted).Adapt<ICollection<CandidateExamQuestionsByCandidateAnswersListDto>>();


            candidateExamQuestionsByCandidateListDto = new CandidateExamQuestionsByCandidateListDto()
            {
                Id = question.Id,
                Content = question.Question.Content,
                Image = question.Question.Image,
                Answers = answers.ToList(),
                Order = i,
                MaxScore = question.MaxScore,
                Score = question.Score,
                CandidateQuestionType = question.Question.CandidateQuestionType,
            };


            dto.Questions.Add(candidateExamQuestionsByCandidateListDto);
            i++;
        }

        return new SuccessDataResult<CandidateExamQuestionsByCandidateDto>(dto, Messages.FoundSuccess);
    }

    public async Task<IDataResult<List<CandidateCandidateAnswersDto>>> GetGivenAnswersAsync(Guid examId, Guid candidateId)
    {
        var candidateQuestions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExams.CandidateExamId == examId && x.CandidatesExams.CandidateId == candidateId);
        if (candidateQuestions is null)
        {
            return new ErrorDataResult<List<CandidateCandidateAnswersDto>>(Messages.ListNotFound);
        }
        try
        {
            List<CandidateCandidateAnswersDto> candidateAnswerList = candidateQuestions
                .Select(x => new CandidateCandidateAnswersDto
                {
                    Id = (Guid)x.CandidateAnswerId!,
                    Answer = x.CandidateAnswer!.Answer,
                    IsRightAnswer = x.CandidateAnswer!.IsRightAnswer,
                    AIAssessment = x.CandidateAnswer!.AIAssessment,
                    CandidateQuestionId = x.CandidateAnswer!.CandidateQuestionId,
                    CandidateAnswerId = x.CandidateAnswer!.CandidateAnswerId

                }).ToList();

            return new SuccessDataResult<List<CandidateCandidateAnswersDto>>(candidateAnswerList, Messages.FoundSuccess);

        }
        catch (Exception)
        {
            return new ErrorDataResult<List<CandidateCandidateAnswersDto>>(Messages.ListNotFound);
        }


    }

    public async Task<IDataResult<List<CandidateExamListDto>>> StartExamAndNotifyCandidates(Guid examId, string link)
    {
        try
        {
            var exam = await _candidateExamRepository.GetByIdAsync(examId);
            if (exam is null)
            {
                return new ErrorDataResult<List<CandidateExamListDto>>(Messages.ExamNotFound);
            }

            var updateExamResult = await UpdateExamStatusAsync(exam);
            if (!updateExamResult.IsSuccess)
            {
                return new ErrorDataResult<List<CandidateExamListDto>>(updateExamResult.Message);
            }

            var candidateEmails = await GetCandidateExamEmailsAsync(examId, link);
            if (candidateEmails == null || candidateEmails.Count == 0)
            {
                return new ErrorDataResult<List<CandidateExamListDto>>(Messages.ExamStartedMailError);
            }


            foreach (var emailDto in candidateEmails)
            {
                BackgroundJob.Enqueue(() => _sendMailService.SendEmailToCandidateNewExam(emailDto));
            }

            return new SuccessDataResult<List<CandidateExamListDto>>(Messages.ListedSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<CandidateExamListDto>>(Messages.ExamNotFound);
        }
    }


    public async Task<IDataResult<List<ExamFinishedNotifyToCandidateAdminDto>>> ExamFinishedNotifyToCandidateAdmin(Guid candidateId, Guid examId)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(candidateId);
            var candidateExam = await _candidateCandidatesExamsRepository.GetAsync(x => x.CandidateExamId == examId);
            if (candidateExam == null)
            {
                return new ErrorDataResult<List<ExamFinishedNotifyToCandidateAdminDto>>(Messages.CandidateExamNotFound);
            }

            var questions = await _candidateCandidateQuestionRepository.GetAllAsync(x => x.CandidatesExamsId == candidateExam.Id);
            var result = new ExamFinishedNotifyToCandidateAdminDto
            {
                ExamId = candidateExam.CandidateExam.Id,
                ExamName = candidateExam.CandidateExam.Name,
                Score = questions.Sum(x => x.Score) ?? 0,
                MaxScore = questions.Sum(x => x.MaxScore)


            };
            var candidateAdmins = await _candidateAdminRepository.GetAllAsync();

            var candidateAdminsNotifyEmails = new List<ExamFinishedNotifyToCandidateAdminDto>();
            List<string> participantContents = new List<string>();
            participantContents.Add($"{result.ExamName}" + "*?*" + $"{candidate.FirstName + " " + candidate.LastName}" + "*?*" + $"{candidate.Email}" + "*?*" + $"{result.Score} / {result.MaxScore}");

            foreach (var candidateAdmin in candidateAdmins)
            {
                candidateAdminsNotifyEmails.Add(new ExamFinishedNotifyToCandidateAdminDto()
                {
                    EmailAddress = candidateAdmin.Email,
                    Result = participantContents
                    //EmailAddress = candidateAdmin.Email,
                    //StudentFullName = candidate.FirstName + " " + candidate.LastName,              
                    //ExamName = result.ExamName,
                    //Score = result.Score,
                    //MaxScore = result.MaxScore

                });
            }

            foreach (var candidateNotifyEmail in candidateAdminsNotifyEmails)
            {
                BackgroundJob.Enqueue(() => _sendMailService.SendExamFinishedNotifyMailToCandidateAdmin(candidateNotifyEmail));
            }

            return new SuccessDataResult<List<ExamFinishedNotifyToCandidateAdminDto>>(Messages.ListedSuccess);

        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task<IResult> UpdateExamStatusAsync(CandidateExam candidateExam)
    {
        candidateExam.IsStarted = true;
        await _candidateExamRepository.UpdateAsync(candidateExam);
        await _candidateExamRepository.SaveChangesAsync();
        return new SuccessResult();
    }

    private async Task<List<CandidateNewExamMailDto>> GetCandidateExamEmailsAsync(Guid examId, string baseUrl)
    {
        var candidatesExams = await _candidateCandidatesExamsRepository.GetAllAsync(ce => ce.CandidateExamId == examId);

        List<Guid> examCandidateIds = candidatesExams.Select(c => c.CandidateId).Distinct().ToList();
        var candidates = await _candidateRepository.GetAllAsync(c => examCandidateIds.Contains(c.Id));

        var candidateExam = await _candidateExamRepository.GetByIdAsync(examId);

        var candidatesExamEmails = new List<CandidateNewExamMailDto>();

        foreach (var candidate in candidates)
        {
            string candidateLink = $"{baseUrl}&candidateId={candidate.Id}";

            candidatesExamEmails.Add(new CandidateNewExamMailDto()
            {
                EmailAdress = candidate.Email,
                Url = candidateLink,
                CandidateExamId = candidateExam.Id,
                ExamDate = candidateExam.ExamDateTime,
                ExamDuration = candidateExam.ExamDuration,
                ExamName = candidateExam.Name,
            });
        }

        return candidatesExamEmails;
    }

    public async Task<IResult> NotifyCandidateAdminAboutExamCreationAsync(Guid examId, string baseUrl)
    {
        var exam = await _candidateExamRepository.GetByIdAsync(examId);
        if (exam == null)
        {
            return new ErrorResult(Messages.ExamNotFound);
        }

        var adminId = _contextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var adminEmail = await _userService.GetEmailByUserId(adminId, Roles.CandidateAdmin);
        var candidateEmails = await GetCandidateExamEmailsAsync(examId, baseUrl);

        var candidateContents = candidateEmails.Select(ce => $"{ce.ExamName}*?*{ce.ExamDate}*?*{ce.EmailAdress}*?*{ce.Url}").ToList();

        var candidateAdminNewExamMailDto = new CandidateAdminNewExamMailDto()
        {
            CandidateAdminEmailAdress = adminEmail,
            CandidateContents = candidateContents
        };

        BackgroundJob.Enqueue(() => _sendMailService.SendEmailToCandidateAdminNewExam(candidateAdminNewExamMailDto));

        return new SuccessResult(Messages.ExamCreatedSuccessfully);
    }

    public async Task<IResult> NotifyCandidateAdminAboutExamLinkCreationAsync(Guid examId, string baseUrl)
    {
        var exam = await _candidateExamRepository.GetByIdAsync(examId);
        if (exam == null)
        {
            return new ErrorResult(Messages.ExamNotFound);
        }

        var adminId = _contextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var adminEmail = await _userService.GetEmailByUserId(adminId, Roles.CandidateAdmin);


        var candidateAdminNewExamLinkMailDto = new CandidateAdminNewExamLinkMailDto()
        {
            CandidateAdminEmailAdress = adminEmail,
            ExamLink = baseUrl,
        };

        BackgroundJob.Enqueue(() => _sendMailService.SendEmailToCandidateAdminNewExamLink(candidateAdminNewExamLinkMailDto));

        return new SuccessResult(Messages.ExamCreatedSuccessfully);
    }
    public async Task<IResult> AddCandidateAnswersAsync(Guid questionId, string givenAnswer)
    {
        try
        {
            // Tüm uygun candidateAnswer kayıtlarını al
            var candidateAnswers = await _candidateCandidateAnswerRepository.GetAllAsync(ca => ca.CandidateQuestionId == questionId && ca.Answer == givenAnswer);

            // İlgili candidateQuestion kaydını al
            var candidateQuestion = await _candidateCandidateQuestionRepository.GetAsync(ca => ca.Id == questionId);


            // Yeni bir CandidateAnswer nesnesi oluştur
            var newCandidateAnswer = new CandidateAnswer
            {
                Answer = givenAnswer,
                IsImageAnswer = false,
                IsRightAnswer = true,
                QuestionId = candidateQuestion.QuestionId,
                Status = Status.Active
            };

            // CandidateAnswer'ı veritabanına ekle
            await _candidateAnswerRepository.AddAsync(newCandidateAnswer);
            await _candidateAnswerRepository.SaveChangesAsync();

            return new SuccessResult(Messages.CandidateTextAnswerAddedSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorResult(Messages.CandidateAnswerAddFailed);
        }
    }

}

