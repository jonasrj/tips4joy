using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    public class FacebookPreMadeQueries
    {
        public static FBIdBirthUserInformation GetMeFBIdBirthUserInformation(FacebookConnection fb, string uid)
        {
            List<FBIdBirthUserInformation> fbIdBirthUserInformation = FacebookPreMadeQueries.GetFBIdBirthUserInformation(fb, string.Format(FBIdBirthUserInformation.QUERY_BY_UID, uid));

            return fbIdBirthUserInformation.FirstOrDefault();
        }

        private static List<FBIdBirthUserInformation> GetFBIdBirthUserInformation(FacebookConnection fb, string query)
        {
            FacebookDyamicTable fbReturn = fb.Query(query);

            List<FBIdBirthUserInformation> fbIdBirthUserInformation = new List<FBIdBirthUserInformation>();

            foreach (dynamic item in fbReturn)
            {
                fbIdBirthUserInformation.Add(new FBIdBirthUserInformation()
                {
                    Id = item.uid,
                    Name = item.name,
                    BirthDay = item.birthday_date
                });
            }

            return fbIdBirthUserInformation;
        }

        public static FBUserInformation GetMeFBUserInformation(FacebookConnection fb, string uid)
        {
            List<FBUserInformation> fBUserInformation = FacebookPreMadeQueries.GetFBUserInformation(fb, string.Format(FBUserInformation.QUERY_BY_UID, uid));

            return fBUserInformation.FirstOrDefault();
        }

        private static List<FBUserInformation> GetFBUserInformation(FacebookConnection fb, string query)
        {
            FacebookDyamicTable fbReturn = fb.Query(query);

            List<FBUserInformation> fBUserInformation = new List<FBUserInformation>();

            foreach (dynamic item in fbReturn)
            {
                fBUserInformation.Add(new FBUserInformation()
                {
                    Id = item.uid,
                    Name = item.name,
                    BirthDay = item.birthday_date,
                    PictureSmallPath = item.pic_small,
                    PictureSquarePath = item.pic_square,
                    PictureBigPath = item.pic_big,
                    Movies = item.movies,
                    Books = item.books,
                    Music = item.music,
                    Games = item.games,
                });
            }

            return fBUserInformation;
        }
    }
}
