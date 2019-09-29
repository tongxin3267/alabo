using System.Collections.Generic;
using Alabo.Industry.Cms.Articles.Domain.Entities;

namespace Alabo.Industry.Cms.Articles.Domain.Dto
{
    public class ManualOutput
    {
        public long RelationId { get; set; }

        public string RelationName { get; set; }

        public List<Article> Article { get; set; }
    }
}