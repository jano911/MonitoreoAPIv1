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
    public class UsuarioM
    {
        #region Attributes

        /// <summary>
        /// Property IdUsuario
        /// </summary>
        [DataMember]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Property IdRol
        /// </summary>
        [DataMember]
        public int IdRol { get; set; }

        /// <summary>
        /// Property Nombre
        /// </summary>
        [DataMember]
        public String Nombre { get; set; }

        /// <summary>
        /// Property Email
        /// </summary>
        [DataMember]
        public String Email { get; set; }

        /// <summary>
        /// Property Usuario
        /// </summary>
        [DataMember]
        public String Usuario { get; set; }

        /// <summary>
        /// Property Password
        /// </summary>
        [DataMember]
        public String Password { get; set; }

        /// <summary>
        /// Property Estado
        /// </summary>
        [DataMember]
        public bool Estado { get; set; }


        [DataMember]
        public Role Role { get; set; }


        [DataMember]
        public String Token { get; set; }


        [DataMember]
        public Permiso Permiso { get; set; }


        [DataMember]
        public List<Permiso> permiso2 { get; set; }


        [DataMember]
        public List<Menu> Menu { get; set; }



        [DataMember]
        public int Op { get; set; }


        [DataMember]
        public String Usuario2 { get; set; }

        [DataMember]
        public String NombreRol { get; set; }



        [DataMember]
        public int Bnd { get; set; }

        [DataMember]
        public String Msg { get; set; }

        #endregion
    }
}