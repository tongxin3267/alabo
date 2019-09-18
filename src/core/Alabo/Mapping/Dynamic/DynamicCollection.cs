using System;
using System.Collections.Generic;

namespace Alabo.Mapping.Dynamic
{
    public class DynamicCollection<TModel> : List<Dynamic<TModel>> where TModel : class, new()
    {
        public void Apply(IList<TModel> deltas)
        {
            if (deltas == null) {
                throw new ArgumentNullException(nameof(deltas));
            }

            for (var i = 0; i < deltas.Count; i++) {
                this[i].SetValue(deltas[i]);
            }
        }
    }
}