using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Apps;

namespace ZKCloud.Web.Apps.Demo01 {
    public class App : AppBase {
        public override string Name {
            get {
                return "demo01";
            }
        }

        public override string Description {
            get {
                return "demo02 app description";
            }
        }
    }
}
