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
	public class UmiejêtnoœciRzemios³aGump : Gump
	{
		public UmiejêtnoœciRzemios³aGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"UMIEJETNOŒCI RZEMIOS£A POSTACI");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //ALCHEMIA
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //KOWALSTWO
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //WYRABIANIE LUKÓW
			this.AddButton(140, 203, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //STOLARSTWO
			this.AddButton(140, 243, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //GOTOWANIE
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //INSKRYPCJA
			this.AddButton(140, 323, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //KRAWIECTWO
			this.AddButton(140, 363, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //MAJSTERKOWANIE
			this.AddButton(140, 403, 4502, 4502, (int)Buttons.Button9, GumpButtonType.Reply, 0); //OCENA EKWIPUNKU
			this.AddButton(140, 443, 4502, 4502, (int)Buttons.Button10, GumpButtonType.Reply, 0); //KARTOGRAFIA
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button11, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(140, 523, 4502, 4502, (int)Buttons.Button12, GumpButtonType.Reply, 0); //COFNIJ

                        this.AddLabel(210, 100, 567, @"+50 ALCHEMIA");
                        this.AddLabel(210, 140, 567, @"+50 KOWALSTWO");
			this.AddLabel(210, 180, 567, @"+50 WYRABIANIE £UKÓW");
			this.AddLabel(210, 220, 567, @"+50 STOLARSTWO");
			this.AddLabel(210, 260, 567, @"+50 GOTOWANIE");
			this.AddLabel(210, 300, 567, @"+50 INSKRYPCJA");
			this.AddLabel(210, 340, 567, @"+50 KRAWIECTWO");
			this.AddLabel(210, 380, 567, @"+50 MAJSTERKOWANIE");
			this.AddLabel(210, 420, 567, @"+50 OCENA EKWIPUNKU");
			this.AddLabel(210, 460, 567, @"+50 KARTOGRAFIA");

			this.AddLabel(210, 500, 37, @"ZATWIERDZ");
			this.AddLabel(210, 540, 37, @"COFNIJ");
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
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Alchemy.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}
				
				case 2: 
				{
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Blacksmith.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 3: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Fletching.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 4: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Carpentry.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 5: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Cooking.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 6: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Inscribe.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 7: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Tailoring.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 8: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Tinkering.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 9: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.ArmsLore.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 10: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Cartography.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciRzemios³aGump());
				break; 
				}

				case 11: 
				{						            
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

				case 12: 
				{						            
					Server.Skills skills = pm.Skills;
					for (int i = 0; i < skills.Length; ++i)
                                        if ( pm.Skills[i].Base > 0.0 )
                                        {
					pm.Skills.Alchemy.Base = 0;
					pm.Skills.Blacksmith.Base = 0;
					pm.Skills.Fletching.Base = 0;
					pm.Skills.Carpentry.Base = 0;
					pm.Skills.Cooking.Base = 0;
					pm.Skills.Inscribe.Base = 0;
					pm.Skills.Tailoring.Base = 0;	
					pm.Skills.Tinkering.Base = 0;
					pm.Skills.ArmsLore.Base = 0;
					pm.Skills.Cartography.Base = 0;
                                        }
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

			}
		}
    }
}