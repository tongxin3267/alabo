using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Cms.Articles.Domain.Dto {

    public class ManualOutput {
        public long RelationId { get; set; }

        public string RelationName { get; set; }

        public List<Domain.Entities.Article> Article { get; set; }
    }
}