using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SharpCmd.Attributes;
using SharpCmd.Exceptions;
using SharpCmd.Results;

namespace SharpCmd
{
    public class CommandExecutor<TContext, TMainController> where TContext : class where TMainController : CommandController<TContext>
    {
        private readonly TContext _context;

        public CommandExecutor(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ICommandResult> Execute(string commmandString)
        {
            if (commmandString == null)
                throw new ArgumentNullException(nameof(commmandString));

            return Execute(new Command(commmandString));
        }

        public async Task<ICommandResult> Execute(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var iterator = new CommandIterator(command);

            var currentController = (CommandController<TContext>)Activator.CreateInstance(typeof(TMainController), _context, iterator);

            while (iterator.HasNext)
            {
                string methodName = iterator.GetNext();
                ICommandResult result = await ExecuteControllerMethod(currentController, methodName, iterator).ConfigureAwait(false);

                if (result is ForwardResult forwardResult)
                {
                    var args = new List<object>();
                    args.Add(_context);
                    args.Add(iterator);
                    args.AddRange(forwardResult.ControllerArgs);

                    currentController = (CommandController<TContext>)Activator.CreateInstance(forwardResult.ControllerType, args.ToArray());
                }
                else
                {
                    return result;
                }
            }

            throw new IncompleteCommandException("Command iterator reached end but execution chain goes on.");
        }

        private Task<ICommandResult> ExecuteControllerMethod(CommandController<TContext> controller, string methodName, CommandIterator commandIterator)
        {
            MethodInfo method = controller.GetType().GetMethods().FirstOrDefault(m => m.GetCustomAttribute<CommandAttribute>(true)?.Command == methodName);
            if (method == null)
                throw new InvalidCommandException($"Controller {controller} has no handler for the {methodName} command.");

            var args = new List<object>();
            foreach (ParameterInfo parameter in method.GetParameters())
            {
                if (!commandIterator.HasNext)
                    throw new IncompleteCommandException("Command iterator reached end but command method requires argument.");

                args.Add(commandIterator.GetNext(parameter.ParameterType));
            }

            return (Task<ICommandResult>)method.Invoke(controller, args.ToArray());
        }
    }
}
