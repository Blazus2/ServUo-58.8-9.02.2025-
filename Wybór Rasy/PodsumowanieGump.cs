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

	public class PodsumowanieGump : Gump
	{
		public PodsumowanieGump(Mobile m)
			: base( 0, 0 )
		{

		     PlayerMobile pm = m as PlayerMobile;

			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"PODSUMOWANIE WYBORÓW");

 			AddHtml(210, 100, 250, 250, @"<BASEFONT COLOR=#FFA500>IMIE POSTACI: </font>" + pm.Name,( bool ) false, ( bool ) false); 
			AddHtml(210, 130, 250, 250, @"<BASEFONT COLOR=#FFA500>WIEK: </font>" + ((PlayerMobile)pm).A4Wiek + " lat",( bool ) false, ( bool ) false);
 			AddHtml(210, 160, 250, 250, @"<BASEFONT COLOR=#FFA500>RASA: </font>" + ((PlayerMobile)pm).A1Race1,( bool ) false, ( bool ) false);
			if (pm.Female == true)
			{
 			AddHtml(210, 190, 250, 250, @"<BASEFONT COLOR=#FFA500>P£EÆ: Kobieta </font>",( bool ) false, ( bool ) false);
			}
			if (pm.Female == false)
			{
			AddHtml(210, 190, 250, 250, @"<BASEFONT COLOR=#FFA500>P£EÆ: Mê¿czyzna </font>",( bool ) false, ( bool ) false);
			}
			AddHtml(210, 220, 250, 250, @"<BASEFONT COLOR=#FFA500>SI£A: </font>" + ((PlayerMobile)pm).RawStr,( bool ) false, ( bool ) false);
			AddHtml(210, 250, 250, 250, @"<BASEFONT COLOR=#FFA500>ZRÊCZNOŒÆ: </font>" + ((PlayerMobile)pm).RawDex,( bool ) false, ( bool ) false);
			AddHtml(210, 280, 250, 250, @"<BASEFONT COLOR=#FFA500>INTELEKT: </font>" + ((PlayerMobile)pm).RawInt,( bool ) false, ( bool ) false);

                        this.AddButton(140, 435, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0); // ZATWIERDZ I PRZENIEŒ
 			this.AddButton(140, 495, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); // ZACZNIJ OD POCZ¥TKU
 
			this.AddLabel(210, 450, 33, @"ZATWIERDZ I PRZENIEŒ");
			this.AddLabel(210, 510, 33, @"ZACZNIJ OD POCZ¥TKU");
		}

		public enum Buttons
		{
			Close,
			Button1,
			Button2,

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
				pm.A8TworzenieUkonczone = true;
				if ( pm.A1Race1 == A1Race1.Czlowiek )
				{ 
				    pm.MoveToWorld(new Point3D(1344, 866, 5), Map.Felucca); //Edonia-Dziedziniec
                                }
				if ( pm.A1Race1 == A1Race1.Krasnolud )
				{ 
				    pm.MoveToWorld(new Point3D(1706, 394, 19), Map.Felucca); //Alafar-(miasto krasnoludów)
                                }
				if ( pm.A1Race1 == A1Race1.Elf )
				{ 
				    pm.MoveToWorld(new Point3D(351, 1714, -70), Map.Felucca); //Elfie-Miasto
                                }
				if ( pm.A1Race1 == A1Race1.Drow )
				{ 
				    pm.MoveToWorld(new Point3D(5553, 592, 0), Map.Felucca); //Przeklête-Miasto
                                }


				break; 
				}
				
				case 2: 
				{
				pm.A6TworzenieWygladu = false;
				pm.A7TworzenieCech = false;
				break; 
				}

			}
		}

    }
}