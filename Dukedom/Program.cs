using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dukedom
{
	class Program
	{
		static void Main(string[] args)
		{
            bool Play = true;
            do
            {
                Dukedom dukeDom = new Dukedom();

                dukeDom.RunGame();

                Play = Functions.ReadYesNo("Do you wish to play again? (Y/N)");
            }
            while (Play);
		}
	}
}
