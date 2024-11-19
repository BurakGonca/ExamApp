using BAExamApp.Core.Utilities.Results.Concrete;
using BAExamApp.DataAccess.Contexts;
using BAExamApp.Dtos.Emails;
using BAExamApp.Dtos.SendMails;
using BAExamApp.Dtos.SentMails;
using BAExamApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace BAExamApp.Business.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly IOptions<EmailConfigurationDto> _emailConfiguration;
        private readonly ISentMailService _sentMailService;
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly ITrainerRepository _trainerRepository;


        public SendMailService(IStudentExamRepository studentExamRepository, ITrainerRepository tarnierRepository, IStudentRepository studentRepository, IOptions<EmailConfigurationDto> emailConfiguration, ISentMailService sentMailService, IConfiguration configuration)
        {
            _emailConfiguration = emailConfiguration;
            _sentMailService = sentMailService;
            _configuration = configuration;
            _studentRepository = studentRepository;
            _trainerRepository = tarnierRepository;
            _studentExamRepository = studentExamRepository;
        }


        // Helper Methods
        private int GenerateVerificationCode()
        {
            Random code = new Random();
            return code.Next(100000, 999999);
        }

        private async Task<MailMessage> CreateEmailContent(MailMessageDto message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_emailConfiguration.Value.From);
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Content;

            return emailMessage;
        }

        private async Task<MailMessage> CreateEmailContentWithHtml(MailMessageDto message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_emailConfiguration.Value.From);
            emailMessage.To.Add(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.IsBodyHtml = true;
            emailMessage.Body = message.Content;

            return emailMessage;
        }

        private string CreateHtmlBody(List<string> participantContents)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Body Builder</title>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append("table { border-collapse: collapse; background-color: #fff; }");
            htmlBuilder.Append("table tr, table td, table th {background-color:#ffffff; border: 1px solid #bbb; padding: 10px 20px; }");
            htmlBuilder.Append("table th {background-color:#40bfed; color:#fff; font-weight: 600; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<p><b>İlgili sınavlara ait bilgiler aşağıda verilmiştir:</b></p>");
            htmlBuilder.Append("<table>");

            List<string> headers = new List<string>
            {
                "Sınav Adı","Sınıf/Grup Adı", "Sınav Tarihi", "Email", "Sınav Linki"
            };

            htmlBuilder.Append("<tr>");
            foreach (string header in headers)
            {
                htmlBuilder.Append("<th>").Append(header).Append("</th>");
            }
            htmlBuilder.Append("</tr>");

            foreach (string participantContent in participantContents)
            {
                string[] examResult = participantContent.Split("*?*");

                htmlBuilder.Append("<tr>");

                // Verileri sırayla yerleştir
                for (int i = 0; i < headers.Count; i++)
                {
                    // Veri varsa ekle, yoksa özel bir mesaj ekle
                    string detail = i < examResult.Length ? examResult[i] : "Sınafa özel düzenlenmemiştir.";
                    htmlBuilder.Append("<td>").Append(detail).Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            string htmlOutput = htmlBuilder.ToString();

            Console.WriteLine("HTML Body Content: " + htmlOutput);

            return htmlOutput;
        }

        private string ExamResultHtmlBodyForCandidateAdmin(List<string> participantContents)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Body Builder</title>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append("table { border-collapse: collapse; background-color: #fff; }");
            htmlBuilder.Append("table tr, table td, table th {background-color:#ffffff; border: 1px solid #bbb; padding: 10px 20px; }");
            htmlBuilder.Append("table th {background-color:#40bfed; color:#fff; font-weight: 600; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<p><b>İlgili sınava ait öğrenci sınav sonuç bilgisi aşağıda verilmiştir:</b></p>");
            htmlBuilder.Append("<table>");

            List<string> headers = new List<string>
            {
                "Sınav Adı", "Öğrenci Adı", "Email", "Sınav Sonucu"
            };

            htmlBuilder.Append("<tr>");
            foreach (string header in headers)
            {
                htmlBuilder.Append("<th>").Append(header).Append("</th>");
            }
            htmlBuilder.Append("</tr>");

            foreach (string participantContent in participantContents)
            {
                string[] examResult = participantContent.Split("*?*");

                htmlBuilder.Append("<tr>");

                // Verileri sırayla yerleştir
                for (int i = 0; i < headers.Count; i++)
                {
                    // Veri varsa ekle, yoksa özel bir mesaj ekle
                    string detail = i < examResult.Length ? examResult[i] : "Sınafa özel düzenlenmemiştir.";
                    htmlBuilder.Append("<td>").Append(detail).Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            string htmlOutput = htmlBuilder.ToString();

            Console.WriteLine("HTML Body Content: " + htmlOutput);

            return htmlOutput;
        }

        /// <summary>
        /// Öğrenciye gönderilecek olan sınav sonucu mailinin içeriğinde bulunması gereken parametrelere göre tablo oluşturur.
        /// </summary>
        /// <param name="participantContents"></param>
        /// <returns></returns>
        private string ExamResultHtmlBody(List<string> participantContents)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Body Builder</title>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append("table { border-collapse: collapse; background-color: #fff; }");
            htmlBuilder.Append("table tr, table td, table th {background-color:#ffffff; border: 1px solid #bbb; padding: 10px 20px; }");
            htmlBuilder.Append("table th {background-color:#40bfed; color:#fff; font-weight: 600; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<p><b>Sınav sonucunuz: </b></p>");
            htmlBuilder.Append("<table>");

            List<string> headers = new List<string>
            {
                "Öğrenci Adı", "Sınav Adı", "Sınav Tarihi", "Puan",  "Soru Sayısı", "Doğru Cevap Sayısı", "Yanlış Cevap Sayısı", "Boş Cevap Sayısı"
            };

            htmlBuilder.Append("<tr>");
            foreach (string header in headers)
            {
                htmlBuilder.Append("<th>").Append(header).Append("</th>");
            }
            htmlBuilder.Append("</tr>");

            foreach (string participantContent in participantContents)
            {
                string[] examResult = participantContent.Split("*?*");

                htmlBuilder.Append("<tr>");

                for (int i = 0; i < headers.Count; i++)
                {
                    // Veri varsa ekle, yoksa özel bir mesaj ekle
                    string detail = i < examResult.Length ? examResult[i] : "Öğrenciye özel düzenlenmemiştir.";
                    htmlBuilder.Append("<td>").Append(detail).Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            string htmlOutput = htmlBuilder.ToString();

            Console.WriteLine("HTML Body Content: " + htmlOutput);

            return htmlOutput;
        }





        private string ExamResultsHtmlBody(List<string> participantContents)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Body Builder</title>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append("table { border-collapse: collapse; background-color: #fff; }");
            htmlBuilder.Append("table tr, table td, table th {background-color:#ffffff; border: 1px solid #bbb; padding: 10px 20px; }");
            htmlBuilder.Append("table th {background-color:#40bfed; color:#fff; font-weight: 600; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");

            string examName = participantContents.FirstOrDefault().Split("*?*")[2];
            htmlBuilder.Append($"<p><b>{examName} Sınavı Sonuçları</b></p>");
            htmlBuilder.Append("<table>");

            List<string> headers = new List<string>
            {
                "Öğrenci Adı", "Puan"
            };

            htmlBuilder.Append("<tr>");
            foreach (string header in headers)
            {
                htmlBuilder.Append("<th>").Append(header).Append("</th>");
            }
            htmlBuilder.Append("</tr>");

            foreach (string participantContent in participantContents)
            {
                string[] examResult = participantContent.Split("*?*");

                htmlBuilder.Append("<tr>");

                for (int i = 0; i < headers.Count; i++)
                {
                    string detail = i < examResult.Length ? examResult[i] : "Öğrenciye özel düzenlenmemiştir.";
                    htmlBuilder.Append("<td>").Append(detail).Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            string htmlOutput = htmlBuilder.ToString();

            Console.WriteLine("HTML Body Content: " + htmlOutput);

            return htmlOutput;
        }


        private string FormatExamDuration(TimeSpan examDuration)
        {
            string formattedDuration = "";

            if (examDuration.Hours > 0 && examDuration.Minutes > 0)
            {
                formattedDuration = $"{examDuration.Hours:00}:{examDuration.Minutes:00} ({examDuration.Hours} saat {examDuration.Minutes} dakika)";
            }
            else if (examDuration.Hours > 0 && examDuration.Minutes == 0)
            {
                formattedDuration = $"{examDuration.Hours:00} saat";
            }
            else if (examDuration.Hours == 0 && examDuration.Minutes > 0)
            {
                formattedDuration = $"{examDuration.Minutes} dakika";
            }

            return formattedDuration;
        }

        private async Task SendMail(MailMessageDto message)
        {
            var mailMessage = await CreateEmailContent(message);

            using (var client = new SmtpClient(_emailConfiguration.Value.SmtpServer, _emailConfiguration.Value.Port))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(_emailConfiguration.Value.From, _emailConfiguration.Value.Password);
                client.EnableSsl = true;
                client.Send(mailMessage);
            }

        }

        private async Task SendMailWithHtml(MailMessageDto message)
        {
            var mailMessage = await CreateEmailContentWithHtml(message);

            using (var client = new SmtpClient(_emailConfiguration.Value.SmtpServer, _emailConfiguration.Value.Port))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(_emailConfiguration.Value.From, _emailConfiguration.Value.Password);
                client.EnableSsl = true;

                client.Send(mailMessage);

            }
        }

        public async Task<SentMail> ResendMail(SentMail sentMail)
        {
            MailMessageDto message = new MailMessageDto(sentMail.Email, sentMail.Subject, sentMail.Content);
            var sentMailCreateDto = new SentMailCreateDto()
            {
                Content = message.Content,
                Email = message.To,
                Subject = message.Subject,
                IsSuccess = false
            };
            try
            {
                await SendMail(message);
                sentMailCreateDto.IsSuccess = true;
                await _sentMailService.AddAsync(sentMailCreateDto);
                return sentMail;
            }
            catch (Exception)
            {
                sentMailCreateDto.IsSuccess = false;
                await _sentMailService.AddAsync(sentMailCreateDto);
                return sentMail;
            }
        }

        public async Task<string> GetStudentEmailById(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            return student?.Email;
        }


        public async Task<SentMail> GetSentMail(Guid sentMailId)
        {
            var dbContextBuilder = new DbContextOptionsBuilder<BAExamAppDbContext>();

            dbContextBuilder.UseSqlServer(_configuration.GetConnectionString(BAExamAppDbContext.ConnectionName));
            using (BAExamAppDbContext context = new(dbContextBuilder.Options))
            {
                return context.SentMails.Find(sentMailId);
            }
        }


        // Mail Send To Somebody  Methods
        public async Task<int> SendEmailVerificationCode(string email)
        {
            int verificationCode = GenerateVerificationCode();
            MailMessageDto message = new MailMessageDto(email, "Bilge Adam Giriş Şifresi", $"Giriş Şifreniz {verificationCode}");

            await SendMail(message);
            return verificationCode;
        }

        public async Task<SentMailCreateDto> SendEmailNewStudent(NewUserMailDto newUserMailDto)
        {
            MailMessageDto message = new MailMessageDto(newUserMailDto.Email, "Bilge Adam'a Hoşgeldiniz", $"Yeni hesabınızla giriş yapmak için aşağıdaki linke tıklayabilirsiniz.\n{newUserMailDto.Url} \n Giriş Bilgileriniz \nEmail : {newUserMailDto.Email} \nŞifre : newPassword+0");

            try
            {
                await SendMail(message);

                var sentMailCreateDto = message.Adapt<SentMailCreateDto>();
                sentMailCreateDto.IsSuccess = true;
                return sentMailCreateDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }
        }

        public async Task SendEmailNewTrainer(NewUserMailDto newUserMailDto)
        {
            MailMessageDto message = new MailMessageDto(newUserMailDto.Email, "Bilge Adam'a Hoşgeldiniz", $"Yeni hesabınızla giriş yapmak için aşağıdaki linke tıklayabilirsiniz.\n{newUserMailDto.Url} \n Giriş Bilgileriniz \nEmail : {newUserMailDto.Email} \nŞifre : newPassword+0");
            await SendMail(message);
        }

        public async Task SendEmailNewAdmin(NewUserMailDto newUserMailDto)
        {
            MailMessageDto message = new MailMessageDto(newUserMailDto.Email, "Bilge Adam'a Hoşgeldiniz", $"Yeni hesabınızla giriş yapmak için aşağıdaki linke tıklayabilirsiniz.\n{newUserMailDto.Url} \n Giriş Bilgileriniz \nEmail : {newUserMailDto.Email} \nŞifre : newPassword+0");
            await SendMail(message);
        }


        public async Task<SentMailCreateDto> SendExamFinishedNotifyMailToCandidateAdmin(ExamFinishedNotifyToCandidateAdminDto examFinishedNotifyToCandidateAdminDto)
        {
            var subject = examFinishedNotifyToCandidateAdminDto.Result[0].Split("*?*")[0] + " " + "Sınav Sonucu Hakkında Bilgilendirme";
            var body = ExamResultHtmlBodyForCandidateAdmin(examFinishedNotifyToCandidateAdminDto.Result);
            var message = new MailMessageDto(examFinishedNotifyToCandidateAdminDto.EmailAddress, subject, body);
            var result = new SentMailCreateDto()
            {
                Content = message.Content,
                Subject = message.Subject,
                Email = message.To,
                IsSuccess = false,
            };
            try
            {
                if (examFinishedNotifyToCandidateAdminDto == null)
                {
                    throw new ArgumentNullException(nameof(examFinishedNotifyToCandidateAdminDto), "ExamFinishedNotifyToCandidateAdminDto cannot be nulll");
                }
                if (examFinishedNotifyToCandidateAdminDto.Result == null || examFinishedNotifyToCandidateAdminDto.Result.Count == 0)
                {
                    throw new ArgumentException("Result cannot be null or empty.", nameof(examFinishedNotifyToCandidateAdminDto.Result));
                }
                if (string.IsNullOrEmpty(examFinishedNotifyToCandidateAdminDto.EmailAddress))
                {
                    throw new ArgumentException("Candidate admin email address cannot be null or empty.", nameof(examFinishedNotifyToCandidateAdminDto.EmailAddress));
                }


                await SendMailWithHtml(message);
                result.IsSuccess = true;
                await _sentMailService.AddAsync(result);
                var sentMailCreateDto = message.Adapt<SentMailCreateDto>();
                return sentMailCreateDto;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                await _sentMailService.AddAsync(result);
                throw new ArgumentNullException(nameof(examFinishedNotifyToCandidateAdminDto), ex);
            }

        }

        public async Task<SentMailCreateDto> SendEmailToStudentNewExam(StudentNewExamMailDto studentNewExamMailDto)
        {
            string emailSubject = $"{studentNewExamMailDto.ExamName} Sınavı Hakkında Bilgilendirme";
            string formattedExamDuration = FormatExamDuration(studentNewExamMailDto.ExamDuration);
            string emailBody = $"Merhaba,\n\n" +
                               $"Yaklaşan '{studentNewExamMailDto.ExamName}' sınavınız hakkında sizi bilgilendirmek isteriz. Sınav detayları aşağıda yer almaktadır:\n\n" +
                               $"Sınav Tarihi: {studentNewExamMailDto.ExamDate.ToString("dd.MM.yyyy HH:mm")}\n" +
                               $"Sınav Süresi: {formattedExamDuration}\n\n" +
                               $"Sınava giriş yapmak için lütfen aşağıdaki linke tıklayın:\n" +
                               $"{studentNewExamMailDto.Url}/{studentNewExamMailDto.StudentExamId}\n\n" +
                               $"Sınavınız hakkında herhangi bir sorunuz olursa, lütfen bizimle iletişime geçin.\n\n" +
                               $"Başarılar dileriz,\n" +
                               $"Eğitim Ekibiniz";

            MailMessageDto message = new MailMessageDto(studentNewExamMailDto.EmailAdress, emailSubject, emailBody);
            //Bu kod gönderilen maillerin DB'de SentMails tablosuna eklenmesini ve dolayısıyla E-mail Contentin görüntülenebilmesini sağlar
            //Gerekli yerlere benzer şekilde eklenerek kullanılabilir.
            var sentMailCreateDto = new SentMailCreateDto()
            {
                Content = message.Content,
                Email = message.To,
                Subject = message.Subject,
                IsSuccess = false // Başlangıçta başarısız olarak ayarlıyoruz
            };

            try
            {
                await SendMail(message);
                // Mail gönderimi başarılı ise durumu güncelliyoruz
                sentMailCreateDto.IsSuccess = true;

                // Başarılı mail gönderimi için veritabanına ekleme
                await _sentMailService.AddAsync(sentMailCreateDto);
            }
            catch (Exception ex)
            {
                // Hata durumunda durumu güncelle
                sentMailCreateDto.IsSuccess = false;
                await _sentMailService.AddAsync(sentMailCreateDto);
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }

            return sentMailCreateDto;
        }

        public async Task<SentMailCreateDto> SendEmailToStudentCancelExam(StudentTrainerCancelExamMailDto studentTrainerCancelExamMailDto)
        {
            string emailSubject = $"{studentTrainerCancelExamMailDto.ExamName} Sınavı Hakkında Bilgilendirme";
            string emailBody = $"Merhaba,\n\n" +
                               $"{studentTrainerCancelExamMailDto.ExamDate.ToString("dd.MM.yyyy HH:mm")} tarihinde '{studentTrainerCancelExamMailDto.ExamName}' isimli sınavınız iptal edilmiştir.\n\n" +
                               $"Yeni bir sınav tarihi belirlendiğinde size bilgilendirme yapılacaktır. Anlayışınız için teşekkür ederiz.\n\n" +
                               $"Sınavınız hakkında herhangi bir sorunuz olursa, lütfen bizimle iletişime geçin.\n\n" +
                               $"Saygılarımızla,\n" +
                               $"Eğitim Ekibiniz";

            MailMessageDto message = new MailMessageDto(studentTrainerCancelExamMailDto.EmailAdress, emailSubject, emailBody);

            var sentMailCreateDto = new SentMailCreateDto()
            {
                Content = message.Content,
                Email = message.To,
                Subject = message.Subject,
                IsSuccess = false
            };

            try
            {
                await SendMail(message);
                sentMailCreateDto.IsSuccess = true;

                await _sentMailService.AddAsync(sentMailCreateDto);
            }
            catch (Exception ex)
            {
                sentMailCreateDto.IsSuccess = false;
                await _sentMailService.AddAsync(sentMailCreateDto);
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }

            return sentMailCreateDto;
        }



        public async Task<SentMailCreateDto> SendEmailToCandidateNewExam(CandidateNewExamMailDto candidateNewExamMailDto)
        {
            string emailSubject = $"{candidateNewExamMailDto.ExamName} Sınavı Hakkında Bilgilendirme";
            string formattedExamDuration = FormatExamDuration(candidateNewExamMailDto.ExamDuration);

            string emailBody = $"Merhaba,\n\n" +
                               $"Yaklaşan '{candidateNewExamMailDto.ExamName}' sınavınız hakkında sizi bilgilendirmek isteriz. Sınav detayları aşağıda yer almaktadır:\n\n" +
                               $"Sınav Tarihi: {candidateNewExamMailDto.ExamDate.ToString("dd.MM.yyyy HH:mm")}\n" +
                               $"Sınav Süresi: {formattedExamDuration}\n\n" +
                               $"Sınava giriş yapmak için lütfen aşağıdaki linke tıklayın:\n" +
                               $"{candidateNewExamMailDto.Url}\n\n" +
                               $"Sınavınız hakkında herhangi bir sorunuz olursa, lütfen bizimle iletişime geçin.\n\n" +
                               $"Başarılar dileriz,\n" +
                               $"Eğitim Ekibiniz";

            MailMessageDto message = new MailMessageDto(candidateNewExamMailDto.EmailAdress, emailSubject, emailBody);

            var sentMailCreateDto = new SentMailCreateDto()
            {
                Content = message.Content,
                Email = message.To,
                Subject = message.Subject,
                IsSuccess = false
            };

            try
            {
                await SendMail(message);
                sentMailCreateDto.IsSuccess = true;
                await _sentMailService.AddAsync(sentMailCreateDto);
            }
            catch (Exception ex)
            {
                sentMailCreateDto.IsSuccess = false;
                await _sentMailService.AddAsync(sentMailCreateDto);
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }

            return sentMailCreateDto;
        }

        public async Task SendAfterExamMail(AfterExamMailDto afterExamMailDto)
        {
            TimeSpan timeElapsed = TimeSpan.FromSeconds(afterExamMailDto.TotalTimeSpent);
            string timeElapsedFormatted = timeElapsed.ToString(@"m\:ss");
            MailMessageDto message = new MailMessageDto(afterExamMailDto.Email, $"{afterExamMailDto.ExamName} adlı öğrencinin sınavı hakkında", $"{afterExamMailDto.StudentFullName} isimli öğrenci {afterExamMailDto.ExamName} sınavını {timeElapsedFormatted} sürede tamamlamıştır. Öğrenci notu: {afterExamMailDto.StudentPoint}");
            var result = new SentMailCreateDto()
            {
                Content = message.Content,
                Email = message.To,
                Subject = message.Subject,
                IsSuccess = false
            };
            try
            {
                await SendMail(message);
                result.IsSuccess = true;
                await _sentMailService.AddAsync(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                await _sentMailService.AddAsync(result);
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }
        }

        public async Task SendEmailToCandidateAdminNewExam(CandidateAdminNewExamMailDto candidateAdminNewExamMailDto)
        {
            var subject = candidateAdminNewExamMailDto.CandidateContents[0].Split("*?*")[0] + " Sınavı Oluşturuldu";
            var body = CreateHtmlBody(candidateAdminNewExamMailDto.CandidateContents);
            var candidateAdminMessage = new MailMessageDto(candidateAdminNewExamMailDto.CandidateAdminEmailAdress, subject, body);
            var result = new SentMailCreateDto()
            {
                Content = candidateAdminMessage.Content,
                Email = candidateAdminMessage.To,
                Subject = candidateAdminMessage.Subject,
                IsSuccess = false
            };
            try
            {
                if (candidateAdminNewExamMailDto == null)
                    throw new ArgumentNullException(nameof(candidateAdminNewExamMailDto), "CandidateAdminNewExamMailDto cannot be null.");
                
                if (candidateAdminNewExamMailDto.CandidateContents == null || candidateAdminNewExamMailDto.CandidateContents.Count == 0)
                    throw new ArgumentException("Candidate contents cannot be null or empty.", nameof(candidateAdminNewExamMailDto.CandidateContents));
               
                if (string.IsNullOrEmpty(candidateAdminNewExamMailDto.CandidateAdminEmailAdress))
                    throw new ArgumentException("Candidate admin email address cannot be null or empty.", nameof(candidateAdminNewExamMailDto.CandidateAdminEmailAdress));
               

                await SendMailWithHtml(candidateAdminMessage);
                result.IsSuccess = true;
                await _sentMailService.AddAsync(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                await _sentMailService.AddAsync(result);
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
                throw;
            }
        }

        public async Task SendEmailToCandidateAdminNewExamLink(CandidateAdminNewExamLinkMailDto candidateAdminNewExamLinkMailDto)
        {
            var subject = "Sınav Oluşturuldu";
            var body = candidateAdminNewExamLinkMailDto.ExamLink;
            var candidateAdminMessage = new MailMessageDto(candidateAdminNewExamLinkMailDto.CandidateAdminEmailAdress, subject, body);
            var result = new SentMailCreateDto()
            {
                Content = candidateAdminMessage.Content,
                Email = candidateAdminMessage.To,
                Subject = candidateAdminMessage.Subject,
                IsSuccess = false
            };
            try
            {
                if (candidateAdminNewExamLinkMailDto == null)
                {
                    throw new ArgumentNullException(nameof(candidateAdminNewExamLinkMailDto), "CandidateAdminNewExamLinkMailDto cannot be null.");
                }

                if (string.IsNullOrEmpty(candidateAdminNewExamLinkMailDto.CandidateAdminEmailAdress))
                {
                    throw new ArgumentException("Candidate admin email address cannot be null or empty.", nameof(candidateAdminNewExamLinkMailDto.CandidateAdminEmailAdress));
                }

                await SendMailWithHtml(candidateAdminMessage);
                result.IsSuccess = true;
                await _sentMailService.AddAsync(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                await _sentMailService.AddAsync(result);
                Console.WriteLine($"ArgumentNullException: {ex.Message}");
                throw;
            }
        }

        public async Task SendEmailToTrainerNewExam(TrainerNewExamMailDto trainerNewExamMailDto)
        {
            if (trainerNewExamMailDto.StudentContents != null && !string.IsNullOrEmpty(trainerNewExamMailDto.TrainerEmailAdress))
            {
                string subject = $"{trainerNewExamMailDto.StudentContents[0].Split("*?*")[0]} Sınavı Oluşturuldu";
                string htmlBody = CreateHtmlBody(trainerNewExamMailDto.StudentContents);

                MailMessageDto trainerMessage = new MailMessageDto(trainerNewExamMailDto.TrainerEmailAdress, subject, htmlBody);
                var result = new SentMailCreateDto()
                {
                    Content = trainerMessage.Content,
                    Email = trainerMessage.To,
                    Subject = trainerMessage.Subject,
                    IsSuccess = false
                };
                try
                {
                    await SendMailWithHtml(trainerMessage);
                    result.IsSuccess = true;
                    await _sentMailService.AddAsync(result);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    await _sentMailService.AddAsync(result);
                    throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
                }
            }
            else
            {
                throw new Exception("Öğrenci bilgisi bulunamadı veya eğitmen mail adresi eksik.");
            }
        }

        /// <summary>
        /// Öğrenciye kendi sınav notunun gönderilmesini sağlar.
        /// </summary>
        /// <param name="studentAssesmentMailDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<SentMailCreateDto> SendEmailToStudentAssessment(StudentAssesmentMailDto studentAssesmentMailDto)
        {
            string subject = $"{studentAssesmentMailDto.ExamName} Sınav Sonucu Değerlendirmesi";
            string body = string.Empty;

            if (studentAssesmentMailDto.Result != null)
            {

                body = ExamResultHtmlBody(studentAssesmentMailDto.Result);
            }
            else if (studentAssesmentMailDto.Result == null)
            {
                body = StudentTrainerAssesmentHtmlBody(studentAssesmentMailDto);
            }


            MailMessageDto message = new MailMessageDto(studentAssesmentMailDto.StudentEmailAddress, subject, body);

            SentMailCreateDto sentMailCreateDto;

            TypeAdapterConfig<MailMessageDto, SentMailCreateDto>.NewConfig()
    .Map(dest => dest.Email, src => src.To);

            try
            {
                await SendMailWithHtml(message);

                sentMailCreateDto = message.Adapt<SentMailCreateDto>();
                sentMailCreateDto.IsSuccess = true;
                await _sentMailService.AddAsync(sentMailCreateDto);
            }
            catch (Exception ex)
            {
                sentMailCreateDto = new SentMailCreateDto { IsSuccess = false };
                throw new Exception("Mail gönderiminde bir hata oluştu.", ex);
            }

            return sentMailCreateDto;
        }



        /// <summary>
        /// Eğitmelerin öğrencilerin sınavları için yaptığı değerlendirmeyi, ilgili öğrenciye mail atması için body haline getiren metod!
        /// </summary>
        /// <param name="studentTrainerAssesmentDTO"></param>
        /// <returns></returns>
        private string StudentTrainerAssesmentHtmlBody(StudentAssesmentMailDto studentTrainerAssesmentDTO)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<!DOCTYPE html>");
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>Assessment Result</title>");
            htmlBuilder.Append("<style>");
            htmlBuilder.Append("body { font-family: Arial, sans-serif; margin: 0; padding: 20px; background-color: #f4f4f4; }");
            htmlBuilder.Append(".container { margin: auto; background-color: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }");
            htmlBuilder.Append("h2 { color: #333; text-align: left; }");
            htmlBuilder.Append("p { line-height: 1.6; color: #555; text-align: left; }");
            htmlBuilder.Append("</style>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<div class='container'>");
            htmlBuilder.Append("<h2>Sınav Değerlendirme Sonucu</h2>");

            htmlBuilder.Append("<p><b>Sınav Adı:</b> ").Append(studentTrainerAssesmentDTO.ExamName).Append("</p>");
            htmlBuilder.Append("<p><b>Öğrenci E-posta Adresi:</b> ").Append(studentTrainerAssesmentDTO.StudentEmailAddress).Append("</p>");
            htmlBuilder.Append("<p><b>Değerlendirme:</b> ").Append(studentTrainerAssesmentDTO.Assessment).Append("</p>");
            htmlBuilder.Append("<p><b>Öğretmen Adı:</b> ").Append(studentTrainerAssesmentDTO.TrainerName).Append("</p>");

            htmlBuilder.Append("<p>Sınavınızla ilgili herhangi bir sorunuz olursa, lütfen öğretmeninizle iletişime geçin.</p>");

            htmlBuilder.Append("<p>Başarılar,</p>");
            htmlBuilder.Append("<p>BilgeAdam Değerlendirme Ekibi</p>");
            htmlBuilder.Append("</div>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            return htmlBuilder.ToString();
        }

        public async Task AllStudentEmail(AllStudentsEmailDto allStudents)
        {
            var emails = new List<string>();
            foreach (var student in allStudents.Students)
            {
                var email = await GetStudentEmailById(student.StudentId);
                emails.Add(email);
            }

            var resultStrings = allStudents.Students
                .Select(result => $"{result.StudentFullName}*?*{(result.Score.HasValue ? result.Score.ToString() : "Girmedi")}*?*{result.ExamName}")
                .ToList();

            string subject = "Sonuçlar";
            string body = ExamResultsHtmlBody(resultStrings);

            foreach (var email in emails)
            {
                await SendMailWithHtml(new MailMessageDto(email, subject, body));
            }
        }
    }

}