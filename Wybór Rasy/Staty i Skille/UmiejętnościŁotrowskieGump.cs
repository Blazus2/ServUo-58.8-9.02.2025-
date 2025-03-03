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
	public class Umiejêtnoœci£otrowskieGump : Gump
	{
		public Umiejêtnoœci£otrowskieGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"UMIEJETNOŒCI £OTROWSKIE POSTACI");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //WYKRYWANIE UKRYTYCH
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //OTWIERANIE ZAMKÓW
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //ZAGL¥DANIE
			this.AddButton(140, 203, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //ZATRUWANIE
			this.AddButton(140, 243, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //OKRADANIE
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //WAMPIRYZM
			this.AddButton(140, 323, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //SKRADANIE
			this.AddButton(140, 363, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //USUWANIE PU£APEK
			this.AddButton(140, 403, 4502, 4502, (int)Buttons.Button9, GumpButtonType.Reply, 0); //SZTUKA CIENIA
			this.AddButton(140, 443, 4502, 4502, (int)Buttons.Button10, GumpButtonType.Reply, 0); //UKRYWANIE
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button11, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(140, 523, 4502, 4502, (int)Buttons.Button12, GumpButtonType.Reply, 0); //COFNIJ

                        this.AddLabel(210, 100, 567, @"+50 WYKRYWANIE UKRYTYCH");
                        this.AddLabel(210, 140, 567, @"+50 OTWIERANIE ZAMKÓW");
			this.AddLabel(210, 180, 567, @"+50 ZAGL¥DANIE");
			this.AddLabel(210, 220, 567, @"+50 ZATRUWANIE");
			this.AddLabel(210, 260, 567, @"+50 OKRADANIE");
			this.AddLabel(210, 300, 567, @"+50 WAMPIRYZM");
			this.AddLabel(210, 340, 567, @"+50 SKRADANIE");
			this.AddLabel(210, 380, 567, @"+50 USUWANIE PU£APEK");
			this.AddLabel(210, 420, 567, @"+50 SZTUKA CIENIA");
			this.AddLabel(210, 460, 567, @"+50 UKRYWANIE");

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
					pm.Skills.DetectHidden.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}
				
				case 2: 
				{
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Lockpicking.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 3: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Snooping.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 4: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Poisoning.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 5: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Stealing.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 6: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Wampiryzm.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 7: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Stealth.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 8: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.RemoveTrap.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 9: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Ninjitsu.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
				break; 
				}

				case 10: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Hiding.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new Umiejêtnoœci£otrowskieGump());
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
					pm.Skills.DetectHidden.Base = 0;
					pm.Skills.Lockpicking.Base = 0;
					pm.Skills.Snooping.Base = 0;
					pm.Skills.Poisoning.Base = 0;
					pm.Skills.Stealing.Base = 0;
					pm.Skills.Wampiryzm.Base = 0;
					pm.Skills.Stealth.Base = 0;	
					pm.Skills.RemoveTrap.Base = 0;
					pm.Skills.Ninjitsu.Base = 0;
					pm.Skills.Hiding.Base = 0;
                                        }
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

			}
		}
    }
}