using System;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
    public class ElvenSkinToneChangeDeed : Item
    {
        public override string DefaultName
        {
            get { return "akt zmieniający odcień skóry"; }
        }

        [Constructable]
        public ElvenSkinToneChangeDeed()
            : base(0x14F0)
        {
            Weight = 1.0;
        }

        public ElvenSkinToneChangeDeed(Serial serial)
            : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {

            base.GetProperties(list);
            list.Add("To pozwoli ci zmienić odcień skóry, użycie tego aktu usunie go samoczynnie, niezależnie od tego, czy zmienisz odcień skóry, czy nie.");

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

        public override void OnDoubleClick(Mobile from)
        {
		PlayerMobile pm = (PlayerMobile)from;
		if ( from.Race == Race.Elf && pm.A1Race1 != A1Race1.Drow )
		{ 
		from.SendGump(new ElvenSkinToneGump(from));
		}
		else
		{
		from.SendMessage( "Nie możesz tego użyć!" );
		}

            this.Delete();
        }
    }
    
}


