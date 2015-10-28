using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dukedom
{
	public enum Victory
	{
		CompleteLoss,
		SimpleLoss,
		Draw,
		SimpleWin,
		CompleteWin
	}

	class War
	{
		public int Mercenaries { get; set; }
		public int GrainCost { get; set; }
		public int SoWGrain { get; set; }
		public int SoWLand { get; set; }
		public int DeathToll { get; set; }

		/// <summary>
		/// WAR
		/// </summary>
		public Victory WithDuke(Grain grain, Land land, Population population)
		{
			int Casualties;
			int LandTaken;
			int Offense;
			int Defense;
			int Hired = 0;

			Functions.PrintF(ConsoleColor.Red, "A near by Duke threatens war; ");
			Offense = (85 + 18 * Functions.FNR(1, 6));
			Defense = (population.Total * Functions.FNR(1, 3)) + 13;

			if (Functions.ReadYesNo("Will you attack first?") == false)
			{
				if (Offense < Defense)
				{
					Functions.PrintF(ConsoleColor.Green, "Peace negotiations successful");
					return Victory.Draw;
				}
			}
			else
			{
				if (Functions.FNR(1, 20) != 1)
				{
					Functions.PrintF(ConsoleColor.DarkYellow, "First strike failed - you need professionals");
					Hired = HireMercinaries(40, 75);
					GrainCost = Hired * 40;
				}
			}

			LandTaken = Offense - (Defense + (Hired * 2));

			if (-LandTaken > land.Total)
			{
				Functions.PrintF(ConsoleColor.Red, "You have been over run and have lost your entire Dukedom");
				return Victory.CompleteLoss;
			}

			if (LandTaken > 400)
			{
				Functions.PrintF(ConsoleColor.DarkGreen, "You have overrun the enemy and annexed his entire Dukedom");

				SoWGrain = 3513;
				SoWLand = LandTaken;
				DeathToll = Defense - 200;
				return Victory.CompleteWin;
			}

			if (LandTaken > 1)
			{
				Functions.PrintF(ConsoleColor.Green, "You have won the war");
				SoWGrain = (int)((float)LandTaken * 1.7);
				SoWLand = LandTaken;
				DeathToll = -Defense;
				return Victory.SimpleWin;
			}
			else
			{
				Functions.PrintF(ConsoleColor.Red, "You have lost the war");
				SoWGrain = -(int)((float)LandTaken * 1.7);
				SoWLand = -LandTaken;
				DeathToll = -Defense;
				return Victory.SimpleLoss;
			}

			return Victory.Draw;
		}

		/// <summary>
		/// Hire some mercinaries
		/// </summary>
		/// <param name="Cost">Cost in grain</param>
		/// <returns></returns>
		private int HireMercinaries(int Cost, int MaxNumber)
		{
			bool loop = true;
			int Result;

			do
			{
				Result = Functions.ReadInt("How many mercenaries will you hire at " + Cost.ToString() + " HL. each = ");
				if (Result > MaxNumber)
				{
					Functions.PrintF(ConsoleColor.Yellow, "There are only " + MaxNumber.ToString() + " avaiable for hire.");
				}
				else
				{
					loop = false;
				}
			}
			while (loop);

			return Result;
		}

		/// <summary>
		/// War with the King
		/// </summary>
		/// <returns></returns>
		public bool WithKing(Grain grain, Land land, Population population)
		{
			int Count = (grain.Total / 100);

			Functions.PrintF(ConsoleColor.Red, "\nThe king's army is about to attack your ducy!!!");
			Functions.PrintF(ConsoleColor.Yellow, "At 100 HL. each (pay in advance).");
			Functions.PrintF(ConsoleColor.Yellow, "You have hired " + Count.ToString() + " foreign mercinaries.");

			if (land.Total * Count + population.Total < 2500)
			{
				Functions.PrintF(ConsoleColor.DarkRed, "\nYour head is placed atop of the castle gate.");
				return false;
			}
			else
			{
				Functions.PrintF(ConsoleColor.DarkGreen, "\nWipe the blood from the crown - you are High King!");
				Functions.PrintF(ConsoleColor.Yellow, "A near by monarchy THREATENS WAR!");
				Functions.PrintF(ConsoleColor.Yellow, "HOW MANY ........\n\n\n");
				return true;
			}
		}
	}
}
