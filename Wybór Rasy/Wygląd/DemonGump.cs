using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class DemonGump : Gump
	{
		public DemonGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(310, 65, 567, @"DEMON RACE");
			AddImage(210, 100, 465); //Obrazem
			AddImage(210, 100, 459); //Ramka
			AddImage(160, 70, 10400); //SmokLewo
			AddImage(160, 210, 10401); //OgonekLewo
			AddImage(160, 350, 10402); //KoniecOgonekLewo
			AddImage(463, 70, 10410); //SmokPrawo
			AddImage(463, 210, 10411); //OgonekPrawo
			AddImage(463, 350, 10412); //KoniecOgonekPrawo

			this.AddHtml( 170, 410, 360, 100, @"Demons are creatures of extremely diverse nature that can take different forms and have various powers. Their origins are often associated with dark dimensions where chaos and destruction reign. Many of these beings were created by ancient deities or were the result of failed magical rituals. Appearance Demons can take many shapes, from grotesque, terrifying figures with large horns, wings and tails, to more subtle forms that can attract with their beauty while masking their true intentions. Their skin often ranges in color from deep red to shades of black or gray, and their eyes can glow in the dark, emanating a sinister glow.", (bool)true, (bool)true);
			this.AddButton(180, 520, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //DEMON
                        this.AddLabel(240, 525, 567, @"BECOME A DEMON");
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

                                m.SendMessage( 0x35, "You decide to become a Demon!" );
                                pm.A1Race1 = A1Race1.Demon;
                                pm.A3DisplayRaseTitle = true;
         		        m.Race = Race.Gargoyle;
				if (m.Female == false)
				{
				m.BodyValue = 666;
				}
				if (m.Female == true)
				{
				m.BodyValue = 667;
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