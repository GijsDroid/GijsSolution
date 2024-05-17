using SoftwareSearch.repositories;

namespace SoftwareSearchTests
{
	[TestClass]
	public class SoftwareRepositoryTests
	{
		[TestMethod]
		[DataRow("test")]
		[DataRow("1,2,3,4,5,6")]
		[DataRow(null)]
		[DataRow("1..3")]
		[DataRow("1.1.-3")]
		[DataRow("")]
		public void ÍsVersionNumber_NotAVersionNumber_False(string input)
		{
			// Act

			var result = SoftwareRepository.IsVersionNumber(input);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		[DataRow("1")]
		[DataRow("1.3.5")]
		[DataRow("1.2.3.4.5")]
		public void ÍsVersionNumber_AVersionNumber_True(string input)
		{
			// Act

			var result = SoftwareRepository.IsVersionNumber(input);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		[DataRow("1", "2")]
		[DataRow("1.3.5", "2.3")]

		[DataRow("3", "4.2")]
		public void VersionNumberALessThenVersionNumberB_BiIsEqualOrBigger_True(string inputA, string inputB)
		{
			// Act

			var result = SoftwareRepository.VersionNumberALessThenVersionNumberB(inputA, inputB);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		[DataRow("3", "2")]
		[DataRow("4.3.5", "2.3")]
		[DataRow("2.2.3.4.5", "1.2.3.4.5")]
		[DataRow("5", "4.2")]
		[DataRow("1.2.3.4.5", "1.2.3.4.5")]
		public void VersionNumberALessThenVersionNumberB_BiIsSmaller_False(string inputA, string inputB)
		{
			// Act

			var result = SoftwareRepository.VersionNumberALessThenVersionNumberB(inputA, inputB);

			// Assert
			Assert.IsFalse(result);
		}


		[ExpectedException(typeof(Exception))]
		[TestMethod]
		[DataRow("1", "test")]
		[DataRow("test", "2.3")]
		[DataRow("test", "test")]
		public void VersionNumberALessThenVersionNumberB_InputIsNotAVersionNumber(string inputA, string inputB)
		{
			// Act
			var result = SoftwareRepository.VersionNumberALessThenVersionNumberB(inputA, inputB);

		}

		[TestMethod]
		[DataRow("test", "es")]
		[DataRow("Gijs", "IJS")]
		[DataRow("helemaal", "helemaal")]
		[DataRow("test", null)]
		[DataRow(null, null)]
		public void AContainsB_AContainsB_True(string inputA, string inputB)
		{
			// Act

			var result = SoftwareRepository.AContainsB(inputA, inputB);

			// Assert
			Assert.IsTrue(result);
		}


		[TestMethod]
		[DataRow("te123", "es")]
		[DataRow("Giasd", "IJS")]
		[DataRow("helqweqwel", "helemaal")]
		[DataRow(null, "test")]
		public void AContainsB_AContainsNotB_False(string inputA, string inputB)
		{
			// Act

			var result = SoftwareRepository.AContainsB(inputA, inputB);

			// Assert
			Assert.IsFalse(result);
		}
	}
}