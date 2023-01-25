using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.IServices;
using DTO.CustomerDTO;
using Entities;
using HotelReception.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using System.IO;
using DAL.IRepository;
using DTO.RoomDto;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using Microsoft.AspNetCore.Identity;
using HotelReception.Areas.Identity.Data;
using DTO;
using System.Security.Claims;

namespace HotelReception.Controllers
{
    public class HotelReceptionController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICustomerService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRoomServices _roomservice;
        private readonly IReservationService _reservationservice;
        private readonly UserManager<ReceptionUser> _usermanager;

        public HotelReceptionController(UserManager<ReceptionUser> usermanager,IHttpContextAccessor contextAccessor,
                                IReservationService res, IRoomServices roomServices,
                                IWebHostEnvironment webHostEnvironment,
                                ICustomerService customerService)
        {
            _usermanager= usermanager;
            _contextAccessor=contextAccessor;
            _roomservice = roomServices;
            _service = customerService;
            _webHostEnvironment = webHostEnvironment;
            _reservationservice = res;
        }




        // CUSTOMERS
        [Authorize(Roles = "Administrator, Employee , SpuerAdmin")]
        public IActionResult Customers(int? page, string searchString)
        {
            ViewBag.BackgroundImage = "swimmingpool1.jpg";

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            var customers = _service.GetAsync(pageNumber, pageSize, searchString);

            return View(customers);
        }

        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public async Task<IActionResult> RegisterCustomer(int? Id)
        {
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            CustomerToAddOrUpdateDto customer = new CustomerToAddOrUpdateDto();
            if (Id != null)
            {
                customer = await _service.GetByIdAsync(Id);
            }
            return View(customer);
        }
        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public async Task<IActionResult> AddOrUpdateCustomer(int? Id, CustomerToAddOrUpdateDto customerToAddOrUpdateDto)
        {
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            var pins = await _service.GetPins();
            if (Id == null)
            {
                if (pins.Contains(customerToAddOrUpdateDto.PinCode.ToLower()))
                {
                    ModelState.AddModelError("PinCode", "Another user with this pincode is already in our database");
                    return View("RegisterCustomer", customerToAddOrUpdateDto);
                }
                else
                {
                    ViewBag.Btn = "AddCustomer";

                    customerToAddOrUpdateDto.PinCode.ToLower();
                    await _service.AddAsync(customerToAddOrUpdateDto);
                }
            }
            else
            {
                ViewBag.Btn = "Update Customer";
                customerToAddOrUpdateDto.CustomerId = Id;
                await _service.UpdateAsync(customerToAddOrUpdateDto);
                return View("Customers", _service.ByIdPaged(customerToAddOrUpdateDto.CustomerId));

            }
            return View("Customers", _service.GetLastRecord());
        }
        public async Task<IActionResult> Delete(int CustomerId, string confirm_value)
        {
            var reservations = await _reservationservice.GetReservationsAsync();
            int roomId;

            if (reservations.Contains(reservations.Where(x => x.CustomerId == CustomerId).FirstOrDefault()))
            {
                if (confirm_value == "Yes")
                {

                    roomId = reservations.Where(x => x.CustomerId == CustomerId).Single().RoomId;
                    roomId = reservations.Where(x => x.CustomerId == CustomerId).Single().RoomId;
                    var room = await _roomservice.GetByIdAsync(roomId);
                    await _roomservice.Update(room);
                    await _reservationservice.Delete(reservations.Where(x => x.RoomId == room.RoomId).Single().BookingId);
                    await _service.Delete(CustomerId);
                }
                else
                {

                }

                return RedirectToAction("Customers");

            }
            else
            {
                await _service.Delete(CustomerId);

            }
            return RedirectToAction("Customers");
        }
        // CUSTOMERS

        public IActionResult Index()
        {
            return View();
        }

        // ROOMS

        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]

        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var pics = await _roomservice.GetPicNames(Id);

            foreach (var pic in pics)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets2/forupload/", pic);

                if (System.IO.File.Exists(path))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(path);
                }
            }
            await _roomservice.DeleteAsync(Id);
            return RedirectToAction("Rooms");
        }


        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public async Task<IActionResult> AddingRoom(int? Id)
        {
            ViewBag.BackgroundImage = "349835.jpg";
            List<RoomTypeToListDto> roomtypes = await _roomservice.GetRoomTypesAsync();
            SelectList fortypes = new SelectList(roomtypes, "RoomTypeId", "RoomTypeName");
            ViewBag.Roomtypes = fortypes;
            List<Floor> floors = await _roomservice.GetFloorsAsync();
            SelectList floorss = new SelectList(floors, "FloorId", "FloorName");
            ViewBag.Floors = floorss;
            RoomToAddDto room = new RoomToAddDto();
            if (Id != null)
            {
                room = await _roomservice.GetByIdNullabel(Id);
            }

            return View(room);
        }

        //UpdateRoomPictures
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoom(int? Id, RoomToAddDto room, IFormFile[] RoomPictures)
        {
            var rooms = await _roomservice.GetAsync();
            var room2 = rooms.Select(x => x.RoomNumber).ToList();
            var roompics = await _roomservice.RoomPictures();

            ViewBag.BackgroundImage = "349835.jpg";
            if (Id == null)
            {
                if (room2.Contains(room.RoomNumber))
                {
                    List<RoomTypeToListDto> roomtypes = await _roomservice.GetRoomTypesAsync();
                    SelectList fortypes = new SelectList(roomtypes, "RoomTypeId", "RoomTypeName");
                    ViewBag.Roomtypes = fortypes;
                    List<Floor> floors = await _roomservice.GetFloorsAsync();
                    SelectList floorss = new SelectList(floors, "FloorId", "FloorName");
                    ViewBag.Floors = floorss;
                    ModelState.AddModelError("RoomNumber", "This room is already in our database.");
                    return View("AddingRoom", room);
                }
                else
                {
                    room.RoomPictures = new List<RoomPictures>();
                    foreach (IFormFile pic in RoomPictures)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets2/forupload/images", pic.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        await pic.CopyToAsync(stream);
                        //product.Photos.Add(photo.FileName);
                        room.RoomPictures.Add(new RoomPictures { Image = "images/".ToLower() + pic.FileName });
                        stream.Close();
                    }
                }
                await _roomservice.Add(room);
            }


            return RedirectToAction("Rooms");
        }

        [Authorize]

        public async Task<IActionResult> Rooms(int FloorId, int RoomTypeId)
        {
            ViewBag.BackgroundImage = "349835.jpg";
            List<RoomToListDto> rooms = await _roomservice.GetAsync();
            List<Floor> Floors = await _roomservice.GetFloorsAsync();
            ViewBag.FloorList = new SelectList(Floors, "FloorId", "FloorName");
            List<RoomTypeToListDto> roomtypes = await _roomservice.GetRoomTypesAsync();
            ViewBag.RoomTypeList = new SelectList(roomtypes, "RoomTypeId", "RoomTypeName");
            if (FloorId == 0 && RoomTypeId == 0)
            {
                return View(rooms);
            }
            else if (RoomTypeId == 0)
            {
                rooms = (from room in rooms where room.Floor.FloorId == FloorId select room).ToList();
                return View(rooms);
            }
            else if (FloorId == 0)
            {
                rooms = (from room in rooms where room.RoomType.RoomTypeId == RoomTypeId select room).ToList();
                return View(rooms);
            }
            else
            {
                rooms = (from room in rooms where room.Floor.FloorId == FloorId && room.RoomType.RoomTypeId == RoomTypeId select room).ToList();
                return View(rooms);
            }
        }

        // ROOMS

        //RESERVATION


        public async Task<IActionResult> Reservation(int Id, ReservationToAddDto reservation1)
        {
            ViewBag.BackgroundImage = "349835.jpg";
            reservation1.Room1 = await _roomservice.GetByIdAsync(Id);
            return View(reservation1);
        }
        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public async Task<IActionResult> AddReservation(int Id, ReservationToAddDto res, string PinCode)
        {
            ViewBag.BackgroundImage = "349835.jpg";
            res.Room1 = await _roomservice.GetByIdAsync(Id);
            Customer customer = await _service.GetByPinCodeAsync(PinCode.ToUpper());
            var res1 = await _reservationservice.GetReservationsAsync();
            var cus2 = res1.Select(x => x.CustomerId);
            int id2 = customer.CustomerId;
            if (!cus2.Contains(id2))
            {
                res.Room1.CustomerId = id2;
                res.CustomerId = id2;
                res.ArrivalDate = DateTime.Now;
                DateTime dt = res.DepartureDate;
                DateTime dt2 = res.ArrivalDate;
                int totaldays = Convert.ToInt32((dt - dt2).TotalDays);
                int totalprice1 = (totaldays * (int)res.Room1.Price);
                res.TotalPrice = Math.Abs(totalprice1);
                res.RoomId = res.Room1.RoomId;
                await _reservationservice.Add(res);
                await _roomservice.Update(res.Room1);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "This customer already got reservation");
                return View("Reservation", res);

            }
            return RedirectToAction("Reservations");

        }
        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public ActionResult Reservations(int? page, string SearchString)
        {
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            var reservations = _reservationservice.GetAsync(pageNumber, pageSize, SearchString);
            ViewBag.BackgroundImage = "swimmingpool1.jpg";
            return View(reservations);
        }

        [Authorize(Roles = "Administrator,Employee, SpuerAdmin")]
        public async Task<IActionResult> RemoveReservation(int Id)
        {
            var reservation = await _reservationservice.GetById(Id);
            var rooms = await _roomservice.GetAsync();
            var roomforcustid = rooms.Where(x => x.RoomId == reservation.RoomId).FirstOrDefault();
            roomforcustid.CustomerId = null;
            await _roomservice.Update(roomforcustid);
            await _reservationservice.Delete(Id);
            return RedirectToAction("Rooms");
        }
        public async Task<IActionResult> RoomDesignTest(int FloorId, int RoomTypeId)
        {
            ViewBag.BackgroundImage = "349835.jpg";
            List<RoomToListDto> rooms = await _roomservice.GetAsync();
            List<Floor> Floors = await _roomservice.GetFloorsAsync();
            ViewBag.FloorList = new SelectList(Floors, "FloorId", "FloorName");
            List<RoomTypeToListDto> roomtypes = await _roomservice.GetRoomTypesAsync();
            ViewBag.RoomTypeList = new SelectList(roomtypes, "RoomTypeId", "RoomTypeName");
            if (FloorId == 0 && RoomTypeId == 0)
            {
                return View(rooms);
            }
            else if (RoomTypeId == 0)
            {
                rooms = (from room in rooms where room.Floor.FloorId == FloorId select room).ToList();
                return View(rooms);
            }
            else if (FloorId == 0)
            {
                rooms = (from room in rooms where room.RoomType.RoomTypeId == RoomTypeId select room).ToList();
                return View(rooms);
            }
            else
            {
                rooms = (from room in rooms where room.Floor.FloorId == FloorId && room.RoomType.RoomTypeId == RoomTypeId select room).ToList();
                return View(rooms);
            }
        }
    }
}
