using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_COMP.DOMAIN.Entities
{
    public class Plant
    {
        public int id { get; set; }
        public string name { get; set; }
        public double air_temperature { get; set; }
        public double air_humidity { get; set; }
        public double soil_humidity { get; set; }
        public double solar_light { get; set; }
    }
}
