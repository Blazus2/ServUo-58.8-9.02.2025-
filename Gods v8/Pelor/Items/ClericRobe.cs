using System;
using Server.Mobiles;

namespace Server.Items
{
    [Flipable(0x4B9D, 0x4B9E)]
    public class ClericRobe : BaseOuterTorso
	{
		public override bool IsArtifact { get { return true; } }

        [Constructable]
        public ClericRobe() : base(0x4B9D)
        {
	Name = "Cleric Robe";
	Weight = 1.0;
        }

        public ClericRobe(Serial serial)
            : base(serial)
        {
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