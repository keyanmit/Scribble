using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcWebRole2.BusinessLogic
{
    public static class IdUrlGenerator
    {
        private enum RoundRobinStates
        {
            TennatOne,
            TennatTwo,
            TennatThree
        }

        private static readonly int[] startBase = new[]
        {
            (1 << 15),
            (1 << 30),
            (1 << 45),
        };

        private static readonly Random randGenerator = new Random(DateTime.Now.GetHashCode());
        private static RoundRobinStates previousTennat = RoundRobinStates.TennatOne;
        private static readonly object lockObject = new object();

        public static UInt64 GetIdForRequest()
        {
            lock (lockObject)
            {
                var randValue = randGenerator.Next(10000);
                var returnVal = (UInt32)startBase[(int) previousTennat] + (UInt64)randValue;
                previousTennat = (RoundRobinStates)(((int)previousTennat + 1)%3);
                return returnVal;
            }
        }
    }
}
