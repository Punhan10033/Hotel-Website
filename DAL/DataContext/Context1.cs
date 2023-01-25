using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CustomerDTO;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataContext
{
    public class Context1:DbContext
    {
        public Context1(DbContextOptions<Context1>options):base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomPictures> RoomPictures { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
