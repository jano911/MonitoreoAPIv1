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
    public class Role
    {
        [DataMember]
        public int? IdRol { get; set; }

        [DataMember]
        public String Nombre { get; set; }

        [DataMember]
        public String Descripcion { get; set; }

        [DataMember]
        public bool? Estado { get; set; }

        [DataMember]
        public UsuarioM Usuario { get; set; }

        [DataMember]
        public Permiso Permiso { get; set; }

        [DataMember]
        public List<Module> Modulos { get; set; }


        [DataMember]
        public List<Permiso> LstPer { get; set; }

    }
}