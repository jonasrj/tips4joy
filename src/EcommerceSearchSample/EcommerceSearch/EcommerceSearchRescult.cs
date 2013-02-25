using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSearch
{
    public class EcommerceSearchRescult
    {
        internal EcommerceSearchRescult(List<EcommerceSearchRescultItem> ecommerceSearchRetultItems)
        {
            this.EcommerceSearchRetultItems = ecommerceSearchRetultItems;
        }

        public List<EcommerceSearchRescultItem> EcommerceSearchRetultItems { get; private set; }
    }
}
