using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebAPI.Repository;
using TrainingWebAPI.Service.Models;

namespace TrainingWebAPI.Service
{
    public interface IActorService
    {
        List<ActorModel> GetActorsPlayedInMovie(string movieName);
        Task<List<ActorModel>> GetActorsPlayedInMovieAsync(string movieName);
        void UpdateActor(ActorModel actor);
        void AddNewActor(ActorModel actor);
        bool IsActorExisted(int id);
        void DeleteActor(int id);
    }

    [BusinessLogic]
    public class ActorService : IActorService
    {
        private IActorRepository actorRepository;
        private ICastRepository castRepository;
        private IMovieService movieService;
        public ActorService(IActorRepository actorRepository, IMovieService movieService, ICastRepository castRepository)
        {
            this.actorRepository = actorRepository;
            this.movieService = movieService;
            this.castRepository = castRepository;
        }

        public void AddNewActor(ActorModel actorModel)
        {
            var actor = new Entity.Actor
            {
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName,
                Gender = actorModel.Gender
            };
            actorRepository.Insert(actor);
        }

        public void DeleteActor(int id)
        {
            var actor = actorRepository.GetById(id);
            if (actor != null)
            {
                castRepository.DeleteRange(c => c.ActorId == id);
                actorRepository.Delete(actor);
            }
        }

        public async Task<List<ActorModel>> GetActorsPlayedInMovieAsync(string movieName)
        {
            List<ActorModel> actors = new List<ActorModel>();

            var movie = await Task.Run(() => movieService.GetMovieByName(movieName));
            if (movie != null)
            {
                actors = actorRepository
                                .GetActorsInMovie(movie.Id)
                                .Select(t => new ActorModel
                                {
                                    Id = t.Id,
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Gender = t.Gender
                                })
                                .ToList();               
            }
            return actors;
        }

        public List<ActorModel> GetActorsPlayedInMovie(string movieName)
        {
            List<ActorModel> actors = new List<ActorModel>();

            var movie = movieService.GetMovieByName(movieName);
            if (movie != null)
            {
                actors = actorRepository
                                .GetActorsInMovie(movie.Id)
                                .Select(t => new ActorModel
                                {
                                    Id = t.Id,
                                    FirstName = t.FirstName,
                                    LastName = t.LastName,
                                    Gender = t.Gender
                                })
                                .ToList();
            }

            return actors;
        }

        public bool IsActorExisted(int id)
        {
            bool isExisted = false;
            try
            {
                var existedEntity = actorRepository.GetById(id);
                if (existedEntity != null)
                {
                    isExisted = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isExisted;
        }

        public void UpdateActor(ActorModel actor)
        {
            try
            {
                var existedEntity = actorRepository.GetById(actor.Id);
                if (existedEntity != null)
                {
                    existedEntity.FirstName = actor.FirstName;
                    existedEntity.LastName = actor.LastName;
                    existedEntity.Gender = actor.Gender;
                    actorRepository.Update(existedEntity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
