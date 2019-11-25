using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TrainingWebAPI.Service;
using TrainingWebAPI.Service.Models;
using TrainingWebAPI.WebAPI.Filters;
using TrainingWebAPI.WebAPI.Logging;
using TrainingWebAPI.WebAPI.Ultilities;

namespace TrainingWebAPI.WebAPI.Controllers
{
    [LogActionWebApiFilter]
    [RoutePrefix("api/actors")]
    public class ActorsController : ApiController
    {
        private IActorService actorService;
        public ActorsController(IActorService actorService)
        {
            this.actorService = actorService;
        }
        [HttpGet]
        [Route("async/search")]
        [ResponseType(typeof(List<ActorModel>))]
        public async Task<IHttpActionResult> GetActorsPlayedInMovieAsync(string movieName)
        {
            List<ActorModel> list = new List<ActorModel>();
            list = await actorService.GetActorsPlayedInMovieAsync(movieName);
            return Ok(list);
        }

        [HttpGet]
        [Route("search")]
        [ResponseType(typeof(List<ActorModel>))]
        public IHttpActionResult GetActorsPlayedInMovie(string movieName)
        {
            List<ActorModel> list = new List<ActorModel>();
            list = actorService.GetActorsPlayedInMovie(movieName);

            return Ok(list);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteActor(int id)
        {
            try
            {
                bool isExisted = actorService.IsActorExisted(id);
                if (isExisted == false)
                {
                    return BadRequest("Actor is not existed");
                }
                actorService.DeleteActor(id);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            return new MyMessageResult("Delete Actor Successfully", Request);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateActor(int id, ActorModel actor)
        {
            try
            {
                if (id != actor.Id || ModelState.IsValid == false)
                {
                    return BadRequest();
                }
                actorService.UpdateActor(actor);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            finally
            {

            }

            return new MyMessageResult("Update Actor Successfully", Request);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddNewActor(ActorModel actor)
        {
            try
            {
                if (actor == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                actorService.AddNewActor(actor);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
            return new MyMessageResult("New Actor Successfully", Request);
        }

    }
}
