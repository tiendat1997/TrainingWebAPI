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
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Actor> GetActorsInMovie(int movieId)
        {
            var actors = entity.Where(a => a.Casts.Any(c => c.MovieId == movieId)).ToList();
            return actors;
        }
    }

    public interface IActorRepository : IRepository<Actor>
    {
        IEnumerable<Actor> GetActorsInMovie(int movieId);
    }
}
