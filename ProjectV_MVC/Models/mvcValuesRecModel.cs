using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectV_MVC.Models
{
    [Bind(Include = "DeviceID,CreateDate,RecordedVal")]
    public class mvcValuesRecModel
    {
        public short ValueID { get; set; }
        public short DeviceID { get; set; }

        [Required(ErrorMessage = "Value Creation Date cannot be empty")]
        [DisplayName("Value Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Recorded Value cannot be empty")]
        [DisplayName("Recorded Value")]
        public int RecordedVal { get; set; }

        public string Username { get; set; }

        public virtual mvcDeviceModel Device { get; set; }
    }
}