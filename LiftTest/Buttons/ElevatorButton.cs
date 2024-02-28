using LiftTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftTest.Buttons
{
    public class ElevatorButton : IButton
    {
        private Action<FloorNumber> ClickAction { get; set; }

        public ElevatorButton(Action<FloorNumber> AddFloor)
        {
            ClickAction= AddFloor;
        }        

        public void Click(FloorNumber floorNumber)
        {
            ClickAction(floorNumber);
        }
    }
}
