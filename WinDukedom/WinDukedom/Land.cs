using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinDukedom
{
	public partial class Land : Form
	{
		public int AtStart { get; set; }
		public int Bought { get; set; }
		public int Sold { get; set; }
		public int FruitsOfWar { get; set; }
		public int Total { get; set; }
		public int[] Percent { get; set; }
		public int Value { get; set; }

		public Land()
		{
			InitializeComponent();

			AtStart = 600; // 530 FOR I% = 1 TO 3: READ : NEXT
			Bought = 0;
			Sold = 0;
			FruitsOfWar = 0;
			Value = 20;
			Total = AtStart + Bought + Sold + FruitsOfWar;

			//L$(1)="Land at start"; // 570 FOR I% = 1 TO 3: READ L$(I%): NEXT
			//L$(2)="Bought/sold";
			//L$(3)="Fruits of war";
			Percent = new int[6];

			Percent[0] = 216; // @100% // 550 FOR I% = 1 TO 6: READ S(I%): NEXT
			Percent[1] = 200; // @80%
			Percent[2] = 184; // @60%
			Percent[3] = 0; // @40%
			Percent[4] = 0; // @20%
			Percent[5] = 0; // Depleated
		}

		/// <summary>
		/// Buy Land
		/// </summary>
		public bool BuyLand(Grain grain)
		{
			int Amount;
			bool Loop = true;

			Value += Functions.FNR(1, 10) - 5; // TODO : Need to bell curve this price with a trend

			do
			{
				//int X1 = INT(2 * C + FNX % (1) - 5);
				//X1 = -X1 * (X1 >= 4) - 4 * (X1 < 4);
				Amount = Functions.ReadInt("Land to buy at " + Value.ToString() + "HL./HA. = ");
				if (Amount == 0)
				{
					return false;
				}
				else if ((Amount * Value) > grain.Total)
				{
					Console.WriteLine("But you don't have enough grain for that.");
					Console.WriteLine("You have " + grain.Total.ToString() + "HL. of grain left.");
					int Total = grain.Total / Value;
					Console.WriteLine("Enough to buy " + Total.ToString() + "HA. of land");
				}
				else
				{
					Loop = false;
				}
			}
			while (Loop);

			//Distribute new land into the land buckets.
			int BinLand = Amount / 6;
			int BonusLand = Amount - (BinLand * 6); // Catch any missed in rounding MOD ought to do this as well

			for (int buck = 0; buck < 6; buck++) // Assign a bit of each percent
			{
				Percent[buck] += BinLand;
			}

			Percent[Functions.FNR(0, 5)] += BonusLand; // add the extra to lucky winner
			Bought = Amount + BonusLand;
			return true;
		}

		/// <summary>
		/// Sell Land
		/// </summary>
		/// <param name="TotalGrain"></param>
		public bool SellLand(int KingStatus)
		{
			int Amount;
			int RunningTotal;

			for (int J1 = 0; J1 <= 3; J1++)
			{
				Value--;
				Amount = Functions.ReadInt("Land to sell at " + Value.ToString() + "HL./HA = ");
				if (Amount == 0)
				{
					return false;
				}
				else if (Amount > Total)
				{
					Functions.PrintF(ConsoleColor.Red, "But you only have " + Total.ToString() + "HA. of good land.");
				}
				else
				{
					if ((Value * Amount) > 4000)
					{
						Functions.PrintF(ConsoleColor.Red, "No buyers have that much grain try less.");
					}
					else
					{
						if (Value < 10 && KingStatus == 0)
						{
							Functions.PrintF(ConsoleColor.DarkRed, "The High King appropiaters half of your earnings as punishment for selling at such a low price.");
							Value = Value / 2;
						}

						RunningTotal = Amount;

						// Sell the least productive land first
						for (int buck = 5; buck >= 0; buck--)
						{
							Percent[buck] -= RunningTotal;
							if (Percent[buck] < 0)
							{
								RunningTotal = -Percent[buck]; // Save off the excess
								Percent[buck] = 0; // set to min
							}
							else // Stop the loop (we're done enough)
							{
								break;
							}
						}
						Sold = Amount;
						return true;
					}
				}
			}
			Functions.PrintF(ConsoleColor.Red, "Buyers have lost interest.");
			return false;
		}

		/// <summary>
		/// Advance land to be more productive
		/// </summary>
		public void Events()
		{
			for (int buck = 5; buck > 0; buck--)
			{
				int move;

				if (Percent[buck] <= 10) // If the total is less than 10 arcres move the whole thing instead.
					move = Percent[buck];
				else
					move = Functions.FNR(0, Percent[buck]); // Maybe have a MAX value as well?

				Percent[buck - 1] += move;
				Percent[buck] -= move;
			}
			// Reset yearly totals
			AtStart = Total;
			Total = AtStart + Bought + Sold + FruitsOfWar;
			Bought = 0;
			Sold = 0;
			FruitsOfWar = 0;
		}

		/// <summary>
		/// Return a total amount of producing arcres
		/// </summary>
		/// <param name="LandPlanted"></param>
		/// <returns></returns>
		public int Plant(Grain grain, Population population)
		{
			int Amount;
			int Remaining;
			int RunningTotal = 0;
			float Per = 1;

			bool Loop = true;
			do
			{
				Amount = Functions.ReadInt("Land to be planted = ");
				if (Amount > Total)
				{
					Functions.PrintF(ConsoleColor.Red, "But you don't have enough land.");
					Functions.PrintF(ConsoleColor.Yellow, "You only have " + Total.ToString() + "HA. of land.");
				}
				else
				{
					if ((Amount * 2) > grain.Total)
					{
						int Plantable = grain.Total / 2;
						Functions.PrintF(ConsoleColor.Red, "Not enough grain.");
						Functions.PrintF(ConsoleColor.Yellow, "You only have enough to plant " + Plantable.ToString() + "HA. of land.");
					}
					else
					{
						if (Amount > (population.Total * 4))
						{
							int Workable = population.Total * 4;
							Functions.PrintF(ConsoleColor.Red, "But you don't have enoung peasants.");
							Functions.PrintF(ConsoleColor.Yellow, "Your peasants can only plant " + Workable.ToString() + "HA. of land.");
						}
						else
						{
							//grain.Production(land.Plant(Amount));
							Loop = false;
						}
					}
				}
			} while (Loop);

			Remaining = Amount;

			for (int buck = 0; buck < 6; buck++)
			{
				if (Remaining > Percent[buck])
				{
					RunningTotal += (int)((float)Percent[buck] * Per);
					Remaining -= Percent[buck];
				}
				else
				{
					RunningTotal += Remaining;
					Remaining = 0;
				}
				Per -= 0.20f;
			}

			return RunningTotal;
		}
	}
}
