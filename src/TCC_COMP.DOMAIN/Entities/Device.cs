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
        public Guid Device_id { get; set; }

        public string Device_name { get; set; }

        public bool Connected { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        #endregion

        #region Relation

        public List<Sensor> Sensors { get; set; }

        #endregion
    }
}
