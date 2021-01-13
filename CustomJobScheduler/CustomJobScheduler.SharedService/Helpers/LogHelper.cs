using System;
using Serilog;

namespace CustomJobScheduler.SharedService.Helpers
{
    public class LogHelper
    {
        public static void Print(string info)
        {
            Log.Information(info);
        }

        public static void Print(Exception ex, string error, bool isCriticalError = false)
        {
            if (ex == null)
            {
                if (isCriticalError)
                {
                    Log.Fatal(error);
                }
                else
                {
                    Log.Error(error);
                }
            }
            else
            {
                if (isCriticalError)
                {
                    Log.Fatal(ex, $"{error}{Environment.NewLine}Error details --->");
                }
                else
                {
                    Log.Error(ex, $"{error}{Environment.NewLine}Error details --->");
                }
            }
        }
    }
}
