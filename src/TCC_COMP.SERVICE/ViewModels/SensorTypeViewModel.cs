namespace TCC_COMP.SERVICE.ViewModels
{
    using System;

    public class SensorTypeViewModel
    {
        
        public SensorTypeViewModel()
        {
            Created_At = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #region Atributos

        public int Sensor_Type_Id { get; set; }
        public string Tipo { get; set; }
        public string Unitofmeasurement { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public bool Active { get; set; }

        #endregion

    }
}
