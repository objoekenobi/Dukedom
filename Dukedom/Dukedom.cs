using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dukedom
{
	class Dukedom
	{
		public Grain grain { get; set; }
		public Land land { get; set; }
		public Population population { get; set; }
		public War war { get; set; }
		private int Year { get; set; }
		private bool Reports { get; set; }
		private int KingStatus { get; set; }
		private bool Dead { get; set; }

		/// <summary>
		/// DUKEDOM - Quick Basic
		/// </summary>
		public Dukedom()
		{
			///
			/// Intro to the game
			///
			Functions.PrintF(ConsoleColor.Yellow, "D U K E D O M");
			Functions.PrintF(ConsoleColor.Gray, "Converted by");
			Functions.PrintF(ConsoleColor.White, "Bob Anderson");
			Reports = !Functions.ReadYesNo("Do you want to skip detailed reports?");

			grain = new Grain();
			land = new Land();
			population = new Population();
			war = new War();

			///
			/// Start New Game
			///
			Year = 0; // Set Year
			KingStatus = 0;
			Dead = false;
		}

		/// <summary>
		/// Main program loop
		/// </summary>
		public void RunGame()
		{
			while (CheckGame())
			{
				DisplayLastYear();

				#region Feed the Peasants

				population.FeedPeasant(grain, land); // OK commit the feeding

				#endregion

				#region Buy and Sell land

				if (land.BuyLand(grain)) // Buy land
				{
					grain.LandDeals = -(land.Value * land.Bought);
				}
				else if (land.SellLand(KingStatus)) // or Sell land
				{
					grain.LandDeals = (land.Value * land.Sold);
				}
				#endregion

				#region War with King
				if (KingStatus == 2)
				{
					war.WithKing(grain, land, population);
				}
				#endregion

				#region Planting and taxes
				grain.Seeding = (land.Plant(grain, population) * 2);
				#endregion

				#region Taxes and such
				if (KingStatus > 0)
				{
					grain.RoyalTax = land.Total / 2;

					if (Functions.FNR(1, 4) == 1)
					{
						int Servants = population.Total * (Functions.FNR(1, 3) * 10);
						int Fee = land.Total % 25;
						Console.WriteLine("The King requires" + Servants.ToString() + "peasants for his estate and mines.");

						if (Functions.ReadYesNo("Will you supply them (Y)es or pay" + (Fee * 100) + "HL. of grain instead (N)o?"))
						{
							population.KingsLevy = Servants;
						}
						else
						{
							grain.RoyalTax = -100 * Fee;
						}
					}
					else
					{
						if (grain.RoyalTax > (grain.Total + grain.RoyalTax))
						{
							Console.WriteLine("You have insufficient grain to pay the royal tax.");
							grain.RoyalTax = 0;
							KingStatus = 2;
						}
					}
				}
				else
				{
					grain.RoyalTax = land.Total / 3;
					if (grain.RoyalTax > (grain.Total + grain.RoyalTax))
					{
						Console.WriteLine("You have insufficient grain to pay the royal tax.");
						grain.RoyalTax = 0;
						KingStatus = 2;
					}
				}
				#endregion

				#region War with a Duke

				int WarChance = Prosperity();

				if (KingStatus == 1 || population.Total <= 109 || 17 * (land.Total - 400) + grain.Total > 10600)
				{
					Console.WriteLine("The High King grows uneasy and may be subsidizing wars against you.");
					WarChance += 2;
				}

				if (Functions.FNR(1, 6) < WarChance)
				{
					Victory Result;
					Result = war.WithDuke(grain, land, population);

					if (Result == Victory.CompleteWin)
					{
						if (KingStatus == 0)
						{
							Console.WriteLine("The King fears for his throne and may be planing direct action.");
							KingStatus = 1;
						}
						else if (KingStatus == 1)
						{
							Console.WriteLine("The High King calls for peasants levies and hires many foreign mercenaries.");
							KingStatus = 2;
						}
					}

					if (war.GrainCost > grain.Total)
					{
						Console.WriteLine("There isn't enough grain to pay the mercenaries");
						grain.MercenaryHire = grain.Total;
						population.WarCasualties = (war.GrainCost - grain.Total) / 50;
					}
					else if (Result == Victory.SimpleWin)
					{
						population.WarCasualties = war.DeathToll;
						grain.MercenaryHire = war.GrainCost;
						grain.FruitsOfWar = war.SoWGrain;
					}
					else if (Result == Victory.Draw)
					{
					}
					else if (Result == Victory.SimpleLoss)
					{
						population.WarCasualties = war.DeathToll;
						grain.MercenaryHire = war.GrainCost;
						grain.FruitsOfWar = war.SoWGrain;
					}
					else if (Result == Victory.CompleteLoss)
					{
						Dead = true;
					}
				}

				#endregion

				grain.Events(Year, KingStatus);
				land.Events();
				population.Events();
				Year++;

				Functions.PrintF(ConsoleColor.Cyan, "Press any key to continue.");
				Console.ReadKey();
			}
		}

		/// <summary>
		/// Test for the end if game
		/// </summary>
		/// <returns></returns>
		private bool CheckGame()
		{
			if (Dead)
			{
				return false;
			}

			if (population.Total < 33)
			{
				Functions.PrintF(ConsoleColor.Red, "You have so few peasants left that the High King has abolished your Ducal right.");
				return false;
			}

			if (land.Total < 199)
			{
				Functions.PrintF(ConsoleColor.Red, "You have so little land left that you are desposed.");
				return false;
			}

			if (population.UnhappyWar > population.Total || population.UnhappyStavation > population.Total)
			{
				Functions.PrintF(ConsoleColor.Red, "The peasants tired of war and stavation you are desposed");
				return false;
			}

			if (Year > 45 && KingStatus == 0)
			{
				Functions.PrintF(ConsoleColor.Green, "You have reached the age of retirement.");
				return false;
			}

			if (KingStatus > 0)
			{
				KingStatus = 2;
				if (Functions.ReadYesNo("The King demands twice the royal tax in IN THE HOPE OF WAR. WILL YOU PAY?"))
					KingStatus = 1;
			}

			return true;
		}

		/// <summary>
		/// Display Last years results
		/// </summary>
		public void DisplayLastYear()
		{
			Functions.PrintF(ConsoleColor.Green, "\nYear :" + Year + "  Peasents :" + population.Total + "  Land :" + land.Total + "  Grain :" + grain.Total);

			if (Reports)
			{
				Functions.PrintF(ConsoleColor.White, "\nPeasants at start   :" + population.AtStart);
				if (population.Starvations != 0)
					Functions.PrintF(ConsoleColor.White, "Starvations         :" + population.Starvations);
				if (population.KingsLevy != 0)
					Functions.PrintF(ConsoleColor.White, "King's levy         :" + population.KingsLevy);
				if (population.WarCasualties != 0)
					Functions.PrintF(ConsoleColor.White, "War casualties      :" + population.WarCasualties);
				if (population.LootingVictims != 0)
					Functions.PrintF(ConsoleColor.White, "Looting victims     :" + population.LootingVictims);
				if (population.DiseaseVictims != 0)
					Functions.PrintF(ConsoleColor.White, "Disease victims     :" + population.DiseaseVictims);
				if (population.NaturalDeaths != 0)
					Functions.PrintF(ConsoleColor.White, "Natural deaths      :" + population.NaturalDeaths);
				if (population.Births != 0)
					Functions.PrintF(ConsoleColor.White, "Births              :" + population.Births);
				if (population.AtStart != population.Total)
					Functions.PrintF(ConsoleColor.White, "Peasants at end     :" + population.Total);

				Functions.PrintF(ConsoleColor.Yellow, "\nLand at start       :" + land.AtStart);
				if (land.Bought != 0)
					Functions.PrintF(ConsoleColor.Yellow, "Bought        :" + land.Bought);
				if (land.Sold != 0)
					Functions.PrintF(ConsoleColor.Yellow, "Sold          :" + land.Sold);
				if (land.FruitsOfWar != 0)
					Functions.PrintF(ConsoleColor.Yellow, "Fruits of war :" + land.FruitsOfWar);
				if (land.AtStart != land.Total)
					Functions.PrintF(ConsoleColor.Yellow, "Land at end of year :" + land.Total);

				Functions.PrintF(ConsoleColor.Green, "\n100%\t80%\t60%\t40%\t20%\tDepl");
				Functions.PrintF(ConsoleColor.Green, string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", land.Percent[0], land.Percent[1], land.Percent[2], land.Percent[3], land.Percent[4], land.Percent[5]));

				Functions.PrintF(ConsoleColor.Blue, "\nGrain at start       :" + grain.AtStart);
				if (grain.UsedForFood != 0)
					Functions.PrintF(ConsoleColor.Blue, "Used for food        :" + grain.UsedForFood);
				if (grain.LandDeals != 0)
					Functions.PrintF(ConsoleColor.Blue, "Land deals           :" + grain.LandDeals);
				if (grain.Seeding != 0)
					Functions.PrintF(ConsoleColor.Blue, "Seeding              :" + grain.Seeding);
				if (grain.RatLossess != 0)
					Functions.PrintF(ConsoleColor.Blue, "Rat lossess          :" + grain.RatLossess);
				if (grain.MercenaryHire != 0)
					Functions.PrintF(ConsoleColor.Blue, "Mercenary hire       :" + grain.MercenaryHire);
				if (grain.FruitsOfWar != 0)
					Functions.PrintF(ConsoleColor.Blue, "Fruits of war        :" + grain.FruitsOfWar);
				if (grain.CropYield != 0)
					Functions.PrintF(ConsoleColor.Blue, "Crop yield           :" + grain.CropYield);
				if (grain.CastleExpense != 0)
					Functions.PrintF(ConsoleColor.Blue, "Castle expense       :" + grain.CastleExpense);
				if (grain.RoyalTax != 0)
					Functions.PrintF(ConsoleColor.Blue, "Royal tax            :" + grain.RoyalTax);
				Functions.PrintF(ConsoleColor.Blue, "Grain at end of year :" + grain.Total);

				if (Year <= 0)
					Functions.PrintF(ConsoleColor.Red, "\n(Severe crop damage due to seven year locusts)");
				else
					Functions.PrintF(ConsoleColor.Red, "\n");
			}
		}

		/// <summary>
		/// Quick calculation of prosperity
		/// </summary>
		/// <returns></returns>
		private int Prosperity()
		{
			int Result;

			Result = 0;
			if (population.Total <= 109)
				Result++;

			if (land.Total > 400)
				Result++;

			if (grain.Total > 10600)
				Result++;

			return Result;
		}
	}
}
