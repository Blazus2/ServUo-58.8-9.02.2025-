using System;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
    public class MedallionofDevotedFollowerVecna : Item
    {

        [Constructable]
        public MedallionofDevotedFollowerVecna()
            : base(0xA429)
        {
            Name = "Medallion of Devoted Follower";
            Weight = 1.0;
            Layer = Layer.Neck;
        }

        public MedallionofDevotedFollowerVecna(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.HasGump(typeof(MedallionofDevotedFollowerVecnaGump)))
            {
                from.SendGump(new MedallionofDevotedFollowerVecnaGump());
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

            if (pm.A9Faith == A9Faith.Necromancer)
                return true;

            if (pm.A9Faith != A9Faith.Necromancer)
            {
		m.SendMessage("This item can only be used by the Necromancer."); 

                return false;
            }

            return true;
        }

        private class MedallionofDevotedFollowerVecnaGump : Gump
        {
            public MedallionofDevotedFollowerVecnaGump()
                : base(100, 100)
            {
                AddPage(0);

                AddBackground(0, 0, 454, 400, 0x24A4);
                AddItem(75, 120, 0xA429);
                AddHtml(177, 50, 250, 18, @"Medallion of Devoted Follower", (bool)false, (bool)false);
                AddHtml(177, 77, 250, 36, @"A gift from the temple of Vecna.", (bool)false, (bool)false);
                AddHtml(177, 122, 250, 228, @"In the central part of the medal there is a round, impenetrable eye, in the iris of which you can see twinkling points of light resembling stars. This eye is surrounded by an intricate pattern of runes that seem to whisper centuries-old spells. The edges of the medallion are decorated with small, carved symbols that represent various aspects of magic, and their meaning is known only to a few.", (bool)false, (bool)false);
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
