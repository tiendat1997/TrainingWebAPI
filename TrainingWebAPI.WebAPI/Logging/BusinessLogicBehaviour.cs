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

            LogHandler.LogMessage(TracingLevel.INFO, String.Format("[{0}] {1}:{2}", TracingLayer.BUSINESS_LOGIC.ToString(), input.Target.ToString(), "Invoke"));

            var methodReturn = getNext().Invoke(input, getNext);

            if (methodReturn.Exception == null)
            {
                LogHandler.LogMessage(TracingLevel.INFO, String.Format("[{0}] {1}:{2}", TracingLayer.BUSINESS_LOGIC.ToString(), input.MethodBase.ToString(), "Execute Succesfully"));
            }
            else
            {
                LogHandler.LogMessage(TracingLevel.ERROR, String.Format("Exception occurs [{0}] {1}", input.MethodBase.ToString(), input.Target.ToString()), methodReturn.Exception);
            }

            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}