using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetRx.Entities;
using JetRx.Common.Data.SqlServer;

namespace JetRx.Common.Services
{
    public class LoggingService
    {
        public static void LogRequest(string request)
        {
            using (var context = new JetRxContext())
            {
                context.Log.Add(new ApplicationLog {

                     RawRequest = request,
                     RecievedTime = DateTime.UtcNow


                });

                context.SaveChanges();
            }
        }
    }
}
