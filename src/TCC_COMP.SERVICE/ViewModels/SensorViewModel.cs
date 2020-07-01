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

        public Guid Sensor_Id { get; set; }
        public string Sensor_Name { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }

        #endregion

        #region Foreing_Keys

        public Guid Device { get; set; }
        public int Sensor_Type { get; set; }
        public List<SensorEventViewModel> Events { get; set; }

        #endregion

    }
}
