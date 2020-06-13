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
        public Guid Device_Id { get; set; }

        public string Device_Name { get; set; }

        public bool Connected { get; set; }

        public DateTime Created_At { get; set; }

        public DateTime? Updated_At { get; set; }

        #endregion

        #region Relation

        public List<Sensor> Sensors { get; set; }

        #endregion
    }
}
