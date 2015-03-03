using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.SQLite;

namespace RecordWebService.Models
{
    //TODO:ANYTIME AN UPDATE TO THE DATABASE OCCURS RefreshDatabaseTables MUST BE CALLED
    public class DatabaseSingleton
    {
        #region Properties

        private SQLiteConnection db;
        private SQLiteConnection dbConnection;

        private string databasePath = @"C:\RecordWebApi\Master.";
        private string sqliteString = "sqlite";
        private string dbString = "db";

        public Table<tblAlbum> DbAlbums;
        public Table<tblSong> DbSongs;

        private static DatabaseSingleton instance;

        public static DatabaseSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseSingleton();
                }
                return instance;
            }
        }

        #endregion

        #region Constructors

        private DatabaseSingleton()
        {
            Setup();
        }

        #endregion

        #region Methods

        public void Setup()
        {
            if (
                !File.Exists(databasePath + sqliteString))
            {
                //CREATING DB
                db = new SQLiteConnection(databasePath + dbString);

                //CONNECTING
                dbConnection = new SQLiteConnection("Data Source=" + databasePath + sqliteString + ";Version=3;");
                dbConnection.Open();

                string sql;
                SQLiteCommand command;

                //CREATE TABLES
                sql =
                    "CREATE TABLE tblSong (Id integer primary key, Key blob, Title text, Artist text, Album text, Break_Number integer)";
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                sql =
                    "CREATE TABLE tblAlbum (Id integer primary key, Key blob, Album text, Artist text, Calculated integer, Breaks integer, Image blob)";
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            else
            {
                try
                {
                    db = new SQLiteConnection(databasePath + dbString);

                    //CONNECTING
                    dbConnection = new SQLiteConnection("Data Source=" + databasePath + sqliteString + ";Version=3;");
                    dbConnection.Open();
                    RefreshDatabaseTables();
                }
                catch (Exception e)
                {

                }
            }
        }

        /// <summary>
        /// Get the Database contents upon startup and after an update to the database
        /// </summary>
        public void RefreshDatabaseTables()
        {
            var context = new DataContext(dbConnection);

            DbSongs = context.GetTable<tblSong>();
            DbAlbums = context.GetTable<tblAlbum>();
        }

        public List<tblSong> GetTblSongs(byte[] b)
        {
            var ret = (from item in DbSongs
                       where item.Key == b
                       select item).ToList();

            return ret;
        }

        public List<string> GetSongNames(byte[] b)
        {
            var ret = (from item in DbSongs
                       where item.Key == b
                       select item.Title).ToList();

            return ret;
        } 

        public tblAlbum GetTblAlbums(byte[] b)
        {
            var ret = (from item in DbAlbums
                       where item.Key == b
                       select item).ToList();

            return ret.FirstOrDefault();
        }

        public void AddTblAlbum(tblAlbum ta)
        {
            string sql;
            SQLiteCommand command;
            string artist = ta.Artist.Replace("'", "''");
            string album = ta.Album.Replace("'", "''");
            sql = "insert into tblAlbum (Key, Album, Artist, Calculated, Breaks, Image) values (@Key,'" + album + "','" + artist + "','" + 1 + "','" + ta.Breaks + "', @Image)";
            command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.Add("@Key", DbType.Binary).Value = ta.Key;
            command.Parameters.Add("@Image", DbType.Binary).Value = ta.Image;
            command.ExecuteNonQuery();
        }

        public void AddTblSong(tblSong ts, int breaknum)
        {
            string sql;
            SQLiteCommand command;
            sql = "insert into tblSong (Key, Title, Artist, Album, Break_Number) values (@Key,'" + ts.Title.Replace("'", "''") + "','" + ts.Artist.Replace("'", "''") + "','" + ts.Album.Replace("'", "''") + "','" + breaknum + "')";
            command = new SQLiteCommand(sql, dbConnection);
            command.Parameters.Add("@Key", DbType.Binary).Value = ts.Key;
            command.ExecuteNonQuery();
        }

        public byte[] GetImageData(byte[] key)
        {
            return (from item in DbAlbums
                       where item.Key == key
                       select item.Image).ToList().FirstOrDefault();
        }

        #endregion
    }
}