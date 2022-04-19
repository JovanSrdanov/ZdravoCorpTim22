using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.Model.Generic
{
    public interface IHasInterval
    {
        Interval Interval { get; set; }
    }
}
