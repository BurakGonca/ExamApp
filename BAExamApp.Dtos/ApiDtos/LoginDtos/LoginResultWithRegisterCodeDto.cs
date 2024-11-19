using BAExamApp.Core.Utilities.Results;
using BAExamApp.Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.ApiDtos.LoginDtos;
public class LoginResultWithRegisterCodeDto
{
    public string Code { get; set; }
    public string Message { get; set; }
}
