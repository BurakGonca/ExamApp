using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.Dtos.Users;
public class UserListDto:UserRoleAssingDto
{
    public string ID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string IdentityId { get; set; }
    public List<UserRoleAssingDto> UserRoles { get; set; }
}
