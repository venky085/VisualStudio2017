using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class Person
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public async Task Demo()
		{
			string fullName = string.Format("{0} {1}", FirstName, LastName);

			Person person = new Person();
			person.FirstName = "James";
			person.LastName = "Bond";

			switch (person.FirstName)
			{
				case "James":
					Console.WriteLine("Spectre");
					break;
			}

			//JsonTextReader jsonTextReader = new JsonTextReader();

			string letters = "abcdefghijklmnopqrstuvwxyz";
		}

		public string GetAddress()
		{
			return "a";
		}
	}

	public class Customer : Person
	{

	}
}