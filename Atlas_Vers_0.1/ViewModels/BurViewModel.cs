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
        public ObservableCollection<Unit> Units { get; set; }

        public void AddUnits()
        {
            Units = new ObservableCollection<Unit>
            {
                new Unit {Id = 1, Image="/Resourses/Pictures/BUR/BUR_Break_Transparent@4x.png", firstTemp = 15, secondTemp = 30},
                new Unit {Id = 2, Image="/Resourses/Pictures/BUR/BUR_Break_Transparent@4x.png", firstTemp = 17, secondTemp = 40},
                new Unit {Id = 3, Image="/Resourses/Pictures/BUR/BUR_Break_Transparent@4x.png", firstTemp = 18, secondTemp = 50},
                new Unit {Id = 4, Image="/Resourses/Pictures/BUR/BUR_Break_Transparent@4x.png", firstTemp = 19, secondTemp = 60},
                new Unit {Id = 5, Image="/Resourses/Pictures/BUR/BUR_Break_Transparent@4x.png", firstTemp = 30, secondTemp = 70}
            };

        }
        

    }
}
