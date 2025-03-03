/*This was made by TheArt from ServUO.  This was made to include horns in the hair restyling deed
Do not sell this and if you remake this please give me credit.  Please and thank you.
*/
using System;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
        public class RaceHumanBaseGump : Gump
        {
            /* 
            gump data: bgX, bgY, htmlX, htmlY, imgX, imgY, butX, butY 
            */
            readonly int[][] LayoutArray =
            {
                new int[] { 0 },
                /* padding: its more efficient than code to ++ the index/buttonid */ new int[] { 425, 280, 342, 295, 000, 000, 310, 292 },
                new int[] { 235, 060, 150, 075, 168, 020, 118, 073 },
                new int[] { 235, 115, 150, 130, 168, 070, 118, 128 },
                new int[] { 235, 170, 150, 185, 168, 130, 118, 183 },
                new int[] { 235, 225, 150, 240, 168, 185, 118, 238 },
                new int[] { 425, 060, 342, 075, 358, 018, 310, 073 },
                new int[] { 425, 115, 342, 130, 358, 075, 310, 128 },
                new int[] { 425, 170, 342, 185, 358, 125, 310, 183 },
                new int[] { 425, 225, 342, 240, 358, 185, 310, 238 },
                new int[] { 235, 280, 150, 295, 168, 245, 118, 292 },// slot 10, Curly - N/A for elfs.
///DODANE
                new int[] { 235, 335, 150, 350, 168, 290, 118, 346 },// slot 11, Afro
                new int[] { 235, 390, 150, 405, 168, 345, 118, 401 },// slot 12, Hoko
//Tylko Kobiety
                new int[] { 235, 465, 150, 480, 168, 420, 118, 476 }, // slot 13, DługiKuc
                new int[] { 425, 465, 342, 480, 358, 420, 310, 476 }, // slot 14, Nieład        //prawa strona gumpa
                new int[] { 235, 520, 150, 535, 168, 475, 118, 531 }, // slot 15, Kucyki
                new int[] { 425, 520, 342, 535, 358, 475, 310, 531 }, // slot 16, Szlachcianka  //prawa strona gumpa
                new int[] { 235, 575, 150, 590, 168, 530, 118, 587 }, // slot 17, Loki
                new int[] { 425, 575, 342, 590, 358, 530, 310, 587 } // slot 18, KokNieład     //prawa strona gumpa
            };
            /*
            racial arrays are: cliloc_F, cliloc_M, ItemID_F, ItemID_M, gump_img_F, gump_img_M
            */
            readonly int[][] HumanArray =
            {
                new int[] { 0 },
                new int[] { 1011064, 1011064, 0, 0, 0, 0 }, // bald 
                new int[] { 1011052, 1011052, 0x203B, 0x203B, 0xed1c, 0xC60C }, // Short
                new int[] { 1011053, 1011053, 0x203C, 0x203C, 0xed1d, 0xc60d }, // Long
                new int[] { 1011054, 1011054, 0x203D, 0x203D, 0xed1e, 0xc60e }, // Ponytail
                new int[] { 1011055, 1011055, 0x2044, 0x2044, 0xed27, 0xC60F }, // Mohawk
                new int[] { 1011047, 1011047, 0x2045, 0x2045, 0xED26, 0xED26 }, // Pageboy
                new int[] { 1074393, 1011048, 0x2046, 0x2048, 0xed28, 0xEDE5 }, // Buns, Receding
                new int[] { 1011049, 1011049, 0x2049, 0x2049, 0xede6, 0xede6 }, // 2-tails
                new int[] { 1011050, 1011050, 0x204A, 0x204A, 0xED29, 0xED29 }, // Topknot
                new int[] { 1011396, 1011396, 0x2047, 0x2047, 0xed25, 0xc618 }, // Curly
////DODANE
                new int[] { 1154610, 1154610, 0xA1A5, 0xA1A5, 0xc8f7, 0xc8f7 }, // Afro
                new int[] { 1154610, 1154610, 0xA1A4, 0xA1A4, 0xc8f8, 0xc8f8 }, // Hoko
//Tylko Kobiety
                new int[] { 1154610, 1154610, 0xA1AC, 0, 0xf00f, 0xf00f },  // DługiKuc
                new int[] { 1154610, 1154610, 0xA1AD, 0, 0xf010, 0xf010 },  // Nieład
                new int[] { 1154610, 1154610, 0xA1AE, 0, 0xf011, 0xf011 },  // Kucyki
                new int[] { 1154610, 1154610, 0xA1AF, 0, 0xf012, 0xf012 },  // Szlachcianka
                new int[] { 1154610, 1154610, 0xA1B4, 0, 0xf017, 0xf017 },  // Loki
                new int[] { 1154610, 1154610, 0xA1B5, 0, 0xf018, 0xf018 }  // KokNieład
            };
            
            public RaceHumanBaseGump(Mobile m)
                : base(50, 50)
            {

                this.AddBackground(100, 10, 400, 685, 0xA28);

                this.AddHtmlLocalized(100, 25, 400, 35, 1013008, false, false); // <CENTER>HAIRSTYLE SELECTION MENU</center>

                this.AddHtml(235, 445, 90, 35, "FEMALE ONLY", false, false);//FEMALE ONLY

                this.AddButton(375, 650, 0xFA5, 0xFA7, 0x0, GumpButtonType.Reply, 0); // CANCEL
                this.AddHtmlLocalized(410, 652, 90, 35, 1011012, false, false);// CANCEL

                int[][] RacialData = (m.Race == Race.Human) ? this.HumanArray : this.HumanArray;

                for (int i = 1; i < RacialData.Length; i++)
                {
                    this.AddHtmlLocalized(this.LayoutArray[i][2], this.LayoutArray[i][3], (i == 1) ? 125 : 80, (i == 1) ? 70 : 35, (m.Female) ? RacialData[i][0] : RacialData[i][1], false, false);
                    if (this.LayoutArray[i][4] != 0)
                    {
                        this.AddBackground(this.LayoutArray[i][0], this.LayoutArray[i][1], 50, 50, 0xA3C);
                        this.AddImage(this.LayoutArray[i][4], this.LayoutArray[i][5], (m.Female) ? RacialData[i][4] : RacialData[i][5]);
                    }
                    this.AddButton(this.LayoutArray[i][6], this.LayoutArray[i][7], 0xFA5, 0xFA7, i, GumpButtonType.Reply, 0);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
   		    Mobile m = sender.Mobile;
		    PlayerMobile pm = (PlayerMobile)m;

            if (m == null || m.Deleted )
                return;

                if (info.ButtonID < 1 || info.ButtonID > 18)
                    return;

                int[][] RacialData = (m.Race == Race.Human) ? this.HumanArray : this.HumanArray;

                if (m is PlayerMobile)
                {

                    pm.SetHairMods(-1, -1); // clear any hairmods (disguise kit, incognito)
                    m.HairItemID = (m.Female) ? RacialData[info.ButtonID][2] : RacialData[info.ButtonID][3];
      				m.SendGump( new DostosowanieWygląduGump());
                }
            }
        }
        public class RaceElvenBaseGump : Gump
        {
            /* 
            gump data: bgX, bgY, htmlX, htmlY, imgX, imgY, butX, butY 
            */
            readonly int[][] LayoutArray =
            {
                new int[] { 0 },
                /* padding: its more efficient than code to ++ the index/buttonid */ new int[] { 425, 280, 342, 295, 000, 000, 310, 292 },
                new int[] { 235, 060, 150, 075, 168, 020, 118, 073 },
                new int[] { 235, 115, 150, 130, 168, 070, 118, 128 },
                new int[] { 235, 170, 150, 185, 168, 130, 118, 183 },
                new int[] { 235, 225, 150, 240, 168, 185, 118, 238 },
                new int[] { 425, 060, 342, 075, 358, 018, 310, 073 },
                new int[] { 425, 115, 342, 130, 358, 075, 310, 128 },
                new int[] { 425, 170, 342, 185, 358, 125, 310, 183 },
                new int[] { 425, 225, 342, 240, 358, 185, 310, 238 },
                new int[] { 235, 280, 150, 295, 168, 245, 118, 292 }// slot 10, Curly - N/A for elfs.
            };
            /*
            racial arrays are: cliloc_F, cliloc_M, ItemID_F, ItemID_M, gump_img_F, gump_img_M
            */
            readonly int[][] ElvenArray =
            {
                new int[] { 0 },
                //new int[] { 1011064, 1011064, 0, 0, 0, 0, }, // bald
                new int[] { 1074385, 1074385, 0x2fbf, 0x2fbf, 0xedda, 0xc6e4 }, // mid-long
                new int[] { 1074386, 1074386, 0x2fc0, 0x2fc0, 0xedf5, 0xc6e5 }, // long feather
                new int[] { 1074387, 1074387, 0x2fc1, 0x2fc1, 0xedf6, 0xc6e6 }, // short
                new int[] { 1074388, 1074388, 0x2fc2, 0x2fc2, 0xedf7, 0xc6e7 }, // mullet
                new int[] { 1074391, 1074391, 0x2fce, 0x2fce, 0xeddc, 0xc6cc }, // knob
                new int[] { 1074392, 1074392, 0x2fcf, 0x2fcf, 0xeddd, 0xc6cd }, // braided
                new int[] { 1074394, 1074394, 0x2fd1, 0x2fd1, 0xeddf, 0xc6cf }, // spiked
                new int[] { 1074389, 1074385, 0x2fcc, 0x2fbf, 0xedda, 0xc6e4 }, // flower, mid-long
                new int[] { 1074393, 1074390, 0x2fd0, 0x2fcd, 0xedde, 0xc6cb }// buns, long
            };
            
            public RaceElvenBaseGump(Mobile m)
                : base(50, 50)
            {
                this.AddBackground(100, 10, 400, 385, 0xA28);

                this.AddHtmlLocalized(100, 25, 400, 35, 1013008, false, false);
                this.AddButton(175, 340, 0xFA5, 0xFA7, 0x0, GumpButtonType.Reply, 0); // CANCEL

                this.AddHtmlLocalized(210, 342, 90, 35, 1011012, false, false);// <CENTER>HAIRSTYLE SELECTION MENU</center>

                int[][] RacialData = (m.Race == Race.Elf) ? this.ElvenArray : this.ElvenArray;

                for (int i = 1; i < RacialData.Length; i++)
                {
                    this.AddHtmlLocalized(this.LayoutArray[i][2], this.LayoutArray[i][3], (i == 1) ? 125 : 80, (i == 1) ? 70 : 35, (m.Female) ? RacialData[i][0] : RacialData[i][1], false, false);
                    if (this.LayoutArray[i][4] != 0)
                    {
                        this.AddBackground(this.LayoutArray[i][0], this.LayoutArray[i][1], 50, 50, 0xA3C);
                        this.AddImage(this.LayoutArray[i][4], this.LayoutArray[i][5], (m.Female) ? RacialData[i][4] : RacialData[i][5]);
                    }
                    this.AddButton(this.LayoutArray[i][6], this.LayoutArray[i][7], 0xFA5, 0xFA7, i, GumpButtonType.Reply, 0);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
   		    Mobile m = sender.Mobile;
		    PlayerMobile pm = (PlayerMobile)m;

            if (m == null || m.Deleted )
                return;

                if (info.ButtonID < 1 || info.ButtonID > 10)
                    return;

                int[][] RacialData = (m.Race == Race.Elf) ? this.ElvenArray : this.ElvenArray;

                if (m is PlayerMobile)
                {

                    pm.SetHairMods(-1, -1); // clear any hairmods (disguise kit, incognito)
                    m.HairItemID = (m.Female) ? RacialData[info.ButtonID][2] : RacialData[info.ButtonID][3];
      				m.SendGump( new DostosowanieWygląduGump());
                }
            }
        }
        public class RaceGargoyleBaseGump : Gump
        {
            /* 
            gump data: bgX, bgY, htmlX, htmlY, imgX, imgY, butX, butY 
            */
            readonly int[][] LayoutArray =
            {
                new int[] { 0 },
                /* padding: its more efficient than code to ++ the index/buttonid */ new int[] { 425, 280, 342, 295, 000, 000, 310, 292 },
                new int[] { 235, 060, 150, 075, 168, 020, 118, 073 },
                new int[] { 235, 115, 150, 130, 168, 070, 118, 128 },
                new int[] { 235, 170, 150, 185, 168, 130, 118, 183 },
                new int[] { 235, 225, 150, 240, 168, 185, 118, 238 },
                new int[] { 425, 060, 342, 075, 358, 018, 310, 073 },
                new int[] { 425, 115, 342, 130, 358, 075, 310, 128 },
                new int[] { 425, 170, 342, 185, 358, 125, 310, 183 },
                new int[] { 425, 225, 342, 240, 358, 185, 310, 238 },
                new int[] { 235, 280, 150, 295, 168, 245, 118, 292 }// slot 10, Curly - N/A for elfs.
            };
            /*
            racial arrays are: cliloc_F, cliloc_M, ItemID_F, ItemID_M, gump_img_F, gump_img_M
            */

            readonly int[][] GargoyleArray =
            {
                new int[] { 0 },
                new int[] { 1011064, 1011064, 0, 0, 0, 0, }, // bald
                new int[] { 1112735, 1112735, 0x4258, 0x4258, 0xC533, 0xC533 }, // HeadHorns
                new int[] { 1112735, 1112735, 0x4259, 0x4259, 0xC535, 0xC535 }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425A, 0x425A, 0xC69B, 0xC69B }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425B, 0x425B, 0xC543, 0xC543 }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425C, 0x425C, 0xC608, 0xC608 }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425D, 0x425D, 0xC609, 0xC609 }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425E, 0x425E, 0xC60A, 0xC60A }, // HeadHorns
                new int[] { 1112735, 1112735, 0x425F, 0x425F, 0xC60B, 0xC60B }// HeadHorns
            };
            public RaceGargoyleBaseGump(Mobile m)
                : base(50, 50)
            {
                this.AddBackground(100, 10, 400, 385, 0xA28);

                this.AddHtmlLocalized(100, 25, 400, 35, 1013008, false, false);
                this.AddButton(175, 340, 0xFA5, 0xFA7, 0x0, GumpButtonType.Reply, 0); // CANCEL

                this.AddHtmlLocalized(210, 342, 90, 35, 1011012, false, false);// <CENTER>HAIRSTYLE SELECTION MENU</center>

                int[][] RacialData = (m.Race == Race.Human) ? this.GargoyleArray : this.GargoyleArray;

                for (int i = 1; i < RacialData.Length; i++)
                {
                    this.AddHtmlLocalized(this.LayoutArray[i][2], this.LayoutArray[i][3], (i == 1) ? 125 : 80, (i == 1) ? 70 : 35, (m.Female) ? RacialData[i][0] : RacialData[i][1], false, false);
                    if (this.LayoutArray[i][4] != 0)
                    {
                        this.AddBackground(this.LayoutArray[i][0], this.LayoutArray[i][1], 50, 50, 0xA3C);
                        this.AddImage(this.LayoutArray[i][4], this.LayoutArray[i][5], (m.Female) ? RacialData[i][4] : RacialData[i][5]);
                    }
                    this.AddButton(this.LayoutArray[i][6], this.LayoutArray[i][7], 0xFA5, 0xFA7, i, GumpButtonType.Reply, 0);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
   		    Mobile m = sender.Mobile;
		    PlayerMobile pm = (PlayerMobile)m;

            if (m == null || m.Deleted )
                return;

                if (info.ButtonID < 1 || info.ButtonID > 10)
                    return;

                int[][] RacialData = (m.Race == Race.Human) ? this.GargoyleArray : this.GargoyleArray;

                if (m is PlayerMobile)
                {

                    pm.SetHairMods(-1, -1); // clear any hairmods (disguise kit, incognito)
                    m.HairItemID = (m.Female) ? RacialData[info.ButtonID][2] : RacialData[info.ButtonID][3];
      				m.SendGump( new DostosowanieWygląduGump());
                }
            }
        }
    }