using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_COMP.SERVICE.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class DeviceViewModel
    {
        #region Controller

        public DeviceViewModel()
        {
            Device_Id = Guid.NewGuid();
            Created_At = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        #endregion

        #region Atributos

        public Guid Device_Id { get; set; }

        public string Device_Name { get; set; }

        public bool Connected { get; set; }

        public string Created_At { get; set; }

        public string Updated_At { get; set; }

        #endregion

        #region Relation

        public List<SensorViewModel> Sensors { get; set; }

        #endregion
    }
}
