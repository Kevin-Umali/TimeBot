using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeBot.API
{
    public class APIRequest
    {
        public string Type(int? value, string area)
        {
            string response = string.Empty;
            ApiHandler apiHandler = new ApiHandler();
            string _area = new AreaList().getArea(area);
            
            switch (value)
            {
                case 1:
                    apiHandler.endPoint = string.Format("http://www.worldtimeapi.org/api/timezone/{0}", _area);
                    response = apiHandler.makeRequest();
                    break;
            }
            return response;
        }
    }
}
