namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SensorType
    {
        #region Atributos

        [Key]
        public int Sensor_type_id { get; set; }

        public string Tipo { get; set; }

        public string Unitofmeasurement { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public bool Active { get; set; }

        #endregion

    }
}
