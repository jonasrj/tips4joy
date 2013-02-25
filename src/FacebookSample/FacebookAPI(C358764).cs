using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vtex.Practices.Configuration;
using Vtex.Commerce.AddOn.Web.ViewParts;

namespace Vtex.Social
{
    public class FacebookAPI
    {
        private const string FACEBOOK_QUERY_URL = "https://api.facebook.com/method/fql.query?query={0}&access_token={1}";
        private const string FACEBOOK_QUERY = "SELECT uid, name, pic_small, pic_square, pic_big, birthday_date, movies, books, music, games FROM user";

        private const string FACEBOOK_QUERY_ID = "SELECT uid FROM user";

        private const string FACEBOOK_QUERY_BASE = "SELECT uid, name, pic_square FROM user";

        private const string FACEBOOK_QUERY_ID_BIRTH = "SELECT uid, name, birthday_date FROM user";

        private const string FACEBOOK_QUERY_ME_DETAIL = "SELECT movies, books, music, games, tv, activities, interests, sports, favorite_teams, education FROM user";

        //private const string FACEBOOK_QUERY_BIRTH_COUNT = "SELECT COUNT(uid) FROM user";


        public static List<string> GetFriendsByName(string me, string access_token, int page, int qtd)
        {
            //page = page - 1;
            List<string> ret = new List<string>();
            if (page < 0)
            {
                throw new ArgumentException("Page can not be less than 0");
            }

            string query = string.Format(FACEBOOK_QUERY + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) ORDER BY name LIMIT {1},{2}", me, page * qtd, qtd);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);
            ret.AddRange(FillUserIdsCollection(doc.ChildNodes[1].ChildNodes));
            //return FillUserInformationCollection(doc.ChildNodes[1].ChildNodes);
            return ret;
        }

        public static List<string> GetFriendsByIdsSortByName(string me, string access_token, int page, int qtd, string ids)
        {
            //page = page - 1;
            List<string> ret = new List<string>();
            if (page < 0)
            {
                throw new ArgumentException("Page can not be less than 0");
            }

            string query = string.Format(FACEBOOK_QUERY + " WHERE uid IN (" + ids + ") ORDER BY name LIMIT {1},{2}", me, page * qtd, qtd);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);
            ret.AddRange(FillUserIdsCollection(doc.ChildNodes[1].ChildNodes));
            //return FillUserInformationCollection(doc.ChildNodes[1].ChildNodes);
            return ret;
        }

        public static List<string> GetFriendsByIdsSortByBirthDate(string me, string access_token, int page, int qtd, string ids)
        {

            string query = string.Format(FACEBOOK_QUERY_ID + " WHERE uid IN (" + ids + ") AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) >= \"{1}\") AND (substr(birthday_date, 3, 2) >= \"{2}\")) OR (substr(birthday_date, 0, 2) >= \"{3}\")) ORDER by birthday_date", me, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Month + 1);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            List<string> ret = FillUserIdsCollection(doc.ChildNodes[1].ChildNodes);

            query = string.Format(FACEBOOK_QUERY_ID + " WHERE uid IN ("+ids+") AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) <= \"{1}\") AND (substr(birthday_date, 3, 2) < \"{2}\")) OR (substr(birthday_date, 0, 2) <= \"{3}\")) ORDER by birthday_date", me, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Month - 1);

            reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            ret.AddRange(FillUserIdsCollection(doc.ChildNodes[1].ChildNodes));

            ret = ret.Skip(page * qtd).Take(qtd).ToList();
            return ret;
            
            
            
            
            ////page = page - 1;
            //List<string> ret = new List<string>();
            //if (page < 0)
            //{
            //    throw new ArgumentException("Page can not be less than 0");
            //}

            //string query = string.Format(FACEBOOK_QUERY + " WHERE uid IN (" + ids + ") and strlen(birthday_date) != 0 ORDER BY birthday_date LIMIT {1},{2}", me, page * qtd, qtd);

            //System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            //System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.Load(reader);
            //ret.AddRange(FillUserIdsCollection(doc.ChildNodes[1].ChildNodes));
            ////return FillUserInformationCollection(doc.ChildNodes[1].ChildNodes);
            //return ret;
        }

        public static List<FBBaseUserInformation> GetFriendsBaseInformation(string me, string access_token, int page, int qtd)
        {
            //page = page - 1;

            if (page < 0)
            {
                throw new ArgumentException("Page can not be less than 0");
            }

            string query = string.Format(FACEBOOK_QUERY_BASE + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) ORDER BY name LIMIT {1},{2}", me, page * qtd, qtd);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            return FillUserBaseInformationCollection(doc.ChildNodes[1].ChildNodes);
        }

        public static List<FBIdBirthUserInformation> GetFriendsIdAndBirthInformation(string me, string access_token, int page, int qtd)
        {
            //page = page - 1;

            if (page < 0)
            {
                throw new ArgumentException("Page can not be less than 0");
            }

            string query = string.Format(FACEBOOK_QUERY_ID_BIRTH + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) ORDER BY name LIMIT {1},{2}", me, page * qtd, qtd);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            return FillUserIDAndBirthInformationCollection(doc.ChildNodes[1].ChildNodes);
        }

        public static int GetCountBirthdayIDs(string me, string access_token)
        {
            string query = string.Format(FACEBOOK_QUERY_ID + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0", me);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            List<string> ret = FillUserIdsCollection(doc.ChildNodes[1].ChildNodes);

            return ret.Count;
        }

        public static List<string> GetUpcomingBirthdayIDs(string me, string access_token, int page, int qtd)
        {
            string query = string.Format(FACEBOOK_QUERY_ID + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) >= \"{1}\") AND (substr(birthday_date, 3, 2) >= \"{2}\")) OR (substr(birthday_date, 0, 2) >= \"{3}\")) ORDER by birthday_date", me, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Month + 1);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            List<string> ret = FillUserIdsCollection(doc.ChildNodes[1].ChildNodes);

            query = string.Format(FACEBOOK_QUERY_ID + " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) <= \"{1}\") AND (substr(birthday_date, 3, 2) < \"{2}\")) OR (substr(birthday_date, 0, 2) <= \"{3}\")) ORDER by birthday_date", me, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Month - 1);

            reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            ret.AddRange(FillUserIdsCollection(doc.ChildNodes[1].ChildNodes));
            
            ret = ret.Skip(page * qtd).Take(qtd).ToList();
            return ret;
        }

        public static Vtex.Commerce.Connect.DTO.DepartamentWithKeywords GetMeDetail(string me, string access_token)
        {
           string query = string.Format(FACEBOOK_QUERY_ME_DETAIL + " WHERE uid = {0}", me);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);
           
            return FillUserDetail(doc.ChildNodes[1].ChildNodes);
        }

        public static FBUserInformation GetUser(string me, string access_token)
        {
            string query = string.Format(FACEBOOK_QUERY + " WHERE uid = {0}", me);

            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(string.Format(FACEBOOK_QUERY_URL, query, access_token));

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(reader);

            FBUserInformation info = FillUserInformation(doc.ChildNodes[1].FirstChild);
            info.AccessToken = access_token;
            return info;
        }

        private static List<FBUserInformation> FillUserInformationCollection(XmlNodeList childNodes)
        {
            List<FBUserInformation> ret = new List<FBUserInformation>();

            FBUserInformation fbUserInformation = null;

            foreach (XmlNode item in childNodes)
            {

                fbUserInformation = FillUserInformation(item);

                ret.Add(fbUserInformation);
            }

            return ret;
        }

        private static List<FBBaseUserInformation> FillUserBaseInformationCollection(XmlNodeList childNodes)
        {
            List<FBBaseUserInformation> ret = new List<FBBaseUserInformation>();

            FBBaseUserInformation fbBaseUserInformation = null;

            foreach (XmlNode item in childNodes)
            {

                fbBaseUserInformation = FillBaseUserInformation(item);

                ret.Add(fbBaseUserInformation);
            }

            return ret;
        }

        private static List<FBIdBirthUserInformation> FillUserIDAndBirthInformationCollection(XmlNodeList childNodes)
        {
            List<FBIdBirthUserInformation> ret = new List<FBIdBirthUserInformation>();

            FBIdBirthUserInformation fbIdBirthUserInformation = null;

            foreach (XmlNode item in childNodes)
            {

                fbIdBirthUserInformation = FillIdAndBirthUserInformation(item);

                ret.Add(fbIdBirthUserInformation);
            }

            return ret;
        }

        private static List<string> FillUserIdsCollection(XmlNodeList childNodes)
        {
            List<string> ret = new List<string>();

            string fbUserId = string.Empty;

            foreach (XmlNode item in childNodes)
            {
                foreach (XmlNode ids in item.ChildNodes)
                {
                    if (ids.Name == "uid")
                    {
                        fbUserId = ids.InnerText;
                        break;
                    }
                }

                ret.Add(fbUserId);
            }

            return ret;
        }

        private static FBUserInformation FillUserInformation(XmlNode node)
        {
            FBUserInformation fbUserInformation = new FBUserInformation();
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "uid")
                {
                    fbUserInformation.Id = item.InnerText;
                }

                if (item.Name == "name")
                {
                    fbUserInformation.Name = item.InnerText;
                }

                if (item.Name == "birthday_date")
                {
                    fbUserInformation.BirthDay = item.InnerText;
                }
                if (item.Name == "pic_small")
                {
                    fbUserInformation.PictureSmallPath = item.InnerText;
                }

                if (item.Name == "pic_square")
                {
                    fbUserInformation.PictureSquarePath = item.InnerText;
                }

                if (item.Name == "pic_big")
                {
                    fbUserInformation.PictureBigPath = item.InnerText;
                }

                if (item.Name == "movies" || item.Name == "books" || item.Name == "music" || item.Name == "games")
                {
                    List<string> lst = item.InnerText.Split(',').ToList<string>();

                    if (lst.Count > 24)
                    {
                        lst = RandonSupressExccess(lst, 24);
                    }

                    fbUserInformation.Perfil.DepartamentWithKeywordsAdd(item.Name, lst);
                }
            }

            return fbUserInformation;
        }

        private static Vtex.Commerce.Connect.DTO.DepartamentWithKeywords FillUserDetail(XmlNodeList childNodes)
        {
            Vtex.Commerce.Connect.DTO.DepartamentWithKeywords keys = new Vtex.Commerce.Connect.DTO.DepartamentWithKeywords();
            foreach (XmlNode item in childNodes)
            {
                foreach (XmlNode item2 in item){

                    List<string> lst = item2.InnerText.Split(',').ToList<string>();
                    keys.DepartamentWithKeywordsAdd(item2.Name, lst);
                }
            }

            return keys;
        }

        private static List<string> RandonSupressExccess(List<string> lst, int length)
        {
            var rand = new Random();
            for (int i = lst.Count - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                string temp = lst[i];
                lst[i] = lst[n];
                lst[n] = temp;
            }
            return lst.Take(length).ToList();
        }

        private static FBBaseUserInformation FillBaseUserInformation(XmlNode node)
        {
            FBBaseUserInformation fbBaseUserInformation = new FBBaseUserInformation();
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "uid")
                {
                    fbBaseUserInformation.id = item.InnerText;
                }

                if (item.Name == "name")
                {
                    fbBaseUserInformation.value = item.InnerText;
                    fbBaseUserInformation.label = item.InnerText;
                }

                if (item.Name == "pic_square")
                {
                    fbBaseUserInformation.icon = item.InnerText;
                }
            }

            return fbBaseUserInformation;
        }


        private static FBIdBirthUserInformation FillIdAndBirthUserInformation(XmlNode node)
        {
            FBIdBirthUserInformation fbIdBirthUserInformation = new FBIdBirthUserInformation();
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.Name == "uid")
                {
                    fbIdBirthUserInformation.Id = item.InnerText;
                }

                if (item.Name == "name")
                {
                    fbIdBirthUserInformation.Name = item.InnerText;
                }

                if (item.Name == "birthday_date")
                {
                    fbIdBirthUserInformation.Birthday = item.InnerText;
                }
            }

            return fbIdBirthUserInformation;
        }
    }
}
