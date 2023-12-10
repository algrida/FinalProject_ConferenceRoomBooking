using Microsoft.AspNetCore.Identity;

namespace FinalProject_ConferenceRoomBooking.Data_Layer.Entities
{

    namespace FinalProject_ConferenceRoomBooking.Data
    {
        public class ApplicationUser : IdentityUser
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public bool IsDeleted { get; set; }

            // You can include these properties from the base IdentityUser class
            public override string Id { get; set; }
            public override string Email { get; set; }
        }
    }
}
