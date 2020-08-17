using System;
using System.IO;
using SQLite;

namespace XamTestApp
{
    public static class Consts
    {

        #region SQLite
        public const string DatabaseFilename = "ContactsSQLite.db3";
        public const SQLiteOpenFlags DatabaseFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        #endregion

        public const int CacheTTLMinutes = 1;
    }
}
