using CentralAPI.Data;
using CentralAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        // DB access
        private readonly dbContext _context;

        public ComplaintsController(dbContext context)
        {
            _context = context;
        }

        // Methods
        [HttpPost]
        public IActionResult PostComplaint([FromBody] ComplaintDTO complaintObj)
        {
            if (ModelState.IsValid)
            {
                if (complaintObj != null)
                {
                    //Making object to store
                    Complaint complaint = new Complaint();
                    complaint.FirstName = complaintObj.FirstName;
                    complaint.LastName = complaintObj.LastName;
                    complaint.MobileNumber = complaintObj.MobileNumber;
                    complaint.Email = complaintObj.Email;
                    complaint.IPAddress = complaintObj.IPAddress;
                    complaint.CreatedDate = complaintObj.CreatedDate;
                    complaint.ComplaintDetails = complaintObj.ComplaintDetails;

                    _context.Complaints.Add(complaint);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
        {
            var complaintsResponse = _context.Complaints;
            
            return Ok(complaintsResponse);
        }

        //[Authorize]
        [HttpGet("{complaintId}")]
        [Route("api/[controller]/{complaintId}")]
        public IActionResult GetComplaints(int complaintId)
        {
            var complaintsResponse = _context.Complaints.FirstOrDefault(x => x.Id == complaintId);
            return Ok(complaintsResponse);
        }
    }

}
