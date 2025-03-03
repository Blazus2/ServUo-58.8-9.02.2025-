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
	public class UmiejêtnoœciBojoweGump : Gump
	{
		public UmiejêtnoœciBojoweGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"UMIEJETNOŒCI BOJOWE POSTACI");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //SZERMIERKA
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //WALKA MIECZAMI
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //WALKA OBUCHAMI
			this.AddButton(140, 203, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //£UCZNICTWO
			this.AddButton(140, 243, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //WALKA PIÊŒCIAMI
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //RZUCANIE
			this.AddButton(140, 323, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //TAKTYKA
			this.AddButton(140, 363, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //PAROWANIE TARCZ¥
			this.AddButton(140, 403, 4502, 4502, (int)Buttons.Button9, GumpButtonType.Reply, 0); //DROGA BUSHIDO
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button10, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(140, 523, 4502, 4502, (int)Buttons.Button11, GumpButtonType.Reply, 0); //COFNIJ

                        this.AddLabel(210, 100, 567, @"+50 SZERMIERKA");
                        this.AddLabel(210, 140, 567, @"+50 WALKA MIECZAMI");
			this.AddLabel(210, 180, 567, @"+50 WALKA OBUCHAMI");
			this.AddLabel(210, 220, 567, @"+50 £UCZNICTWO");
			this.AddLabel(210, 260, 567, @"+50 WALKA PIÊŒCIAMI");
			this.AddLabel(210, 300, 567, @"+50 RZUCANIE (Tylko Rasy Demoniczne)");
			this.AddLabel(210, 340, 567, @"+50 TAKTYKA");
			this.AddLabel(210, 380, 567, @"+50 PAROWANIE TARCZ¥");
			this.AddLabel(210, 420, 567, @"+50 DROGA BUSHIDO");

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
					pm.Skills.Fencing.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}
				
				case 2: 
				{
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Swords.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 3: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Macing.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 4: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Archery.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 5: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Wrestling.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 6: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
						if ( pm.Race == Race.Gargoyle )
						{
						pm.Skills.Throwing.Base = 50;
						m.PlaySound(0x051);

						}
						if ( pm.Race != Race.Gargoyle )
						{
						pm.SendMessage( 33, "Ta umiejêtnoœc dostêpna jest tylko dla ras Demonicznych." );
						}
					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 7: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Tactics.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 8: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Parry.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 9: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Bushido.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciBojoweGump());
				break; 
				}

				case 10: 
				{						            
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

				case 11: 
				{						            
					Server.Skills skills = pm.Skills;
					for (int i = 0; i < skills.Length; ++i)
                                        if ( pm.Skills[i].Base > 0.0 )
                                        {
					pm.Skills.Fencing.Base = 0;
					pm.Skills.Swords.Base = 0;
					pm.Skills.Macing.Base = 0;
					pm.Skills.Archery.Base = 0;
					pm.Skills.Wrestling.Base = 0;
					pm.Skills.Throwing.Base = 0;
					pm.Skills.Tactics.Base = 0;
					pm.Skills.Parry.Base = 0;
					pm.Skills.Bushido.Base = 0;
                                        }
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

			}
		}
    }
}