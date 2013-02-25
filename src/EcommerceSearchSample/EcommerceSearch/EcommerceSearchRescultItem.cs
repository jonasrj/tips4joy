using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSearch
{
    public class EcommerceSearchRescultItem
    {
        internal EcommerceSearchRescultItem(string name, string url, string image)
        {
            // TODO: Complete member initialization
            this.ItemName = name;
            this.ItemUrl = url;
            this.ItemImageUrl = image;
        }

        public string ItemName { get; private set; }
        public string ItemUrl { get; private set; }
        public string ItemImageUrl { get; private set; }
    }
}
