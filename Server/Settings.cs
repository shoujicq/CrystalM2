﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Server.MirDatabase;

namespace Server
{
    internal static class Settings
    {
        public const int Day = 24 * Hour, Hour = 60 * Minute, Minute = 60 * Second, Second = 1000;

        public const string MapPath = @".\Maps\",
                            NPCPath = @".\NPCs\",
                            DropPath = @".\Drops\";

        private static readonly InIReader Reader = new InIReader(@".\Setup.ini");


        //General
        public static string VersionPath = @".\Mir2.Exe";
        public static bool CheckVersion = true;
        public static byte[] VersionHash;


        //Network
        public static string IPAddress = "127.0.0.1";

        public static ushort Port = 7000,
                             TimeOut = 10000,
                             MaxUser = 50,
                             RelogDelay = 50;


        //Permission
        public static bool AllowNewAccount = true,
                           AllowChangePassword = true,
                           AllowLogin = true,
                           AllowNewCharacter = true,
                           AllowDeleteCharacter = true,
                           AllowStartGame;

        //Database
        public static int SaveDelay = 5;

        //Game
        public static List<long> ExperienceList = new List<long>();

        public static float DropRate = 2F, ExpRate = 3F;

        public static int ItemTimeOut = 30,
                          DropRange = 4,
                          DropStackSize = 5,
                          PKDelay = 12;

        public static string SkeletonName = "Bone Familiar",
                             ShinsuName = "Shinsu",
                             BugBatName = "Bug Bat",
                             Zuma1 = "Zuma Statue",
                             Zuma2 = "Zuma Guardian",
                             Zuma3 = "Zuma Archer",
                             Zuma4 = "Wedge Moth",
                             Zuma5 = "Zuma Archer3",
                             Zuma6 = "Zuma Statue3",
                             Zuma7 = "Zuma Guardian3";


        public static void Load()
        {
            //General
            VersionPath = Reader.ReadString("General", "VersionPath", VersionPath);
            CheckVersion = Reader.ReadBoolean("General", "CheckVersion", CheckVersion);
            RelogDelay = Reader.ReadUInt16("General", "RelogDelay", RelogDelay);

            //Paths
            IPAddress = Reader.ReadString("Network", "IPAddress", IPAddress);
            Port = Reader.ReadUInt16("Network", "Port", Port);
            TimeOut = Reader.ReadUInt16("Network", "TimeOut", TimeOut);
            MaxUser = Reader.ReadUInt16("Network", "MaxUser", MaxUser);

            //Permission
            AllowNewAccount = Reader.ReadBoolean("Permission", "AllowNewAccount", AllowNewAccount);
            AllowChangePassword = Reader.ReadBoolean("Permission", "AllowChangePassword", AllowChangePassword);
            AllowLogin = Reader.ReadBoolean("Permission", "AllowLogin", AllowLogin);
            AllowNewCharacter = Reader.ReadBoolean("Permission", "AllowNewCharacter", AllowNewCharacter);
            AllowDeleteCharacter = Reader.ReadBoolean("Permission", "AllowDeleteCharacter", AllowDeleteCharacter);
            AllowStartGame = Reader.ReadBoolean("Permission", "AllowStartGame", AllowStartGame);

            //Database
            SaveDelay = Reader.ReadInt32("Database", "SaveDelay", SaveDelay);

            //Game
            DropRate = Reader.ReadSingle("Game", "DropRate", DropRate);
            ExpRate = Reader.ReadSingle("Game", "ExpRate", ExpRate);
            ItemTimeOut = Reader.ReadInt32("Game", "ItemTimeOut", ItemTimeOut);
            ItemTimeOut = Reader.ReadInt32("Game", "PKDelay", PKDelay);
            SkeletonName = Reader.ReadString("Game", "SkeletonName", SkeletonName);
            BugBatName = Reader.ReadString("Game", "BugBatName", BugBatName);
            ShinsuName = Reader.ReadString("Game", "ShinsuName", ShinsuName);
            Zuma1 = Reader.ReadString("Game", "Zuma1", Zuma1);
            Zuma2 = Reader.ReadString("Game", "Zuma2", Zuma2);
            Zuma3 = Reader.ReadString("Game", "Zuma3", Zuma3);
            Zuma4 = Reader.ReadString("Game", "Zuma4", Zuma4);
            Zuma5 = Reader.ReadString("Game", "Zuma5", Zuma5);
            Zuma6 = Reader.ReadString("Game", "Zuma6", Zuma6);
            Zuma7 = Reader.ReadString("Game", "Zuma7", Zuma7);


            if (!Directory.Exists(MapPath))
                Directory.CreateDirectory(MapPath);
            if (!Directory.Exists(NPCPath))
                Directory.CreateDirectory(NPCPath);
            if (!Directory.Exists(DropPath))
                Directory.CreateDirectory(DropPath);

            LoadVersion();
            LoadEXP();
        }

        public static void LoadVersion()
        {
            try
            {
                if (File.Exists(VersionPath))
                    using (FileStream stream = new FileStream(VersionPath, FileMode.Open, FileAccess.Read))
                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                        VersionHash = md5.ComputeHash(stream);
            }
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
            }
        }
        public static void Save()
        {
            //General
            Reader.Write("General", "VersionPath", VersionPath);
            Reader.Write("General", "CheckVersion", CheckVersion);
            Reader.Write("General", "RelogDelay", RelogDelay);

            //Paths
            Reader.Write("Network", "IPAddress", IPAddress);
            Reader.Write("Network", "Port", Port);
            Reader.Write("Network", "TimeOut", TimeOut);
            Reader.Write("Network", "MaxUser", MaxUser);


            //Permission
            Reader.Write("Permission", "AllowNewAccount", AllowNewAccount);
            Reader.Write("Permission", "AllowChangePassword", AllowChangePassword);
            Reader.Write("Permission", "AllowLogin", AllowLogin);
            Reader.Write("Permission", "AllowNewCharacter", AllowNewCharacter);
            Reader.Write("Permission", "AllowDeleteCharacter", AllowDeleteCharacter);
            Reader.Write("Permission", "AllowStartGame", AllowStartGame);
            
            //Database
            Reader.Write("Database", "SaveDelay", SaveDelay);

            //Game
            Reader.Write("Game", "DropRate", DropRate);
            Reader.Write("Game", "ExpRate", ExpRate);
            Reader.Write("Game", "ItemTimeOut", ItemTimeOut);
            Reader.Write("Game", "PKDelay", PKDelay);
            Reader.Write("Game", "SkeletonName", SkeletonName);
            Reader.Write("Game", "BugBatName", BugBatName);
            Reader.Write("Game", "ShinsuName", ShinsuName);

            Reader.Write("Game", "Zuma1", Zuma1);
            Reader.Write("Game", "Zuma2", Zuma2);
            Reader.Write("Game", "Zuma3", Zuma3);
            Reader.Write("Game", "Zuma4", Zuma4);
            Reader.Write("Game", "Zuma5", Zuma5);
            Reader.Write("Game", "Zuma6", Zuma6);
            Reader.Write("Game", "Zuma7", Zuma7);

        }

        public static void LoadEXP()
        {
            long exp = 100;
            InIReader reader = new InIReader(@".\ExpList.ini");

            for (int i = 1; i <= 50; i++)
            {
                exp = reader.ReadInt64("Exp", "Level" + i, exp);
                ExperienceList.Add(exp);
            }
        }
    }
}
