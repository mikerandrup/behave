using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Behave.BehaveWeb.Models
{
    public class ViewModelBase
    {
        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
    }
}