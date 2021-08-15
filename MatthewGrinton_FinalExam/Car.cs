using System;
using System.Collections.Generic;
using System.Text;

namespace MatthewGrinton_FinalExam
{
    class Car
    {
        private int car_id, vin_num;
        private string car_name;

        public Car()
        {
            car_id = 0;
            vin_num = 0;
            car_name = "";
        }
        public Car(int cid, int vnum, string cname)
        {
            car_id = cid;
            vin_num = vnum;
            car_name = cname;
        }

        private int Car_ID { get; set; }
        private int Vin_Num { get; set; }
        private string Car_Name { get; set; }
    }
}
