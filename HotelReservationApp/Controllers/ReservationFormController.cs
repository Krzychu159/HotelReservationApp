using Microsoft.AspNetCore.Mvc;
using HotelReservationApp.Models;

namespace HotelReservationApp.Controllers
{
    public class ReservationFormController : Controller
    {
        private readonly HotelContext _context;

        public ReservationFormController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int roomId, string fullName, string email, DateTime checkInDate, DateTime checkOutDate)
        {
            if (!_context.IsRoomAvailable(roomId, checkInDate, checkOutDate))
            {
                ModelState.AddModelError("", "The selected room is not available for the chosen dates.");
            }

            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    RoomId = roomId,
                    FullName = fullName,
                    Email = email,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate
                };

                _context.Reservations.Add(reservation);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }
    }
}
