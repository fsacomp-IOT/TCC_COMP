namespace TCC_COMP.SERVICE.ViewModels
{
    using System;

    public class SensorEventViewModel
    {
        #region Atributos

        public int event_id { get; set; }
        public double value { get; set; }
        public DateTime created_at { get; set; }

        #endregion

        #region Relations

        public Guid sensor_id { get; set; }

        #endregion
    }
}
