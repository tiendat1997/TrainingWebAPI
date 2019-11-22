using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebAPI.Service
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class BusinessLogicAttribute : Attribute
    {
        public double version;

        public BusinessLogicAttribute()
        {
            version = 1.0;
        }
    }
}
