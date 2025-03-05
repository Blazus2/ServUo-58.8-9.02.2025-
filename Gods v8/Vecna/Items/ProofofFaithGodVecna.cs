using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class ProofofFaithGodVecna : Item
    {
        [Constructable]
        public ProofofFaithGodVecna() : base( 0x9F97 )
        {
		Name = "Proof Of Faith - God Vecna";
		Hue = 0;
		Weight = 5.0;
        }

        public ProofofFaithGodVecna(Serial serial) : base(serial)
        {
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            list.Add("<basefont color=#FF9925>Take this proof of faith to the temple of God Vecna to be promoted to Necromancer.<basefont color=#FFFFFF>");
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
