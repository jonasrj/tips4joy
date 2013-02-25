using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class FacebookDyamicRow : DynamicObject
    {
        private dynamic data;

        internal FacebookDyamicRow(string json)
        {
            data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
        }

        internal FacebookDyamicRow(dynamic data)
        {
            this.data = data;
        }

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            string name = binder.Name;


            result = data[name];

            return true;
        }
    }
}
