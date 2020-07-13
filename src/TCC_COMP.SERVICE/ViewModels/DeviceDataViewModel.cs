namespace TCC_COMP.SERVICE.ViewModels
{
    using System;

    public class DeviceDataViewModel
    {

        public DeviceDataViewModel()
        {
            created_at = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #region Atributos

        public int id { get; set; }

        public double soil_humidity { get; set; }

        public double air_humidity { get; set; }

        public double air_temperature { get; set; }

        public double solar_light { get; set; }

        public string created_at { get; set; }

        #endregion

        #region Relations

        public string device_id { get; set; }

        #endregion
    }
}
