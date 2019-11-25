using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Extension;

namespace TrainingWebAPI.WebAPI.Logging
{
    public class LogCreation : UnityContainerExtension
    {
        protected override void Initialize()
        {
            LayoutRenderer.Register("elapsed-time", typeof(ElapsedTimeLayoutRenderer)); //dynamic
        }
    }
}