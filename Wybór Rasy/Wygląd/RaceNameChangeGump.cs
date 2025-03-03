using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using System;

namespace Server.Gumps
{
    public class RaceNameChangeGump : Gump
    {
        public void AddBlackAlpha(int x, int y, int width, int height)
        {
            AddImageTiled(x, y, width, height, 2624);
            AddAlphaRegion(x, y, width, height);
        }

        public void AddTextField(int x, int y, int width, int height, int index)
        {
            AddBackground(x - 2, y - 2, width + 4, height + 4, 0x2486);
            AddTextEntry(x + 2, y + 2, width - 4, height - 4, 0, index, "");
        }

        public string Center(string text)
        {
            return string.Format("<CENTER>{0}</CENTER>", text);
        }

        public string Color(string text, int color)
        {
            return string.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
        }

        public void AddButtonLabeled(int x, int y, int buttonID, string text)
        {
            AddButton(x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0);
            AddHtml(x + 35, y, 240, 20, Color(text, 0xFFFFFF), false, false);
        }

        public RaceNameChangeGump()
            : base(50, 50)
        {

            Closable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddBlackAlpha(10, 120, 250, 85);
            AddHtml(10, 125, 250, 20, Color(Center("ZMIANA IMIENIA"), 0xFFFFFF), false, false);

            AddLabel(73, 15, 1152, "");
            AddLabel(20, 150, 0x480, "Nowe Imiê:");
            AddTextField(100, 150, 150, 20, 0);

            AddButtonLabeled(75, 180, 1, "ZATWIERDZ");
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
   		    Mobile m = sender.Mobile;

		    PlayerMobile pm = (PlayerMobile)m;

            if (m == null || m.Deleted )
                return;

            TextRelay nameEntry = info.GetTextEntry(0);

            string newName = (nameEntry == null ? null : nameEntry.Text.Trim());

            if (!NameVerification.Validate(newName, 2, 16, true, true, true, 1, NameVerification.SpaceDashPeriodQuote))
            {
                m.SendMessage(37, "To imiê jest niedozwolone.");
		m.SendGump( new DostosowanieWygl¹duGump());
                return;
            }
            else
            {
                m.RawName = newName;
                m.SendMessage("Twoje imiê zosta³o zmienione!");
                m.SendMessage(string.Format("Nosisz teraz imiê {0}", newName));
		m.SendGump( new DostosowanieWygl¹duGump());
            }
        }
    }
}