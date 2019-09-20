using System;

namespace Alabo.Domains.Entities.Core
{
    public class AutoConfigBase : EntityCommon<AutoConfigBase, Guid>
    {
        public AutoConfigBase(Guid id) : base(id)
        {
        }

        public AutoConfigBase() : this(Guid.Empty)
        {
        }
    }
}