namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sensor
    {
        #region Construtor

        public Sensor()
        {
            Sensor_id = Guid.NewGuid();
            Created_at = DateTime.Now;
        }

        #endregion

        #region Atributos

        [Key]
        public Guid Sensor_id { get; set; }

        public string Sensor_name { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Update_at { get; set; }

        #endregion

        #region Foreing_Keys

        public Guid Device_id { get; set; }

        public int Sensor_type_id { get; set; }

        public List<SensorEvent> Events { get; set; }

        #endregion

    }
}
