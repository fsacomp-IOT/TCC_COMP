using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_COMP.SERVICE.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class SensorViewModel
    {

        #region Atributos

        public Guid sensor_id { get; set; }
        public string sensor_name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }

        #endregion

        #region Foreing_Keys

        public Guid device_id { get; set; }
        public Guid sensor_type_id { get; set; }
        public List<SensorEventViewModel> events { get; set; }

        #endregion

    }
}
