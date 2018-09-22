using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CommandAttribute : Attribute
    {
        public string Command { get; }

        public CommandAttribute(string command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }
    }
}
