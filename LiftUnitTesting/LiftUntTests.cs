using LiftTest.Enums;
using LiftTest.Lift;

namespace LiftUnitTesting
{
    public class LiftUntTests
    {
        [Test]
        public void TestInvalidConstructorArgument()
        {
            var ex = Assert.Throws<InvalidOperationException>(() => new Elevator((FloorNumber)100));
            Assert.That(ex.Message, Is.EqualTo("initialFloor parameter should be a valid floor"));
        }

        [Test]
        public void TestInvalidRequestForMaxFloorThanMaxFloor()
        {
            Elevator lift = new();
            var ex = Assert.Throws<InvalidOperationException>(() => lift.ElevatorButton.Click((FloorNumber)10));
            Assert.That(ex.Message, Is.EqualTo("RequestedFloor parameter should be a valid floor"));
        }

        [Test]
        public void TestInvalidRequestForLowerLimitFloor()
        {
            Elevator lift = new();
            var ex = Assert.Throws<InvalidOperationException>(() => lift.ElevatorButton.Click((FloorNumber)(-1)));
            Assert.That(ex.Message, Is.EqualTo("RequestedFloor parameter should be a valid floor"));
        }

        [Test]
        public void TestUpRequestsAfterSeveralButtonRequests()
        {
            Elevator lift = new();
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Third);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.First);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Fifth);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Fifth);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Fifth);

            Assert.IsTrue(lift.UpRequests.Count() == 2);
        }

        [Test]
        public void TestDownRequestsAfterSeveralButtonRequests()
        {
            Elevator lift = new(FloorNumber.Fourth);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.First);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.First);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Second);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Second);
            lift.ElevatorButton.Click(LiftTest.Enums.FloorNumber.Fifth);

            Assert.IsTrue(lift.DownRequests.Count() == 2);
        }
    }
}