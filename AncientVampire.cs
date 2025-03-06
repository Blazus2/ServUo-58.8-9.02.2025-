using System;
using System.Collections;
using Server.Items;
using Server.ACC.CSS.Systems.Undead;

namespace Server.Mobiles
{
    [CorpseName("ancient vampire corpse")]
    public class AncientVampire : BaseCreature
    {
        private DateTime m_NextAbilityTime;
        private static readonly string[] Titles = new string[]
        {
            "the Bloodshade Fiend",
            "the Crimson Overlord",
            "the Darkfang Wraith",
            "the Sanguine Sovereign",
            "the Shadowfang Revenant",
            "the Nocturnal Sovereign",
            "the Eldritch Nosferatu",
            "the Shadowblood Demon"
        };

        [Constructable]
        public AncientVampire()
            : base(AIType.AI_NecroMage, FightMode.Aggressor, 10, 1, 0.2, 0.4)
        {
            this.Name = "Ancient Vampire";
            this.Body = 1585;
            this.SetStr(416, 505);
            this.SetDex(146, 165);
            this.SetInt(566, 655);

            this.SetHits(250, 303);

            this.SetDamage(11, 13);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 45, 55);
            this.SetResistance(ResistanceType.Fire, 15, 25);
            this.SetResistance(ResistanceType.Cold, 45, 55);
            this.SetResistance(ResistanceType.Poison, 30, 40);
            this.SetResistance(ResistanceType.Energy, 30, 40);

            this.SetSkill(SkillName.OcenaIntelektu, 90.1, 100.0);
            this.SetSkill(SkillName.Magia, 90.1, 100.0);
            this.SetSkill(SkillName.Medytacja, 90.1, 100.0);
            this.SetSkill(SkillName.OdpornoscNaMagie, 150.5, 200.0);
            this.SetSkill(SkillName.Taktyka, 50.1, 70.0);
            this.SetSkill(SkillName.WalkaPiesciami, 60.1, 80.0);
            this.SetSkill(SkillName.Nekromancja, 90.1, 100.0);
            this.SetSkill(SkillName.MowaDuchow, 90.1, 100.0);

            this.Fame = 18000;
            this.Karma = 18000;

            this.VirtualArmor = 50;

        if (Utility.RandomDouble() < 0.1) //10%
        {
            switch (Utility.Random(13))
            {
                case 0: PackItem(new VampireBatFormScroll()); break;
                case 1: PackItem(new VampireShadowBatsSwarmScroll()); break;
                case 2: PackItem(new VampireWallofSpikesScroll()); break;
                case 3: PackItem(new VampireGiftofBloodScroll()); break;
                case 4: PackItem(new VampireSpeedScroll()); break;
                case 5: PackItem(new VampireSummonServantScroll()); break;
                case 6: PackItem(new VampireBloodSignScroll()); break;
                case 7: PackItem(new VampireJourneyScroll()); break;
                case 8: PackItem(new VampireDarknessOfNightScroll()); break;
                case 9: PackItem(new VampireHypnosisScroll()); break;
                case 10: PackItem(new VampireGraveSoilScroll()); break;
                case 11: PackItem(new VampirePoisonIvyScroll()); break;
                case 12: PackItem(new VampireCmentaryGateScroll()); break;
                case 13: PackItem(new VampireMassDeathScroll()); break;
            }
        }

	this.m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(2, 5));
	}

        public AncientVampire(Serial serial)
            : base(serial)
        {
        }

	public override bool AutoDispel { get { return true; } }
	public override bool BardImmune { get { return !Core.AOS; } }
	public override bool CanRummageCorpses { get { return true; } }
	public override Poison PoisonImmune { get { return Poison.Lethal; } }
	public override bool InitialInnocent { get { return true; } }
	public override bool AlwaysMurderer{ get{ return true; } }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 2);
        }

        public override int GetHurtSound()
        {
            return 0x167;
        }

        public override int GetDeathSound()
        {
            return 0xBC;
        }

        public override int GetAttackSound()
        {
            return 0x28B;
        }

        public override void OnThink()
        {
            if (DateTime.UtcNow >= this.m_NextAbilityTime)
            {
                Mobile combatant = this.Combatant as Mobile;

                if (combatant != null && combatant.Map == this.Map && combatant.InRange(this, 12))
                {
                    this.m_NextAbilityTime = DateTime.UtcNow + TimeSpan.FromSeconds(Utility.RandomMinMax(10, 15));

                    int ability = Utility.Random(4);

                    switch ( ability )
                    {
                        case 0:
                            this.DoFocusedLeech(combatant, "Thine essence will fill my withering body with strength!");
                            break;
                        case 1:
                            this.DoFocusedLeech(combatant, "I rebuke thee, worm, and cleanse thy vile spirit of its tainted blood!");
                            break;
                        case 2:
                            this.DoFocusedLeech(combatant, "I devour your life's essence to strengthen my resolve!");
                            break;
                        case 3:
                            this.DoAreaLeech();
                            break;
                    // TODO: Resurrect ability
                    }
                }
            }

            base.OnThink();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        private void DoAreaLeech()
        {
            this.m_NextAbilityTime += TimeSpan.FromSeconds(2.5);

            this.Say(true, "Beware, mortals!  You have provoked my wrath!");
            this.FixedParticles(0x376A, 10, 10, 9537, 33, 0, EffectLayer.Waist);

            Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(DoAreaLeech_Finish));
        }

        private void DoAreaLeech_Finish()
        {
            ArrayList list = new ArrayList();
            IPooledEnumerable eable = GetMobilesInRange(6);

            foreach (Mobile m in eable)
            {
                if (this.CanBeHarmful(m) && this.IsEnemy(m))
                    list.Add(m);
            }
            eable.Free();

            if (list.Count == 0)
            {
                this.Say(true, "Bah! You have escaped my grasp this time, mortal!");
            }
            else
            {
                double scalar;

                if (list.Count == 1)
                    scalar = 0.75;
                else if (list.Count == 2)
                    scalar = 0.50;
                else
                    scalar = 0.25;

                for (int i = 0; i < list.Count; ++i)
                {
                    Mobile m = (Mobile)list[i];

                    int damage = (int)(m.Hits * scalar);

                    damage += Utility.RandomMinMax(-5, 5);

                    if (damage < 1)
                        damage = 1;

                    m.MovingParticles(this, 0x36F4, 1, 0, false, false, 32, 0, 9535, 1, 0, (EffectLayer)255, 0x100);
                    m.MovingParticles(this, 0x0001, 1, 0, false, true, 32, 0, 9535, 9536, 0, (EffectLayer)255, 0);

                    this.DoHarmful(m);
                    this.Hits += AOS.Damage(m, this, damage, 100, 0, 0, 0, 0);
                }

                this.Say(true, "If I cannot cleanse thy soul, I will destroy it!");
            }
        }

        private void DoFocusedLeech(Mobile combatant, string message)
        {
            this.Say(true, message);

            Timer.DelayCall(TimeSpan.FromSeconds(0.5), new TimerStateCallback(DoFocusedLeech_Stage1), combatant);
        }

        private void DoFocusedLeech_Stage1(object state)
        {
            Mobile combatant = (Mobile)state;

            if (this.CanBeHarmful(combatant))
            {
                this.MovingParticles(combatant, 0x36FA, 1, 0, false, false, 1108, 0, 9533, 1, 0, (EffectLayer)255, 0x100);
                this.MovingParticles(combatant, 0x0001, 1, 0, false, true, 1108, 0, 9533, 9534, 0, (EffectLayer)255, 0);
                this.PlaySound(0x1FB);

                Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(DoFocusedLeech_Stage2), combatant);
            }
        }

        private void DoFocusedLeech_Stage2(object state)
        {
            Mobile combatant = (Mobile)state;

            if (this.CanBeHarmful(combatant))
            {
                combatant.MovingParticles(this, 0x36F4, 1, 0, false, false, 32, 0, 9535, 1, 0, (EffectLayer)255, 0x100);
                combatant.MovingParticles(this, 0x0001, 1, 0, false, true, 32, 0, 9535, 9536, 0, (EffectLayer)255, 0);

                this.PlaySound(0x209);
                this.DoHarmful(combatant);
                this.Hits += AOS.Damage(combatant, this, Utility.RandomMinMax(30, 40) - (Core.AOS ? 0 : 10), 100, 0, 0, 0, 0);
            }
        }
    }
}