using System.Collections.Generic;

namespace Alabo.Industry.Cms.Articles.Domain.Dto {

    public class ManualOutput {
        public long RelationId { get; set; }

        public string RelationName { get; set; }

        public List<Domain.Entities.Article> Article { get; set; }
    }
}