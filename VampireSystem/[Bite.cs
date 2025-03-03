using System; 
using System.Collections; 
using Server; 
using Server.Mobiles; 
using Server.Network; 
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Targeting;
using Server.Commands;

namespace Server.Mobiles
{ 
	public class Uk¹szenie
	{ 

		public static void Initialize()
		{
			CommandSystem.Register( "Bite", AccessLevel.Player, new CommandEventHandler( Bite_OnCommand ) );    
		} 

		public static void Bite_OnCommand( CommandEventArgs args )
		{ 
			Mobile from = args.Mobile; 
			PlayerMobile pm = (PlayerMobile)from;
          
			if(pm.IsVampire == true) 
			{  
				from.SendMessage ( "Choose the target you want to bite." );
				from.Target = new InternalTarget();
			} 
			else
			{  
				return;
			} 
		} 

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 18, false, TargetFlags.None )
			{
			}

		protected override void OnTarget( Mobile from, object target )
		{
			if ( target is BaseFamiliar && target != from ) 
			{ 
				BaseFamiliar x = ( BaseFamiliar ) target; 
				if ( x.Alive )
				{
					from.SendMessage( "You can't bite it." );
					return;
				}
			}
			if ( target is AbilityCreature && target != from && target is CursedShadow ) 
			{ 
				AbilityCreature a = ( CursedShadow ) target;
				if ( a.Alive )
				{
					from.SendMessage( "You can't bite it." );
					return;
				}
			}
			 if ( target is BaseCreature && target != from ) 
			 { 
				PlayerMobile player = from as PlayerMobile;
				BaseCreature t = ( BaseCreature ) target; 

				if (!from.InRange(t, 1))
				{
					from.SendMessage( "The target is too far away." );
					return;
                                }
				if ( !t.Alive )
				{
					from.SendMessage( "It's dead, you can't bite it." );
					return;
				}
				if ( t.AccessLevel == AccessLevel.Administrator  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( t.AccessLevel == AccessLevel.GameMaster  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( t.AccessLevel == AccessLevel.Seer  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( t.AccessLevel == AccessLevel.Counselor  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( player.VampireBiteTime != TimeSpan.Zero )
				{
					from.SendMessage( "You need to rest after your last bite {0} h {1} min {2} sec.", player.VampireBiteTime.Hours.ToString(), player.VampireBiteTime.Minutes.ToString(), player.VampireBiteTime.Seconds.ToString() );
					return;
				}
				else if ( t.Hits == t.HitsMax )
				{
					player.VampireBiteTime = TimeSpan.FromMinutes(2.0);
                                        if ( player.PulaKrwi < 100 )
                                        {
                                        player.PulaKrwi += 20;
					t.Hits -= 30;
					player.FixedParticles(0x3779, 1, 15, 9905, 32, 7, EffectLayer.Head);
					player.FixedParticles(0x37B9, 1, 14, 9502, 32, 7, (EffectLayer)255);
					player.PlaySound(0x031);
					from.CloseGump( typeof( gumppula ) );
					from.SendGump( new gumppula ( from ) );
					}
                                        if ( player.PulaKrwi >= 100 )
                                        {
                                        player.PulaKrwi = 100;
					t.Hits -= 30;
					player.FixedParticles(0x3779, 1, 15, 9905, 32, 2, EffectLayer.Head);
					player.FixedParticles(0x37B9, 1, 14, 9502, 32, 5, (EffectLayer)255);
					player.PlaySound(0x031);
					from.CloseGump( typeof( gumppula ) );
					from.SendGump( new gumppula ( from ) );
					}
				}
				else if ( t.Hits < t.HitsMax )
				{
					from.SendMessage( "The target is injured and does not have sufficient blood supplies." );
					return;
				}
				else
				{
					from.SendMessage( "This is not a valid target." );  
					return;
				}
			}
			else if ( target is Mobile && target != from ) 
			{ 
				    Mobile m = ( Mobile ) target; 
				    PlayerMobile player = from as PlayerMobile;

				if (!from.InRange(m, 1))
				{
					from.SendMessage( "The target is too far away." );
					return;
                                }
				if ( !m.Alive )
				{
					from.SendMessage( "It's dead, you can't bite it." );
					return;
				}
				if ( m.AccessLevel == AccessLevel.Administrator  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( m.AccessLevel == AccessLevel.GameMaster  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( m.AccessLevel == AccessLevel.Seer  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( m.AccessLevel == AccessLevel.Counselor  )
				{
					from.SendMessage( "A strange force prevents you from doing so." );
					return;
				}
				else if ( player.VampireBiteTime != TimeSpan.Zero )
				{
					from.SendMessage( "You need to rest after your last bite {0} h {1} min {2} sec.", player.VampireBiteTime.Hours.ToString(), player.VampireBiteTime.Minutes.ToString(), player.VampireBiteTime.Seconds.ToString() );
					return;
				}
				else if ( m.Hits == m.HitsMax )
				{
					player.VampireBiteTime = TimeSpan.FromHours(1.0);
                                        if ( player.PulaKrwi < 100 )
                                        {
////Parali¿
	double duration;
	double duration1;
	double duration2;

	if ( player.Skills[SkillName.OdpornoscNaMagie].Value > m.Skills[SkillName.OdpornoscNaMagie].Value )
	{
	    duration1 = 15.0 - (player.Skills[SkillName.OdpornoscNaMagie].Value / 20);
	    player.Paralyze(TimeSpan.FromSeconds(duration1));
	}
	if ( m.Skills[SkillName.OdpornoscNaMagie].Value > player.Skills[SkillName.OdpornoscNaMagie].Value )
	{
	    duration2 = 15.0 - (m.Skills[SkillName.OdpornoscNaMagie].Value / 20);
	    m.Paralyze(TimeSpan.FromSeconds(duration2));
	}
	else
	{
	duration = 15.0;
	player.Paralyze(TimeSpan.FromSeconds(duration));
	m.Paralyze(TimeSpan.FromSeconds(duration));
	}

	m.PlaySound(0x204);
	m.FixedEffect(0x376A, 6, 200);
	player.PlaySound(0x204);
	player.FixedEffect(0x376A, 6, 200);
////Parali¿
                                        player.PulaKrwi = 100;
					m.Hits -= 30;
					if (!m.Mounted )
					{
					m.Animate( 22, 5, 1, true, false, 0 );
					}
					player.FixedParticles(0x3779, 1, 15, 9905, 32, 7, EffectLayer.Head);
					player.FixedParticles(0x37B9, 1, 14, 9502, 32, 7, (EffectLayer)255);
					player.PlaySound(0x031);
					player.LocalOverheadMessage(MessageType.Emote, 1161, true, string.Format("*You bite into the arteries and drink the blood*") );
					player.Criminal = true;
					from.CloseGump( typeof( gumppula ) );
					from.SendGump( new gumppula ( from ) );
                                        }
				}
				else if ( m.Hits < m.HitsMax )
				{
					from.SendMessage( "The target is injured and does not have sufficient blood supplies." );
					return;
				}
				else
				{
					    from.SendMessage( "This is not a valid target." );  
						return;
				}
			}

			 else 
			 { 
			    from.SendMessage( "This is not a valid target." );  
				return;
			 } 
		}

    }
} 
}