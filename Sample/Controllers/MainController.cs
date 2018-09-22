using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpCmd;
using SharpCmd.Attributes;
using SharpCmd.Results;

namespace Sample.Controllers
{
    public class MainController : CommandController<SampleContext>
    {
        public MainController(SampleContext context, CommandIterator commandIterator) : base(context, commandIterator) { }

        [Command("dogs")]
        public Task<ICommandResult> GetDogControllerAsync(string dogName)
        {
            return Task.FromResult<ICommandResult>(new ForwardResult(typeof(DogController), dogName));
        }
    }
}
