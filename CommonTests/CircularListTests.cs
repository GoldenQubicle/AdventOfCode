using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NUnit.Framework;

namespace CommonTests
{
	internal class CircularListTests
	{
		private CircularList<int> sut;

		[SetUp]
		public void Setup()
		{
			sut = new( ) { 2, 3, 4, 5, 6 };
		}

		[Test]
		public void AddShouldWorkInObjectInitializer()
		{
			var expected = new List<int> { 2, 3, 4, 5, 6 };

			Assert.That(sut.ToList( ), Is.EqualTo(expected));
		}

		
		[Test]
		public void MovePointerShouldWorkOnEmptyList()
		{
			var cl = new CircularList<int> { 1 };

			var moveLeft = () => cl.MoveLeft(5);
			var moveRight = () => cl.MoveRight(5);

			Assert.DoesNotThrow(moveLeft.Invoke);
			Assert.DoesNotThrow(moveRight.Invoke);
		}

		[Test]
		public void MovePointerShouldWrapAround()
		{
			sut.ResetHead();
			Assert.That(sut.Current, Is.EqualTo(2));

			sut.MoveLeft(8);
			Assert.That(sut.Current, Is.EqualTo(4));

			sut.MoveRight(4);
			Assert.That(sut.Current, Is.EqualTo(3));

			sut.ResetHead(toFirst: false);
			Assert.That(sut.Current, Is.EqualTo(6));

			sut.MoveRight(4);
			Assert.That(sut.Current, Is.EqualTo(5));
		}


		[TestCaseSource(nameof(GetInsertTestCases))]
		public void InsertShouldBeAtCurrentPointerPosition((bool after, List<int> expected) test)
		{
			sut.ResetHead();
			sut.MoveRight(2);

			sut.Insert(7, test.after);

			Assert.That(sut.ToList( ), Is.EqualTo(test.expected));

		}

		private static List<(bool, List<int>)> GetInsertTestCases() => new( )
		{
			( true,  new List<int> { 2, 3, 4, 7, 5, 6 } ),
			( false, new List<int> { 2, 3, 7, 4, 5, 6 } )
		};

	}
}
