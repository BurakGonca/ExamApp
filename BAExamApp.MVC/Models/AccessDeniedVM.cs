namespace BAExamApp.MVC.Models
{
    public class AccessDeniedVM
    {
        private string? returnUrl;
        private string? returnUrlName;

        /// <summary>
        /// Doğrudan html formatında yazılabilir. <p>Mesaj</p> şeklinde.
        /// </summary>
        public string? Message { get; set; }
        public string ToContactEmail { get; set; } = "examyonetici@gmail.com";
        public string ControllerName { get; set; } = "Home";
        public string ActionName { get; set; } = "Index";
        public string? AreaName { get; set; }
        /// <summary>
        /// anchor etiketinde ReturnUrl için yazdırılacak olan isim. "Sınava geri dön" şeklinde;
        /// </summary>
        public string? ReturnUrlName
        {
            get { return returnUrlName; }
            set
            {
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(returnUrl))
                {
                    throw new InvalidOperationException("ReturnUrlName doluyken ReturnUrl boş olamaz.");
                }
                returnUrlName = value;
            }
        }
        /// <summary>
        /// Kullanıcı eğer bir url'e gönderilmek isteniyorsa doldurulmalıdır.
        /// </summary>
        public string? ReturnUrl
        {
            get { return returnUrl; }
            set
            {
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(returnUrlName))
                {
                    throw new InvalidOperationException("ReturnUrl doluyken ReturnUrlName boş olamaz.");
                }
                returnUrl = value;
            }
        }

        
    }
}
