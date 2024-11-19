namespace BAExamApp.Business.Interfaces.Services;
public interface IObjectConvertFormatService
{
    // RabbitMQ queue da veriyi byte[] tipinde saklamaktadır.
    // Kuyrukta (Queue) bu veriyi gönderebilmemiz için tip dönüşümüne gerek duyarız.

    T JsonToObject<T>(string jsonString) where T : class, new();
    string ObjectToJson<T>(T entityObject) where T : class, new();
    T ParseObjectDataArray<T>(byte[] rawBytes) where T : class, new();
}
