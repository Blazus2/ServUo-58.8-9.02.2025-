using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Gumps
{
	public class RaceGump : Gump
	{
		public RaceGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 590, 9350);
			this.AddLabel(200, 65, 567, @"CHOOSING CHARACTER RACE (SUGGESTED BELIEF)");

			this.AddButton(140, 97, 9722, 9723, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //CZ£OWIEK
			this.AddButton(140, 147, 9722, 9723, (int)Buttons.Button2, GumpButtonType.Reply, 0); //ELF
			this.AddButton(140, 197, 9722, 9723, (int)Buttons.Button3, GumpButtonType.Reply, 0); //KRASNOLUD

			this.AddButton(140, 247, 9722, 9723, (int)Buttons.Button4, GumpButtonType.Reply, 0); //GARGULEC
			this.AddButton(140, 297, 9722, 9723, (int)Buttons.Button5, GumpButtonType.Reply, 0); //DROW
			this.AddButton(140, 347, 9722, 9723, (int)Buttons.Button6, GumpButtonType.Reply, 0); //DEMON


this.AddLabel(210, 100, 567, @"- HUMAN");
this.AddLabel(210, 150, 567, @"- ELF");
this.AddLabel(210, 200, 567, @"- DWARF");
////
this.AddLabel(210, 250, 37, @"- GARGOYLE");
this.AddLabel(210, 300, 37, @"- DROW");
this.AddLabel(210, 350, 37, @"- DEMON");
////
this.AddLabel(380, 100, 945, @"PANTHEON OF GODS");
this.AddLabel(380, 120, 567, @"Order:");
AddImage(360, 140, 123); //Baner Pelor
AddImage(440, 140, 126); //Baner Bahamut
this.AddButton(370, 205, 1209, 1209, (int)Buttons.Button7, GumpButtonType.Reply, 0); //Pelor
this.AddButton(450, 205, 1209, 1209, (int)Buttons.Button8, GumpButtonType.Reply, 0); //Bahamut

this.AddLabel(380, 230, 945, @"Neutral:");
AddImage(360, 250, 127); //Baner Corellon
AddImage(440, 250, 129); //Kord
AddImage(520, 250, 131); //Moradin
this.AddButton(370, 315, 1209, 1209, (int)Buttons.Button9, GumpButtonType.Reply, 0);  //Corellon
this.AddButton(450, 315, 1209, 1209, (int)Buttons.Button10, GumpButtonType.Reply, 0); //Kord
this.AddButton(530, 315, 1209, 1209, (int)Buttons.Button11, GumpButtonType.Reply, 0); //Moradin

this.AddLabel(380, 340, 37, @"Chaos:");
AddImage(360, 360, 124); //Baner Vacna
AddImage(440, 360, 125); //Tiamat
AddImage(520, 360, 132); //Tharizdun
this.AddButton(370, 425, 1209, 1209, (int)Buttons.Button12, GumpButtonType.Reply, 0); //Vacna
this.AddButton(450, 425, 1209, 1209, (int)Buttons.Button13, GumpButtonType.Reply, 0); //Tiamat
this.AddButton(530, 425, 1209, 1209, (int)Buttons.Button14, GumpButtonType.Reply, 0); //Tharizdun

this.AddLabel(370, 450, 37, @"Drow God:");
AddImage(360, 470, 128); //Baner Lolth
this.AddButton(370, 535, 1209, 1209, (int)Buttons.Button15, GumpButtonType.Reply, 0); //Lolth

this.AddLabel(490, 450, 945, @"Gargoyle God:");
AddImage(520, 470, 130); //Baner Zaharak
this.AddButton(530, 535, 1209, 1209, (int)Buttons.Button16, GumpButtonType.Reply, 0); //Zaharak

		}
		
		public enum Buttons
		{
			Close,
			Button1,
			Button2,
			Button3,
		        Button4,
		        Button5,
		        Button6,
		        Button7,
		        Button8,
		        Button9,
		        Button10,
		        Button11,
		        Button12,
		        Button13,
		        Button14,
		        Button15,
		        Button16,
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
     				m.SendGump( new HumanGump());
                                m.PlaySound(0x051);
     
         		        break; 
				}
				

				case 2: 
				{
     				m.SendGump( new ElvenGump());
                                m.PlaySound(0x051);

				break; 
				}

				case 3: 
				{
     				m.SendGump( new DwarfGump());
                                m.PlaySound(0x051);
	 	                        
                                break; 
				}
			
			        case 4:
			        {
     				m.SendGump( new GargoyleGump());
                                m.PlaySound(0x051);

				break; 						
				}			

				case 5: 
				{						            
      				m.SendGump( new DrowGump());
                                m.PlaySound(0x051);
 
         		        break; 
				}

				case 6: 
				{						            
      				m.SendGump( new DemonGump());
                                m.PlaySound(0x051); 
 
         		        break; 
				}

				case 7: 
				{						            
      				m.SendGump( new PelorDescriptionGump());
                                m.PlaySound(0x051); 
 
         		        break; 
				}
	     }
        }
    }
}