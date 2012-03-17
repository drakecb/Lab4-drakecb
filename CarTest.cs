using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarDoesGetCarLocationFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            String carLocation = "Hawaii";
            String carLocation2 = "Australia";

            using (mocks.Record())
            {
                //The mock will return "Hawaii" when the call is made with 9871
                mockDatabase.getCarLocation(9871);
                LastCall.Return(carLocation);

                //The mock will return "Australia" when the call is made with 12
                mockDatabase.getCarLocation(12);
                LastCall.Return(carLocation2);
            }


            targetCar.Database = mockDatabase;

            Assert.AreEqual(targetCar.getCarLocation(9871), carLocation);
            Assert.AreEqual(targetCar.getCarLocation(12), carLocation2);
        }

        [Test()]
        public void TestThatCarDoeGetMilageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            
            mockDatabase.Miles = 2497;

            targetCar.Database = mockDatabase;

            Assert.AreEqual(targetCar.Mileage, 2497);
        }

        [Test()]
        public void TestThatCarHasCorrectBasePriceForBMW()
        {
            Assert.AreEqual(80, ObjectMother.BMW().getBasePrice());
        }
	}
}
