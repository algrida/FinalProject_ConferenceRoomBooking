using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject_ConferenceRoomBooking.Data;
using FinalProject_ConferenceRoomBooking.Data_Layer.Entities;
using FinalProject_ConferenceRoomBooking.Services;
using FinalProject_ConferenceRoomBooking.Services.Interfaces;
using FinalProject_ConferenceRoomBooking.ViewModels;

namespace FinalProject_ConferenceRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public IActionResult UpdateBooking(Bookings updatedBooking)
        {
            try
            {
                // Ensure _bookingService is not null and properly injected
                if (_bookingService != null)
                {
                    _bookingService.UpdateBooking(updatedBooking);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where _bookingService is not properly injected
                    return RedirectToAction("Index");
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Handle unauthorized access (e.g., show an error message)
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return RedirectToAction("Index");
            }
        }
         [HttpPost]
    public IActionResult SoftDeleteReservationHolder(int id)
    {
        try
        {
            _bookingService.SoftDeleteReservationHolder(id);
            return RedirectToAction("Index");
        }
        catch (UnauthorizedAccessException)
        {
            return RedirectToAction("Index");
        }
        catch (InvalidOperationException ex)
        {
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index");
        }
    }


    }
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return _context.Bookings != null ?
                        View(await _context.Bookings.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Bookings'  is null.");
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Code == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,IsConfirmed,isDeleted,ConferenceRoomCode,Id,StartDate,EndDate,RoomId,NumberOfPeople")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Code,IsConfirmed,isDeleted,ConferenceRoomCode,Id,StartDate,EndDate,RoomId,NumberOfPeople")] Bookings bookings)
        {
            if (id != bookings.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.Code))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.Code == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bookings'  is null.");
            }
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(string id)
        {
            return (_context.Bookings?.Any(e => e.Code == id)).GetValueOrDefault();
        }

        public IActionResult Create(BookingViewModel model, Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                // Map data from the view model to the Booking entity
                var newBooking = new Bookings
                {
                    Code = model.Code,
                    NumberOfPeople = model.NumberOfPeople,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    RoomId = model.RoomId,
                    // Set other properties as needed
                };

                // Add the new booking to the context
                _context.Bookings.Add(newBooking);

                // Save changes to the database
                _context.SaveChanges();

                var reservationHolder = new ReservationHolder
                {
                    // Set properties from the model
                };
                bookings.ReservationHolder = reservationHolder;

                // Redirect to a success page or another action
                return RedirectToAction("Index");
            }

            // If model state is not valid, return to the create view with errors
            return View(model);
        }

    }

}