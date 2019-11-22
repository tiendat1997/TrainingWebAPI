using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebAPI.Repository
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DataAccessAttribute : Attribute
    {
        public double version;

        public DataAccessAttribute()
        {
            version = 1.0;
        }
    }
}
