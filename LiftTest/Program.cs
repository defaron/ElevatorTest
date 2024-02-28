using LiftTest.Lift;

internal class Program
{
    private static void Main(string[] args)
    {
        Elevator lift = new(LiftTest.Enums.FloorNumber.First);
        lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Third);
        lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.First);
        lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Fifth);
        lift.Start();
    }
}