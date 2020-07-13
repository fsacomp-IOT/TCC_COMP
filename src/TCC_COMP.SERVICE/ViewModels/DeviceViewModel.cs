﻿namespace TCC_COMP.SERVICE.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DeviceViewModel
    {
        #region Controller

        public DeviceViewModel()
        {
            created_at = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #endregion

        #region Atributos

        public string id { get; set; }

        public string name { get; set; }

        public string created_at { get; set; }

        public string updated_at { get; set; }

        #endregion

        #region Relation

        public List<DeviceDataViewModel> DeviceData { get; set; }

        #endregion
    }
}