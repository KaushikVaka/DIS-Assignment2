using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommerceWebApplication.Models
{
    public class Operator
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        
        public string LicenseId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }


        public int LicenseTypeId { get; set; }
        public int CityId { get; set; }
        [ForeignKey("LicenseTypeId")]
        public LicenseType LicenseType { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
