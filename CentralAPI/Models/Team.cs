using System.ComponentModel.DataAnnotations;

namespace CentralAPI.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
