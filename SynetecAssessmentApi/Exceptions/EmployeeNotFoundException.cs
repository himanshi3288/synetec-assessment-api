using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Exceptions
{
    /// <summary>
    /// Exception is raised, when an employee is not found in the system.
    /// </summary>
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException()
        {
        }

        public EmployeeNotFoundException(string message) : base(message)
        {
        }

        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmployeeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
