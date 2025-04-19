// SICKLE v 7.0 by PAN POMIDOR
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
    public class Sickle : BaseSword, IHarvestTool
    {
        private bool m_Cutting;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Cutting
        {
            get => m_Cutting;
            set => m_Cutting = value;
        }

        [Constructable]
        public Sickle() : base(0x26BB)
        {
            Name = "herbal sickle";
            Weight = 10.0;
        }

        public override WeaponAbility PrimaryAbility => WeaponAbility.ParalyzingBlow;
        public override WeaponAbility SecondaryAbility => WeaponAbility.MortalStrike;
        public override int AosStrengthReq => 25;
        public override int AosMinDamage => 12;
        public override int AosMaxDamage => 16;
        public override int AosSpeed => 36;
        public override float MlSpeed => 3.00f;
        public override int OldStrengthReq => 25;
        public override int OldMinDamage => 13;
        public override int OldMaxDamage => 15;
        public override int OldSpeed => 36;
        public override int DefHitSound => 0x23B;
        public override int DefMissSound => 0x23A;
        public override int InitMinHits => 31;
        public override int InitMaxHits => 70;

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1060662, "Use\t{0}", UsesRemaining);
        }

        public Sickle(Serial serial) : base(serial) { }

        public override void OnDoubleClick(Mobile from)
        {
            if (Parent != from)
            {
                from.SendMessage("You must hold a sickle in your hand.");
                return;
            }

            if (from.Mounted)
            {
                from.SendMessage(0x21, "You can't do this while mounted.");
                return;
            }

            if (from is PlayerMobile player)
            {
                if (player.IsHarvesting)
                {
                    Cutting = true;
                    return;
                }

                Cutting = false;

                if (!Cutting)
                {
                    from.Target = new SickleTarget(this, from);
                    from.SendMessage("Where do you want to cut?");
                }
                else
                {
                    from.SendMessage("You must wait to use this item again.");
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version 
            writer.Write(m_Cutting);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Cutting = reader.ReadBool();
        }

        private class SickleTarget : Target
        {
            private readonly Sickle m_Item;
            private readonly Mobile m_From;
            private Timer timer;

            public SickleTarget(Sickle item, Mobile from) : base(1, true, TargetFlags.None)
            {
                m_Item = item;
                m_From = from;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (from is PlayerMobile player)
                {
                    int herbalismSkillValue = (int)from.Skills[SkillName.Herbalism].Value;

                    if (targeted is Item item)
                    {
                        from.Direction = from.GetDirectionTo(item.Location);

                        void HandleHarvestLogic(int requiredSkill, Item targetItem, string foundMessage, Func<int, Item> createItem)
                        {
                            if (herbalismSkillValue >= requiredSkill)
                            {
                                if (!from.InRange(targetItem.GetWorldLocation(), 2))
                                {
                                    from.SendLocalizedMessage(500446); // That is too far away.
                                    return;
                                }

                                m_Item.UsesRemaining -= 1;

                                if (m_Item.UsesRemaining == 0)
                                {
                                    m_Item.Delete();
                                    m_From.SendMessage("You broke the tool.");
                                    timer?.Stop();
                                }

                                if (!player.IsHarvesting)
                                {
                                    if (timer != null)
                                        timer.Stop();

                                    from.SendMessage("You start cutting.");
                                    timer = new HarvestingHerbalismTimer(from, targetItem, foundMessage, createItem);
                                    timer.Start();
                                    player.IsHarvesting = true;
                                }
                                else
                                {
                                    from.SendMessage("You are already harvesting.");
                                    return;
                                }
                            }
                        }

			if (item is BushGarlic garlic)
			{
			    HandleHarvestLogic(0, garlic, "You found some Garlic.", bonus => new Garlic(bonus));
			}
			else if (item is BushGinseng ginseng)
			{
			    HandleHarvestLogic(70, ginseng, "You found some Ginseng.", bonus => new Ginseng(bonus));
			}
			else if (item is BushBloodyMoss bloodMoss)
			{
			    HandleHarvestLogic(60, bloodMoss, "You found some Bloodmoss.", bonus => new Bloodmoss(bonus));
			}
			else if (item is BushMandrake mandrake)
			{
			    HandleHarvestLogic(50, mandrake, "You found some Mandrake Root.", bonus => new MandrakeRoot(bonus));
			}
			else if (item is BushNightshade nightshade)
			{
			    HandleHarvestLogic(80, nightshade, "You found some Nightshade.", bonus => new Nightshade(bonus));
			}
			else if (item is BushSage sage)
			{
			    HandleHarvestLogic(90, sage, "You found some Sage.", bonus => new Sage(bonus));
			}
			else if (item is BushAcacia acacia)
			{
			    HandleHarvestLogic(90, acacia, "You found some Acacia.", bonus => new Acacia(bonus));
			}
			else if (item is BushAnise anise)
			{
			    HandleHarvestLogic(90, anise, "You found some Anise.", bonus => new Anise(bonus));
			}
			else if (item is BushBasil basil)
			{
			    HandleHarvestLogic(90, basil, "You found some Basil.", bonus => new Basil(bonus));
			}
			else if (item is BushLaurel laurel)
			{
			    HandleHarvestLogic(90, laurel, "You found some Laurel.", bonus => new Laurel(bonus));
			}
			else if (item is BushChamomile chamomile)
			{
			    HandleHarvestLogic(90, chamomile, "You found some Chamomile.", bonus => new Chamomile(bonus));
			}
			else if (item is BushCaraway caraway)
			{
			    HandleHarvestLogic(90, caraway, "You found some Caraway.", bonus => new Caraway(bonus));
			}
			else if (item is BushCoriander coriander)
			{
			    HandleHarvestLogic(90, coriander, "You found some Coriander.", bonus => new Coriander(bonus));
			}
			else if (item is BushCinnamon cinnamon)
			{
			    HandleHarvestLogic(90, cinnamon, "You found some Cinnamon.", bonus => new Cinnamon(bonus));
			}
			else if (item is BushCarnation carnation)
			{
			    HandleHarvestLogic(90, carnation, "You found some Carnation.", bonus => new Carnation(bonus));
			}
			else if (item is BushBranchesResinous resinousBranches)
			{
			    HandleHarvestLogic(90, resinousBranches, "You found some Resin.", bonus => new Resin(bonus));
			}
			else if (item is BushCorianderCove corianderCove)
			{
			    HandleHarvestLogic(90, corianderCove, "You found some Coriander Cove.", bonus => new CorianderCove(bonus));
			}
			else if (item is BushDill dill)
			{
			    HandleHarvestLogic(90, dill, "You found some Dill.", bonus => new Dill(bonus));
			}
			else if (item is BushDragonBlood dragonBlood)
			{
			    HandleHarvestLogic(90, dragonBlood, "You found some Dragon Blood.", bonus => new DragonBlood(bonus));
			}
			else if (item is BushBranchesOlibanum olibanumBranches)
			{
			    HandleHarvestLogic(90, olibanumBranches, "You found some Olibanum.", bonus => new Olibanum(bonus));
			}
			else if (item is BushLavender lavender)
			{
			    HandleHarvestLogic(90, lavender, "You found some Lavender.", bonus => new Lavender(bonus));
			}
			else if (item is BushMarjoram marjoram)
			{
			    HandleHarvestLogic(90, marjoram, "You found some Marjoram.", bonus => new Marjoram(bonus));
			}
			else if (item is BushAconite aconite)
			{
			    HandleHarvestLogic(90, aconite, "You found some Aconite.", bonus => new Aconite(bonus));
			}
			else if (item is BushMint mint)
			{
			    HandleHarvestLogic(90, mint, "You found some Mint.", bonus => new Mint(bonus));
			}
			else if (item is BushCommonMugwort commonMugwort)
			{
			    HandleHarvestLogic(90, commonMugwort, "You found some Common Mugwort.", bonus => new CommonMugwort(bonus));
			}
			else if (item is BushMustard mustard)
			{
			    HandleHarvestLogic(90, mustard, "You found some Mustard.", bonus => new Mustard(bonus));
			}
			else if (item is BushMyrrh myrrh)
			{
			    HandleHarvestLogic(90, myrrh, "You found some Myrrh.", bonus => new Myrrh(bonus));
			}
			else if (item is BushOlive olive)
			{
			    HandleHarvestLogic(90, olive, "You found some Olive.", bonus => new Olive(bonus));
			}
			else if (item is BushOregano oregano)
			{
			    HandleHarvestLogic(90, oregano, "You found some Oregano.", bonus => new Oregano(bonus));
			}
			else if (item is BushIris iris)
			{
			    HandleHarvestLogic(90, iris, "You found some Iris.", bonus => new Iris(bonus));
			}
			else if (item is BushPatchouli patchouli)
			{
			    HandleHarvestLogic(90, patchouli, "You found some Patchouli.", bonus => new Patchouli(bonus));
			}
			else if (item is BushPepper pepper)
			{
			    HandleHarvestLogic(90, pepper, "You found some Pepper.", bonus => new Pepper(bonus));
			}
			else if (item is BushWildRose wildRose)
			{
			    HandleHarvestLogic(90, wildRose, "You found some Wild Rose.", bonus => new WildRose(bonus));
			}
			else if (item is BushRosemary rosemary)
			{
			    HandleHarvestLogic(90, rosemary, "You found some Rosemary.", bonus => new Rosemary(bonus));
			}
			else if (item is BushSaffron saffron)
			{
			    HandleHarvestLogic(90, saffron, "You found some Saffron.", bonus => new Saffron(bonus));
			}
			else if (item is BushSandalwood sandalwood)
			{
			    HandleHarvestLogic(90, sandalwood, "You found some Sandalwood.", bonus => new Sandalwood(bonus));
			}
			else if (item is BushSlipperyElm slipperyElm)
			{
			    HandleHarvestLogic(90, slipperyElm, "You found some Slippery Elm.", bonus => new SlipperyElm(bonus));
			}
			else if (item is BushThyme thyme)
			{
			    HandleHarvestLogic(90, thyme, "You found some Thyme.", bonus => new Thyme(bonus));
			}
			else if (item is BushValeriane valeriane)
			{
			    HandleHarvestLogic(90, valeriane, "You found some Valeriane.", bonus => new Valeriane(bonus));
			}
			else if (item is BushWillowBranch willowBranch)
			{
			    HandleHarvestLogic(90, willowBranch, "You found some Willow Branches.", bonus => new WillowBranches(bonus));
			}
                    }
                }
            }
        }

        private class HarvestingHerbalismTimer : Timer
        {
            private readonly Mobile m_From;
            private readonly Item m_Target;
            private readonly string m_FoundMessage;
            private readonly Func<int, Item> m_CreateItem;
            private readonly DateTime m_ATime;

            public HarvestingHerbalismTimer(Mobile from, Item target, string foundMessage, Func<int, Item> createItem)
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
            {
                m_From = from;
                m_Target = target;
                m_FoundMessage = foundMessage;
                m_CreateItem = createItem;
                m_ATime = DateTime.Now;
            }

            protected override void OnTick()
            {
                if (m_From is PlayerMobile player)
                {
                    if (!player.IsHarvesting)
                    {
                        Stop();
                        return;
                    }

                    int herbalismValue = (int)player.Skills[SkillName.Herbalism].Value;
                    int bonus = (herbalismValue / 10) + 1;

                    m_From.PlaySound(Utility.RandomList(0x23A));
                    m_From.Animate(13, 7, 1, true, false, 1);

                    if (m_Target is Item ite)
                    {
                        player.Direction = player.GetDirectionTo(ite.Location);

                        if (DateTime.Now > m_ATime + TimeSpan.FromSeconds(10.0))
                        {
                            player.IsHarvesting = false;
                            m_From.SendMessage(33, m_FoundMessage);
                            m_From.AddToBackpack(m_CreateItem(bonus));
                            Stop();
                            player.CheckSkill(SkillName.Herbalism, herbalismValue, herbalismValue + 10);
                        }
                    }
                }
            }
        }
    }
}