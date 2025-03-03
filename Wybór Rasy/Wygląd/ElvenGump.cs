using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class ElvenGump : Gump
	{
		public ElvenGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(310, 65, 567, @"ELVEN RACE");
			AddImage(210, 100, 461); //Obrazem
			AddImage(210, 100, 459); //Ramka
			AddImage(160, 70, 10400); //SmokLewo
			AddImage(160, 210, 10401); //OgonekLewo
			AddImage(160, 350, 10402); //KoniecOgonekLewo
			AddImage(463, 70, 10410); //SmokPrawo
			AddImage(463, 210, 10411); //OgonekPrawo
			AddImage(463, 350, 10412); //KoniecOgonekPrawo

			this.AddHtml( 170, 410, 360, 100, @"Elves are one of the most fascinating races in the world of FE, known for their extraordinary beauty, longevity and deep connection with nature. They are characterized by slender figures, tall heights and delicate facial features, which give them an almost ethereal appearance. Their ears are elongated and pointed, and their eyes are often intensely colored, from deep green to light blue, giving them a mysterious charm.", (bool)true, (bool)true);
			this.AddButton(180, 520, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //Elf
                        this.AddLabel(240, 525, 567, @"BECOME A ELF");
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

				m.SendMessage( 0x35, "You decide to become an Elf!" );
                                pm.A1Race1 = A1Race1.Elf;
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