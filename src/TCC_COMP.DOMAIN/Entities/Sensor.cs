namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sensor
    {
        #region Atributos

        [Key]
        public Guid Sensor_Id { get; set; }

        public string Sensor_Name { get; set; }

        public DateTime Created_At { get; set; }

        public DateTime Update_At { get; set; }

        #endregion

        #region Foreing_Keys

        public Guid Device { get; set; }

        public int Sensor_Type { get; set; }

        public List<SensorEvent> Events { get; set; }

        #endregion

    }
}
