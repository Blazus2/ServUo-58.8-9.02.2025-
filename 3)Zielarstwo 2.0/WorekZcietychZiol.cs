using System;
using System.Collections;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class WorekZcietychZiol : Bag
    {
        [Constructable]
        public WorekZcietychZiol()
        {
            Name = "Zciete Ziola";
            Hue = 36;

            DropItem(new Sage(10));
            DropItem(new Acacia(10));
            DropItem(new Anise(10));
            DropItem(new Basil(10));
            DropItem(new Laurel(10));
            DropItem(new Chamomile(10));
            DropItem(new Coriander(10));
            DropItem(new Cinnamon(10));
            DropItem(new Carnation(10));
            DropItem(new Resin(10));
            DropItem(new CorianderCove(10));
            DropItem(new Dill(10));
            DropItem(new DragonBloodHerb(10));
            DropItem(new Olibanum(10));
            DropItem(new Lavender(10));
            DropItem(new Marjoram(10));
            DropItem(new Aconite(10));
            DropItem(new Mint(10));
            DropItem(new CommonMugwort(10));
            DropItem(new Mustard(10));
            DropItem(new Myrrh(10));
            DropItem(new Olive(10));
            DropItem(new Oregano(10));
            DropItem(new Iris(10));
            DropItem(new Patchouli(10));
            DropItem(new Pepper(10));
            DropItem(new WildRose(10));
            DropItem(new Rosemary(10));
            DropItem(new Saffron(10));
            DropItem(new Sandalwood(10));
            DropItem(new SlipperyElm(10));
            DropItem(new Thyme(10));
            DropItem(new Valeriane(10));
            DropItem(new WillowBranches(10));
        }

        public WorekZcietychZiol(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}