using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.greencloud.dto
{
    class RoomTypeDto
    {
        public int VD { get; set; }
        public int unsureBookNum { get; set; }
        public int VC { get; set; }
        public int sureBookNum { get; set; }
        public int sellable { get; set; }
       
        public int overBookingSum { get; set; }
        public int bookingSum { get; set; }
        public String descriptEn { get; set; }
        public String descript { get; set; }
        public String rackRate { get; set; }
        public String code { get; set; }
        public String vipRate { get; set; }
        public int listOrder { get; set; }
    }
}
