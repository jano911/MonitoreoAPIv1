using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MonitoreoAPIv1.Models
{
    [DataContract]
    [Serializable]
    public class Permiso
    {
        #region Attributes

        /// <summary>
        /// Property IdRol
        /// </summary>
        [DataMember]
        public int IdRol { get; set; }
        /// <summary>
        /// Property IdModule
        /// </summary>
        [DataMember]
        public int IdModule { get; set; }
        /// <summary>
        /// Property OptAdd
        /// </summary>
        [DataMember]
        public bool OptAdd { get; set; }
        /// <summary>
        /// Property OptSelect
        /// </summary>
        [DataMember]
        public bool OptSelect { get; set; }
        /// <summary>
        /// Property OptUpdate
        /// </summary>
        [DataMember]
        public bool OptUpdate { get; set; }
        /// <summary>
        /// Property OptDelete
        /// </summary>
        [DataMember]
        public bool OptDelete { get; set; }

        [DataMember]
        public Module Module { get; set; }

        [DataMember]
        public Role Role { get; set; }

        #endregion
    }
}