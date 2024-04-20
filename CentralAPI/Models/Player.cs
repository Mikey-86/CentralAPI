using System.ComponentModel.DataAnnotations;

namespace CentralAPI.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string IDNumber { get; set; }
        public string Address { get; set; }
        public string DesiredTeam { get; set; }
    }
}
