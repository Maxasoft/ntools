﻿using CommandLine;
using Launcher;
using OutputColorizer;
using System;
using System.Diagnostics;

namespace nbackup
{
    class Program
    {
        public static int ReturnCode { get; private set; }

        static int Main(string[] args)
        {
            Colorizer.WriteLine($"[{ConsoleColor.Yellow}!{Nversion.Get()}]\n");

            if (!Parser.TryParse(args, out Cli options))
            {
                ReturnCode = ResultHelper.InvalidParameter;
                if (args[0].ToLower() != "--help") Console.WriteLine($"backup completed with '{ReturnCode}'");
                return ReturnCode;
            }

            var watch = Stopwatch.StartNew();
            ResultHelper result = NBackup.Perform(options);

            watch.Stop();

            Console.WriteLine($"Backup completed in {watch.ElapsedMilliseconds/1000.00} s with {ReturnCode}");
            return result.Code;
        }
    }
}
