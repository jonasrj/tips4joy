using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSearch
{
    public class UzgamesSearchEngine: EcommerceSearchEngine
    {
        public override string EcommerceName
        {
            get { return "uzgames"; }
        }

        protected override string EcommerceUrl
        {
            get { return "http://www.uzgames.com/"; }
        }

        protected override List<EcommerceSearchRescultItem> GetSearchResult(HtmlDocument htmlDocument)
        {
            List<EcommerceSearchRescultItem> ecommerceSearchRescultItems = new List<EcommerceSearchRescultItem>();

            HtmlNodeCollection productImageWrapperCollection = htmlDocument.DocumentNode.SelectNodes("//div[@class=\"productImageWrapper\"]");
            if (productImageWrapperCollection != null)
            {
                foreach (HtmlNode node in productImageWrapperCollection)
                {
                    HtmlNode a = node.ChildNodes.Single(x => string.Compare(x.Name, "a", true) == 0);

                    string title = a.Attributes["title"].Value;
                    string href = a.Attributes["href"].Value;

                    HtmlNode img = node.ChildNodes.Single(x => string.Compare(x.Name, "div", true) == 0
                                                            && x.Attributes.Contains("class")
                                                            && x.Attributes["class"].Value == "productImage").FirstChild;

                    string image = img.Attributes["src"].Value;

                    ecommerceSearchRescultItems.Add(new EcommerceSearchRescultItem(title, href, image));
                }
            }

            return ecommerceSearchRescultItems;
        }

        public void do123(){}
    }
}
