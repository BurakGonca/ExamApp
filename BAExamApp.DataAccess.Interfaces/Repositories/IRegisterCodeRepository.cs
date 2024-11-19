using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAExamApp.DataAccess.Interfaces.Repositories;

public interface IRegisterCodeRepository : IAsyncRepository, IAsyncInsertableRepository<RegisterCode>, IAsyncQueryableRepository<RegisterCode>, IAsyncDeleteableRepository<RegisterCode>, IAsyncFindableRepository<RegisterCode>, IAsyncUpdateableRepository<RegisterCode>, IAsyncOrderableRepository<RegisterCode>
{

}
