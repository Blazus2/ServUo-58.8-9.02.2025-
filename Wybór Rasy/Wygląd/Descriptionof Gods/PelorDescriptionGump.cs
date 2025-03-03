using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class PelorDescriptionGump : Gump
	{

		public PelorDescriptionGump()
			: base( 0, 0 )
		{

			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(320, 75, 567, @"Pelor:");

			AddImage(300, 100, 2329); // Ramka
			AddImage(310, 100, 123); // Baner Pelor

			this.AddButton(400, 510, 9722, 9723, 1, GumpButtonType.Reply, 0); //EXIT

			this.AddHtml( 140, 170, 400, 240, @"<h2>Light:</h2> Symbolizes hope, truth and clarity. <BR><h2>Healing:</h2> His powers allow him to heal wounds and diseases, making him the patron saint of healers. <BR><h2>Justice:</h2> Works to defend the weak and fight injustice. <BR><h2>Challenges and Goals:</h2> Followers of Pelor are often engaged in fighting the forces of evil and darkness. Their mission is to restore peace, protect the innocent and spread goodness among people. Sometimes they face moral dilemmas when they have to choose between mercy and justice. <BR><h2>Worship and Rituals:</h2> Temples of Pelor are places of light and healing where people can seek help. Rituals often include prayers to the sun, offerings of herbs and flowers, and healing ceremonies. Feast days are celebrated with joy, where the community comes together to celebrate life and light.", (bool)true, (bool)true);

			this.AddLabel(470, 515, 37, @"EXIT");
		}
		
      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 
			PlayerMobile pm = (PlayerMobile)from;

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
                                from.PlaySound(0x051);
     				from.SendGump( new RaceGump());
				from.CloseGump(typeof(PelorDescriptionGump));
			}
		}
////
	}
} 