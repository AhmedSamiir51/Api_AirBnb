using AirbnbCRUD.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirbnbCRUD.Services
{
    public interface IBooking
    {
        List<Booking> GetAllBookings();
        List<Booking> GetAllBookingsByPersonId(int PersonId);
        List<Booking> GetAllBookingByHouseId(int HouseId);
        Booking GetBooking(int BookingId);
        Booking CreateBooking(Booking book);
        Booking EditBooking(Booking book);
        void DeleteBooking(int BookingId);
        void DeleteAllBookingByHouseId(int HouseId);
        void DeleteAllBooingByPersonId(int PersonId);
        public bool BookingExists(int id);
    }
    public class BookingInjection:IBooking
    {
        private readonly ApplicationContext _context;
        public BookingInjection(ApplicationContext context)
        {
            _context = context;
        }

        public Booking CreateBooking(Booking book)
        {
            _context.Bookings.Add(book);
            _context.SaveChanges();
            return book;
        }

        public void DeleteAllBooingByPersonId(int PersonId)
        {
            var books = GetAllBookingsByPersonId(PersonId);
            foreach(var book in books)
            {
                _context.Bookings.Remove(book);
            }
            _context.SaveChanges();
        }

        public void DeleteAllBookingByHouseId(int HouseId)
        {
            var books = GetAllBookingByHouseId(HouseId);
            foreach(var book in books)
            {
                _context.Bookings.Remove(book);
            }
            _context.SaveChanges();
        }

        public void DeleteBooking(int BookingId)
        {
            var book = GetBooking(BookingId);
            _context.Bookings.Remove(book);
            _context.SaveChanges();
        }

        public Booking EditBooking(Booking book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
            return book;
        }

        public List<Booking> GetAllBookingByHouseId(int HouseId)
        {
            var books = _context.Bookings.Where(a => a.HouseId == HouseId).ToList();
            return books;
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.ToList();
        }

        public List<Booking> GetAllBookingsByPersonId(int PersonId)
        {
            return _context.Bookings.Where(a => a.PersonId == PersonId).ToList();
        }

        public Booking GetBooking(int BookingId)
        {
            return _context.Bookings.Find(BookingId);
        }
        public bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
