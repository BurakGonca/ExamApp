using Microsoft.AspNetCore.Mvc;

namespace BAExamApp.MVC.Controllers;
public class ErrorPageController : BaseController
{
    public IActionResult ErrorIndex(int code)
    {

        string errorMessage;
        string statusCode;

        switch (code)
        {
            case 400:
                errorMessage = "İstek hatalı veya geçersiz...";
                statusCode = $"{code}";
                break;
            case 401:
                errorMessage = "Kimlik doğrulama gerekiyor...";
                statusCode = $"{code}"; 
                break;
            case 403:
                errorMessage = "Bu kaynağa erişim izniniz yok...";
                statusCode = $"{code}"; 
                break;
            case 404:
                errorMessage = "Sayfa bulunamadı...";
                statusCode = $"{code}";
                break;
            case 500:
                errorMessage = "Sunucu hatası oluştu...";
                statusCode = $"{code}";
                break;
            // Diğer durumlar için gerekli case'leri ekleyebilirsiniz.
            default:
                errorMessage = "Bilinmeyen bir hata oluştu...";
                statusCode = $"Bilinmeyen Hata ({code})";
                break;
        }

        ViewBag.ErrorMessage = errorMessage;
        ViewBag.StatusCode = statusCode;

        return View();
    }
}
