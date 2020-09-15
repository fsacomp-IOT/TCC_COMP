namespace TCC_COMP.SERVICE.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DeviceViewModel
    {
        #region Construtor

        public DeviceViewModel()
        {
            created_at = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            deviceData = new DeviceDataViewModel();
        }

        #endregion

        #region Atributos

        public string id { get; set; }

        public string name { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set; }

        public string connected { get; set; }

        #endregion

        #region Relation

        public string plant_id { get; set; }

        public DeviceDataViewModel deviceData { get; set; }

        #endregion
    }
}
