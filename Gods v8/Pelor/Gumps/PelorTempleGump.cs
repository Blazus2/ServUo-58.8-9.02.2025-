using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
    public class PelorTempleGump : Gump
    {
        public BookofPerol m_Stone;
        public ArrayList m_List;
        public int m_ListPage;
        public ArrayList m_CountList;

        public PelorTempleGump() : base(0, 0)
        {
            Closable = true;
            Disposable = false;
            Dragable = false;
            Resizable = false;

            AddPage(0);
            AddBackground(100, 40, 500, 550, 2520);
            AddLabel(290, 75, 567, "Temple of Pelor:");

            AddImage(300, 100, 2329); // Frame
            AddImage(310, 100, 123); // Pelor Banner
            AddButton(140, 340, 9722, 9723, 1, GumpButtonType.Reply, 0);  // Enter your name in the book of followers of God Perol.
            AddButton(140, 370, 9722, 9723, 2, GumpButtonType.Reply, 0);  // Show list of followers.
            AddButton(140, 400, 9722, 9723, 3, GumpButtonType.Reply, 0);  // I want to become a priest of God Perol.
            AddButton(140, 430, 9722, 9723, 4, GumpButtonType.Reply, 0);  // I want to become a Cleric.
            AddButton(140, 460, 9722, 9723, 5, GumpButtonType.Reply, 0);  // I want exchange faith points.
            AddButton(140, 490, 9722, 9723, 6, GumpButtonType.Reply, 0);  // Give up faith in God Perol.
            AddButton(400, 513, 4502, 4502, 7, GumpButtonType.Reply, 0);  // EXIT

            AddHtml(140, 170, 400, 140, "Pelor is the god of the sun, light and healing. A person who believes in God Perol is guided by values such as love, peace, harmony and respect for nature. He be...", false, false);

            AddLabel(180, 340, 567, "Enter your name in the book of Followers of God Perol.");
            AddItem(105, 340, 4032); // pen and ink

            AddLabel(180, 370, 567, "Show list of Followers.");

            AddLabel(180, 400, 567, "I want to become a Priest of God Perol.");
            AddItem(105, 400, 4032); // pen and ink

            AddLabel(180, 430, 567, "I want to become a Cleric.");
            AddItem(105, 430, 4032); // pen and ink

            AddLabel(180, 460, 567, "I want exchange my Faith Points.");

            AddLabel(180, 490, 37, "Give up faith in God Perol.");

            AddLabel(470, 530, 37, "EXIT");
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;
            if (from == null) return;

            var pm = from as PlayerMobile;
            if (pm == null) return;

            switch (info.ButtonID)
            {
                case 1:
                    HandleFollowerButton(from, pm);
                    break;
                case 2:
                    from.SendGump(new ViewPerolFollowersGump(m_Stone, m_ListPage, m_List, m_CountList));
                    from.PlaySound(0x051);
                    break;
                case 3:
                    HandlePriestButton(from, pm);
                    break;
                case 4:
                    HandleClericButton(from, pm);
                    break;
                case 5:
                    from.CloseGump(typeof(PelorTempleGump));
                    from.SendGump(new FaithPointsPelorStoreGump());
                    from.PlaySound(0x051);
                    break;
                case 6:
                    from.SendGump(new GiveUpPerolFaithGump(m_Stone, m_ListPage, m_List, m_CountList));
                    from.PlaySound(0x051);
                    break;
                case 7:
                    from.CloseGump(typeof(PelorTempleGump));
                    from.PlaySound(0x051);
                    break;
            }
        }

        private void HandleFollowerButton(Mobile from, PlayerMobile pm)
        {
            from.PlaySound(0x051);

            switch (pm.A9Faith)
            {
                case A9Faith.None:
                    from.SendMessage(0x35, "You write your name in the book, you are now a [Follower] of the God Pelor!");
                    pm.A9Faith = A9Faith.FaithInPerol;
                    break;
                case A9Faith.FaithInPerol:
                    from.SendMessage(0x35, "You are a [Follower] of the God Pelor!");
                    break;
                case A9Faith.PriestOfPerol:
                    from.SendMessage(0x35, "You are a [Priest Of Perol], you cannot change your faith.");
                    break;
                case A9Faith.ClericOfPerol:
                    from.SendMessage(0x35, "You are a [Cleric Of Perol], you cannot change your faith.");
                    break;
                default:
                    from.SendMessage(0x35, "You must abandon another religion to become a [Follower] of the God Pelor!");
                    break;
            }
        }

        private void HandlePriestButton(Mobile from, PlayerMobile pm)
        {
            from.PlaySound(0x051);

            if (pm.A9Faith == A9Faith.FaithInPerol)
            {
                Item mark = from.Backpack.FindItemByType(typeof(MarkOfPriestGodPerol));
                if (mark != null)
                {
                    mark.Delete();
                    from.SendMessage(0x35, "You gently prick your finger and write your name in the book with blood, you are now a [Priest Of Perol]!");
                    pm.A9Faith = A9Faith.PriestOfPerol;
                }
                else
                {
                    from.SendMessage(0x35, "You don't have the [Mark Of Priest] God Perol!");
                }
            }
            else
            {
                from.SendMessage(0x35, "To become a [Priest Of Perol], you must first become a [Follower] and obtain the [Priest Mark].");
            }
        }

        private void HandleClericButton(Mobile from, PlayerMobile pm)
        {
            from.PlaySound(0x051);

            if (pm.A9Faith == A9Faith.FaithInPerol)
            {
                Item proof = from.Backpack.FindItemByType(typeof(ProofofFaithGodPerol));
                if (proof != null)
                {
                    proof.Delete();
                    from.SendMessage(0x35, "You have written your name in the book of Perol with golden letters, you are now a [Cleric Of Perol]!");
                    pm.A9Faith = A9Faith.ClericOfPerol;
                }
                else
                {
                    from.SendMessage(0x35, "You don't have the [Proof Of Faith] in the God Perol.");
                }
            }
            else
            {
                from.SendMessage(0x35, "To become a [Cleric], bring a [Proof Of Faith] in the God Perol.");
            }
        }
    }
}