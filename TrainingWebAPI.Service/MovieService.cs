using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebAPI.Repository;
using TrainingWebAPI.Service.Models;

namespace TrainingWebAPI.Service
{
    public interface IMovieService
    {
        MovieModel GetMovieByName(string name);
    }

    [BusinessLogic]
    public class MovieService : IMovieService
    {
        private IMovieRepository movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public MovieModel GetMovieByName(string name)
        {
            var movie = movieRepository.FindBy(m => m.Name.Equals(name))
                           .Select(t => new MovieModel
                           {
                               Id = t.Id,
                               Name = t.Name
                           })
                           .FirstOrDefault();
            return movie;
        }
    }
}
