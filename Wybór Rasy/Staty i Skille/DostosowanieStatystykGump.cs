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
	public class DostosowanieStatystykGump : Gump
	{
		public DostosowanieStatystykGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 9270);
			this.AddLabel(250, 65, 567, @"DOSTOSOWANIE UMIEJÊTNOŒCI POSTACI");

			this.AddButton(140, 83, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0);  //STATYSTYKI
			this.AddButton(140, 123, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); //UMIEJETNOŒCI BOJOWE
			this.AddButton(140, 163, 4502, 4502, (int)Buttons.Button3, GumpButtonType.Reply, 0); //UMIEJETNOŒCI MAGICZNE
			this.AddButton(140, 203, 4502, 4502, (int)Buttons.Button4, GumpButtonType.Reply, 0); //UMIEJETNOŒCI RZEMIOS£A
			this.AddButton(140, 243, 4502, 4502, (int)Buttons.Button5, GumpButtonType.Reply, 0); //UMIEJETNOŒCI POZYSKIWANIA
			this.AddButton(140, 283, 4502, 4502, (int)Buttons.Button6, GumpButtonType.Reply, 0); //UMIEJETNOŒCI BARDOWSKIE
			this.AddButton(140, 323, 4502, 4502, (int)Buttons.Button7, GumpButtonType.Reply, 0); //UMIEJETNOŒCI W DZICZY
			this.AddButton(140, 363, 4502, 4502, (int)Buttons.Button8, GumpButtonType.Reply, 0); //UMIEJETNOŒCI £OTROWSKIE
			this.AddButton(140, 403, 4502, 4502, (int)Buttons.Button9, GumpButtonType.Reply, 0); //UMIEJETNOŒCI INNE
			this.AddButton(140, 483, 4502, 4502, (int)Buttons.Button10, GumpButtonType.Reply, 0); //ZATWIERDZ
			this.AddButton(300, 483, 4502, 4502, (int)Buttons.Button11, GumpButtonType.Reply, 0); //COFNIJ WSZYSTKO

                        this.AddLabel(210, 100, 567, @"STATYSTYKI");
                        this.AddLabel(210, 140, 567, @"UMIEJETNOŒCI BOJOWE");
			this.AddLabel(210, 180, 567, @"UMIEJETNOŒCI MAGICZNE");
			this.AddLabel(210, 220, 567, @"UMIEJETNOŒCI RZEMIOS£A");
			this.AddLabel(210, 260, 567, @"UMIEJETNOŒCI POZYSKIWANIA");
			this.AddLabel(210, 300, 567, @"UMIEJETNOŒCI BARDOWSKIE");
			this.AddLabel(210, 340, 567, @"UMIEJETNOŒCI W DZICZY");
			this.AddLabel(210, 380, 567, @"UMIEJETNOŒCI £OTROWSKIE");
			this.AddLabel(210, 420, 567, @"UMIEJETNOŒCI INNE");
			this.AddLabel(210, 500, 37, @"ZATWIERDZ");
			this.AddLabel(370, 500, 37, @"COFNIJ WSZYSTKO");
			this.AddLabel(140, 540, 37, @"[Zatwierdzenie statystyk oznacza ,¿e zmiany nie mo¿na ju¿ cofnaæ.]");
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
                                        pm.RawStr = 10;
                                        pm.RawDex = 10;
                                        pm.RawInt = 10;
					m.SendGump( new StatyGump());
					m.PlaySound(0x051);
				break; 
				}
				
				case 2: 
				{
					m.SendGump( new UmiejêtnoœciBojoweGump());
					m.PlaySound(0x051);

				break; 
				}

				case 3: 
				{						            
					m.SendGump( new UmiejêtnoœciMagiczneGump());
					m.PlaySound(0x051);

				break; 
				}

				case 4: 
				{						            
					m.SendGump( new UmiejêtnoœciRzemios³aGump());
					m.PlaySound(0x051);

				break; 
				}

				case 5: 
				{						            
					m.SendGump( new UmiejêtnoœciPozyskiwaniaGump());
					m.PlaySound(0x051);

				break; 
				}

				case 6: 
				{						            
					m.SendGump( new UmiejêtnoœciBardowskieGump());
					m.PlaySound(0x051);

				break; 
				}

				case 7: 
				{						            
					m.SendGump( new UmiejêtnoœciWDziczyGump());
					m.PlaySound(0x051);

				break; 
				}

				case 8: 
				{						            
					m.SendGump( new Umiejêtnoœci£otrowskieGump());
					m.PlaySound(0x051);

				break; 
				}

				case 9: 
				{						            
					m.SendGump( new UmiejêtnoœciInneGump());
					m.PlaySound(0x051);

				break; 
				}

				case 10: 
				{						            
				m.SendMessage( "Zatwierdzono cechy postaci! Przechodzimy do podsumowania tworzenia postaci!" );

				if ( pm.RawStatTotal >= 80 && pm.SkillsTotal >= 1000 )
				{
					pm.A7TworzenieCech = true;
					m.CloseGump(typeof(DostosowanieStatystykGump));
					m.SendGump( new PodsumowanieGump(m));
					m.PlaySound(0x051);

				}
				else
				{
					m.SendGump( new DostosowanieStatystykGump());
					m.SendMessage( 33, "Zosta³y jeszcze statystyki do rozdysponowania." );
				}

				break; 
				}

				case 11: 
				{						            
				m.PlaySound(0x051);
				m.RawStr = 10;
				m.RawDex = 10;
				m.RawInt = 10;
				m.Skills.Alchemy.Base = 0;
				m.Skills.Anatomy.Base = 0;
				m.Skills.AnimalLore.Base = 0;
				m.Skills.ItemID.Base = 0;
				m.Skills.ArmsLore.Base = 0;
				m.Skills.Parry.Base = 0;
				m.Skills.Begging.Base = 0;
				m.Skills.Blacksmith.Base = 0;
				m.Skills.Fletching.Base = 0;
				m.Skills.Peacemaking.Base = 0;
				m.Skills.Camping.Base = 0;
				m.Skills.Carpentry.Base = 0;
				m.Skills.Cartography.Base = 0;
				m.Skills.Cooking.Base = 0;
				m.Skills.DetectHidden.Base = 0;
				m.Skills.Discordance.Base = 0;
				m.Skills.EvalInt.Base = 0;
				m.Skills.Healing.Base = 0;
				m.Skills.Fishing.Base = 0;
				m.Skills.Forensics.Base = 0;
				m.Skills.Herding.Base = 0;
				m.Skills.Hiding.Base = 0;
				m.Skills.Provocation.Base = 0;
				m.Skills.Inscribe.Base = 0;
				m.Skills.Lockpicking.Base = 0;
				m.Skills.Magery.Base = 0;
				m.Skills.MagicResist.Base = 0;
				m.Skills.Tactics.Base = 0;
				m.Skills.Snooping.Base = 0;
				m.Skills.Musicianship.Base = 0;
				m.Skills.Poisoning.Base = 0;
				m.Skills.Archery.Base = 0;
				m.Skills.SpiritSpeak.Base = 0;
				m.Skills.Stealing.Base = 0;
				m.Skills.Tailoring.Base = 0;
				m.Skills.AnimalTaming.Base = 0;
				m.Skills.Wampiryzm.Base = 0;
				m.Skills.Tinkering.Base = 0;
				m.Skills.Tracking.Base = 0;
				m.Skills.Veterinary.Base = 0;
				m.Skills.Swords.Base = 0;
				m.Skills.Macing.Base = 0;
				m.Skills.Fencing.Base = 0;
				m.Skills.Wrestling.Base = 0;
				m.Skills.Lumberjacking.Base = 0;
				m.Skills.Mining.Base = 0;
				m.Skills.Meditation.Base = 0;
				m.Skills.Stealth.Base = 0;
				m.Skills.RemoveTrap.Base = 0;
				m.Skills.Necromancy.Base = 0;
				m.Skills.Focus.Base = 0;
				m.Skills.Chivalry.Base = 0;
				m.Skills.Bushido.Base = 0;
				m.Skills.Ninjitsu.Base = 0;
				m.Skills.Spellweaving.Base = 0;
				m.Skills.Mysticism.Base = 0;
				m.Skills.Imbuing.Base = 0;
				m.Skills.Throwing.Base = 0;
				m.SendGump(new DostosowanieStatystykGump());

				break; 
				}

			}
		}
    }
}