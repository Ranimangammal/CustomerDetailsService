using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CustomerDetailsService.Models.Data
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [StringLength(512)]
        public string FirstName { get; set; }

        [StringLength(512)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public AddressEntity CurrentAddress { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
