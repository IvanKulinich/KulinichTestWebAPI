using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebitelASP.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }
        [Required]
        [RegularExpression(@"[\d]{1,18}$", ErrorMessage = "Amount can't have any decimal places")]
        [Range(0, 1E+18)]
        public decimal Amount { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}