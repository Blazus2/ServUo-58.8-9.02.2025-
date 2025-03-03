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
	public class DostosowanieWygl¹duGump : Gump
	{
		public DostosowanieWygl¹duGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"CUSTOMIZING CHARACTER APPEARANCE");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //MÊ¯CZYZNA
			this.AddButton(385, 83, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //KOBIETA
			this.AddButton(140, 133, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //IMIÊ POSTACI
			this.AddButton(140, 183, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //FRYZURA
			this.AddButton(140, 233, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //BRODA
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //KOLOR OW£OSIENIA
			this.AddButton(140, 333, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //KOLOR SKÓRY
			this.AddButton(140, 433, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //ZATWIERDZ WYGL¥D

                        this.AddLabel(210, 100, 567, @"MAN");
                        this.AddLabel(455, 100, 567, @"WOMAN");
			this.AddLabel(210, 150, 567, @"CHARACTER NAME");
			this.AddLabel(210, 200, 567, @"HAIR");
			this.AddLabel(210, 250, 567, @"BEARD");
			this.AddLabel(210, 300, 567, @"HAIR COLOR");
			this.AddLabel(210, 350, 567, @"SKIN COLOR");
			this.AddLabel(210, 450, 37, @"CONFIRM APPEARANCE");
			this.AddLabel(140, 470, 37, @"[Approval of the appearance means that the change can no be undone.]");
			this.AddLabel(140, 490, 37, @"[If you have not made choices appropriate to your chosen race,]");
			this.AddLabel(140, 510, 37, @"[Once approved, adjustments will be made randomly.]");
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
				m.FacialHairItemID = 0;
				m.HairItemID = 0;
				m.Female = false;
                                m.PlaySound(0x051);

					if ( m.Race == Race.Human )
					{ 
					m.BodyValue = 400;
					}

					if ( m.Race == Race.Elf )
					{ 
					m.BodyValue = 605;
					}

					if ( m.Race == Race.Gargoyle )
					{ 
					m.BodyValue = 666;
					}

					m.SendGump( new DostosowanieWygl¹duGump());

				break; 
				}
				

				case 2: 
				{
				m.FacialHairItemID = 0;
				m.HairItemID = 0;				
				m.Female = true;
                                m.PlaySound(0x051);

					if ( m.Race == Race.Human )
					{ 
					m.BodyValue = 401;
					}

					if ( m.Race == Race.Elf )
					{ 
					m.BodyValue = 606;
					}

					if ( m.Race == Race.Gargoyle )
					{ 
					m.BodyValue = 667;
				        }

					m.SendGump( new DostosowanieWygl¹duGump());

				break; 
				}

				case 3: 
				{						            
                                m.PlaySound(0x051);
				m.SendGump(new RaceNameChangeGump());

				break; 
				}

				case 4: 
				{						            
					m.PlaySound(0x051);
					if ( m.Race == Race.Human )
					{ 
					   m.SendGump(new RaceHumanBaseGump(m));
					}

					if ( m.Race == Race.Elf )
					{ 
					    m.SendGump(new RaceElvenBaseGump(m));
					}

					if ( m.Race == Race.Gargoyle )
					{ 
					    m.SendGump(new RaceGargoyleBaseGump(m));
				        }

				break; 
				}

				case 5: 
				{						            
					m.PlaySound(0x051);
					if ( m.Race == Race.Human && m.Female == false )
					{ 
					   m.SendGump(new RaceBeardGump(m));
					}

					else
					{ 
					   m.SendMessage( "You can't choose the beard!" );
					   m.SendGump( new DostosowanieWygl¹duGump());
					}

				break; 
				}

				case 6: 
				{						            
					m.PlaySound(0x051);
					if ( pm.A1Race1 == A1Race1.Drow )
					{ 
					   m.SendMessage( "You can't change the color of your hair!" );
					   m.SendGump( new DostosowanieWygl¹duGump());
					}
					else
					{
					   m.SendGump( new RaceHairDyeGump(m));
					}

				break; 
				}

				case 7: 
				{						            
					if ( m.Hue == 33770 )
					{

						if ( m.HasGump(typeof(SkinToneGump)) || m.HasGump(typeof(ElvenSkinToneGump)) || m.HasGump(typeof(DrowSkinToneGump)) || m.HasGump(typeof(DemonSkinToneGump)) )
						{
						m.SendMessage( "You can't do this!" );
						return;
						}

						m.PlaySound(0x051);
						if ( m.Race == Race.Human )
						{ 
						   m.SendGump( new SkinToneGump(m));
						}
						if ( m.Race == Race.Elf && pm.A1Race1 != A1Race1.Drow )
						{ 
						   m.SendGump( new ElvenSkinToneGump(m));
						}
						if ( m.Race == Race.Elf && pm.A1Race1 != A1Race1.Elf )
						{ 
						   m.SendGump( new DrowSkinToneGump(m));
						}
						if ( m.Race == Race.Gargoyle )
						{ 
						   m.SendGump( new DemonSkinToneGump(m));
						}
					}
					else
					{
					   m.SendMessage( "You have already chosen your skin color, if you want to make changes, start over!" );
					   m.SendGump( new DostosowanieWygl¹duGump());
					}

				break; 
				}

				case 8: 
				{						            
					m.SendMessage( "Character appearance approved! We're moving on to the next stage of character creation!" );
					pm.A6TworzenieWygladu = true;


						if ( m.Race == Race.Elf && pm.A1Race1 == A1Race1.Drow )
						{ 
							m.FacialHairItemID = 0;
							if ( m.Hue != 0x76D || m.Hue != 0x384 || m.Hue != 0x579 || m.Hue != 0x3E9 || m.Hue != 0x3E8 || m.Hue != 0x3DE || m.Hue != 0x389 || m.Hue != 0x385 || m.Hue != 0x376 || m.Hue != 0x53F || m.Hue != 0x381 || m.Hue != 0x382 || m.Hue != 0x383 || m.Hue != 0x76B || m.Hue != 0x3E5 || m.Hue != 0x51D || m.Hue != 0x3E6 )
                                                        {  
							m.Hue = 0x76D;
							}
							if ( m.HairItemID == 0 )
							{
							m.HairItemID = 0x2fbf;
							}

						}
						if ( m.Race == Race.Elf && pm.A1Race1 == A1Race1.Elf )
						{ 
							m.FacialHairItemID = 0;
							if ( m.HairItemID == 0 )
							{
							m.HairItemID = 0x2fbf;
							}

						}

				break; 
				}

			}
		}
    }
}