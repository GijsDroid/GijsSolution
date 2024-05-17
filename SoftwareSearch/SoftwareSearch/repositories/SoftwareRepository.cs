using SoftwareSearch.Data;

namespace SoftwareSearch.repositories
{
	public class SoftwareRepository
	{
		public IEnumerable<Software> GetAll()
		{
			return SoftwareManager.GetAllSoftware();
		}

		public IEnumerable<Software> Search(string searchQuery)
		{
			var result = GetAll();

			if (string.IsNullOrEmpty(searchQuery))
			{
				return result;
			}

			var parts = searchQuery.Split(' ');
			foreach (var part in parts)
			{
				if (string.IsNullOrEmpty(part))
				{
					continue;
				}

				var isVersionNumber = IsVersionNumber(part);

				if (isVersionNumber)
				{
					result = result.Where(software => VersionNumberALessThenVersionNumberB(part, software.Version));
					continue;
				}

				result = result.Where(software => AContainsB(software.Name, part));
			}

			return result;
		}

		public static bool AContainsB(string a, string b)
		{
			if (string.IsNullOrEmpty(b))
			{
				return true;
			}

			if (string.IsNullOrEmpty(a))
			{
				return false;
			}

			return a.Contains(b, StringComparison.OrdinalIgnoreCase);
		}

		public static bool VersionNumberALessThenVersionNumberB(string versionNumberA, string versionNumberB)
		{
			if (!IsVersionNumber(versionNumberA) || !IsVersionNumber(versionNumberB))
			{
				throw new Exception("Input was not a versionNumber");
			}

			var numbersA = versionNumberA.Split('.').Select(o => int.Parse(o)).ToArray();
			var numbersB = versionNumberB.Split('.').Select(o => int.Parse(o)).ToArray();

			for (var index = 0; index < 5; index++)
			{
				var numberA = 0;
				var numberB = 0;

				if (index + 1 <= numbersA.Length)
				{
					numberA = numbersA[index];
				}

				if (index + 1 <= numbersB.Length)
				{
					numberB = numbersB[index];
				}

				if (numberA == numberB)
				{
					continue;
				}

				return numberA <= numberB;
			}

			return false;
		}

		public static bool IsVersionNumber(string versionNumber)
		{
			if (string.IsNullOrEmpty(versionNumber))
			{
				return false;
			}

			var numbers = versionNumber.Split('.');

			if (numbers.Length < 0 || numbers.Length > 5)
			{
				// version number is one to five numbers.
				return false;
			}

			return numbers.All(number => uint.TryParse(number, out var _));
		}
	}
}
