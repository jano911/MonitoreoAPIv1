using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MonitoreoAPIv1.Models
{
    [DataContract]
    [Serializable]
    public class Module
    {
        [DataMember]
        public int IdModule { get; set; }
        /// <summary>
        /// Property Description
        /// </summary>
        [DataMember]
        public String Description { get; set; }
        /// <summary>
        /// Property ModulePath
        /// </summary>
        [DataMember]
        public String ModulePath { get; set; }
        /// <summary>
        /// Property ModuleView
        /// </summary>
        [DataMember]
        public String ModuleView { get; set; }
        /// <summary>
        /// Property ParentId
        /// </summary>
        [DataMember]
        public int ParentId { get; set; }
        /// <summary>
        /// Property SortOrder
        /// </summary>
        [DataMember]
        public int SortOrder { get; set; }
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
        public Permiso Permiso { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public String Icono { get; set; }

        [DataMember]
        public String Viewname { get; set; }

        [DataMember]
        public bool Display { get; set; }


        [DataMember]
        public int tipo { get; set; }
    }
}