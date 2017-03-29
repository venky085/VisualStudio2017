using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			// Out Variables
			string input = "1";
			int numericResult;
			if (int.TryParse(input, out numericResult))
				Console.WriteLine(numericResult);
			else
				Console.WriteLine("Could not parse input");

			if (int.TryParse(input, out int result))
				Console.WriteLine(result);
			else
				Console.WriteLine("Could not parse input");

			if (int.TryParse(input, out var answer))
				Console.WriteLine(answer);
			else
				Console.WriteLine("Could not parse input");


			// Tuples
			var letters = ("a", "b");
			(string Alpha, string Beta) namedLetters = ("a", "b");
			var alphabetStart = (Alpha: "a", Beta: "b");
			(string First, string Second) firstLetters = (Alpha: "a", Beta: "b");


			// error CS8182: Predefined type 'ValueTuple`2' must be a struct.
			//var numbers = Enumerable.Range(1, 10);
			//var range = Range(numbers);
			//(int min, int max) = Range(numbers);

			//var p = new Point(3.14, 2.71);
			//(double X, double Y) = p;
			//(double horizontalDistance, double verticalDistance) = p;


			// Pattern Matching
			var values = new object[] { 1, 2, new object[] { 3, 4 } };
			var diceSum2 = DiceSum2(values);
			Console.WriteLine("DiceSum2: {0}", diceSum2);

			// Ref locals and returns
			int[,] matrix = new int[,] { { 0, 1, 2 }, { 10, 11, 12 }, { 20, 21, 22 }, { 30, 31, 32 }, { 40, 41, 42 } };
			//var indices = Find(matrix, (val) => val == 42);
			//Console.WriteLine(indices);
			//matrix[indices.i, indices.j] = 24;

			//var valItem = Find3(matrix, (val) => val == 42);
			//Console.WriteLine(valItem);
			//valItem = 24;
			//Console.WriteLine(matrix[4, 2]);

			ref var item = ref Find3(matrix, (val) => val == 42);
			Console.WriteLine(item);
			item = 24;
			Console.WriteLine(matrix[4, 2]);

			// Local functions
			AlphabetSubset3('a', 'z');

			// Throw expressions
			Person person = GetPerson();
			if (person == null)
				throw new InvalidOperationException("Person is null");

			Person person2 = GetPerson() ?? throw new InvalidOperationException("Person is null");

			//throw new Exception("Root exception", new Exception("Inner Exception", new Exception("Inner Inner Exception", new Exception("Inner Inner Inner Exception"))));

			person2.Demo();

			Console.ReadLine();
		}

		private static (int Min, int Max) Range(IEnumerable<int> numbers)
		{
			int min = int.MaxValue;
			int max = int.MinValue;
			foreach (var n in numbers)
			{
				min = (n < min) ? n : min;
				max = (n > max) ? n : max;
			}
			return (min, max);
		}

		public static int DiceSum(IEnumerable<int> values)
		{
			return values.Sum();
		}

		public static int DiceSum2(IEnumerable<object> values)
		{
			var sum = 0;
			foreach (var item in values)
			{
				if (item is int val)
					sum += val;
				else if (item is IEnumerable<object> subList)
					sum += DiceSum2(subList);
			}
			return sum;
		}

		public static int DiceSum3(IEnumerable<object> values)
		{
			var sum = 0;
			foreach (var item in values)
			{
				switch (item)
				{
					case int val:
						sum += val;
						break;
					case IEnumerable<object> subList:
						sum += DiceSum3(subList);
						break;
				}
			}
			return sum;
		}

		public static int DiceSum4(IEnumerable<object> values)
		{
			var sum = 0;
			foreach (var item in values)
			{
				switch (item)
				{
					case 0:
						break;
					case int val:
						sum += val;
						break;
					case IEnumerable<object> subList when subList.Any():
						sum += DiceSum4(subList);
						break;
					case IEnumerable<object> subList:
						break;
					case null:
						break;
					default:
						throw new InvalidOperationException("unknown item type");
				}
			}

			return sum;
		}

		public static int DiceSum5(IEnumerable<object> values)
		{
			var sum = 0;
			foreach (var item in values)
			{
				switch (item)
				{
					case 0:
						break;
					case int val:
						sum += val;
						break;
					case PercentileDie die:
						sum += die.Multiplier * die.Value;
						break;
					case IEnumerable<object> subList when subList.Any():
						sum += DiceSum5(subList);
						break;
					case IEnumerable<object> subList:
						break;
					case null:
						break;
					default:
						throw new InvalidOperationException("unknown item type");
				}
			}

			return sum;
		}

		public static (int i, int j) Find(int[,] matrix, Func<int, bool> predicate)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					if (predicate(matrix[i, j]))
						return (i, j);
			return (-1, -1); // Not found
		}

		// Note that this won't compile. 
		// Method declaration indicates ref return,
		// but return statement specifies a value return.
		//public static ref int Find2(int[,] matrix, Func<int, bool> predicate)
		//{
		//	for (int i = 0; i < matrix.GetLength(0); i++)
		//		for (int j = 0; j < matrix.GetLength(1); j++)
		//			if (predicate(matrix[i, j]))
		//				return matrix[i, j];
		//	throw new InvalidOperationException("Not found");
		//}

		public static ref int Find3(int[,] matrix, Func<int, bool> predicate)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
				for (int j = 0; j < matrix.GetLength(1); j++)
					if (predicate(matrix[i, j]))
						return ref matrix[i, j];
			throw new InvalidOperationException("Not found");
		}

		public static IEnumerable<char> AlphabetSubset3(char start, char end)
		{
			if ((start < 'a') || (start > 'z'))
				throw new ArgumentOutOfRangeException(paramName: nameof(start), message: "start must be a letter");
			if ((end < 'a') || (end > 'z'))
				throw new ArgumentOutOfRangeException(paramName: nameof(end), message: "end must be a letter");

			if (end <= start)
				throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

			return alphabetSubsetImplementation();

			IEnumerable<char> alphabetSubsetImplementation()
			{
				for (var c = start; c < end; c++)
					yield return c;
			}
		}

		public static Person GetPerson() => new Person();

		// Generalized async return types
		public async ValueTask<int> Func()
		{
			await Task.Delay(100);
			return 5;
		}

		// Numeric literal syntax improvements
		public const long BillionsAndBillions = 100_000_000_000;
		public const double AvogadroConstant = 6.022_140_857_747_474e23;
		public const decimal GoldenRatio = 1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;

		public const int One = 0b0001;
		public const int Sixteen = 0b0001_0000;
	}

	// More expression-bodied members
	public class ExpressionMembersExample
	{
		// Expression-bodied constructor
		public ExpressionMembersExample(string label) => this.Label = label;

		// Expression-bodied finalizer
		~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");

		private string label;

		// Expression-bodied get / set accessors.
		public string Label
		{
			get => label;
			set => this.label = value ?? "Default label";
		}
	}
}
