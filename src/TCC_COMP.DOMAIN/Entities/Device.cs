namespace TCC_COMP.DOMAIN.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Device
    {
        #region Construtor

        #endregion

        #region Atributos

        [Key]
        public string id { get; set; }

        public string name { get; set; }

        public DateTime created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public string connected { get; set; }

        #endregion

        #region Relations

        public string plant_id { get; set; }

        public DeviceData deviceData { get; set; }

        #endregion
    }
}
