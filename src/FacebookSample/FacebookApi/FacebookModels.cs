using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApi
{
    internal class FacebookQueryWhere
    {
        internal static string WHERE_BY_UID = " WHERE uid  = {0}";
        internal static string WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0";
        internal static string WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID + " AND substr(birthday_date, 0, 2) >= \"{1}\"";
        internal static string WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) >= \"{1}\") AND (substr(birthday_date, 3, 2) >= \"{2}\")) OR (substr(birthday_date, 0, 2) >= \"{3}\")) ORDER by birthday_date";
        internal static string WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = " WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) AND strlen(birthday_date) != 0 AND (((substr(birthday_date, 0, 2) <= \"{1}\") AND (substr(birthday_date, 3, 2) < \"{2}\")) OR (substr(birthday_date, 0, 2) <= \"{3}\")) ORDER by birthday_date";

    }

    public class FBIdBirthUserInformation
    {
        #region Queries
        private static string QUERY = "SELECT uid, name, birthday_date FROM user";
        internal static string QUERY_BY_UID = FBIdBirthUserInformation.QUERY + FacebookQueryWhere.WHERE_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = FBIdBirthUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FBIdBirthUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBIdBirthUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBIdBirthUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        #endregion

        internal FBIdBirthUserInformation()
        {
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string BirthDay { get; internal set; }
    }

    public class FBUserInformation
    {
        #region Queries
        private const string QUERY = "SELECT uid, name, pic_small, pic_square, pic_big, birthday_date, movies, books, music, games FROM user";
        internal static string QUERY_BY_UID = FBUserInformation.QUERY + FacebookQueryWhere.WHERE_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = FBUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FBUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        #endregion

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string BirthDay { get; internal set; }
        public string PictureSmallPath { get; internal set; }
        public string PictureSquarePath { get; internal set; }
        public string PictureBigPath { get; set; }
        public string Movies { get; internal set; }
        public string Books { get; internal set; }
        public string Music { get; internal set; }
        public string Games { get; internal set; }
    }

    public class FBUserId
    {
        #region Queries
        private const string QUERY = "SELECT uid FROM user";
        internal static string QUERY_BY_UID = FBUserId.QUERY + FacebookQueryWhere.WHERE_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = FBUserId.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FBUserId.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserId.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserId.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        #endregion

        public string Id { get; internal set; }
    }

    public class FBUserBasicInformation
    {
        #region Queries
        private const string QUERY = "SELECT uid, name, pic_square FROM user";
        internal static string QUERY_BY_UID = FBUserBasicInformation.QUERY + FacebookQueryWhere.WHERE_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = FBUserBasicInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FBUserBasicInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserBasicInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserBasicInformation.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        #endregion

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public string PictureSquarePath { get; internal set; }
    }

    public class FBUserLikes
    {
        #region Queries
        private const string QUERY = "SELECT movies, books, music, games, tv, activities, interests, sports, favorite_teams, education FROM user";
        internal static string QUERY_BY_UID = FBUserLikes.QUERY + FacebookQueryWhere.WHERE_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID = FBUserLikes.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH = FBUserLikes.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_BY_UID_BY_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserLikes.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_FOWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        internal static string QUERY_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH = FBUserLikes.QUERY + FacebookQueryWhere.WHERE_IN_FRIENDS_BIRTHDAY_NOT_NULL_MONTH_BACKWARD_BY_UID_BY_MONTH_BY_DAY_BY_NEXT_MONTH;
        #endregion

        public string Movies { get; internal set; }
        public string Books { get; internal set; }
        public string Music { get; internal set; }
        public string Games { get; internal set; }
        public string TV { get; internal set; }
        public string Activities { get; internal set; }
        public string Interests { get; internal set; }
        public string Sports { get; internal set; }
        public string Favorite_teams { get; internal set; }
        public string Education { get; internal set; }
    }
}
