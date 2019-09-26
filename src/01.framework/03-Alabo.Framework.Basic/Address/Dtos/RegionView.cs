using System.Collections.Generic;

namespace Alabo.Framework.Basic.Relations.Domain.Entities {

    public class RegionView {
        public string Label { get; set; }

        public string Value { get; set; }

        public List<RegionView> RegionViews { get; set; }
    }

    public class RegionProvince {
        public string Label { get; set; }

        public string Value { get; set; }

        public List<RegionCity> RegionCitys { get; set; }
    }

    public class RegionCity {
        public string Label { get; set; }

        public string Value { get; set; }

        public List<RegionArea> RegionAreas { get; set; }
    }

    public class RegionArea {
        public string Label { get; set; }

        public string Value { get; set; }

        public List<RegionView> RegionViews { get; set; }
    }
}