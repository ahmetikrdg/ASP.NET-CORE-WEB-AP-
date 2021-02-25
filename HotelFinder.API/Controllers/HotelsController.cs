using HotelFinder.Business.Abstract;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//apiController validationu kendisi yapar
    public class HotelsController : ControllerBase
    {
        private IHotelServices _hotelService;
        public HotelsController(IHotelServices hotelServices)
        {
            _hotelService = hotelServices;
        }
        /// <summary>
        /// Get All Hotels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()  
        {
            var hotels= await _hotelService.GetAllHotels();
            return Ok(hotels);//response olarak 200 döndür ve body kısmınada bu hoteli ekle dedim.
        }
        /// <summary>
        /// GetHotel By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("GetHotelById/{id}")] //routem şuan şöyle oldu api/hotels/gethotelbyıd/2 sadece httpget içinde yazsaydık aşağıdada get olduğu için çakışırdı.Neyse şimdi actionuda değiştirelim.
        [Route("[action]/{id}")]//route ile action aynı isimleya o yüzden direk action dedim
        public async Task<IActionResult> GetHotelById(int id)//buradaki id ile httpgetteki id ismi aynı olmak zorunda
        {
            var hotel=await _hotelService.GetHotelById(id);
            if (hotel!=null)    
            {
                return Ok(hotel);   
            }
            return NotFound();//404
        }

        [HttpGet]
        //[Route("GetHotelByName/{name}")]
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)//buradaki id ile httpgetteki id ismi aynı olmak zorunda
        {
            var hotel =await _hotelService.GetHotelByName(name);

            if (hotel != null)
            {
                return Ok(hotel);//200+data
            }
            return NotFound();
        }       

        //[HttpGet]
        //[Route("[action]/{id}/{name}")]
        //public async Task<IActionResult> GetHotelByIdAndName(int Id,string name)//buradaki id ile httpgetteki id ismi aynı olmak zorunda
        //{
        //    return Ok();
        //}

        /// <summary>
        /// Create an Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody]Hotel hotel)
        {
            var createdHotel =await _hotelService.CreateHotel(hotel);
            return Ok(createdHotel);
             //return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);
            //createdataction avantajı dönen responsenin header kısmında oluşturulan kısmında hangi urlde olduğunuda belirtebilcem
            //bizden bir action name ister getbyid olanı kullanıcam, tabii benden ıd parametresi ister, ve birde oluşturulan oteli body kısmına ekliycem
        }
        //[HttpPost]
        //[Route("[action]/{name}")]
        //public async Task<IActionResult> Post2([FromBody] Hotel hotel)
        //{
        //    var createdHotel = await _hotelService.CreateHotel(hotel);
        //    return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);
        //    //createdataction avantajı dönen responsenin header kısmında oluşturulan kısmında hangi urlde olduğunuda belirtebilcem
        //    //bizden bir action name ister getbyid olanı kullanıcam, tabii benden ıd parametresi ister, ve birde oluşturulan oteli body kısmına ekliycem
        //}
        /// <summary>   
        /// Update the Hotel
        /// </summary>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody]Hotel hotel)
        {
            if ( await _hotelService.GetHotelById(hotel.Id)!=null)//bi bak eğer ıd null değilse
            {

                return Ok(await _hotelService.UpdateHotel(hotel));//200+ data
            }
            return NotFound();
        }
        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="hotelid"></param>
        [HttpDelete]
        [Route("[action]/{hotelid}")]
        public async Task<IActionResult> Delete(int hotelid)
        {
            if (await _hotelService.GetHotelById(hotelid) != null)
            {
               await _hotelService.DeleteHotel(hotelid);
               return Ok();//200
            }
            return NotFound();
        }
    }
}
