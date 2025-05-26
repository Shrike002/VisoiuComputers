using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisoiuComputers.Core.Exceptions
{
    public class ComponentNotFoundException : Exception
    {
        public ComponentNotFoundException(int componentId)
            : base($"Component with ID {componentId} not found")
        {
        }
    }
}