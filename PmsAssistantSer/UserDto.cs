using System;
using com.greencloud.entity;

namespace com.greencloud.dto
{
    public class UserDto
    {
        public App app { get; set; }
        public String authCode { get; set; }
        public String hessianJump { get; set; }
        public String alert { get; set; }
        public String localString { get; set; }
        public String loginType { get; set; }
        public HotelGroup hotelGroup { get; set; }
        public String hotelRange { get; set; }
        public WorkStation workStation { get; set; }
        public String version { get; set; }
        public String buildingStr { get; set; }
        public String idle { get; set; }
        public DateTime loginTimeClient { get; set; }
        public DateTime currentDate { get; set; }
        public Hotel hotel { get; set; }
        public DateTime bizDate { get; set; }
        public String bizDateStr { get; set; }
        public User user { get; set; }
        public DateTime loginTimeAppServer { get; set; }
        public int cashier { get; set; }
    }
}




