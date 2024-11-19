using Newtonsoft.Json;
using System.Text;

namespace BAExamApp.Business.Services;
public class ObjectConvertFormatService : IObjectConvertFormatService
{
    public T JsonToObject<T>(string jsonString) where T : class, new()
    {
        var objectData = JsonConvert.DeserializeObject<T>(jsonString);
        return objectData;
    }

    public string ObjectToJson<T>(T entityObject) where T : class, new()
    {
        var jsonString = JsonConvert.SerializeObject(entityObject);
        return jsonString;
    }

    public T ParseObjectDataArray<T>(byte[] rawBytes) where T : class, new()
    {
        var stringData = Encoding.UTF8.GetString(rawBytes);
        return JsonToObject<T>(stringData);
    }
}
