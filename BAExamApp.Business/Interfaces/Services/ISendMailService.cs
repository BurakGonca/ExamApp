using BAExamApp.Dtos.Emails;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.SentMails;
using BAExamApp.Dtos.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Business.Interfaces.Services;
public interface ISendMailService
{
    /// <summary>
    /// Yeni oluşturulan öğrenci hesabının bilgilerinin gönderilmesi için kullanırlır.
    /// </summary>
    /// <param name="email">Mailin gönderileceği mail adresi</param>
    /// <param name="url">Login sayfasının url adresi</param>
    /// <returns></returns>
    Task<SentMailCreateDto> SendEmailNewStudent(NewUserMailDto newUserMailDto);

    /// <summary>
    /// Gönderilmeyen maili id'ye göre tekrar gönderir
    /// </summary>
    /// <param name="id">Gönderilmeyen mailin id'si</param>
    /// <returns>Gönderim başarılı olursa  SentMailDto modeli tipinde değer döner</returns>
    Task<SentMail> ResendMail(SentMail sentMail);

    /// <summary>
    /// Yeni oluşturulan eğitmen hesabının bilgilerinin gönderilmesi için kullanırlır.
    /// </summary>
    /// <param name="email">Mailin gönderileceği mail adresi</param>
    /// <param name="url">Login sayfasının url adresi</param>
    /// <returns></returns>
    Task SendEmailNewTrainer(NewUserMailDto newUserMailDto);

    /// <summary>
    /// Yeni oluşturulan admin hesabının bilgilerinin gönderilmesi için kullanırlır.
    /// </summary>
    /// <param name="email">Mailin gönderileceği mail adresi</param>
    /// <param name="url">Login sayfasının url adresi</param>
    /// <returns></returns>
    Task SendEmailNewAdmin(NewUserMailDto newUserMailDto);

    /// <summary>
    /// Giriş yapan kullanıcının iki adımlı doğrulamadaki güvenlik kodunun gönderilmesi için kullanılır.
    /// </summary>
    /// <param name="email">Mailin gönderileceği mail adresi</param>
    /// <returns>Gönderilen güvenlik kodu</returns>
    Task<int> SendEmailVerificationCode(string email);


    /// <summary>
    /// Belirtilen mail adresine sınav raporu göndermek için kullanılır.
    /// </summary>
    /// <param name="email">Mailin gönderileceği mail adresi</param>
    /// <returns></returns>
    Task SendAfterExamMail(AfterExamMailDto afterExamMailDto);

    /// <summary>
    /// Öğrencinin oluşturulan yeni sınava girebilmesi için gerekli bilgileri ve linki gönderir. 
    /// </summary>
    /// <param name="student">Öğrenci bilgileirini içerir</param>
    /// <param name="url">Sınav sayfasının url adresi</param>
    /// <param name="examId">Öğrencinin gireceği sınavın ID'si</param>
    /// <returns></returns>
    Task<SentMailCreateDto> SendEmailToStudentNewExam(StudentNewExamMailDto studentNewExamMailDto);

    /// <summary>
    /// İptal edilen sınav için öğrencilere ve eğitmene iptal hakkında bilgi maili gönderir.
    /// </summary>
    /// <param name="studentTrainerCancelExamMailDto"></param>
    /// <returns></returns>
    Task<SentMailCreateDto> SendEmailToStudentCancelExam(StudentTrainerCancelExamMailDto studentTrainerCancelExamMailDto);

    Task<string> GetStudentEmailById(Guid studentId);

    Task<SentMail> GetSentMail(Guid sentMailId);

    Task SendEmailToTrainerNewExam(TrainerNewExamMailDto trainerNewExamMailDto);

    /// <summary>
    /// Eğitmen tarafından öğrencinin sınav sonucu değerlendirmesini göndermek için kullanılır.
    /// </summary>
    /// <param name="assessment">Yapılan değerlendirme</param>
    /// <param name="studentEmailAddress">Öğrencinin mail adresi</param>
    /// <param name="examName">Değerlendirme yapılan sınav</param>
    /// <param name="trainerName">Değerlendirme yapan eğitmenin adı</param>
    /// <returns></returns>
    Task<SentMailCreateDto> SendEmailToStudentAssessment(StudentAssesmentMailDto studentAssesmentMailDto);

    Task<SentMailCreateDto> SendEmailToCandidateNewExam(CandidateNewExamMailDto candidateNewExamMailDto);

    Task SendEmailToCandidateAdminNewExam(CandidateAdminNewExamMailDto candidateAdminNewExamMailDto);

    /// <summary>
    /// Tüm öğrencilere sınav notlarının toplu bir şekilde gönderilmesini sağlar.
    /// </summary>
    /// <param name="allStudents"></param>
    /// <returns></returns>
    Task AllStudentEmail(AllStudentsEmailDto allStudents);



    Task SendEmailToCandidateAdminNewExamLink(CandidateAdminNewExamLinkMailDto candidateAdminNewExamLinkMailDto);

    Task<SentMailCreateDto> SendExamFinishedNotifyMailToCandidateAdmin(ExamFinishedNotifyToCandidateAdminDto examFinishedNotifyMailToCandidateAdminDto);
}