using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class ProofofFaithGodPerol : Item
    {
        [Constructable]
        public ProofofFaithGodPerol() : base( 0x9F97 )
        {
		Name = "Proof Of Faith - God Perol";
		Hue = 0;
		Weight = 5.0;
        }

        public ProofofFaithGodPerol(Serial serial) : base(serial)
        {
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            list.Add("<basefont color=#FF9925>Take this proof of faith to the temple of God Perol to be promoted to Cleric.<basefont color=#FFFFFF>");
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
