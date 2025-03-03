using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class HumanGump : Gump
	{
		public HumanGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9350);
			this.AddLabel(310, 65, 567, @"HUMAN RACE");
			AddImage(210, 100, 460); //Obrazem
			AddImage(210, 100, 459); //Ramka
			AddImage(160, 70, 10400); //SmokLewo
			AddImage(160, 210, 10401); //OgonekLewo
			AddImage(160, 350, 10402); //KoniecOgonekLewo
			AddImage(463, 70, 10410); //SmokPrawo
			AddImage(463, 210, 10411); //OgonekPrawo
			AddImage(463, 350, 10412); //KoniecOgonekPrawo

			this.AddHtml( 170, 410, 360, 100, @"Humanity, the most numerous and diverse race in the kingdom, has shaped the face of this magical world for centuries. Humans are creatures of extraordinary adaptability, which allows them to survive in a variety of environments - from green valleys to rugged mountains, from vast beaches to dense forests. Their culture, traditions and languages vary greatly depending on the region in which they live.", (bool)true, (bool)true);
			this.AddButton(180, 520, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //CZ£OWIEK
                        this.AddLabel(240, 525, 567, @"BECOME A HUMAN");
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

                                m.SendMessage( 0x35, "You decide to become Human!" );
                                pm.A1Race1 = A1Race1.Czlowiek;
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