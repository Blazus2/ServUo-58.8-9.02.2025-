////Created by Pan Pomidor////
using System;
using Server.Targeting;
using Server.Gumps;
//
using Server.Network;
//

namespace Server.Items
{
    public class Wool : Item, IDyable, ICommodity
    {
        [Constructable]
        public Wool()
            : this(1)
        {
        }

        [Constructable]
        public Wool(int amount)
            : base(0xDF8)
        {
            this.Stackable = true;
            this.Weight = 4.0;
            this.Amount = amount;
        }

        public Wool(Serial serial)
            : base(serial)
        {
        }

        TextDefinition ICommodity.Description { get { return LabelNumber; } }
        bool ICommodity.IsDeedable { get { return true; } }

        public static void OnSpun(ISpinningWheel wheel, Mobile from, int hue, int amount)
        {
            Item item = new DarkYarn(amount);
            item.Hue = hue;

            from.AddToBackpack(item);
            from.SendLocalizedMessage(1010576); // You put the balls of yarn in your backpack.
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

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (this.Deleted)
                return false;

            this.Hue = sender.DyedHue;

            return true;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (this.IsChildOf(from.Backpack))
            {
                // Check if the gump is already open
                if (from.HasGump(typeof(WoolAmountGump)))
                {
                    // Close the gump
                    from.CloseGump(typeof(WoolAmountGump));
                }
                else
                {
                    // Open gump to enter the amount of wool
                    from.SendGump(new WoolAmountGump(from, this));
                }
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        private class PickWheelTarget : Target
        {
            private readonly Wool m_Wool;
            private int m_AmountToSpin;

            public PickWheelTarget(Wool wool, int amountToSpin)
                : base(3, false, TargetFlags.None)
            {
                this.m_Wool = wool;
                this.m_AmountToSpin = amountToSpin;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (this.m_Wool.Deleted)
                    return;

                ISpinningWheel wheel = targeted as ISpinningWheel;

                if (wheel == null && targeted is AddonComponent)
                    wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

                if (wheel is Item)
                {
                    Item item = (Item)wheel;

                    if (!this.m_Wool.IsChildOf(from.Backpack))
                    {
                        from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                    }
                    else if (wheel.Spinning)
                    {
                        from.SendLocalizedMessage(502656); // That spinning wheel is being used.
                    }
                    else
                    {
                        int totalAmount = this.m_AmountToSpin; // Use the amount of processed wool
                        this.m_Wool.Consume(totalAmount);
                        wheel.BeginSpin(new SpinCallback((w, m, h) => Wool.OnSpun(w, m, h, totalAmount)), from, this.m_Wool.Hue);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(502658); // Use that on a spinning wheel.
                }
            }
        }

        public void ProcessWool(Mobile from, int amount)
        {
            if (amount > this.Amount)
            {
                from.SendMessage("You do not have enough wool.");
                return;
            }

            from.Target = new PickWheelTarget(this, amount);
        }
    }

    public class WoolAmountGump : Gump
    {
        private Wool m_Wool;
        private Mobile m_From;

        public WoolAmountGump(Mobile from, Wool wool)
            : base(50, 50)
        {
            m_From = from;
            m_Wool = wool;

            AddPage(0);
            AddBackground(0, 0, 250, 150, 5054);
            AddLabel(50, 20, 1152, "Amount of wool to process:");

            // Add a frame around the text entry box
            AddBackground(50 - 2, 50 - 2, 150 + 4, 30 + 4, 9200);
            AddTextEntry(50, 50, 150, 30, 0, 0, "0");

            // Adjusted button positions
            AddButton(70, 100, 4005, 4007, 1, GumpButtonType.Reply, 0); // "OK" button
            AddButton(150, 100, 4017, 4019, 0, GumpButtonType.Reply, 0); // "Cancel" button
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 1) // If "OK" was pressed
            {
                TextRelay textEntry = info.GetTextEntry(0);
                if (textEntry != null)
                {
                    int amount;
                    if (int.TryParse(textEntry.Text, out amount) && amount > 0)
                    {
                        m_Wool.ProcessWool(m_From, amount);
                    }
                    else
                    {
                        m_From.SendMessage("Enter a valid amount.");
                    }
                }
            }
        }
    }

    public class TaintedWool : Wool
    {
        [Constructable]
        public TaintedWool()
            : this(1)
        {
        }

        [Constructable]
        public TaintedWool(int amount)
            : base(0x101F)
        {
            this.Stackable = true;
            this.Weight = 4.0;
            this.Amount = amount;
        }

        public TaintedWool(Serial serial)
            : base(serial)
        {
        }

        new public static void OnSpun(ISpinningWheel wheel, Mobile from, int hue, int amount)
        {
            Item item = new DarkYarn(amount);
            item.Hue = hue;

            from.AddToBackpack(item);
            from.SendLocalizedMessage(1010574); // You put a ball of yarn in your backpack.
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
