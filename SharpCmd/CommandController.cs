using System;
using SharpCmd.Results;

namespace SharpCmd
{
    public abstract class CommandController<TContext> where TContext : class
    {
        protected TContext Context { get; }

        protected CommandIterator CommandIterator { get; }

        protected Command Command => CommandIterator.Command;

        protected CommandController(TContext context, CommandIterator commandIterator)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            CommandIterator = commandIterator ?? throw new ArgumentNullException(nameof(commandIterator));
        }
    }
}