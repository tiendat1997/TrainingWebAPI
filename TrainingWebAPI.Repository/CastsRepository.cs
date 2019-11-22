using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebAPI.Entity;

namespace TrainingWebAPI.Repository
{
    [DataAccess]
    public class CastRepository : Repository<Cast>, ICastRepository
    {
        public CastRepository(DbContext context) : base(context)
        {
        }
    }

    public interface ICastRepository : IRepository<Cast>
    {
    }
}
