using System;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Scripts.Commands
{
	public class MyHunger
	{
		public static void Initialize()
		{
			CommandSystem.Register ( "hunger", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
			CommandSystem.Register ( "thirst", AccessLevel.Player, new CommandEventHandler ( MyHunger_OnCommand ) );
		}
		public static void MyHunger_OnCommand( CommandEventArgs e )
		{
		    PlayerMobile player = e.Mobile as PlayerMobile;
                    
			if ( player.IsVampire == true )
			{
				int h = player.PulaKrwi; // Zmienna przechowuj¹ca wartoœæ PuliKrwi gracza
							// te wartoœci s¹ pobierane z PlayerMobile.cs i odnosz¹ siê bezpoœrednio do wiadomoœci
							//dostajesz, kiedy jesz.
			if (h <= 0 )
			e.Mobile.SendMessage( 33, "You are dying from lack of blood." );

			else if ( h <= 5 )
			e.Mobile.SendMessage( 33, "You feel a great hunger for blood." );

			else if ( h <= 10 )
			e.Mobile.SendMessage( 33, "You are very thirsty for blood." );

			else if ( h <= 15 )
			e.Mobile.SendMessage( 33, "You are a little thirsty for blood." );

			else if ( h <= 19 )
			e.Mobile.SendMessage( 33, "You are not thirsty for blood." );

			else if ( h > 19 )
			e.Mobile.SendMessage( 33, "You are saturated with blood." ); 

			else
			e.Mobile.SendMessage( 33, "Error: Please report this error: hunger not found." );
			}

			else if ( player.IsVampire == false )
			{

			int g = e.Mobile.Hunger;

			if (g <= 0 )
			e.Mobile.SendMessage( "You are starving." );

			else if ( g <= 5 )
			e.Mobile.SendMessage( "You are starving." );

			else if ( g <= 10 )
			e.Mobile.SendMessage( "You are very hungry." );

			else if ( g <= 15 )
			e.Mobile.SendMessage( "You are a little hungry." );

			else if ( g <= 19 )
			e.Mobile.SendMessage( "You are not hungry." );

			else if ( g > 19 )
			e.Mobile.SendMessage( "You are full." );

			else
			e.Mobile.SendMessage( "Error: Please report this error: hunger not found." );

			int t = e.Mobile.Thirst;

			if ( t <= 0 )
			e.Mobile.SendMessage( "You are dying of thirst." );

			else if ( t <= 5 )
			e.Mobile.SendMessage( "You are very thirsty." );

			else if ( t <= 10 )
			e.Mobile.SendMessage( "You are thirsty." );

			else if ( t <= 15 )
			e.Mobile.SendMessage( "You are a little thirsty." );

			else if ( t <= 19 )
			e.Mobile.SendMessage( "You are not thirsty." );

			else if ( t > 19 )
			e.Mobile.SendMessage( "You are watered." );

			else
			e.Mobile.SendMessage( "Error: Please report this error: thirst not found." );
			}

		}
	}
}
