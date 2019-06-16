using System;
using Xunit;

namespace CatalogApiUnitTest
{
    public class UnitTest1
    {
        private Object myObject = new Object();
        [Fact]
        public void RefKeyWordTest()
        {
            Object HisObject = myObject;
            // should be same
            Assert.Equal(HisObject.GetHashCode(), myObject.GetHashCode());

            CalledMethod(myObject);

            // same
            Assert.Equal(HisObject.GetHashCode(), myObject.GetHashCode());



        }

        private void CalledMethod(Object yourObject)
        {
            // same
            Assert.Equal(myObject.GetHashCode(), yourObject.GetHashCode());

            yourObject = new Object();

            //Assert.Equal(myObject.GetHashCode(), yourObject.GetHashCode());

        }

        [Fact] 
        public void ToInt32_Convertor_ShouldOnlyConvertNumberString()
        {
            Assert.Equal(1, Convert.ToInt32("1"));
        }
    }
}
