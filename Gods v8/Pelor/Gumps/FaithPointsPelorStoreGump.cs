using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;
using Server.ACC.CSS.Systems.Cleric;

namespace Server.Gumps
{
	public class FaithPointsPelorStoreGump : Gump
	{

		public FaithPointsPelorStoreGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 2520);
			this.AddLabel(290, 75, 567, @"Faith Points Store:");

			AddImage(300, 100, 2329); // Ramka
			AddImage(310, 100, 123); // Baner Pelor

			this.AddButton(140, 180, 9722, 9723, 1, GumpButtonType.Reply, 0);
			AddImage(190, 170, 2329); // Ramka
			AddItem(210, 190, 0x2DB3); //MarkOfPriest
			this.AddLabel(280, 170, 567, @"Mark Of Priest - promotion to Priest");
			this.AddLabel(280, 190, 567, @"[buy 3000 Faith Points]");

			this.AddButton(140, 250, 9722, 9723, 2, GumpButtonType.Reply, 0);
			AddImage(190, 240, 2329); // Ramka
			AddItem(210, 250, 0x9F97); //ProofofFaith
			this.AddLabel(280, 240, 567, @"Proof of Faith - promotion to Cleric");
			this.AddLabel(280, 260, 567, @"[buy 10000 Faith Points]");

			this.AddButton(140, 320, 9722, 9723, 3, GumpButtonType.Reply, 0);
			AddImage(190, 310, 2329); // Ramka
			AddItem(210, 317, 0x4B9D); //Cleric Robe
			this.AddLabel(280, 310, 567, @"Cleric Robe");
			this.AddLabel(280, 330, 567, @"[buy 15000 Faith Points]");

			this.AddButton(140, 390, 9722, 9723, 4, GumpButtonType.Reply, 0);
			AddImage(190, 380, 2329); // Ramka
			AddItem(210, 400, 0xEFA); //Cleric Spellbook
			this.AddLabel(280, 380, 567, @"Cleric Spellbook");
			this.AddLabel(280, 400, 567, @"[buy 20000 Faith Points]");

			this.AddButton(140, 460, 9722, 9723, 5, GumpButtonType.Reply, 0);
			AddImage(190, 450, 2329); // Ramka
			AddItem(210, 460, 0xA429); //Medallion of Devoted Follower
			this.AddLabel(280, 450, 567, @"Medallion of Devoted Follower");
			this.AddLabel(280, 470, 567, @"[buy 5000 Faith Points]");

			this.AddLabel(470, 530, 37, @"EXIT");
			this.AddButton(400, 513, 4502, 4502, 6, GumpButtonType.Reply, 0); //EXIT
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

				if ( pm.AB1FaithPoints < 3000 )
				{
					from.SendMessage( 0x37, "You have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
				else if ( pm.AB1FaithPoints >= 3000 )
				{
					Item mp = new MarkOfPriestGodPerol();
					from.AddToBackpack(mp);
					pm.AB1FaithPoints -= 3000;
					from.SendMessage( 0x37, "You purchase the item, now have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
			}
        		if ( info.ButtonID == 2 )
         		{
                                from.PlaySound(0x051);

				if ( pm.AB1FaithPoints < 10000 )
				{
					from.SendMessage( 0x37, "You have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
				else if ( pm.AB1FaithPoints >= 10000 )
				{
					Item pp = new ProofofFaithGodPerol();
					from.AddToBackpack(pp);
					pm.AB1FaithPoints -= 10000;
					from.SendMessage( 0x37, "You purchase the item, now have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
			}
        		if ( info.ButtonID == 3 )
         		{
                                from.PlaySound(0x051);

				if ( pm.AB1FaithPoints < 15000 )
				{
					from.SendMessage( 0x37, "You have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
				else if ( pm.AB1FaithPoints >= 15000 )
				{
					Item cr = new ClericRobe();
					from.AddToBackpack(cr);
					pm.AB1FaithPoints -= 15000;
					from.SendMessage( 0x37, "You purchase the item, now have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
			}
        		if ( info.ButtonID == 4 )
         		{
                                from.PlaySound(0x051);

				if ( pm.AB1FaithPoints < 20000 )
				{
					from.SendMessage( 0x37, "You have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
				else if ( pm.AB1FaithPoints >= 20000 )
				{
					Item csb = new ClericSpellbook();
					from.AddToBackpack(csb);
					pm.AB1FaithPoints -= 20000;
					from.SendMessage( 0x37, "You purchase the item, now have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
			}
        		if ( info.ButtonID == 5 )
         		{
                                from.PlaySound(0x051);

				if ( pm.AB1FaithPoints < 5000 )
				{
					from.SendMessage( 0x37, "You have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
				else if ( pm.AB1FaithPoints >= 5000 )
				{
					Item modf = new MedallionofDevotedFollower();
					from.AddToBackpack(modf);
					pm.AB1FaithPoints -= 5000;
					from.SendMessage( 0x37, "You purchase the item, now have {0} [Faith Points].", pm.AB1FaithPoints.ToString() );
				}
			}
        		if ( info.ButtonID == 6 )
         		{
                                from.PlaySound(0x051);
			}
				
		}
////
	}
} 