using FacebookApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FacebookSample.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public const string appid = "411023742322310";
        public const string appsecret = "0432695a26ee78eb8a8ec494d399da54";
        public const string scope = "email,user_likes,user_about_me,user_birthday,offline_access,friends_likes,friends_birthday, read_stream";

        public ActionResult Index()
        {
            string signed_request = Request["signed_request"];

            if (string.IsNullOrWhiteSpace(signed_request))
            {
                return RedirectToAction("NotFromFacebook");
            }
            
            FacebookConnection fb = new FacebookConnection(appid, appsecret, scope);

            FacebookSignedRequest fbSignedRequest = fb.DecodeSignedRequest(signed_request);

            if (fbSignedRequest.IsUserAuthenticated)
            {
                return RedirectToAction("Login");
            }

            DateTime dtExpire = fbSignedRequest.GetExpireDateTime();

            FBUserInformation model = FacebookPreMadeQueries.GetMeFBUserInformation(fb, fbSignedRequest.GetUserId());

            EcommerceSearch.EcommerceSearchEngine search = new EcommerceSearch.UzgamesSearchEngine();

            EcommerceSearch.EcommerceSearchRescult result = search.Search(model.Games);

            ViewBag.result = result;

            return View(model);
        }

        public string Login()
        {
            return "<script> top.location.href='https://www.facebook.com/dialog/oauth?client_id=" + appid + "&redirect_uri=" + "https://apps.facebook.com/sample_vtex/" + "&scope=" + scope + "'</script>";
        }

        public ActionResult NotFromFacebook()
        {
            return View();
        }

        public string Nav()
        {
            string signed_request = Request["signed_request"];

            return "";
        }

        public string JonasSample()
        {
            string str;

            using (var webClient = new System.Net.WebClient())
            {
                str = webClient.DownloadString("http://www.uzgames.com/dead space");
            }

            return str;
        }
    }    
}
