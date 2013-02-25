using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSearch
{
    public abstract class EcommerceSearchEngine
    {
        public abstract string EcommerceName { get; }
        protected abstract string EcommerceUrl { get; }

        public EcommerceSearchRescult Search(string fullText)
        {
            string str = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                str = webClient.DownloadString(Uri.EscapeUriString(this.EcommerceUrl +  fullText));
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(str);

            List<EcommerceSearchRescultItem> ecommerceSearchRescultItems = this.GetSearchResult(html);

            return new EcommerceSearchRescult(ecommerceSearchRescultItems);
        }

        protected abstract List<EcommerceSearchRescultItem> GetSearchResult(HtmlDocument htmlDocument);
    }
}
