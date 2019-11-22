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
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {

        }
    }

    public interface IMovieRepository : IRepository<Movie>
    {
    }
}
