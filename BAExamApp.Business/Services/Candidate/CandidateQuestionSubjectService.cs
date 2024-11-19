using BAExamApp.DataAccess.Interfaces.Repositories;
using BAExamApp.Dtos.Candidate.CandidateQuestionSubject;
using BAExamApp.Dtos.Products;
using BAExamApp.Dtos.Subjects;
using BAExamApp.Entities.DbSets;
using BAExamApp.Entities.DbSets.Candidates;
using BAExamApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services.Candidate;
public class CandidateQuestionSubjectService : ICandidateQuestionSubjectService
{
    private readonly ICandidateQuestionSubjectRepository _candidateQuestionSubjectRepository;
    private readonly IMapper _mapper;
    private readonly ICandidateQuestionRepository _candidateQuestionRepository;

    public CandidateQuestionSubjectService(ICandidateQuestionRepository candidateQuestionRepository, IMapper mapper, ICandidateQuestionSubjectRepository candidateQuestionSubjectRepository)
    {
        _candidateQuestionSubjectRepository = candidateQuestionSubjectRepository;
        _mapper = mapper;
        _candidateQuestionRepository = candidateQuestionRepository;
    }

    public async Task<IDataResult<CandidateQuestionSubjectDto>> AddAsync(CandidateQuestionSubjectCreateDto candidateQuestionSubjectCreateDto)
    {
        if (await _candidateQuestionSubjectRepository.AnyAsync(x => x.Name.ToLower().Equals(candidateQuestionSubjectCreateDto.Name.Trim().ToLower())))
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectAlreadyExist);
        }

        var subject = candidateQuestionSubjectCreateDto.Adapt<CandidateQuestionSubject>();

        await _candidateQuestionSubjectRepository.AddAsync(subject);
        await _candidateQuestionSubjectRepository.SaveChangesAsync();

        var subjectCreateToSubjectDto = subject.Adapt<CandidateQuestionSubjectDto>();
        return new SuccessDataResult<CandidateQuestionSubjectDto>(subjectCreateToSubjectDto, Messages.AddSuccess);

    }

    // O(n²) karmaşıklığına sahip ve düzenlenebilir bir metod eklendi
    public async Task<IDataResult<CandidateQuestionSubjectDto>> ActivateSubjectAndRelatedQuestionsWithAnswersAsync(Guid id)
    {
        try
        {
            // Konu bilgilerini getir
            var candidateQuestionSubject = await _candidateQuestionSubjectRepository.GetByIdAsync(id);
            if (candidateQuestionSubject == null)
            {
                return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectNotFound);
            }

            // Konu durumunu aktif hale getir
            candidateQuestionSubject.Status = Core.Enums.Status.Active;


            foreach (var candidateQuestion in candidateQuestionSubject.CandidateQuestions)
            {
                candidateQuestion.Status = Core.Enums.Status.Active;
                foreach (var answer in candidateQuestion.QuestionAnswers)
                {
                    answer.Status = Core.Enums.Status.Active;
                }
                await _candidateQuestionRepository.UpdateAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();
            }


            await _candidateQuestionSubjectRepository.UpdateAsync(candidateQuestionSubject);
            await _candidateQuestionSubjectRepository.SaveChangesAsync();

            var candidateQuestionSubjectDto = candidateQuestionSubject.Adapt<CandidateQuestionSubjectDto>();

            return new SuccessDataResult<CandidateQuestionSubjectDto>(candidateQuestionSubjectDto, Messages.QuestionAndAnswersSetInactiveSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.QuestionAndAnswersSetInactiveFailed);
        }
    }

    // O(n²) karmaşıklığına sahip ve düzenlenebilir bir metod eklendi
    public async Task<IDataResult<CandidateQuestionSubjectDto>> InactivateSubjectAndRelatedQuestionsWithAnswersAsync(Guid id)
    {
        try
        {
            // Konu bilgilerini getir
            var candidateQuestionSubject = await _candidateQuestionSubjectRepository.GetByIdAsync(id);
            if (candidateQuestionSubject == null)
            {
                return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectNotFound);
            }

            // Konu durumunu aktif hale getir
            candidateQuestionSubject.Status = Core.Enums.Status.Passive;


            foreach (var candidateQuestion in candidateQuestionSubject.CandidateQuestions)
            {
                candidateQuestion.Status = Core.Enums.Status.Passive;
                foreach (var answer in candidateQuestion.QuestionAnswers)
                {
                    answer.Status = Core.Enums.Status.Passive;
                }
                await _candidateQuestionRepository.UpdateAsync(candidateQuestion);
                await _candidateQuestionRepository.SaveChangesAsync();
            }


            await _candidateQuestionSubjectRepository.UpdateAsync(candidateQuestionSubject);
            await _candidateQuestionSubjectRepository.SaveChangesAsync();

            var candidateQuestionSubjectDto = candidateQuestionSubject.Adapt<CandidateQuestionSubjectDto>();

            return new SuccessDataResult<CandidateQuestionSubjectDto>(candidateQuestionSubjectDto, Messages.QuestionAndAnswersSetInactiveSuccess);
        }
        catch (Exception)
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.QuestionAndAnswersSetInactiveFailed);
        }
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var subject = await _candidateQuestionSubjectRepository.GetByIdAsync(id);

        if (subject is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        // Bağlantılı varlıkların status değerlerini güncelle
        subject.CandidateQuestions.ToList().ForEach(q => q.Status = Core.Enums.Status.Deleted);

        // Subject'in status değerini güncelle
        subject.Status = Core.Enums.Status.Deleted;

        await _candidateQuestionSubjectRepository.DeleteAsync(subject);
        await _candidateQuestionSubjectRepository.SaveChangesAsync();

        return new SuccessResult(Messages.DeleteSuccess);
    }

    public async Task<IDataResult<List<CandidateQuestionSubjectListDto>>> GetAllAsync()
    {
        var subjects = await _candidateQuestionSubjectRepository.GetAllAsync(false);

        var subjectToSubjectListDto = subjects.Adapt<List<CandidateQuestionSubjectListDto>>();

        return new SuccessDataResult<List<CandidateQuestionSubjectListDto>>(subjectToSubjectListDto, Messages.ListedSuccess);
    }

    public async Task<IDataResult<CandidateQuestionSubjectDto>> GetByIdAsync(Guid id)
    {
        var subject = await _candidateQuestionSubjectRepository.GetByIdAsync(id);
        if (subject is null)
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectNotFound);
        }
        var subjectToSubjectDto = subject.Adapt<CandidateQuestionSubjectDto>();

        return new SuccessDataResult<CandidateQuestionSubjectDto>(subjectToSubjectDto, Messages.FoundSuccess);
    }

    public async Task<IDataResult<CandidateQuestionSubjectDetailDto>> GetDetailsByIdAsync(Guid id)
    {
        var subject = await _candidateQuestionSubjectRepository.GetByIdAsync(id);
        if (subject is null)
            return new ErrorDataResult<CandidateQuestionSubjectDetailDto>(Messages.SubjectNotFound);

        var subjectToSubjectDetailDto = subject.Adapt<CandidateQuestionSubjectDetailDto>();

        return new SuccessDataResult<CandidateQuestionSubjectDetailDto>(subjectToSubjectDetailDto, Messages.FoundSuccess);
    }


    public async Task<bool> IsQuestionUsedInSubjectAsync(Guid subjectId)
    {
        var questionUsingSubject = await _candidateQuestionRepository.AnyAsync(e => e.CandidateQuestionSubjectId == subjectId);
        return questionUsingSubject;
    }

    //public async Task<IDataResult<CandidateQuestionSubjectDto>> UpdateAsync(CandidateQuestionSubjectUpdateDto entity)
    //{
    //    if (await _candidateQuestionSubjectRepository.AnyAsync(x => x.Name.ToLower().Equals(entity.Name.Trim().ToLower())))
    //    {
    //        return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectAlreadyExist);
    //    }

    //    var subject = await _candidateQuestionSubjectRepository.GetByIdAsync(entity.Id);
    //    var updatedSubject = _mapper.Map(entity, subject);

    //    await _candidateQuestionSubjectRepository.UpdateAsync(updatedSubject);
    //    await _candidateQuestionSubjectRepository.SaveChangesAsync();

    //    var subjectToSubjectDto = updatedSubject.Adapt<CandidateQuestionSubjectDto>();
    //    return new SuccessDataResult<CandidateQuestionSubjectDto>(subjectToSubjectDto, Messages.UpdateSuccess);
    //}
    public async Task<IDataResult<CandidateQuestionSubjectDto>> UpdateAsync(CandidateQuestionSubjectUpdateDto entity)
    {
        if (await _candidateQuestionSubjectRepository.AnyAsync(x => x.Name.ToLower().Equals(entity.Name.Trim().ToLower())))
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectAlreadyExist);
        }

        var subject = await _candidateQuestionSubjectRepository.GetByIdAsync(entity.Id);
        if (subject is null)
        {
            return new ErrorDataResult<CandidateQuestionSubjectDto>(Messages.SubjectNotFound);
        }

        // Mapster kullanarak entity'yi subject'e dönüştürme
        entity.Adapt(subject);

        await _candidateQuestionSubjectRepository.UpdateAsync(subject);
        await _candidateQuestionSubjectRepository.SaveChangesAsync();

        var subjectToSubjectDto = subject.Adapt<CandidateQuestionSubjectDto>();
        return new SuccessDataResult<CandidateQuestionSubjectDto>(subjectToSubjectDto, Messages.UpdateSuccess);
    }
    async Task<string> GetQuestionCountBySubjectAndType(Guid? subjectId = null, CandidateQuestionType? type = null)
    {
        var questions = await _candidateQuestionRepository.GetAllAsync();

        if (subjectId != null)
        {
            var subject = await _candidateQuestionSubjectRepository.GetByIdAsync((Guid)subjectId);
            questions = questions.Where(x => x.CandidateQuestionSubject == subject &&x.Status!=Core.Enums.Status.Passive).ToList();

        }
        if (type != null)
        {
            questions = questions.Where(x => x.CandidateQuestionType == type && x.Status != Core.Enums.Status.Passive).ToList();
        }

        return questions.Count().ToString();
    }


    /// <summary>
    /// Sorular ve tiplerinin sayılarını gösteren tablo için gerekli bütün listeleri bir string liste listesi olarak döndürür.
    /// </summary>
    /// <returns></returns>
    public async Task<List<List<string>>> GetAllQuestionsCountsBySubjectAndType()
    {
        var subjects = await _candidateQuestionSubjectRepository.GetAllAsync();
        var subjectlist = subjects.Where(x=>x.Status!=Core.Enums.Status.Passive).ToList();
        List<List<string>> questionCountsBySubjectAndType = new List<List<string>>();


        //önce soru tiplerinin isimlerini alıyoruz
        List<string> typeNames = new List<string>() { "" };
        foreach (CandidateQuestionType type in Enum.GetValues(typeof(CandidateQuestionType)))
        {
            typeNames.Add(type.ToString());
        }


        questionCountsBySubjectAndType.Add(typeNames);




        //daha sonra soru tiplerine ve konularına göre sayılarını alıyoruz

        for (int j = 0; j < subjectlist.Count; j++)
        {
            //hangi konuya ait olduğu
            List<string> questionCounts = new List<string>() { subjectlist[j].Name };
            for (int i = 1; i <= Enum.GetValues(typeof(CandidateQuestionType)).Length; i++)
            {
                string count = await GetQuestionCountBySubjectAndType(subjectlist[j].Id, (CandidateQuestionType)i);
                questionCounts.Add(count);
            }


            //konunun tipten bağımsız toplam soruları
            string totalCount = await GetQuestionCountBySubjectAndType(subjectlist[j].Id);
            questionCounts.Add(totalCount);

            questionCountsBySubjectAndType.Add(questionCounts);
        }




        //tiplerin konudan bağımsız toplam soruları
        List<string> totalTypeCount = new List<string>();
        foreach (CandidateQuestionType type in Enum.GetValues(typeof(CandidateQuestionType)))
        {
            string typeCount = await GetQuestionCountBySubjectAndType(null, type);

            totalTypeCount.Add(typeCount);
        }
        totalTypeCount.Add(await GetQuestionCountBySubjectAndType());
        questionCountsBySubjectAndType.Add(totalTypeCount);



        return questionCountsBySubjectAndType;
    }



}
