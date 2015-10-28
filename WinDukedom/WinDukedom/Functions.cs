using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace WinDukedom
{
	class Functions
	{
		public static int FNR(int MinVal, int MaxVal)
		{
			Random random = new Random();
			return random.Next(MinVal, MaxVal);
		}

		/// <summary>
		/// Read Yes/No
		/// </summary>
		/// <returns>(Y)es = True / (N)o = False</returns>
		public static bool ReadYesNo(string Prompt)
		{
			Print(ConsoleColor.Cyan, Prompt);

			while (true)
			{
				string V = Console.ReadLine();
				if (V.Length == 0)
					V = "N";

				V = V.Substring(0, 1).ToUpper();

				if (V == "Y")
					return true;
				if (V == "N")
					return false;

				Print(ConsoleColor.Red, "Please enter (Y)es or (N)o:");
			}
		}

		/// <summary>
		/// Input numeric response
		/// </summary>
		/// <returns></returns>
		public static int ReadInt(string Prompt)
		{
			Print(ConsoleColor.Cyan, Prompt);

			while (true)
			{
				string V = Console.ReadLine();
				if (V.Length == 0)
					V = "0";

				int value;
				if (int.TryParse(V, out value))	// Try to parse the string as an integer
				{
					if (value >= 0)
						return value;
				}

				Print(ConsoleColor.Red, "Please enter a non-negative number:");
			}
		}

		/// <summary>
		/// Output to the console with color enabled.
		///  Black as default background color
		/// </summary>
		/// <param name="color"></param>
		/// <param name="Output"></param>
		public static void PrintF(ConsoleColor foregroundColor, string output)
		{
			PrintF(foregroundColor, ConsoleColor.Black, output);
		}
		/// <summary>
		/// Output to the console with color enabled.
		/// </summary>
		/// <param name="foregroundColor"></param>
		/// <param name="backgroundColor"></param>
		/// <param name="output"></param>
		public static void PrintF(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string output)
		{
			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;
			Console.WriteLine(output);
			Console.ResetColor();
		}

		public static void Print(ConsoleColor foregroundColor, string output)
		{
			Print(foregroundColor, ConsoleColor.Black, output);
		}
		public static void Print(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string output)
		{
			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;
			Console.Write(output);
			Console.ResetColor();
		}

		public DataSet LoadXMLDataSource(string dataPath)
		{
			DataSet result = null;
			//string path = this.MapPath(dataPath);
			if (System.IO.File.Exists(dataPath))
			{
				result = new DataSet();
				result.ReadXml(dataPath, XmlReadMode.ReadSchema);
			}
			else
				result = null;
			return result;
		}

		private void SaveXMLDataSource(string dataPath, DataSet source)
		{
			//string path = this.MapPath(dataPath);
			source.WriteXml(dataPath, XmlWriteMode.WriteSchema);
		}
	}
}
