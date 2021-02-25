using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.UI.Models
{
    public class HotelList
    {
        public IEnumerable<HotelModel> hotelList { get; set; }//json old için bazen listi kabul etmez
    }
}       
