using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	// Utw�rz licznik czasu monitoruj�cy aktualny stan PuliKrwi
	public class FallHPTimer : Timer
	{
		public static void Initialize()
		{
			new FallHPTimer().Start();
		}
		// W oparciu o ten sam okres czasu, co w pliku RegenRates.cs
		public FallHPTimer() : base( TimeSpan.FromSeconds( 11 ), TimeSpan.FromSeconds( 11 ) )
		{
			Priority = TimerPriority.OneSecond;
		}
		
		protected override void OnTick()
		{
			FallHP();
		}
		// Sprawd� NetState i wywo�aj funkcj� zanikaj�c�
		public static void FallHP()
		{
			foreach ( NetState state in NetState.Instances )
			{
				FallHP( state.Mobile );
			}
		}

		// Sprawd� poziom PuliKrwi, je�li poni�ej ustawionej warto�ci zabiera 1 hp
		public static void FallHP( Mobile m )
		{
               PlayerMobile player = m as PlayerMobile;

			if ( player != null && player.PulaKrwi < 5 && player.Hits > 3 && player.IsVampire == true )
			{
				switch (player.PulaKrwi)
				{
                    case 4: player.Hits -= 1; break;
                    case 3: player.Hits -= 1; break;
                    case 2: player.Hits -= 2; break;
                    case 1: player.Hits -= 2; break;
					case 0:
					{
						player.Hits -= 3;
						player.SendMessage( 33, "You are dying from lack of blood!" );
						break;
					}
				}
			}
		}
	}
}
