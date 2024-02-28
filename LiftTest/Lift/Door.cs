using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiftTest.Enums;

namespace LiftTest.Lift
{
    public class Door
    {
        private ElevatorDoorState DoorState;

        public void OpenDoor()
        {
            DoorState = ElevatorDoorState.Opened;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Door Opened");            
        }

        public void CloseDoor()
        {
            DoorState = ElevatorDoorState.Closed;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Door Closed");            
        }

        public Door(ElevatorDoorState doorState)
        {
            DoorState = doorState;
        }
    }
}
