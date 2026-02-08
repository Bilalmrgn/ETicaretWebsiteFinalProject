using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Exceptions
{
    public class IdentityOperationException : Exception
    {
        public IReadOnlyList<string> Errors { get; }

        public IdentityOperationException(IEnumerable<string> errors)
            : base("Identity operation failed")
        {
            Errors = errors.ToList();
        }

    }
}
