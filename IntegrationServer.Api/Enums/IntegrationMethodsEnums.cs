using System.ComponentModel.DataAnnotations;

namespace IntegrationServer.Api.Enums
{
    public enum IntegrationMethodsEnums
    {
        [Display(Name = "Post")]
        PostMethod,
        [Display(Name = "Get")]
        GetMethod,
        [Display(Name = "Put")]
        PutMethod,
        [Display(Name = "Delete")]
        DeleteMethod
    }
}
