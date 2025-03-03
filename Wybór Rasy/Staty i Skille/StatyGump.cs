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
	public class StatyGump : Gump
	{
		public StatyGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"STATISTICS");

			this.AddButton(170, 100, 87, 87, (int)Buttons.Button1, GumpButtonType.Reply, 0); // Si³a +
                        this.AddImage(170, 100, 5402); //+

			this.AddButton(300, 100, 87, 87, (int)Buttons.Button2, GumpButtonType.Reply, 0); // Si³a -
                        this.AddImage(300, 100, 5401); //-

			this.AddButton(170, 150, 87, 87, (int)Buttons.Button3, GumpButtonType.Reply, 0); // Zrêcznoœæ +
                        this.AddImage(170, 150, 5402); //+

			this.AddButton(300, 150, 87, 87, (int)Buttons.Button4, GumpButtonType.Reply, 0); // Zrêcznoœæ -
                        this.AddImage(300, 150, 5401); //-

			this.AddButton(170, 200, 87, 87, (int)Buttons.Button5, GumpButtonType.Reply, 0); // Intelekt +
                        this.AddImage(170, 200, 5402); //+

			this.AddButton(300, 200, 87, 87, (int)Buttons.Button6, GumpButtonType.Reply, 0); // Intelekt -
                        this.AddImage(300, 200, 5401); //-
 
			this.AddLabel(210, 100, 567, @"SI£A");
			this.AddLabel(130, 100, 567, @"+ 1");
			this.AddLabel(330, 100, 567, @"- 1");
                        this.AddLabel(210, 150, 567, @"ZRÊCZNOŒÆ");
			this.AddLabel(130, 150, 567, @"+ 1");
			this.AddLabel(330, 150, 567, @"- 1");
			this.AddLabel(210, 200, 567, @"INTELEKT");
			this.AddLabel(130, 200, 567, @"+ 1");
			this.AddLabel(330, 200, 567, @"- 1");
			this.AddLabel(210, 300, 33, @"ZATWIERDZ");
////
			this.AddButton(170, 120, 87, 87, (int)Buttons.Button7, GumpButtonType.Reply, 0); // Si³a +10
                        this.AddImage(170, 120, 5402); //+
			this.AddLabel(130, 120, 567, @"+ 10");
			this.AddButton(300, 120, 87, 87, (int)Buttons.Button8, GumpButtonType.Reply, 0); // Si³a -10
                        this.AddImage(300, 120, 5401); //-
			this.AddLabel(330, 120, 567, @"- 10");
////
			this.AddButton(170, 170, 87, 87, (int)Buttons.Button9, GumpButtonType.Reply, 0); // Zrêcznoœæ +10
                        this.AddImage(170, 170, 5402); //+
			this.AddLabel(130, 170, 567, @"+ 10");
			this.AddButton(300, 170, 87, 87, (int)Buttons.Button10, GumpButtonType.Reply, 0); // Zrêcznoœæ -10
                        this.AddImage(300, 170, 5401); //-
			this.AddLabel(330, 170, 567, @"- 10");
////
			this.AddButton(170, 220, 87, 87, (int)Buttons.Button11, GumpButtonType.Reply, 0); // Intelekt +10
                        this.AddImage(170, 220, 5402); //+
			this.AddLabel(130, 220, 567, @"+ 10");
			this.AddButton(300, 220, 87, 87, (int)Buttons.Button12, GumpButtonType.Reply, 0); // Intelekt -10
                        this.AddImage(300, 220, 5401); //-
			this.AddLabel(330, 220, 567, @"- 10");
////
                        this.AddButton(140, 280, 4502, 4502, (int)Buttons.Button13, GumpButtonType.Reply, 0); // ZATWIERDZ
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
				if ( pm.RawStr < 60 && pm.RawStatTotal < 80 )
				{
                                m.PlaySound(0x051);
				pm.RawStr += 1;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, si³a nie mo¿e byæ wy¿sza ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}
				
				case 2: 
				{
				if ( pm.RawStr > 10 )
				{
                                m.PlaySound(0x051);
				pm.RawStr -= 1;
				}
				else
				{
				pm.SendMessage( 33, "Nie mo¿esz mieæ mniej ni¿ 10 si³y." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 3: 
				{						            
				if ( pm.RawDex < 60 && pm.RawStatTotal < 80 )
				{
                                m.PlaySound(0x051);
				pm.RawDex += 1;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, zrêcznoœæ nie mo¿e byæ wy¿sza ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 4: 
				{						            
				if ( pm.RawDex > 10 )
				{
                                m.PlaySound(0x051);
				pm.RawDex -= 1;
				}
				else
				{
				pm.SendMessage( 33, "Nie mo¿esz mieæ mniej ni¿ 10 zrêcznoœci." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 5: 
				{						            
				if ( pm.RawInt < 60 && pm.RawStatTotal < 80 )
				{
                                m.PlaySound(0x051);
				pm.RawInt += 1;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, intelekt nie mo¿e byæ wy¿szy ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 6: 
				{						            
				if ( pm.RawInt > 10 )
				{
                                m.PlaySound(0x051);
				pm.RawInt -= 1;
				}
				else
				{
				pm.SendMessage( 33, "Nie mo¿esz mieæ mniej ni¿ 10 intelektu." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 7: 
				{						            
				if ( pm.RawStr < 60 && pm.RawStr <= 50 && pm.RawStatTotal < 80)
				{
                                m.PlaySound(0x051);
				pm.RawStr += 10;
                                m.PlaySound(0x051);
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, si³a nie mo¿e byæ wy¿szy ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 8: 
				{						            
				if ( pm.RawStr <= 60 && pm.RawStr >= 20 && pm.RawStatTotal <= 80)
				{
                                m.PlaySound(0x051);
				pm.RawStr -= 10;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, si³a nie mo¿e byæ ni¿sza ni¿ 10." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 9: 
				{						            
				if ( pm.RawDex < 60 && pm.RawDex <= 50 && pm.RawStatTotal < 80)
				{
                                m.PlaySound(0x051);
				pm.RawDex += 10;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, zrêcznoœæ nie mo¿e byæ wy¿sza ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 10: 
				{						            
				if ( pm.RawDex <= 60 && pm.RawDex >= 20 && pm.RawStatTotal <= 80)
				{
                                m.PlaySound(0x051);
				pm.RawDex -= 10;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, zrêcznoœæ nie mo¿e byæ ni¿sza ni¿ 10." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 11: 
				{						            
				if ( pm.RawInt < 60 && pm.RawInt <= 50 && pm.RawStatTotal < 80)
				{
                                m.PlaySound(0x051);
				pm.RawInt += 10;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, intelekt nie mo¿e byæ wy¿szy ni¿ 60." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 12: 
				{						            
				if ( pm.RawInt <= 60 && pm.RawInt >= 20 && pm.RawStatTotal <= 80)
				{
                                m.PlaySound(0x051);
				pm.RawInt -= 10;
				}
				else
				{
				pm.SendMessage( 33, "Suma maksymalna statystyk nie mo¿e przekroczyæ 80 punktów, intelekt nie mo¿e byæ ni¿szy ni¿ 10." );
				}
				pm.SendGump( new StatyGump());
				break; 
				}

				case 13: 
				{						            
				if ( pm.RawStatTotal < 80 )
				{
				pm.SendMessage( 33, "Pozosta³y jeszcze nie dodane punkty gaina." );
				pm.SendGump( new StatyGump());
				}
				else
				{
                                m.PlaySound(0x051);
				pm.SendGump( new DostosowanieStatystykGump());
				}
				break; 
				}

			}
		}

    }
}