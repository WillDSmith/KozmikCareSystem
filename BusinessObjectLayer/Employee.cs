using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjectLayer
{
    [Table("Employee")]

    public class Employee
    {
        public Employee()
        {
            RoleId = 2;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
        [Key]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string EmployeeId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public DateTime DateUpdated { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
