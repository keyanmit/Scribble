using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRoleContracts.Interfaces
{
    public interface IUrlShortner
    {
        string GetShortUrl(UInt64 urlId);
        UInt64 GetUrlId(string url);

    }
}
