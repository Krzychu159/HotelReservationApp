using Microsoft.EntityFrameworkCore;

namespace HotelReservationApp.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            return !Reservations.Any(r => r.RoomId == roomId &&
                                          ((r.CheckInDate <= checkOutDate && r.CheckOutDate >= checkInDate)));
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public Room Room { get; set; }
    }
}
