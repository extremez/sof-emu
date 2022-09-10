﻿using GameServer.Configs;
using GameServer.Networks;
using GameServer.Services;
using System;
using System.Diagnostics;
using System.Threading;

namespace GameServer
{
    internal class GameServer
    {
        public static Config Config;

        public static Server Server;

        // Services
        public static ApiService ApiService;
        public static AuthService AuthService;
        public static FeedbackService FeedbackService;

        static void Main(string[] args)
        {
            Console.Title = "SOF-Emu GameServer";

            Stopwatch sw = Stopwatch.StartNew();

            Config = new Config();
            OpCodes.Init();
            
            // Services
            ApiService = new ApiService();
            AuthService = new AuthService();
            FeedbackService = new FeedbackService();


            Server = new Server();

            sw.Stop();
            Thread.Sleep(100);
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("           Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-------------------------------------------");

            Process.GetCurrentProcess().WaitForExit();

            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
