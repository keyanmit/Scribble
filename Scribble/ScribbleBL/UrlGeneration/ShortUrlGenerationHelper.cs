using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.Interfaces;

namespace ScribbleBL.UrlGeneration
{
    public class Base62ShortUrlFactory : IUrlShortner
    {
        public static UInt32 UrlBase { get; private set; }        

        static Base62ShortUrlFactory()
        {
            UrlBase = 62;            
        }

        public string GetShortUrl(UInt64 urlId)
        {
            string shortUrl = "";
            UInt64 tmp;

            do
            {
                tmp = urlId % UrlBase;
                shortUrl = ((tmp >= 0 && tmp <= 25) ? (char)((int)'a' + tmp) : (tmp >= 26 && tmp <= 51) ? (char)((int)'A' + (tmp - 26)) : (char)((int)'0' + tmp - 52)).ToString() + shortUrl;
                urlId /= 62;
            } while (urlId > 0);

            return shortUrl;
        }

        public ulong GetUrlId(string url)
        {
            UInt64 urlId = 0;
            foreach (char t in url)
            {
                urlId *= UrlBase;
                var chVal = (t >= 'a' && t <= 'z')
                    ? t - 'a'
                    : (t >= 'A' && t <= 'Z')
                        ? 26 + t - 'A'
                        : 52 + t - '0';
                urlId += (UInt32)chVal;
            }
            return urlId;
        }
    }
}
