using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas_Vers_0._1;

namespace Atlas_Vers_0._1.ViewModels
{
    public class Unit
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int firstTemp { get; set; }
        public int secondTemp { get; set; }
    }



    public class BurViewModel : ViewModel
    {
        
    }
}
