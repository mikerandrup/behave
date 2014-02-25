using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Behave.BehaveCore.DBUtils
{
    public enum DatabaseResultCode
    {
        statusUnknown = -1,
        okay = 1,
        notFound = 2,
        miscError = 3
    }
}