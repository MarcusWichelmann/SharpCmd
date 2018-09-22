using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd.Results
{
    /// <summary>
    /// Forwards the command to a next controller
    /// </summary>
    public class ForwardResult : ICommandResult
    {
        public Type ControllerType { get; }

        public object[] ControllerArgs { get; }

        public ForwardResult(Type controllerType, params object[] controllerArgs) : this(controllerType)
        {
            ControllerArgs = controllerArgs ?? throw new ArgumentNullException(nameof(controllerArgs));
        }

        public ForwardResult(Type controllerType)
        {
            ControllerType = controllerType ?? throw new ArgumentNullException(nameof(controllerType));
        }
    }
}
