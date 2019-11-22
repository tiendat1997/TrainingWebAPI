using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWebAPI.Repository;

namespace TrainingWebAPI.Service
{
    public interface ICastService
    {
        void DeleteCastsByActorId(int actorId);
    }

    [BusinessLogic]
    public class CastService : ICastService
    {
        private ICastRepository castRepository;
        public CastService(ICastRepository castRepository)
        {
            this.castRepository = castRepository;
        }
        public void DeleteCastsByActorId(int actorId)
        {
            castRepository.DeleteRange(c => c.ActorId == actorId);            
        }
    }
}
