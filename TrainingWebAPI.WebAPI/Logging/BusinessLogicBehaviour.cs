using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace TrainingWebAPI.WebAPI.Logging
{
    public class BusinessLogicBehaviour : IInterceptionBehavior
    {
        [Dependency]
        public LogHandler LogHandler {get;set;}
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {

            LogHandler.LogMessage(TracingLevel.INFO, TracingLayer.BUSINESS_LOGIC, String.Format("[{0}:{1}]", this.GetType().Name, "Invoke"));

            var methodReturn = getNext().Invoke(input, getNext);

            if (methodReturn.Exception == null)
            {
                LogHandler.LogMessage(TracingLevel.INFO, TracingLayer.BUSINESS_LOGIC, String.Format("Successfully finished {0} {1}", input.MethodBase.ToString(), input.Target.ToString()));
            }
            else
            {
                LogHandler.LogMessage(TracingLevel.ERROR, TracingLayer.BUSINESS_LOGIC, String.Format("Finished {0} with exception {1}: {2}", input.MethodBase.ToString(), methodReturn.Exception.GetType().Name, methodReturn.Exception.Message));
            }

            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}