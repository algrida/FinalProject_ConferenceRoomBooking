using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FinalProject_ConferenceRoomBooking.Data_Layer.Entities
{
    public class ConferenceRooms
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public int MaximumCapacity { get; set; }
    }
}
