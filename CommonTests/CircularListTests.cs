using System;
using System.Collections.Generic;
using System.Linq;
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

		[TestCase(true, 4)]
		[TestCase(false, 5)]
		public void TakeAtShouldWrapAround(bool moveCurrent, int expectedValue)
		{
			sut.SetHeadByIndex(3);
			Assert.That(sut.Current, Is.EqualTo(5));
			var actual = sut.TakeAt(4, moveCurrent: moveCurrent);
			Assert.That(actual, Is.EqualTo(new List<int> { 5, 6, 2, 3 }));
			Assert.That(sut.Current, Is.EqualTo(expectedValue));
		}


		[Test]
		public void ReplaceRangeShouldWrapAround()
		{
			sut.SetHeadByIndex(3);
			Assert.That(sut.Current, Is.EqualTo(5));
			var sub = sut.TakeAt(4).Reverse();
			sut.ReplaceRange(sub);
			Assert.That(sut.ToList(), Is.EqualTo(new List<int>{ 6, 5, 4, 3, 2 }));
			Assert.That(sut.Current, Is.EqualTo(3));

		}


		[Test]
		public void SetHeadByIndexShouldSetCorrectCurrent()
		{
			sut.ResetHead( );
			sut.SetHeadByIndex(2);
			Assert.That(sut.Current, Is.EqualTo(4));

			sut.MoveRight(4);
			Assert.That(sut.Current, Is.EqualTo(3));

		}

		[Test]
		public void AddShouldWorkInObjectInitializer()
		{
			var expected = new List<int> { 2, 3, 4, 5, 6 };

			Assert.That(sut.ToList( ), Is.EqualTo(expected));
		}


		[Test]
		public void MoveLeftAndRightShouldWorkOnEmptyList()
		{
			var cl = new CircularList<int> { 1 };

			var moveLeft = () => cl.MoveLeft(5);
			var moveRight = () => cl.MoveRight(5);

			Assert.DoesNotThrow(moveLeft.Invoke);
			Assert.DoesNotThrow(moveRight.Invoke);
		}


		[Test]
		public void MoveLeftAndRightShouldThrowOnNegativeValues()
		{
			Assert.Throws<ArgumentException>(() => sut.MoveLeft(-5));
			Assert.Throws<ArgumentException>(() => sut.MoveRight(-5));

		}


		[Test]
		public void MovePointerShouldWrapAround()
		{
			sut.ResetHead( );
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
			sut.ResetHead( );
			sut.MoveRight(2);
			sut.Insert(7, test.after);

			Assert.That(sut.ToList( ), Is.EqualTo(test.expected));

		}

		[TestCase(0, false)]
		[TestCase(2, true)]
		public void TryRemoveShouldNotThrow(int test, bool expected)
		{
			var result = sut.TryRemove(test);
			Assert.That(result, Is.EqualTo(expected));
		}

		private static List<(bool, List<int>)> GetInsertTestCases() => new( )
		{
			( true,  new List<int> { 2, 3, 4, 7, 5, 6 } ),
			( false, new List<int> { 2, 3, 7, 4, 5, 6 } )
		};

	}
}
