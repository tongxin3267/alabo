using System.Collections.Generic;

namespace Alabo.Data.People.Circles.Client {

    public class CircleMapItem {
        public string Name { get; set; }

        public List<CircleCity> Cities { get; set; }
    }

    public class CircleCity {
        public string Name { get; set; }

        public List<CircleCounty> Counties { get; set; }
    }

    public class CircleCounty {
        public string Name { get; set; }

        public List<CircleItem> Circles { get; set; }
    }

    public class CircleItem {
        public string Name { get; set; }
    }
}