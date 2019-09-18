using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alabo.Core.Files;

namespace Alabo.Test.Generation {
    public class Book {

        [Fact]
        public void Import()
        {
            var files = FileHelper.GetFileList();
        }
    }
}
