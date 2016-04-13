using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Domain.Repositories {
    public class RepositoryContextException : Exception {
        public RepositoryContextException(string message)
            : base(message) {
        }
    }
}
