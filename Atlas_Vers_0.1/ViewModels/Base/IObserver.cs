using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_Vers_0._1.ViewModels.Base
{
    public interface IObserver
    {
        void Update(string message);
    }
}
