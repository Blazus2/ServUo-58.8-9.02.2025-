using System;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class MedallionofDevotedFollower : Item
    {

        [Constructable]
        public MedallionofDevotedFollower()
            : base(0xA429)
        {
            Name = "Medallion of Devoted Follower";
            Weight = 1.0;
            Layer = Layer.Neck;
        }

        public MedallionofDevotedFollower(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.HasGump(typeof(MedallionofDevotedFollowerGump)))
            {
                from.SendGump(new MedallionofDevotedFollowerGump());
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1157722, "its origin"); // *Your proficiency in ~1_SKILL~ reveals more about the item*
                from.PlaySound(1050);
            }
        }

        public override bool OnEquip(Mobile from)
        {
            return this.Validate(from) && base.OnEquip(from);
        }

        public override void OnSingleClick(Mobile from)
        {
            if (this.Validate(this.Parent as Mobile))
                base.OnSingleClick(from);
        }

        public virtual bool Validate(Mobile m)
        {
		PlayerMobile pm = (PlayerMobile)m;

            if (pm.A9Faith == A9Faith.ClericOfPerol)
                return true;

            if (pm.A9Faith != A9Faith.ClericOfPerol)
            {
		m.SendMessage("This item can only be used by the Cleric."); 

                return false;
            }

            return true;
        }

        private class MedallionofDevotedFollowerGump : Gump
        {
            public MedallionofDevotedFollowerGump()
                : base(100, 100)
            {
                AddPage(0);

                AddBackground(0, 0, 454, 400, 0x24A4);
                AddItem(75, 120, 0xA429);
                AddHtml(177, 50, 250, 18, @"Medallion of Devoted Follower", (bool)false, (bool)false);
                AddHtml(177, 77, 250, 36, @"A gift from the temple of Pelor.", (bool)false, (bool)false);
                AddHtml(177, 122, 250, 228, @"Made of silver, its surface is intricately decorated with plant motifs, symbolizing life and healing. In the center of the medallion is the Sun.", (bool)false, (bool)false);
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
