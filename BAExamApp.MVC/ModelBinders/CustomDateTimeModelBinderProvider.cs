using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BAExamApp.MVC.ModelBinders;

public class CustomDateTimeModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DateTime))
        {
            return new CustomDateTimeModelBinder();
        }

        return null;
    }
}
