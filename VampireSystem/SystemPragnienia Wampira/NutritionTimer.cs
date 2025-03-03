using System;
using Server.Network;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class NutritionTimer : Timer
	{
		public static void Initialize()
		{
			new NutritionTimer().Start();
		}

		public NutritionTimer() : base( TimeSpan.FromMinutes( 10 ), TimeSpan.FromMinutes( 10 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				if ( state.Mobile != null && state.Mobile.AccessLevel == AccessLevel.Player ) // Check if player
				{
					HungerDecay( state.Mobile );
				}
			}
		}

		public static void HungerDecay( Mobile m )
		{
               PlayerMobile player = m as PlayerMobile;

			if ( m != null )
			{
				if ( player.PulaKrwi >= 1 && player.IsVampire == true )
				{
					player.PulaKrwi -= 1;

// dodano, aby nadaæ wartoœci g³odu prawdziwe znaczenie.
                    if (player.PulaKrwi < 5)
                        m.SendMessage(33, "You are very thirsty for blood.");
                    else if (player.PulaKrwi < 10)
                        m.SendMessage(33, "You're getting thirsty for blood.");
                    else if (player.PulaKrwi < 15)
                        m.SendMessage(33, "You're starting to get a little thirsty for blood.");
				}	
				else
				{
					if ( m.Hits > 5 && player.IsVampire == true )
						m.Hits -= 5;
					m.SendMessage( 33, "You are dying from lack of blood!" );
				}
			}
		}


	}
}
