namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SensorEvent
    {
        #region Atributos

        [Key]
        public int Event_id { get; set; }

        public double Valor { get; set; }

        public DateTime Created_at { get; set; }

        #endregion

        #region Relations

        public Guid Sensor_id { get; set; }

        #endregion

    }
}
