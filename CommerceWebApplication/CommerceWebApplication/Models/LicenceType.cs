using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceWebApplication.Models
{
    public class LicenseType
    {
        [Key]
        public int Id { get; set; }
        public string LicenseTypeName { get; set; }
    }
}
