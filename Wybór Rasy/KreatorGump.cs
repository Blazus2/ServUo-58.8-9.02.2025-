using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Misc;

namespace Server.Gumps
{

	public class KreatorGump : Gump
	{
		public KreatorGump(Mobile m)
			: base( 0, 0 )
		{

		     PlayerMobile pm = m as PlayerMobile;

			this.Closable=false;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(170, 65, 567, @"WELCOME TO THE WORLD OF FORGOTTEN AGE WANDERER");
			this.AddHtml( 140, 95, 400, 140, @"<h2>It appears that you were born as the person known as [</h2>" + pm.Name + "<h2>], it was an ambush, you were killed but you retained the memory of your previous life, now in your subconscious you are between life and death.<br><br>The unknown God gives you a chance to be reborn in a new world. Choose your new incarnation wisely.</h2>", (bool)true, (bool)true);

			AddImage(220, 220, 102); // Nagrobek
			this.AddLabel(300, 280, 0x35, @"HERE RESTS");
			this.AddLabel(280, 310, 0x35, @"" + pm.Name);

 			this.AddButton(140, 495, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0); // FIND SOUL CRYSTAL AND USE IT.
			this.AddLabel(210, 510, 33, @"FIND SOUL CRYSTAL AND USE IT.");
		}

		public enum Buttons
		{
			Close,
			Button1,
		}

		public override void OnResponse(NetState sender, RelayInfo info )
		{
			   Mobile m = sender.Mobile;
               
			if (m == null)
				return;

		    PlayerMobile pm = (PlayerMobile)m;

             
			switch ( info.ButtonID )
			{
				case 0:
				{
				m.SendMessage( "" );
				break;
				}

				case 1: 
				{
				pm.A5UseRaceStone = true;
				pm.A6TworzenieWygladu = false;
				pm.A7TworzenieCech = false;
				pm.A8TworzenieUkonczone = false;
				pm.CloseGump(typeof(KreatorGump));
				break; 
				}

			}
		}

    }
}