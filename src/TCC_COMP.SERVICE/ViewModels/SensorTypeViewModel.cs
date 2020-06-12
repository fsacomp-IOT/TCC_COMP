namespace TCC_COMP.SERVICE.ViewModels
{
    using System;

    public class SensorTypeViewModel
    {
        
        public SensorTypeViewModel()
        {
            created_at = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #region Atributos

        public int sensor_type_id { get; set; }
        public string tipo { get; set; }
        public string unitofmeasurement { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public bool active { get; set; }

        #endregion

    }
}
