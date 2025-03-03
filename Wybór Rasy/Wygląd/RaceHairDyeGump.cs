using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
    public class RaceHairDyeGump : Gump
    {
        private static readonly HairDyeEntry[] m_Entries = new HairDyeEntry[]
        {
            new HairDyeEntry("*****", 1602, 26),
            new HairDyeEntry("*****", 1628, 27),
            new HairDyeEntry("*****", 1502, 32),
            new HairDyeEntry("*****", 1302, 32),
            new HairDyeEntry("*****", 1402, 32),
            new HairDyeEntry("*****", 1202, 24),
            new HairDyeEntry("*****", 2402, 29),
            new HairDyeEntry("*****", 2213, 6),
            new HairDyeEntry("*****", 1102, 8),
            new HairDyeEntry("*****", 1110, 8),
            new HairDyeEntry("*****", 1118, 16),
            new HairDyeEntry("*****", 1134, 16)
        };
        public RaceHairDyeGump(Mobile m)
            : base(50, 50)
        {
   
            AddPage(0);

            AddBackground(100, 10, 350, 355, 2600);
            AddBackground(120, 54, 110, 270, 5100);

            AddHtmlLocalized(70, 25, 400, 35, 1011013, false, false); // <center>Hair Color Selection Menu</center>

            AddButton(149, 328, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(185, 329, 250, 35, 1011014, false, false); // Dye my hair this color!

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                AddLabel(130, 59 + (i * 22), m_Entries[i].HueStart - 1, m_Entries[i].Name);
                AddButton(207, 60 + (i * 22), 5224, 5224, 0, GumpButtonType.Page, i + 1);
            }

            for (int i = 0; i < m_Entries.Length; ++i)
            {
                HairDyeEntry e = m_Entries[i];

                AddPage(i + 1);

                for (int j = 0; j < e.HueCount; ++j)
                {
                    AddLabel(278 + ((j / 16) * 80), 52 + ((j % 16) * 17), e.HueStart + j - 1, "*****");
                    AddRadio(260 + ((j / 16) * 80), 52 + ((j % 16) * 17), 210, 211, false, (i * 100) + j);
                }
            }
        }

        public override void OnResponse(NetState from, RelayInfo info)
        {

            Mobile m = from.Mobile;
            int[] switches = info.Switches;

            if (info.ButtonID != 0 && switches.Length > 0)
            {
                if (m.HairItemID == 0 && m.FacialHairItemID == 0)
                {
                    m.SendLocalizedMessage(502623);	// You have no hair to dye and cannot use this
                }
                else
                {
                    // To prevent this from being exploited, the hue is abstracted into an internal list
                    int entryIndex = switches[0] / 100;
                    int hueOffset = switches[0] % 100;

                    if (entryIndex >= 0 && entryIndex < m_Entries.Length)
                    {
                        HairDyeEntry e = m_Entries[entryIndex];

                        if (hueOffset >= 0 && hueOffset < e.HueCount)
                        {
                            int hue = e.HueStart + hueOffset;

                            m.HairHue = hue;
                            m.FacialHairHue = hue;

                            m.SendLocalizedMessage(501199);  // You dye your hair
                            m.PlaySound(0x4E);
      				m.SendGump( new DostosowanieWygl¹duGump());
                        }
                    }
                }
            }
            else
            {
                m.SendLocalizedMessage(501200); // You decide not to dye your hair
      				m.SendGump( new DostosowanieWygl¹duGump());
            }
        }

        private class HairDyeEntry
        {
            private readonly string m_Name;
            private readonly int m_HueStart;
            private readonly int m_HueCount;
            public HairDyeEntry(string name, int hueStart, int hueCount)
            {
                m_Name = name;
                m_HueStart = hueStart;
                m_HueCount = hueCount;
            }

            public string Name => m_Name;
            public int HueStart => m_HueStart;
            public int HueCount => m_HueCount;
        }
    }
}