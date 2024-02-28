using LiftTest.Buttons;
using LiftTest.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftTest.Lift
{
    public class Elevator
    {
        public Door Door { get; private set; }
        public FloorNumber PreviousFloor { get; private set; }
        public FloorNumber CurrentFloor { get; private set; }
        public SortedSet<int> UpRequests { get; private set; }
        public SortedSet<int> DownRequests { get; private set; }
        public ElevatorButton ElevatorButton { get; private set; }

        public Elevator(FloorNumber initialFloor = 0)
        {
            if (initialFloor > Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().Last() ||
                    initialFloor < Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().First())
                throw new InvalidOperationException("initialFloor parameter should be a valid floor");
            CurrentFloor = initialFloor;
            if(CurrentFloor == FloorNumber.First)
                PreviousFloor = CurrentFloor - 1;
            if(CurrentFloor == FloorNumber.Fifth)
                PreviousFloor = CurrentFloor + 1;
            UpRequests = new SortedSet<int>();
            DownRequests = new SortedSet<int>();
            ElevatorButton = new ElevatorButton(Addfloor);
            Door = new Door(ElevatorDoorState.Opened);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Elevator Started at Floor: {CurrentFloor}");
        }

        public void Start()
        {
            var nextFloor = GetNextfloor();

            while (nextFloor != null)
            {
                if (nextFloor.Value == CurrentFloor)
                {
                    StopElevator(CurrentFloor);
                    nextFloor = GetNextfloor();
                }
                else if (nextFloor.Value > CurrentFloor)
                {
                    IncreaseFloorNumber();
                }
                else if (nextFloor.Value < CurrentFloor)
                {
                    DecreaseFloorNumber();
                }
            }
        }

        private FloorNumber? GetNextfloor()
        {
            if (GetCurrentDirection() == Direction.Down && DownRequests.Count > 0)
            {
                var lastHighestFloorQueue = DownRequests.Last();
                DownRequests.Remove(lastHighestFloorQueue);
                return (FloorNumber)lastHighestFloorQueue;
            }
            else if (GetCurrentDirection() == Direction.Up && UpRequests.Count > 0)
            {
                var lowestFloorInUpQueue = UpRequests.First();
                UpRequests.Remove(lowestFloorInUpQueue);
                return (FloorNumber)lowestFloorInUpQueue;
            }
            return null;
        }

        private void Addfloor(FloorNumber requestedFloor)
        {
            if (requestedFloor > Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().Last() ||
                    requestedFloor < Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().First())
                throw new InvalidOperationException("RequestedFloor parameter should be a valid floor");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Requested Floor:{requestedFloor.ToString()}");
            if (requestedFloor <= CurrentFloor)
            {
                DownRequests.Add((int)requestedFloor);
            }
            else if (requestedFloor > CurrentFloor)
            {
                UpRequests.Add((int)requestedFloor);
            }
        }

        private void StopElevator(FloorNumber floorToStop)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Stopping at '{floorToStop.ToString()}' floor.");
            Door.OpenDoor();
            System.Threading.Thread.Sleep(1000);
            Door.CloseDoor();
        }

        private Direction GetCurrentDirection()
        {
            if (CurrentFloor > PreviousFloor)
                return Direction.Up;
            else if (CurrentFloor < PreviousFloor)
                return Direction.Down;
            else
                return Direction.Idle;
        }

        private void DecreaseFloorNumber()
        {
            if (CurrentFloor > Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().First())
            {
                PreviousFloor = CurrentFloor;
                CurrentFloor--;
                DisplayCurrentFloor();
            }
        }

        private void IncreaseFloorNumber()
        {
            if (CurrentFloor < Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().Last())
            {
                PreviousFloor = CurrentFloor;
                CurrentFloor++;
                if (CurrentFloor == Enum.GetValues(typeof(FloorNumber)).Cast<FloorNumber>().Last())
                    PreviousFloor = CurrentFloor + 1;
                DisplayCurrentFloor();
            }
        }

        private void DisplayCurrentFloor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Current floor: {CurrentFloor.ToString()}");
        }        
    }
}
