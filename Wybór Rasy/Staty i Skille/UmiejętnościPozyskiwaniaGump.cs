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
	public class UmiejêtnoœciPozyskiwaniaGump : Gump
	{
		public UmiejêtnoœciPozyskiwaniaGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"UMIEJETNOŒCI POZYSKIWANIA SUROWCÓW");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //RYBO£ÓSTWO
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //DRWALNICTWO
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //GÓRNICTWO
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(140, 523, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //COFNIJ

                        this.AddLabel(210, 100, 567, @"+50 RYBO£ÓSTWO");
                        this.AddLabel(210, 140, 567, @"+50 DRWALNICTWO");
			this.AddLabel(210, 180, 567, @"+50 GÓRNICTWO");

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
					pm.Skills.Fishing.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciPozyskiwaniaGump());
				break; 
				}
				
				case 2: 
				{
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Lumberjacking.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciPozyskiwaniaGump());
				break; 
				}

				case 3: 
				{						            
					if ( pm.SkillsTotal < 1000 )
					{
					pm.Skills.Mining.Base = 50;
					m.PlaySound(0x051);

					}
					else
					{
					pm.SendMessage( 33, "Mo¿esz wybraæ tylko 2 skille pocz¹tkowe o wartoœci 50." );
					}
					pm.SendGump( new UmiejêtnoœciPozyskiwaniaGump());
				break; 
				}


				case 4: 
				{						            
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

				case 5: 
				{						            
					Server.Skills skills = pm.Skills;
					for (int i = 0; i < skills.Length; ++i)
                                        if ( pm.Skills[i].Base > 0.0 )
                                        {
					pm.Skills.Fishing.Base = 0;
					pm.Skills.Lumberjacking.Base = 0;
					pm.Skills.Mining.Base = 0;
                                        }
					pm.SendGump( new DostosowanieStatystykGump());
					m.PlaySound(0x051);

				break; 
				}

			}
		}
    }
}