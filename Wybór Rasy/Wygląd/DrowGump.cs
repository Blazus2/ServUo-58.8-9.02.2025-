using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class DrowGump : Gump
	{
		public DrowGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(310, 65, 567, @"DROW RACE");
			AddImage(210, 100, 463); //Obrazem
			AddImage(210, 100, 459); //Ramka
			AddImage(160, 70, 10400); //SmokLewo
			AddImage(160, 210, 10401); //OgonekLewo
			AddImage(160, 350, 10402); //KoniecOgonekLewo
			AddImage(463, 70, 10410); //SmokPrawo
			AddImage(463, 210, 10411); //OgonekPrawo
			AddImage(463, 350, 10412); //KoniecOgonekPrawo

			this.AddHtml( 170, 410, 360, 100, @"Drow, also known as dark elves, are a mysterious race. Drow are known for their extraordinary agility, intelligence and magical skills. Unlike other elven races, drow often seek power and domination, which leads them to complex intrigues, betrayals, and brutal power struggles. Their society is organized in hierarchical structures, and women usually play a dominant role in social and political life, as a result of the worship of the goddess Lloth, known as the Queen of Spiders.", (bool)true, (bool)true);
			this.AddButton(180, 520, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //DROW
                        this.AddLabel(240, 525, 567, @"BECOME A DROW");
			this.AddButton(420, 520, 9722, 9723, (int)Buttons.Button2, GumpButtonType.Reply, 0); //RETURN
                        this.AddLabel(480, 525, 567, @"RETURN");
		}
		
		public enum Buttons
		{
			Close,
			Button1,
			Button2,
		}

		public override void OnResponse(NetState sender, RelayInfo info)
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

				m.SendMessage( "You decide to become a Dark Elf!" );
				pm.A1Race1 = A1Race1.Drow;
				pm.A3DisplayRaseTitle = true;
                                m.Race = Race.Elf;
				if (m.Female == false)
				{
				m.BodyValue = 605;
				}
				if (m.Female == true)
				{
				m.BodyValue = 606;
				}
     				m.SendGump( new DostosowanieWygl¹duGump());
				m.HairHue = 1153;

				m.FacialHairItemID = 0;
				m.HairItemID = 0;
                                m.Hue = 33770;
                                m.PlaySound(0x051);

         		        break; 
				}
				

				case 2: 
				{
                     
     				m.SendGump( new RaceGump());
                                m.PlaySound(0x051);

				break; 
				}

	     }
        }
    }
}