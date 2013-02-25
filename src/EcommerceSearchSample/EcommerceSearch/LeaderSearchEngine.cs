using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSearch
{
    public class LeaderSearchEngine: EcommerceSearchEngine
    {
        public override string EcommerceName
        {
            get { return "leader"; }
        }

        protected override string EcommerceUrl
        {
            get { return "http://leader.vtexcommerce.com.br/"; }
        }

        protected override List<EcommerceSearchRescultItem> GetSearchResult(HtmlDocument htmlDocument)
        {
            List<EcommerceSearchRescultItem> ecommerceSearchRescultItems = new List<EcommerceSearchRescultItem>();

            HtmlNodeCollection productImageWrapperCollection = htmlDocument.DocumentNode.SelectNodes("//li[@layout]");

            if (productImageWrapperCollection != null)
            {
                foreach (HtmlNode node in productImageWrapperCollection)
                {
                    HtmlNode innerNode = node.SelectSingleNode("div").SelectSingleNode("div");
                    HtmlNode a = innerNode.ChildNodes.Single(x => string.Compare(x.Name, "a", true) == 0);

                    string title = a.Attributes["title"].Value;
                    string href = a.Attributes["href"].Value;

                    HtmlNode img = innerNode.FirstChild.FirstChild;

                    string image = img.Attributes["src"].Value;

                    ecommerceSearchRescultItems.Add(new EcommerceSearchRescultItem(title, href, image));
                }
            }

            return ecommerceSearchRescultItems;
        }
    }
}
