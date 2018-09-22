using System;
using Sample.Controllers;
using SharpCmd;
using SharpCmd.Results;

namespace Sample
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var context = new SampleContext();
            var executor = new CommandExecutor<SampleContext, MainController>(context);

            string input;
            while (!(input = Console.ReadLine()).Equals("exit", StringComparison.CurrentCultureIgnoreCase))
            {
                ICommandResult result = executor.Execute(input).Result;
            }
        }
    }
}
