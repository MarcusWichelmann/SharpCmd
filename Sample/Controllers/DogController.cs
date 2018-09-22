using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpCmd;
using SharpCmd.Attributes;
using SharpCmd.Results;

namespace Sample.Controllers
{
    public class DogController : CommandController<SampleContext>
    {
        public DogController(SampleContext context, CommandIterator commandIterator, string dogName) : base(context, commandIterator) { }

        [Command("bark")]
        public Task<ICommandResult> BarkAsync(int count)
        {
            return Task.FromResult<ICommandResult>(new SuccessResult());
        }
    }
}
