using BAExamApp.Entities.Enums;
using System.Text;

namespace BAExamApp.MVC.Extensions;

public static class FormFileExtensions
{
    public static async Task<string> FileToStringAsync(this IFormFile formFile)
    {
        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream);
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static async Task<byte[]> FileToByteArrayAsync(this IFormFile formFile)
    {
        using MemoryStream memoryStream = new();
        await formFile.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public static async Task<string> DocumentFileToStringAsync(this IFormFile formFile)
    {
        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            return Encoding.UTF8.GetString(bytes);
        }
    }
    public static bool TryParseQuestionType(string value, out QuestionType questiontype)
    {
        if (string.Equals(value, "MultipleAnswer", StringComparison.OrdinalIgnoreCase))
        {
            questiontype = QuestionType.MultipleAnswer;
            return true;
        }
        else if (string.Equals(value, "Test", StringComparison.OrdinalIgnoreCase))
        {
            questiontype = QuestionType.Test;
            return true;
        }
        else if (string.Equals(value, "TrueFalse", StringComparison.OrdinalIgnoreCase))
        {
            questiontype = QuestionType.TrueFalse;
            return true;
        }

        questiontype = default;
        return false;
    }

    
}