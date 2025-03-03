using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Scripts.Commands
{
  public class BloodPool
  {
     public static void Initialize()
     {
        CommandSystem.Register( "Blood", AccessLevel.Player, new CommandEventHandler( BloodPool_OnCommand ) );
     }

     [Usage( "Blood" )]
     [Description( "Pokazuje BloodPool Wampira." )]
     public static void BloodPool_OnCommand( CommandEventArgs e )
     {
        Mobile from = e.Mobile;
	from.CloseGump( typeof( gumppula ) );
        from.SendGump( new gumppula ( from ) );

     }

  }
}

namespace Server.Gumps
{
public class gumppula : Gump
{

 public gumppula(Mobile from) : base(0,0)
 {
               PlayerMobile player = from as PlayerMobile;

  Closable = true;
  Dragable = true;

  AddPage(0);

  AddBackground( 0, 0, /*295*/ 245, 144, 5054);
  AddBackground( 14, 27, /*261*/ 211, 100, 3500);
  AddLabel( 60, 71, player.PulaKrwi < 6 ? 33 : 0, string.Format( "Blood: {0} / 100", player.PulaKrwi));
  AddItem( 14, 68, 3619);
 }

 public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
 {

}
}
}
