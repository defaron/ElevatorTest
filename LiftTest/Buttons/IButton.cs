using LiftTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiftTest.Buttons
{
    public interface IButton
    {
        public void Click(FloorNumber floorNumber);
    }
}
