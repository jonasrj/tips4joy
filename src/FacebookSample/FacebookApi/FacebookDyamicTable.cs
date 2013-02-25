using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class FacebookDyamicTable : List<FacebookDyamicRow>
    {
        private dynamic data;

        internal FacebookDyamicTable(string json)
        {
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            foreach (dynamic item in data["data"])
            {
                this.Add(new FacebookDyamicRow(item));
            }
        }
    }
}
