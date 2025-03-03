﻿
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class ElvenSkinToneGump : Gump
    {
        ///////////////////////////////////////////////////////////
        //  _____ ______ _______ _______ _____ _   _  _____  _____ 
        // / ____|  ____|__   __|__   __|_   _| \ | |/ ____|/ ____|
        //| (___ | |__     | |     | |    | | |  \| | |  __| (___  
        // \___ \|  __|    | |     | |    | | | . ` | | |_ |\___ \ 
        // ____) | |____   | |     | |   _| |_| |\  | |__| |____) |
        //|_____/|______|  |_|     |_|  |_____|_| \_|\_____|_____/ 
        ///////////////////////////////////////////////////////////

        private static int[] hues = { 0x4DE, 0x76C, 0x835, 0x24D, 0x24E, 0x24F, 0x0BF, 0x353, 0x361, 0x367, 0x374, 0x375, 0x903 }; //Try all the hues you want to use here, can be as little or many as you want, all seperated by a comma. The gump will only show a max of 14 though.
        private static bool Allow_Preview = true; //Set to false if you don't want to let players preview colors.
        private static string Skin_Tone_Message = @"Odcień Twojej skóry został tymczasowo zmieniony i za kilka sekund powróci do starego odcienia.";
        private static string Top_Right_Message = @"Tutaj możesz zmienić odcień skóry, to nie będzie tymczasowe, więc wybieraj mądrze! Za pomocą przycisku [SPRAWDZ] można na chwile  sprawdzić czy wybrany odcień pasuje naszym oczekiwaniom.";
        private static int Start_X = 25, Start_Y = 30;

        //End Settings, do not edit below unless you know what you're doing!


        private Mobile caller;
        private int Last_Selected = 0;

        public static void Initialize()
        {
            CommandSystem.Register("ElvenSkinChange", AccessLevel.Counselor, new CommandEventHandler(ElvenSkinChange_OnCommand));
        }

        [Usage("")]
        [Description("Command used for testing only..")]
        public static void ElvenSkinChange_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile != null)
            {
                if (e.Mobile.HasGump(typeof(ElvenSkinToneGump)))
                    e.Mobile.CloseGump(typeof(ElvenSkinToneGump));
                e.Mobile.SendGump(new ElvenSkinToneGump(e.Mobile));
            }
        }

        public ElvenSkinToneGump(Mobile from, int lastselected)
            : base(Start_X, Start_Y)
        {
            PokazElven_Gump(from, lastselected);
        }

        public ElvenSkinToneGump(Mobile from)
            : base(Start_X, Start_Y)
        {
            PokazElven_Gump(from, Last_Selected);
        }


        private void PokazElven_Gump(Mobile from, int lastselected)
        {
            try
            {
                Last_Selected = lastselected;
                caller = from;
                if (caller == null)
                {
                    ConsoleWrite(ConsoleColor.DarkRed, "ElvenSkinChangeGump: Error 1.");
                    return;
                }
                this.Closable = true;
                this.Disposable = true;
                this.Dragable = true;
                this.Resizable = false;

                AddPage(0);
                AddBackground(0, 1, 472, 572, 9300);
                AddPage(1);

                Show_Top_Right_Message();

                AddButton(320, 548, 2445, 2445, 1000, GumpButtonType.Reply, 1000);//Apply
                AddLabel(355, 549, 42, "WYBÓR");

                if (Allow_Preview) AddButton(200, 548, 2445, 2445, 1001, GumpButtonType.Reply, 1001);//Preview
                if (Allow_Preview) AddLabel(225, 549, 42, "SPRAWDZ");

                int max_height = 7, max_width = 7;

                for (int ii = 0; ii < max_width; ii++)
                    for (int i = 0; i + (ii * max_height) < hues.Length && i < max_height; i++)
                    {
                        AddRadio(10 + (ii * 75), 260 + (i * 40), 2151, 2154, i + (ii * max_height) == Last_Selected ? true : false, (i) + (ii * max_height));
                        AddImage(43 + (ii * 75), 260 + (i * 40), 1227, hues[(i) + (ii * max_height)]);
                    }

            }
            catch (Exception e) { ConsoleWrite(ConsoleColor.DarkRed, "ElvenSkinChangeGump: Error 3: [" + e.Message + "]"); }
        }

        private void Show_Top_Right_Message()
        {
            int Max_Length = 65; //długość tekstu
            int Line_Height = 20; // wysokość linia od lini
            int tempnum, tempnum2;

            for (int i = 0; i <= (Top_Right_Message.Length / Max_Length); i++)
            {
                tempnum = i * Max_Length;
                tempnum2 = Top_Right_Message.Length - tempnum;
                if (tempnum < Top_Right_Message.Length) AddLabel(20, 100 + (Line_Height * i), 42, Top_Right_Message.Substring(tempnum, (tempnum + Max_Length) > Top_Right_Message.Length ? tempnum2 : Max_Length));
            }
                  this.AddLabel(190, 20, 567, "KOLORY ELFA");
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            try
            {
                Mobile from = sender.Mobile;

                switch (info.ButtonID)
                {
                    case 0:
                        {
                            if (from.HasGump(typeof(ElvenSkinToneGump)))
                                from.CloseGump(typeof(ElvenSkinToneGump));
                            return;
                        }
                    case 1000:
                        {
                            for (int i = 0; i < hues.Length; i++)
                                if (info.IsSwitched(i))
                                {
                                    from.Hue = hues[i];
                                    break;
                                }

				from.SendGump( new DostosowanieWygląduGump());

                            break;
                        }
                    case 1001:
                        {
                            if (Allow_Preview)
                                for (int i = 0; i < hues.Length; i++)
                                    if (info.IsSwitched(i))
                                    {
                                        new PreviewColorElven(hues[i], from, i);
                                        from.SendMessage(38, Skin_Tone_Message);
                                        break;
                                    }
                            break;
                        }

                }

            }
            catch (Exception e) { ConsoleWrite(ConsoleColor.DarkRed, "ElvenSkinChangeGump: Error 4: [" + e.Message + "]"); }
        }

        private void ConsoleWrite(ConsoleColor c, string data)
        {
            ConsoleColor o = Console.ForegroundColor;
            Console.ForegroundColor = c;
            Console.WriteLine(data);
            Console.ForegroundColor = o;
        }
    }

    public class PreviewColorElven
    {
        private int oldhue, PAGE;
        private Mobile from;

        public PreviewColorElven(int hue, Mobile who, int page)
        {
            from = who;
            oldhue = who.Hue;
            who.Hue = hue;
            PAGE = page;
            Start_Timer(TimeSpan.FromSeconds(5));
        }

        public void Start_Timer(TimeSpan s)
        {
            Server.Timer.DelayCall(s, new Server.TimerCallback(BackToNormal));
        }

        public void BackToNormal()
        {
            from.Hue = oldhue;
            if (from.HasGump(typeof(ElvenSkinToneGump)))
                from.CloseGump(typeof(ElvenSkinToneGump));
            //from.SendGump(new ElvenSkinToneGump(from, PAGE));
        }
    }
}