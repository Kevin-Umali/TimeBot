using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeBot.API
{
    public class DeserializeJson
    {
        private string _Time = string.Empty;
        public string getTime
        {
            get { return _Time; }
        }
        public async Task deserialize(string _json)
        {
            try
            {
                JsonSerializerSettings jsonSerializerSetting = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                var jTimeZone = JsonConvert.DeserializeObject<jsonTimeZone>(_json, jsonSerializerSetting);

                _Time = jTimeZone.datetime;
                _Time = _Time.Substring(_Time.IndexOf("T") + 1);
                _Time = _Time.Substring(0, _Time.LastIndexOf("."));

                _Time = Convert.ToDateTime(_Time).ToString("hh:mm:ss tt") + " " + jTimeZone.abbreviation;
                await Task.CompletedTask;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
