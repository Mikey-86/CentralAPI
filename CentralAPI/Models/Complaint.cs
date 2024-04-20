using System.ComponentModel.DataAnnotations;

namespace CentralAPI.Models
{
    public class Complaint
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string ComplaintDetails { get; set; }
        public string IPAddress { get; set; }
        public string CreatedDate { get; set; }
    }
}
