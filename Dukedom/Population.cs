using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dukedom
{
	class Population
	{
		public int AtStart { get; set; }
		public int Starvations { get; set; }
		public int KingsLevy { get; set; }
		public int WarCasualties { get; set; }
		public int LootingVictims { get; set; }
		public int DiseaseVictims { get; set; }
		public int NaturalDeaths { get; set; }
		public int Births { get; set; }
		public int Total { get; set; }
		private int BlackPlague { get; set; }
		public int UnhappyWar { get; set; }
		public int UnhappyStavation { get; set; }

		public Population()
		{
			AtStart = 96; //520 FOR I% = 1 TO 8: READ : NEXT
			Starvations = 0;
			KingsLevy = 0;
			WarCasualties = 0;
			LootingVictims = 0;
			DiseaseVictims = 0;
			NaturalDeaths = -4;
			Births = 8;
			BlackPlague = 13;
			//P$(1)="Peasants at start"; // 560 FOR I% = 1 TO 8: READ P$(I%): NEXT
			//P$(2)="Starvations";
			//P$(3)="King's levy";
			//P$(4)="War casualties";
			//P$(5)="Looting victims";
			//P$(6)="Disease victims";
			//P$(7)="Natural deaths";
			//P$(8)="Births";
			UnhappyWar = 0;
			UnhappyStavation = 0;
			//int U1 = 0;Unhappy war 
			//int U2 = 0;Unhappy stavation
			Total = AtStart + Starvations + KingsLevy + WarCasualties + LootingVictims + DiseaseVictims + NaturalDeaths + Births;
		}

		/// <summary>
		/// Food for Peasants
		/// </summary>
		public void FeedPeasant(Grain grain, Land land)
		{
			int Amount;
			bool Loop = true;

			do
			{
				Amount = Functions.ReadInt("Grain for food = ");
				if (Amount < grain.Total)
				{
					Loop = false;
				}
				else
				{
					Functions.PrintF(ConsoleColor.Gray, "You only have " + grain.Total.ToString() + "HL.");
				}
			}
			while (Loop);

			int Fed = Amount / Total;
			Starvations = 0; // So far....

			if (Fed < 14 && Amount < grain.Total)
			{
				Functions.PrintF(ConsoleColor.Red, "The peasent demonstrate before the castle.");
			}
			if (Fed < 13)
			{
				Functions.PrintF(ConsoleColor.DarkYellow, "Some peasants have starved");
				Starvations = -(Total - (Amount / 13));
			}

			UnhappyStavation = UnhappyStavation - (3 * Starvations) - (2 * Fed);

			return;
		}

		/// <summary>
		/// Plague, Birth, Death
		/// </summary>
		public void Events()
		{
			int X1 = Functions.FNR(1, 8);

			// Reset yearly totals
			AtStart = Total;

			if (X1 < 4)
			{
				if (X1 == 1)
				{
					Functions.PrintF(ConsoleColor.Green, "A POX EPIDEMIC has broken out");
					DiseaseVictims = -(Total / (X1 * 5));
				}
				else if (BlackPlague < 0)
				{
					Functions.PrintF(ConsoleColor.DarkGreen, "The BLACK PLAGUE has struck the area");
					BlackPlague = 13;
					DiseaseVictims = -(Total / 3);
				}
			}

			X1 = Functions.FNR(1, 8) + 4;

			if (LootingVictims != 0)
				X1 -= 4;

			Births = (Total / X1);
			NaturalDeaths = (int)(.3 - Total / 22);
			BlackPlague--;
			Total = AtStart + Starvations + KingsLevy + WarCasualties + LootingVictims + DiseaseVictims + NaturalDeaths + Births;
			Starvations = 0;
			KingsLevy = 0;
			WarCasualties = 0;
			LootingVictims = 0;
			DiseaseVictims = 0;
		}
	}
}
