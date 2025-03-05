using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class MarkOfPriestGodVecna : Item
    {

        [Constructable]
        public MarkOfPriestGodVecna() : base( 0x2DB3 )
        {
		Name = "Mark Of Priest - God Vecna";
		Hue = 0;
		Weight = 5.0;
        }

        public MarkOfPriestGodVecna(Serial serial) : base(serial)
        {
        }

        public override void AddWeightProperty(ObjectPropertyList list)
        {
            base.AddWeightProperty(list);

            list.Add("<basefont color=#FF9925>Thanks to this sign, you will be promoted to Priest in the temple of God Vecna.<basefont color=#FFFFFF>");
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
