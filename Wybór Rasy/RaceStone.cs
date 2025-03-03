using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Custom
{
	public class RaceStone : Item
	{
		[Constructable]
		public RaceStone() : base( 0x35EF )
		{
			Movable = false;
			//Hue = 0x489;
			Name = "Soul Crystal";
		}

		public RaceStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( !( m is PlayerMobile ) )
			{
				m.SendMessage( "You can't use this!");
				return;
			}
			
			PlayerMobile from = m as PlayerMobile;
			if ( from.A6TworzenieWygladu == false )
			{
				m.SendMessage( "You can choose your race!" );
                                m.SendGump( new RaceGump());
                        }
				m.CloseGump(typeof(DostosowanieWygl¹duGump));
				
				m.CloseGump(typeof(RaceNameChangeGump));
				
				m.CloseGump(typeof(RaceHumanBaseGump));
				m.CloseGump(typeof(RaceElvenBaseGump));
				m.CloseGump(typeof(RaceGargoyleBaseGump));

				m.CloseGump(typeof(RaceBeardGump));

				m.CloseGump(typeof(RaceHairDyeGump));

			    if (m.HasGump(typeof(SkinToneGump)))
				m.CloseGump(typeof(SkinToneGump));
			    if (m.HasGump(typeof(ElvenSkinToneGump)))
				m.CloseGump(typeof(ElvenSkinToneGump));
			    if (m.HasGump(typeof(DrowSkinToneGump)))
				m.CloseGump(typeof(DrowSkinToneGump));
			    if (m.HasGump(typeof(DemonSkinToneGump)))
				m.CloseGump(typeof(DemonSkinToneGump));

			if ( from.A6TworzenieWygladu == true && from.A7TworzenieCech == false )
                        {
				from.RawStr = 10;
				from.RawDex = 10;
				from.RawInt = 10;
				from.Skills.Alchemy.Base = 0;
				from.Skills.Anatomy.Base = 0;
				from.Skills.AnimalLore.Base = 0;
				from.Skills.ItemID.Base = 0;
				from.Skills.ArmsLore.Base = 0;
				from.Skills.Parry.Base = 0;
				from.Skills.Begging.Base = 0;
				from.Skills.Blacksmith.Base = 0;
				from.Skills.Fletching.Base = 0;
				from.Skills.Peacemaking.Base = 0;
				from.Skills.Camping.Base = 0;
				from.Skills.Carpentry.Base = 0;
				from.Skills.Cartography.Base = 0;
				from.Skills.Cooking.Base = 0;
				from.Skills.DetectHidden.Base = 0;
				from.Skills.Discordance.Base = 0;
				from.Skills.EvalInt.Base = 0;
				from.Skills.Healing.Base = 0;
				from.Skills.Fishing.Base = 0;
				from.Skills.Forensics.Base = 0;
				from.Skills.Herding.Base = 0;
				from.Skills.Hiding.Base = 0;
				from.Skills.Provocation.Base = 0;
				from.Skills.Inscribe.Base = 0;
				from.Skills.Lockpicking.Base = 0;
				from.Skills.Magery.Base = 0;
				from.Skills.MagicResist.Base = 0;
				from.Skills.Tactics.Base = 0;
				from.Skills.Snooping.Base = 0;
				from.Skills.Musicianship.Base = 0;
				from.Skills.Poisoning.Base = 0;
				from.Skills.Archery.Base = 0;
				from.Skills.SpiritSpeak.Base = 0;
				from.Skills.Stealing.Base = 0;
				from.Skills.Tailoring.Base = 0;
				from.Skills.AnimalTaming.Base = 0;
				from.Skills.Wampiryzm.Base = 0;
				from.Skills.Tinkering.Base = 0;
				from.Skills.Tracking.Base = 0;
				from.Skills.Veterinary.Base = 0;
				from.Skills.Swords.Base = 0;
				from.Skills.Macing.Base = 0;
				from.Skills.Fencing.Base = 0;
				from.Skills.Wrestling.Base = 0;
				from.Skills.Lumberjacking.Base = 0;
				from.Skills.Mining.Base = 0;
				from.Skills.Meditation.Base = 0;
				from.Skills.Stealth.Base = 0;
				from.Skills.RemoveTrap.Base = 0;
				from.Skills.Necromancy.Base = 0;
				from.Skills.Focus.Base = 0;
				from.Skills.Chivalry.Base = 0;
				from.Skills.Bushido.Base = 0;
				from.Skills.Ninjitsu.Base = 0;
				from.Skills.Spellweaving.Base = 0;
				from.Skills.Mysticism.Base = 0;
				from.Skills.Imbuing.Base = 0;
				from.Skills.Throwing.Base = 0;
				from.SendMessage( "You already have your race selected!" );
				from.SendGump(new DostosowanieStatystykGump());
                        }

			if ( from.A7TworzenieCech == true )
			{
				m.SendMessage( "We can move on to the summary!" );
                                m.SendGump( new PodsumowanieGump(m));
                        }

		}
	}
}