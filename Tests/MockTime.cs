using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Tests
{
    public class MockTime : ITime
    {
        public DateTime _testDate;

        public MockTime()
        {
            _testDate = DateTime.Today;
        }
        public DateTime Today()
        {
            return _testDate;
        }
        public void SetDateTo(DateTime newDate)
        {
            _testDate = newDate;

        }
    }
}
