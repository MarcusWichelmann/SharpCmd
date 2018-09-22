using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DefaultCommandAttribute : Attribute
    {
    }
}
