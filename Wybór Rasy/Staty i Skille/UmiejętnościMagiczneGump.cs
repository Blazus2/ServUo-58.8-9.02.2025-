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
	public class UmiejêtnoœciMagiczneGump : Gump
	{
		public UmiejêtnoœciMagiczneGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"UMIEJETNOŒCI MAGICZNE POSTACI");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //OCENA INTELEKTU
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //MAGIA
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //MEDYTACJA
			this.AddButton(140, 203, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //RYCERSTWO
			this.AddButton(140, 243, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //MISTYCYZM
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //NEKROMANCJA
			this.AddButton(140, 323, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //MOWA DUCHÓW
			this.AddButton(140, 363, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //MAGIA NATURY
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button9, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(140, 523, 4502, 4502, (int)Buttons.Button10, GumpButtonType.Reply, 0); //COFNIJ

                        this.AddLabel(210, 100, 567, @"+50 OCENA INTELEKTU");
                        this.AddLabel(210, 140, 567, @"+50 MAGIA");
			this.AddLabel(210, 180, 567, @"+50 MEDYTACJA");
			this.AddLabel(210, 220, 567, @"+50 RYCERSTWO");
			this.AddLabel(210, 260, 567, @"+50 MISTYCYZM");
			this.AddLabel(210, 300, 567, @"+50 NEKROMANCJA");
			this.AddLabel(210, 340, 567, @"+50 MOWA DUCHÓW");
			this.AddLabel(210, 380, 567, @"+50 MAGIA NATURY");

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
					pm.Skills.EvalInt.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}
				
				case 2: 
				{
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Magery.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 3: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Meditation.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 4: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Chivalry.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 5: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Mysticism.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 6: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Necromancy.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 7: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.SpiritSpeak.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 8: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Spellweaving.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciMagiczneGump());
				break; 
				}

				case 9: 
				{						            
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

				case 10: 
				{						            
					Server.Skills skills = pm.Skills;
					for (int i = 0; i < skills.Length; ++i)
                                        if ( pm.Skills[i].Base > 0.0 )
                                        {
					pm.Skills.EvalInt.Base = 0;
					pm.Skills.Magery.Base = 0;
					pm.Skills.Meditation.Base = 0;
					pm.Skills.Chivalry.Base = 0;
					pm.Skills.Mysticism.Base = 0;
					pm.Skills.Necromancy.Base = 0;
					pm.Skills.SpiritSpeak.Base = 0;
					pm.Skills.Spellweaving.Base = 0;
                                        }
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

			}
		}
    }
}