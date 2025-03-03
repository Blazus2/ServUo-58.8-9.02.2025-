using System;
using Server.Accounting;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{

    public class WampirDeathNotice : Gump
    {
        public WampirDeathNotice()
            : base(100, 15)
        {
            this.Closable = false;

            this.AddBackground(25, 10, 425, 444, 0x13BE);

            this.AddImageTiled(33, 20, 407, 425, 0xA40);
            this.AddAlphaRegion(33, 20, 407, 425);

            this.AddHtmlLocalized(190, 24, 120, 20, 1046287, 0x7D00, false, false); // You have died.


	this.AddHtml( 50, 50, 380, 40, String.Format("<basefont color = #FFFFFF>As a vampire you do not die, but you are now very weakened.</basefont>"), false, false );
	this.AddHtml(50, 100, 380, 45, String.Format("<basefont color = #FFFFFF>Your will leads you to the Vampire Crypt. Do you wish to go there?</basefont>"), false, false);

			this.AddLabel(160, 158, 33, @"MOVE");
			this.AddLabel(160, 218, 33, @"GIVE UP");

                        this.AddButton(100, 140, 4502, 4502, (int)Buttons.Button1, GumpButtonType.Reply, 0); // ZATWIERDZ
 			this.AddButton(100, 200, 4502, 4502, (int)Buttons.Button2, GumpButtonType.Reply, 0); // ZREZYGNUJ
        }

		public enum Buttons
		{
			Close,
			Button1,
			Button2,
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
     
			if (from == null)
				return;

		    PlayerMobile pm = (PlayerMobile)from;

			switch ( info.ButtonID )
			{
				case 0:
				{
				pm.SendMessage( "" );
				break;
				}

				case 1: 
				{						            
				pm.MoveToWorld(new Point3D(5438, 229, 0), Map.Felucca); //Wampirze-Krypty
				break;
				}
				
				case 2: 
				{
				pm.SendMessage(33, "You cancelled your trip to the Vampire Crypts." );
				break;
				}
			}
        }
    }
}