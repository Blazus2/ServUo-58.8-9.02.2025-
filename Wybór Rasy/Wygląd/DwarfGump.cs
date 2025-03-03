using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class DwarfGump : Gump
	{
		public DwarfGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(310, 65, 567, @"DWARF RACE");
			AddImage(210, 100, 462); //Obrazem
			AddImage(210, 100, 459); //Ramka
			AddImage(160, 70, 10400); //SmokLewo
			AddImage(160, 210, 10401); //OgonekLewo
			AddImage(160, 350, 10402); //KoniecOgonekLewo
			AddImage(463, 70, 10410); //SmokPrawo
			AddImage(463, 210, 10411); //OgonekPrawo
			AddImage(463, 350, 10412); //KoniecOgonekPrawo

			this.AddHtml( 170, 410, 360, 100, @"Dwarves are one of the most distinctive races in the world, known for their strength, determination and craftsmanship. They are characterized by short stature, muscular build and long beards, which are a symbol of honor and masculinity for them. Their skin usually ranges from light brown to darker, and their eyes glow in shades of blue, green or brown. Dwarves mainly inhabit mountainous regions, where they build powerful fortresses and cities in rock caves.", (bool)true, (bool)true);
			this.AddButton(180, 520, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //DWARF
                        this.AddLabel(240, 525, 567, @"BECOME A DWARF");
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

				m.SendMessage( 0x35, "You decide to become a Dwarf!" );
                                pm.A1Race1 = A1Race1.Krasnolud;
                                pm.A3DisplayRaseTitle = true;
         		        m.Race = Race.Human;
				if (m.Female == false)
				{
				m.BodyValue = 400;
				}
				if (m.Female == true)
				{
				m.BodyValue = 401;
				}
     				m.SendGump( new DostosowanieWygl¹duGump());
                                
                                //m.Skills[SkillName.Gornictwo].Cap = 110.0;

				m.HairItemID = 0;				
				m.FacialHairItemID = 0;
				m.FacialHairHue = 0;
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