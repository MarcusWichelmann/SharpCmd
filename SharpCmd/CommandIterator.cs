using System;
using System.Collections.Generic;
using System.Text;
using SharpCmd.Exceptions;

namespace SharpCmd
{
    public class CommandIterator
    {
        public Command Command { get; }

        private string[] CommandParts => Command.CommandParts;

        public bool HasNext => _partIndex < CommandParts.Length;

        private int _partIndex = 0;

        public CommandIterator(Command command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public string GetNext()
        {
            if (!HasNext)
                throw new InvalidOperationException("Command has no more parts.");
            return CommandParts[_partIndex++];
        }

        public TValue GetValue<TValue>() => (TValue)GetNext(typeof(TValue));

        public object GetNext(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            string value = GetNext();
            if (type == typeof(string))
                return value;
            else if (type == typeof(sbyte))
                return sbyte.Parse(value);
            else if (type == typeof(byte))
                return byte.Parse(value);
            else if (type == typeof(char))
                return char.Parse(value);
            else if (type == typeof(uint))
                return uint.Parse(value);
            else if (type == typeof(int))
                return int.Parse(value);
            else if (type == typeof(ushort))
                return ushort.Parse(value);
            else if (type == typeof(short))
                return short.Parse(value);
            else if (type == typeof(ulong))
                return ulong.Parse(value);
            else if (type == typeof(long))
                return long.Parse(value);
            else if (type == typeof(bool))
                return bool.Parse(value);
            else if (type == typeof(float))
                return float.Parse(value);
            else if (type == typeof(double))
                return double.Parse(value);
            else if (type == typeof(decimal))
                return decimal.Parse(value);
            else
                throw new InvalidCommandException($"Cannot parse command part as {type}. Unsupported type.");
        }
    }
}
