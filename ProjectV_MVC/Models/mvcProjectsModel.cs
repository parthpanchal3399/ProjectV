using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectV_MVC.Models
{
    public class mvcProjectsModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcProjectsModel()
        {
            this.Devices = new HashSet<mvcDeviceModel>();
        }

        public short ProjectID { get; set; }

        [Required(ErrorMessage = "Project Name cannot be empty")]
        [DisplayName("Project Name")]
        public string PName { get; set; }


        [Required(ErrorMessage = "Project Create Date cannot be empty")]
        [DisplayName("Project Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreateDate { get; set; }

        public string Username { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcDeviceModel> Devices { get; set; }
    }
}