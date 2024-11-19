using BAExamApp.Core.Enums;
using BAExamApp.DataAccess.Interfaces.Repositories.Candidate;
using BAExamApp.Dtos.Candidate.CandidateExams;
using BAExamApp.Dtos.Candidate.Candidates;
using BAExamApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICandidatesGroupsRepository _candidatesGroupsRepository;
    private readonly ICandidateCandidatesExamsRepository _candidatesExamsRepository;
    private readonly ICandidateCandidateQuestionRepository _candidateCandidateQuestionRepository;

    public CandidateService(ICandidateRepository candidateRepository, ICandidatesGroupsRepository candidatesGroupsRepository, ICandidateCandidatesExamsRepository candidatesExamsRepository, ICandidateCandidateQuestionRepository candidateCandidateQuestionRepository)
    {
        _candidateRepository = candidateRepository;
        _candidatesGroupsRepository = candidatesGroupsRepository;
        _candidatesExamsRepository = candidatesExamsRepository;
        _candidateCandidateQuestionRepository = candidateCandidateQuestionRepository;
    }

    /// <summary>
    /// Adayı id ile çağırma işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate is null)
            {
                return new ErrorDataResult<CandidateDto>(Messages.CandidateNotFound);
            }

            return new SuccessDataResult<CandidateDto>(candidate.Adapt<CandidateDto>(), Messages.CandidateFoundSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateDto>(Messages.CandidateNotFound);
        }
    }

    /// <summary>
    /// Tüm adayları çağırma işlemi
    /// </summary>
    /// <returns></returns>
    public async Task<IDataResult<List<CandidateListDto>>> GetAllAsync()
    {
        try
        {
            var candidateList = await _candidateRepository.GetAllAsync();
            if (candidateList is null || !candidateList.Any())
            {
                return new ErrorDataResult<List<CandidateListDto>>(new List<CandidateListDto>(), Messages.NoAvailableCandidate);
            }

            return new SuccessDataResult<List<CandidateListDto>>(candidateList.Adapt<List<CandidateListDto>>(), Messages.CandidateFoundSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<List<CandidateListDto>>(new List<CandidateListDto>(), Messages.NoAvailableCandidate);
        }
    }

    /// <summary>
    /// Aday ekleme işlemi.
    /// </summary>
    /// <param name="candidateCreateDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateDto>> AddAsync(CandidateCreateDto candidateCreateDto)
    {
        try
        {
            var candidate = await _candidateRepository.GetAsync(x => x.Email == candidateCreateDto.Email);
            if (candidate is not null)
            {
                return new ErrorDataResult<CandidateDto>(Messages.CandidateEmailDuplicate);
            }

            var newCandidate = candidateCreateDto.Adapt<CandidateCandidate>();
            await _candidateRepository.AddAsync(newCandidate);
            await _candidateRepository.SaveChangesAsync();
            return new SuccessDataResult<CandidateDto>(newCandidate.Adapt<CandidateDto>(), Messages.AddSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateDto>(Messages.AddError);
        }
    }

    /// <summary>
    /// Aday bilgilerini güncelleme işlemi.
    /// </summary>
    /// <param name="candidateUpdateDto"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateDto>> UpdateAsync(CandidateUpdateDto candidateUpdateDto)
    {
        try
        {
            var existingCandidate = await _candidateRepository.GetByIdAsync(candidateUpdateDto.Id);
            if (existingCandidate is null)
            {
                return new ErrorDataResult<CandidateDto>(Messages.CandidateNotFound);
            }

            if (existingCandidate.Email != candidateUpdateDto.Email && await _candidateRepository.AnyAsync(x => x.Email == candidateUpdateDto.Email))
            {
                return new ErrorDataResult<CandidateDto>(Messages.CandidateEmailDuplicate);
            }

            var candidate = candidateUpdateDto.Adapt(existingCandidate);
            await _candidateRepository.UpdateAsync(candidate);
            await _candidateRepository.SaveChangesAsync();

            return new SuccessDataResult<CandidateDto>(candidate.Adapt<CandidateDto>(), Messages.UpdateSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateDto>(Messages.UpdateFail);
        }
    }

    /// <summary>
    /// Aday silme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IResult> DeleteAsync(Guid id)
    {
        try
        {

            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate is null)
            {
                return new ErrorResult(Messages.CandidateNotFound);
            }

            await _candidateRepository.DeleteAsync(candidate);
            await _candidateRepository.SaveChangesAsync();
            return new SuccessResult(Messages.DeleteSuccess);
        }
        catch (Exception)
        {
            return new ErrorResult(Messages.DeleteFail);
        }
    }

    /// <summary>
    /// Adaya ait detayları getirme işlemi.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IDataResult<CandidateDetailsDto>> GetDetailsByIdAsync(Guid id)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if(candidate is null)
            {
                return new ErrorDataResult<CandidateDetailsDto>(Messages.CandidateNotFound);
            }

            return new SuccessDataResult<CandidateDetailsDto>(candidate.Adapt<CandidateDetailsDto>(), Messages.CandidateFoundSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateDetailsDto>(Messages.CandidateNotFound);
        }
    }
    public async Task<IDataResult<List<CandidateGroupsDetailsDto>>> GetCandidateGroupsAsync(Guid candidateId)
    {
        try
        {
            var candidateGroups = await _candidatesGroupsRepository.GetAllAsync(cg => cg.CandidateId == candidateId);

            if (candidateGroups == null || !candidateGroups.Any())
            {
                return new ErrorDataResult<List<CandidateGroupsDetailsDto>>(new List<CandidateGroupsDetailsDto>(), Messages.CandidateNotFound);
            }

            var candidateGroupDetails = candidateGroups
                .Select(cg => new CandidateGroupsDetailsDto
                {
                    GroupId = cg.CandidateGroupId,
                    GroupName = cg.CandidateGroup.Name,
                    // Diğer grup detayları eklenebilir
                })
                .ToList();

            return new SuccessDataResult<List<CandidateGroupsDetailsDto>>(candidateGroupDetails, Messages.CandidateNotFound);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<CandidateGroupsDetailsDto>>(Messages.CandidateNotFound + " - " + ex.Message);
        }
    }
    public async Task<IDataResult<CandidateExamsDto>> GetCandidateExamsAsync(Guid candidateId)
    {
        try
        {
            var candidate = await _candidateRepository.GetByIdAsync(candidateId);
            var candidateExams = await _candidatesExamsRepository.GetAllAsync(ce => ce.CandidateId == candidateId);
            if (candidateExams == null || !candidateExams.Any())
            {
                return new ErrorDataResult<CandidateExamsDto>(Messages.CandidateExamsNotFound);
            }

            var examResults = new List<CandidateExamResultDto>();
            foreach (var exam in candidateExams)
            {
                var questions = await _candidateCandidateQuestionRepository.GetAllAsync(q => q.CandidatesExamsId == exam.Id);
                var examResult = new CandidateExamResultDto
                {
                    ExamId = exam.CandidateExam.Id,
                    ExamName = exam.CandidateExam.Name,
                    IsExamStarted = exam.IsExamStarted,
                    IsExamFinished = exam.IsExamFinished,
                    ExamDateTime = exam.CandidateExam.ExamDateTime,
                    ExamDuration = exam.CandidateExam.ExamDuration,
                    Score = questions.Sum(q => q.Score) ?? 0,
                    MaxScore = questions.Sum(q => q.MaxScore)
                };
                examResults.Add(examResult);
            }

            var candidateExamsDetails = new CandidateExamsDto
            {
                CandidateId = candidateId,
                CandidateName = $"{candidate.FirstName} {candidate.LastName}",
                Exams = examResults
            };

            return new SuccessDataResult<CandidateExamsDto>(candidateExamsDetails, Messages.CandidateFoundSuccess);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CandidateExamsDto>(Messages.CandidateNotFound + " - " + ex.Message);
        }
    }
}
