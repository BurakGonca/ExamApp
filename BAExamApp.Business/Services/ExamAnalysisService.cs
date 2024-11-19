using BAExamApp.Dtos.StudentExams;
using BAExamApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Services;
public class ExamAnalysisService : IExamAnalysisService
{
    private readonly IExamRepository _examRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ISubtopicRepository _subtopicRepository;

    public ExamAnalysisService(IExamRepository examRepository, IStudentRepository studentRepository, ISubtopicRepository subtopicRepository)
    {
        _examRepository = examRepository;
        _studentRepository = studentRepository;
        _subtopicRepository = subtopicRepository;

    }
    public async Task<IDictionary<string, double>> AnalysisExamPerformanceAsync(Guid examId)
    {
        var exam = await _examRepository.GetByIdAsync(examId);

        if (exam == null)
        {
            throw new InvalidOperationException("Sınav bulunamadı!");
        }

        var subtopicPerformances = new Dictionary<string, List<double>>();

        foreach (var studentExam in exam.StudentExams)
        {
            foreach (var studentQuestion in studentExam.StudentQuestions)
            {
                var question = studentQuestion.Question;

                if (question != null && question.QuestionSubtopics != null)
                {
                    foreach (var questionSubtopic in question.QuestionSubtopics)
                    {
                        var subtopicName = questionSubtopic.Subtopic.Name;

                        if (!subtopicPerformances.ContainsKey(subtopicName))
                        {
                            subtopicPerformances[subtopicName] = new List<double>();
                        }

                        if (IsQuestionCorrectClassroom(studentQuestion))
                        {
                            subtopicPerformances[subtopicName].Add(1);
                        }
                        else
                        {
                            subtopicPerformances[subtopicName].Add(0);
                        }
                    }
                }
            }
        }

        // Her konunun ortalama performansını hesapla
        return subtopicPerformances.ToDictionary(
            k => k.Key,
            v => v.Value.Average() * 100);
    }

    private bool IsQuestionCorrectClassroom(StudentQuestion studentQuestion)
    {
        var correctAnswer = studentQuestion.Question.QuestionAnswers.FirstOrDefault(qa => qa.IsRightAnswer);
        var selectedAnswer = studentQuestion.StudentAnswers.FirstOrDefault(sa => sa.IsSelected);
        return correctAnswer != null && selectedAnswer != null && correctAnswer.Id == selectedAnswer.QuestionAnswerId;
    }





    public async Task<StudentExamResultDto> AnalysisStudentPerformanceAsync(Guid studentId, Guid examId)
    {
        var exam = await _examRepository.GetByIdAsync(examId);
        var student = await _studentRepository.GetByIdAsync(studentId);

        if (exam == null || student == null)
        {
            throw new InvalidOperationException("Öğrenci ya da Sınav bulunamadı!");
        }

        var studentExam = exam.StudentExams.FirstOrDefault(se => se.StudentId == student.Id);

        if (studentExam == null)
        {
            throw new InvalidOperationException("Öğrenci bu sınava girmedi");
        }


        var subtopicPerformances = CalculateOverallPerformance(studentExam);

        return subtopicPerformances;
    }

    private double CalculateSubtopicPerformance(StudentExam studentExam, string subtopic)
    {
        var questionsInSubtopic = studentExam.StudentQuestions
    .Where(sq => sq.Question != null && sq.Question.QuestionSubtopics != null &&
                 sq.Question.QuestionSubtopics.Any(x => x.Subtopic != null && x.Subtopic.Name == subtopic))
    .ToList();

        int correctQuestions = questionsInSubtopic
            .Count(sq => IsQuestionCorrect(sq));

        int totalQuestions = questionsInSubtopic.Count;

        double subtopicPerformance = 0;
        if (totalQuestions > 0)
        {
            subtopicPerformance = (correctQuestions / (double)totalQuestions) * 100;
        }

        return subtopicPerformance;
    }

   

    /// <summary>
    ///  Her bir alt konu için ayrı ayrı başarı oranlarını hesaplandı ve daha sonra bu başarı oranları ortalama başarı oranına çevrildi.
    /// </summary>
    /// <param name="studentExam"></param>
    /// <returns></returns>
    public StudentExamResultDto CalculateOverallPerformance(StudentExam studentExam)
    {
        var correctAnswers = new Dictionary<string, int>();
        var wrongAnswers = new Dictionary<string, int>();
        var emptyAnswers = new Dictionary<string, int>();
        var subtopicScore = new Dictionary<string, double>();

        foreach (var studentQuestion in studentExam.StudentQuestions)
        {
            ProcessQuestion(studentQuestion, correctAnswers, wrongAnswers, emptyAnswers, subtopicScore, studentExam);
        }

        return new StudentExamResultDto
        {
            RightAnswer = correctAnswers,
            WrongAnswer = wrongAnswers,
            EmptyAnswer = emptyAnswers,
            Score = subtopicScore
        };
    }

    private void ProcessQuestion(StudentQuestion studentQuestion,
                                 Dictionary<string, int> correctAnswers,
                                 Dictionary<string, int> wrongAnswers,
                                 Dictionary<string, int> emptyAnswers,
                                 Dictionary<string, double> subtopicScore,
                                 StudentExam studentExam)
    {
        var question = studentQuestion.Question;
        bool questionProcessed = false;

        if (question != null && question.QuestionSubtopics != null)
        {
            foreach (var questionSubtopic in question.QuestionSubtopics)
            {
                var subtopicName = questionSubtopic.Subtopic?.Name;

                if (!string.IsNullOrEmpty(subtopicName))
                {
                    InitializeSubtopicScores(subtopicName, correctAnswers, wrongAnswers, emptyAnswers);

                    if (!questionProcessed)
                    {
                        ProcessStudentQuestion(studentQuestion, subtopicName, correctAnswers, wrongAnswers, emptyAnswers);
                        questionProcessed = true;
                    }

                    UpdateSubtopicScore(subtopicName, studentExam, subtopicScore);
                }
            }
        }
    }

    private void InitializeSubtopicScores(string subtopicName,
                                           Dictionary<string, int> correctAnswers,
                                           Dictionary<string, int> wrongAnswers,
                                           Dictionary<string, int> emptyAnswers)
    {
        if (!correctAnswers.ContainsKey(subtopicName))
            correctAnswers[subtopicName] = 0;
        if (!wrongAnswers.ContainsKey(subtopicName))
            wrongAnswers[subtopicName] = 0;
        if (!emptyAnswers.ContainsKey(subtopicName))
            emptyAnswers[subtopicName] = 0;
    }

    private void ProcessStudentQuestion(StudentQuestion studentQuestion,
                                         string subtopicName,
                                         Dictionary<string, int> correctAnswers,
                                         Dictionary<string, int> wrongAnswers,
                                         Dictionary<string, int> emptyAnswers)
    {
        if (IsQuestionCorrect(studentQuestion))
        {
            correctAnswers[subtopicName]++;
        }
        else if (!EmptyQuestionCount(studentQuestion))
        {
            wrongAnswers[subtopicName]++;
        }
        else
        {
            emptyAnswers[subtopicName]++;
        }
    }

    private void UpdateSubtopicScore(string subtopicName,
                                      StudentExam studentExam,
                                      Dictionary<string, double> subtopicScore)
    {
        if (!subtopicScore.ContainsKey(subtopicName))
        {
            var subtopicPerformance = CalculateSubtopicPerformance(studentExam, subtopicName);
            subtopicScore[subtopicName] = subtopicPerformance;
        }
    }



    private bool IsQuestionCorrect(StudentQuestion studentQuestion)
    {
        int correctSelected = 0;
        int totalCorrectAnswers = studentQuestion.StudentAnswers
            .Count(sa => sa.QuestionAnswer != null && sa.QuestionAnswer.IsRightAnswer);

        foreach (var answer in studentQuestion.StudentAnswers)
        {
            if (answer.QuestionAnswer != null)
            {
                if (answer.QuestionAnswer.IsRightAnswer && answer.IsSelected)
                {
                    correctSelected++;
                }
                else if (!answer.QuestionAnswer.IsRightAnswer && answer.IsSelected)
                {
                    return false;
                }
            }
        }
        return correctSelected == totalCorrectAnswers;
    }

    private bool EmptyQuestionCount(StudentQuestion studentQuestion)
    {
        int selected = 0;
        foreach (var answer in studentQuestion.StudentAnswers)
        {
            if (answer.QuestionAnswer != null)
            {
                if (!answer.IsSelected)
                {
                    selected++;
                }
                else if (!answer.QuestionAnswer.IsRightAnswer && answer.IsSelected)
                {
                    return false;
                }
            }
        }
        bool some = selected == studentQuestion.Question.QuestionAnswers.Count();
        return some;
    }



}