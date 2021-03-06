﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dukedom
{
	class Grain
	{
		public int AtStart {get;set;}
		public int UsedForFood {get;set;}
		public int LandDeals {get;set;}
		public int Seeding {get;set;}
		public int RatLossess {get;set;}
		public int MercenaryHire {get;set;}
		public int FruitsOfWar {get;set;}
		public int CropYield {get;set;}
		public int CastleExpense {get;set;}
		public int RoyalTax {get;set;}
		public int Total {get { return AtStart+UsedForFood+LandDeals+Seeding+RatLossess+MercenaryHire+FruitsOfWar+CropYield+CastleExpense+RoyalTax;}}

		public Grain()
		{
			AtStart =5193; // 540 FOR I% = 1 TO 10: READ G(I%): NEXT
			UsedForFood =-1344;
			LandDeals =0;
			Seeding =-768;
			RatLossess =0;
			MercenaryHire =0;
			FruitsOfWar =0;
			CropYield =1516;
			CastleExpense =-120;
			RoyalTax =-300;

			//G$(1)="Grain at start"; // 580 FOR I% = 1 TO 10: READ G$(I%): NEXT
			//G$(2)="Used for food";
			//G$(3)="Land deals";
			//G$(4)="Seeding";
			//G$(5)="Rat lossess";
			//G$(6)="Mercenary hire";
			//G$(7)="Fruits of war";
			//G$(8)="Crop yield";
			//G$(9)="Castle expense";
			//G$(10)="Royal tax";
		}

		///
		/// UPDATE Counter and Continue
		///
		public void Events(int YearCount,int KingStatus)
		{
            AtStart = Total;

            CropYield = (Seeding / 2) * Functions.FNR(4, 13);
            Console.WriteLine("Yield : " + CropYield + " HL./HA.");

            if (YearCount > 0 && (YearCount % 7 == 0))
            {
				Console.WriteLine("Seven year locusts");
			    CropYield = (int)(CropYield * .65);
            }

            if(Functions.FNR(1,5) == 1)
            {
			    Console.WriteLine( "Rats infest the gainery");
                float Percent = (float)Functions.FNR(1,10) / 100;
                RatLossess = (int)(CropYield * Percent);
            }
			
			CastleExpense =  - 120;
          	if (CropYield > 4000) // Little extra off the top
            {
                CastleExpense += ((4000   - CropYield ) % 100 ) * 10            ;
            }

            RoyalTax = -300;
            if (KingStatus == 1)
            {
                RoyalTax *= 2;
            }
		}
	}
}
