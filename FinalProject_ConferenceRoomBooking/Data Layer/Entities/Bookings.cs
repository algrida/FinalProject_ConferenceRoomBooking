using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class Bookings
{
    [Key]
    public string Code { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsDeleted { get; set; }
    public string ConferenceRoomCode { get; set; }
    public int Id { get; set; }

    [Required(ErrorMessage = "Start Date is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End Date is required.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Room ID is required.")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Number of People is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Number of People must be greater than 0.")]
    public int NumberOfPeople { get; set; }

    // Assuming Status is for booking status (confirmed, pending, etc.)
    [MaxLength(50)] // Adjust the length as needed
    public string Status { get; set; }

    public void GenerateBookingCode(string v)
    {
        DateTime currentDate = DateTime.UtcNow; // Use UtcNow for consistency
        string datePart = currentDate.ToString("yyyyMMdd");
        string startTimePart = StartDate.ToString("HHmm");
        string endTimePart = EndDate.ToString("HHmm");

        Code = $"{datePart}-{startTimePart}-{endTimePart}-{ConferenceRoomCode}";
    }

    public ReservationHolder ReservationHolder { get; set; }
    public int ReservationHolderId { get; set; }
    public string Notes { get; set; }
}
