using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectV_MVC.Models
{
    public class mvcDeviceModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mvcDeviceModel()
        {
            this.ValueRecs = new HashSet<mvcValuesRecModel>();
        }

        public short DeviceID { get; set; }

        [Required(ErrorMessage = "Device Name cannot be empty")]
        [DisplayName("Device Name")]
        public string DName { get; set; }

        [Required(ErrorMessage = "Device Create Date cannot be empty")]
        [DisplayName("Device Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Please assign to a Project")]
        [Range(1, int.MaxValue, ErrorMessage = "Please assign to a Project")]
        [DisplayName("Assigned Project")]
        public short AssignedProj { get; set; }

        public string Username { get; set; }

        public mvcProjectsModel Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mvcValuesRecModel> ValueRecs { get; set; }
    }
}