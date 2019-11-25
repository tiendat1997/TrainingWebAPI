using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TrainingWebAPI.WebAPI.Common;
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
            string layer = TracingLayer.BUSINESS_LOGIC.ToString();
            LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2} -> {3}", layer, LogMessageStatus.INVOKE, input.Target.ToString(), input.MethodBase.Name));
            var methodReturn = getNext().Invoke(input, getNext);
            if (methodReturn.Exception == null)
            {
                LogHandler.LogMessage(TracingLevel.INFO, string.Format("[{0}:{1}] {2} -> {3}", layer, LogMessageStatus.EXECUTE_SUCCESSFULLY, input.Target.ToString(), input.MethodBase.Name));
            }
            else
            {
                LogHandler.LogException(TracingLevel.ERROR, methodReturn.Exception);
            }

            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}