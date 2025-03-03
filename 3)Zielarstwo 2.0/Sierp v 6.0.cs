//SIERP v 6.0 by PAN POMIDOR
using System;
using Server;
using Server.Gumps; 
using Server.Network; 
using Server.Misc; 
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute(0x26BB, 0x26C5)]
	public class Sierp : BaseSword, IHarvestTool
	{

	private bool m_Zcina;

	[CommandProperty( AccessLevel.GameMaster )]
	public bool Zcina
	{
		get{ return m_Zcina; }
		set{ m_Zcina = value; }
	}

        [Constructable]
        public Sierp()
            : base(0x26BB)
        {
		Name = "herbal sickle";
		Weight = 10.0;
        }

        public override WeaponAbility PrimaryAbility
        {
            get
            {
                return WeaponAbility.ParalyzingBlow;
            }
        }
        public override WeaponAbility SecondaryAbility
        {
            get
            {
                return WeaponAbility.MortalStrike;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 25;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 12;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return 16;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 36;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 3.00f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 25;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 13;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 15;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 36;
            }
        }
        public override int DefHitSound
        {
            get
            {
                return 0x23B;
            }
        }
        public override int DefMissSound
        {
            get
            {
                return 0x23A;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 31;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 70;
            }
        }

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1060662, "Use\t{0}", UsesRemaining );
		}

		public Sierp( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile player = from as PlayerMobile;

			if ( this.Parent != from ) 
			{ 
			from.SendMessage( "You must hold a sickle in your hand." ); 
			return;
			}
			if ( from.Mounted )
			{
			from.SendMessage( 0x21, "You can't do this while mounted." );
			return;
                        }
			if ( player.IsHarvesting == true )
			{
				this.Zcina = true;
			return;
			}
			if ( player.IsHarvesting == false )
			{
				this.Zcina = false;
			}
			if ( Zcina != true )
			{
				from.Target = new SierpTarget( this, from );
				from.SendMessage( "Where do you want to cut?" );
			}
			else
			{
				from.SendMessage( "You must wait to use this item again." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version 

			writer.Write( m_Zcina );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Zcina = reader.ReadBool();
		}

	private class SierpTarget : Target
	{
		private Sierp m_Item;
		private Mobile m_From;
		private Timer timer;

	public SierpTarget( Sierp item, Mobile from ) : base( 1, true, TargetFlags.None )  //Zasiêg targetowania - 1 Pole
	{
		m_Item = item;
		m_From = from;
	}

		protected override void OnTarget( Mobile from, object targeted )
		{

		PlayerMobile player = from as PlayerMobile;
		int zielarstwoValue = (int)from.Skills[SkillName.Zielarstwo].Value;
////ZIO£A MAGA////
			if (targeted is Item ite) //Item Target Start
			{

			from.Direction = from.GetDirectionTo(ite.Location);

			if (targeted is BushGarlic)
			{
				if ( zielarstwoValue >= 0)
				{
					BushGarlic targ = (BushGarlic)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}
			if (targeted is BushGinseng)
			{
				if ( zielarstwoValue >= 70)
				{
					BushGinseng targ1 = (BushGinseng)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ1.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ1);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushBloodyMoss)
			{
				if ( zielarstwoValue >= 60)
				{
					BushBloodyMoss targ2 = (BushBloodyMoss)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ2.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ2);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushMandrake )
			{
				if ( zielarstwoValue >= 50)
				{
					BushMandrake targ3 = (BushMandrake)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ3.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ3);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}
			if (targeted is BushNightshade )
			{
				if ( zielarstwoValue >= 80)
				{
					BushNightshade targ4 = (BushNightshade)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ4.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ4);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}
////ZIO£A MAGA KONIEC////
////ZIO£A////
			if (targeted is BushSage )
			{
				if ( zielarstwoValue >= 90)
				{
					BushSage targ5 = (BushSage)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ5.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ5);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushAcacia )
			{
				if ( zielarstwoValue >= 90)
				{
					BushAcacia targ6 = (BushAcacia)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ6.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ6);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushAnise )
			{
				if ( zielarstwoValue >= 90)
				{
					BushAnise targ7 = (BushAnise)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ7.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ7);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushBasil )
			{
				if ( zielarstwoValue >= 90)
				{
					BushBasil targ8 = (BushBasil)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ8.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ8);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushLaurel )
			{
				if ( zielarstwoValue >= 90)
				{
					BushLaurel targ9 = (BushLaurel)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ9.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ9);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushChamomile )
			{
				if ( zielarstwoValue >= 90)
				{
					BushChamomile targ10 = (BushChamomile)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ10.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ10);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCaraway )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCaraway targ11 = (BushCaraway)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ11.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ11);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCoriander )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCoriander targ12 = (BushCoriander)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ12.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ12);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCinnamon )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCinnamon targ13 = (BushCinnamon)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ13.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ13);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCarnation )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCarnation targ14 = (BushCarnation)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ14.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ14);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushBranchesResinous )
			{
				if ( zielarstwoValue >= 90)
				{
					BushBranchesResinous targ15 = (BushBranchesResinous)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ15.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ15);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCorianderCove )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCorianderCove targ16 = (BushCorianderCove)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ16.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ16);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushDill )
			{
				if ( zielarstwoValue >= 90)
				{
					BushDill targ17 = (BushDill)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ17.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ17);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushDragonBlood )
			{
				if ( zielarstwoValue >= 90)
				{
					BushDragonBlood targ18 = (BushDragonBlood)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ18.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ18);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushBranchesOlibanum )
			{
				if ( zielarstwoValue >= 90)
				{
					BushBranchesOlibanum targ19 = (BushBranchesOlibanum)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ19.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ19);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushLavender )
			{
				if ( zielarstwoValue >= 90)
				{
					BushLavender targ20 = (BushLavender)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ20.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ20);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushMarjoram )
			{
				if ( zielarstwoValue >= 90)
				{
					BushMarjoram targ21 = (BushMarjoram)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ21.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ21);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushAconite )
			{
				if ( zielarstwoValue >= 90)
				{
					BushAconite targ22 = (BushAconite)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ22.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ22);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushMint )
			{
				if ( zielarstwoValue >= 90)
				{
					BushMint targ23 = (BushMint)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ23.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ23);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushCommonMugwort )
			{
				if ( zielarstwoValue >= 90)
				{
					BushCommonMugwort targ24 = (BushCommonMugwort)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ24.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ24);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushMustard)
			{
				if ( zielarstwoValue >= 90)
				{
					BushMustard targ25 = (BushMustard)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ25.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ25);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushMyrrh)
			{
				if ( zielarstwoValue >= 90)
				{
					BushMyrrh targ26 = (BushMyrrh)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ26.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ26);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushOlive)
			{
				if ( zielarstwoValue >= 90)
				{
					BushOlive targ27 = (BushOlive)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ27.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ27);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushOregano)
			{
				if ( zielarstwoValue >= 90)
				{
					BushOregano targ28 = (BushOregano)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ28.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ28);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushIris)
			{
				if ( zielarstwoValue >= 90)
				{
					BushIris targ29 = (BushIris)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ29.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ29);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushPatchouli)
			{
				if ( zielarstwoValue >= 90)
				{
					BushPatchouli targ30 = (BushPatchouli)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ30.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ30);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushPepper)
			{
				if ( zielarstwoValue >= 90)
				{
					BushPepper targ31 = (BushPepper)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ31.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ31);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushWildRose)
			{
				if ( zielarstwoValue >= 90)
				{
					BushWildRose targ32 = (BushWildRose)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ32.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ32);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushRosemary)
			{
				if ( zielarstwoValue >= 90)
				{
					BushRosemary targ33 = (BushRosemary)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ33.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ33);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushSaffron)
			{
				if ( zielarstwoValue >= 90)
				{
					BushSaffron targ34 = (BushSaffron)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ34.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ34);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushSandalwood)
			{
				if ( zielarstwoValue >= 90)
				{
					BushSandalwood targ35 = (BushSandalwood)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ35.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ35);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushSlipperyElm)
			{
				if ( zielarstwoValue >= 90)
				{
					BushSlipperyElm targ36 = (BushSlipperyElm)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ36.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ36);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is  BushThyme)
			{
				if ( zielarstwoValue >= 90)
				{
					BushThyme targ37 = ( BushThyme)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ37.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ37);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is  BushValeriane)
			{
				if ( zielarstwoValue >= 90)
				{
					BushValeriane targ38 = ( BushValeriane)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ38.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ38);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}

			if (targeted is BushWillowBranch)
			{
				if ( zielarstwoValue >= 90)
				{
					BushWillowBranch targ39 = (BushWillowBranch)targeted;
					m_Item.UsesRemaining -= 1;
					if (!from.InRange(targ39.GetWorldLocation(), 2))
					{
						from.SendLocalizedMessage(500446); // That is too far away.
						return;
					}

					if ( m_Item.UsesRemaining == 0 )
					{
						m_Item.Delete();
						if ( m_From != null )
							m_From.SendMessage( "You broke the tool." );
						if (timer != null)
						timer.Stop();
					}

					if ( player.IsHarvesting == false )
					{
						if (timer != null)
						timer.Stop();

						m_From.SendMessage( "You start cutting." );
						timer = new HarvestingZielarstwoTimer(from, targ39);
						timer.Start();
						player.IsHarvesting = true;
					}
				}
				else
				{
				m_From.SendMessage( "You have no idea how to cut it." );
				}
			}
////ZIO£A END
					else
					{
						return;
					}
			}// Item End

					else
					{
						if ( m_From != null )
							m_From.SendMessage( "You can't cut it off with a sickle." );
					}

		}//OnTarget End
	}// SierpTarget End

////The Timer
public class HarvestingZielarstwoTimer : Timer
{    
	private Mobile m_From;
        private readonly Item m_Target;
	private DateTime m_ATime;

      public HarvestingZielarstwoTimer(Mobile from, Item target) : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
      {
		m_From = from;
		m_ATime = DateTime.Now;	
                m_Target = target;
      }
      
      protected override void OnTick()
      {
		PlayerMobile pm = m_From as PlayerMobile;
		int zielarstwoValue = (int)pm.Skills[SkillName.Zielarstwo].Value;
		int bonus = 1;

	      try
	      {
		m_From.PlaySound( Utility.RandomList( 0x23A ) );
		m_From.Animate( 13, 7, 1, true, false, 1 );
	      }
	      catch{} 
////Herbs Start
		BushGarlic targ = m_Target as BushGarlic;
		BushGinseng targ1 = m_Target as BushGinseng;
		BushBloodyMoss targ2 = m_Target as BushBloodyMoss;
		BushMandrake targ3 = m_Target as BushMandrake;
		BushNightshade targ4 = m_Target as BushNightshade;

		BushWillowBranch targ5 = m_Target as BushWillowBranch;
		BushValeriane targ6 = m_Target as BushValeriane;
		BushThyme targ7 = m_Target as BushThyme;
		BushSlipperyElm targ8 = m_Target as BushSlipperyElm;
		BushSandalwood targ9 = m_Target as BushSandalwood;
		BushSaffron targ10 = m_Target as BushSaffron;
		BushRosemary targ11 = m_Target as BushRosemary;
		BushWildRose targ12 = m_Target as BushWildRose;
		BushPepper targ13 = m_Target as BushPepper;
		BushPatchouli targ14 = m_Target as BushPatchouli;
		BushIris targ15 = m_Target as BushIris;
		BushOregano targ16 = m_Target as BushOregano;
		BushOlive targ17 = m_Target as BushOlive;
		BushMyrrh targ18 = m_Target as BushMyrrh;
		BushMustard targ19 = m_Target as BushMustard;
		BushCommonMugwort targ20 = m_Target as BushCommonMugwort;
		BushMint targ21 = m_Target as BushMint;
		BushAconite targ22 = m_Target as BushAconite;
		BushMarjoram targ23 = m_Target as BushMarjoram;
		BushLavender targ24 = m_Target as BushLavender;
		BushBranchesOlibanum targ25 = m_Target as BushBranchesOlibanum;
		BushDragonBlood targ26 = m_Target as BushDragonBlood;
		BushDill targ27 = m_Target as BushDill;
		BushCorianderCove targ28 = m_Target as BushCorianderCove;
		BushBranchesResinous targ29 = m_Target as BushBranchesResinous;
		BushCarnation targ30 = m_Target as BushCarnation;
		BushCinnamon targ31 = m_Target as BushCinnamon;
		BushCoriander targ32 = m_Target as BushCoriander;
		BushCaraway targ33 = m_Target as BushCaraway;
		BushChamomile targ34 = m_Target as BushChamomile;
		BushLaurel targ35 = m_Target as BushLaurel;
		BushBasil targ36 = m_Target as BushBasil;
		BushAnise targ37 = m_Target as BushAnise;
		BushAcacia targ38 = m_Target as BushAcacia;
		BushSage targ39 = m_Target as BushSage;



		if ( zielarstwoValue >= 20 && zielarstwoValue < 30)
		{
		bonus = 2;
		}
		if ( zielarstwoValue >= 30 && zielarstwoValue < 40)
		{
		bonus = 3;
		}
		if ( zielarstwoValue >= 40 && zielarstwoValue < 50)
		{
		bonus = 4;
		}
		if ( zielarstwoValue >= 50 && zielarstwoValue < 60)
		{
		bonus = 5;
		}
		if ( zielarstwoValue >= 60 && zielarstwoValue < 70)
		{
		bonus = 6;
		}
		if ( zielarstwoValue >= 70 && zielarstwoValue < 80)
		{
		bonus = 7;
		}
		if ( zielarstwoValue >= 80 && zielarstwoValue < 90)
		{
		bonus = 8;
		}
		if ( zielarstwoValue >= 90 && zielarstwoValue < 100)
		{
		bonus = 9;
		}
		if ( zielarstwoValue >= 100)
		{
		bonus = 10;
		}

		if ( m_Target is Item ite )
		{
			pm.Direction = pm.GetDirectionTo(ite.Location);

		if (m_Target is BushGarlic)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Garlic." );
				targ.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Garlic(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 0.0, 50.0 );
			}
		}

		if (m_Target is BushGinseng)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ1.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Ginseng." );
				targ1.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Ginseng(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 70.0, 80.0 );
			}
		}

		if (m_Target is BushBloodyMoss)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ2.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Bloodmoss." );
				targ2.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Bloodmoss(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 60.0, 70.0 );
			}
		}

		if (m_Target is BushMandrake)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ3.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some MandrakeRoot." );
				targ3.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new MandrakeRoot(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 50.0, 60.0 );
			}
		}

		if (m_Target is BushNightshade)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ4.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Nightshade." );
				targ4.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Nightshade(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 80.0, 90.0 );
			}
		}

		if (m_Target is BushWillowBranch)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ5.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Willow Branches." );
				targ5.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new WillowBranches(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushValeriane)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ6.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Valeriane." );
				targ6.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Valeriane(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushThyme)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ7.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Thyme." );
				targ7.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Thyme(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushSlipperyElm)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ8.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Slippery Elm." );
				targ8.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new SlipperyElm(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushSandalwood)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ9.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Sandalwood." );
				targ9.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Sandalwood(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushSaffron)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ10.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Saffron." );
				targ10.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Saffron(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushRosemary)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ11.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Rosemary." );
				targ11.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Rosemary(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushWildRose)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ12.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Wild Rose." );
				targ12.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new WildRose(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushPepper)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ13.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Pepper." );
				targ13.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Pepper(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushPatchouli)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ14.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Patchouli." );
				targ14.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Patchouli(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushIris)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ15.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Iris." );
				targ15.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Iris(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushOregano)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ16.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Oregano." );
				targ16.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Oregano(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushOlive)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ17.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Olive." );
				targ17.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Olive(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushMyrrh)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ18.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Myrrh." );
				targ18.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Myrrh(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushMustard)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ19.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Mustard." );
				targ19.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Mustard(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCommonMugwort)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ20.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some CommonMugwort." );
				targ20.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new CommonMugwort(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushMint)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ21.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Mint." );
				targ21.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Mint(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushAconite)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ22.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Aconite." );
				targ22.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Aconite(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushMarjoram)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ23.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Marjoram." );
				targ23.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Marjoram(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushLavender)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ24.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Lavender." );
				targ24.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Lavender(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushBranchesOlibanum)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ25.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Olibanum." );
				targ25.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Olibanum(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushDragonBlood)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ26.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some DragonBlood." );
				targ26.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new DragonBlood(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushDill)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ27.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Dill." );
				targ27.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Dill(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCorianderCove)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ28.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Coriander Cove." );
				targ28.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new CorianderCove(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushBranchesResinous)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ29.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Resin." );
				targ29.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Resin(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCarnation)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ30.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Carnation." );
				targ30.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Carnation(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCinnamon)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ31.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Cinnamon." );
				targ31.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Cinnamon(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCoriander)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ32.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Coriander." );
				targ32.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Coriander(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushCaraway)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ33.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Caraway." );
				targ33.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Caraway(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushChamomile)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ34.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Chamomile." );
				targ34.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Chamomile(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushLaurel)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ35.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Laurel." );
				targ35.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Laurel(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushBasil)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ36.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Basil." );
				targ36.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Basil(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushAnise)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ37.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Anise." );
				targ37.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Anise(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushAcacia)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ38.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Acacia." );
				targ38.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Acacia(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		if (m_Target is BushSage)
		{
			if ( pm.IsHarvesting == false  )
			{      
						this.Stop();
			}

			if ( targ39.m_UsesRemaining <= 0 )
			{      
						pm.SendMessage( "Everything has already been cut." );
						this.Stop();
						m_Target.Delete();
			}

			if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0) )
			{
				pm.IsHarvesting = false;
				m_From.SendMessage(33, "You found some Sage." );
				targ39.m_UsesRemaining -= 1;
				m_From.AddToBackpack( new Sage(bonus) );
				this.Stop();
				pm.CheckSkill( SkillName.Zielarstwo, 90.0, 100.0 );
			}
		}

		}// Item End
	}// Tick End
}// HarvestingZielarstwoTimer End

	}
}