using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MonitoreoAPIv1.Models
{
    [DataContract]
    [Serializable]
    public class Session
    {
        [DataMember]
        public long IdSession { get; set; }
        /// <summary>
        /// Property IdUsuario
        /// </summary>
        [DataMember]
        public int IdUsuario { get; set; }
        /// <summary>
        /// Property Token
        /// </summary>
        [DataMember]
        public String Token { get; set; }
        [DataMember]
        public UsuarioM Usuario { get; set; }
    }
}