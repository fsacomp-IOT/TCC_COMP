namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DeviceData
    {
        #region Atributos

        public int id { get; set; }

        public double soil_humidity { get; set; }

        public double air_humidity { get; set; }

        public double air_temperature { get; set; }

        public double solar_light { get; set; }

        public DateTime created_at { get; set; }

        #endregion

        #region Relations

        public string device_id { get; set; }

        #endregion

    }
}
