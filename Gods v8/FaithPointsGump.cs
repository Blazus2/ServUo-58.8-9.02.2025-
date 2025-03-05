using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Scripts.Commands
{
  public class FaithPoints
  {
     public static void Initialize()
     {
        CommandSystem.Register( "FP", AccessLevel.Player, new CommandEventHandler( FP_OnCommand ) );
     }

     [Usage( "FP" )]
     [Description( "Pokazuje iloœæ Faith Points." )]
     public static void FP_OnCommand( CommandEventArgs e )
     {
        Mobile from = e.Mobile;
	from.CloseGump( typeof( gumpfp ) );
        from.SendGump( new gumpfp ( from ) );
     }

  }
}

namespace Server.Gumps
{
public class gumpfp : Gump
{

 public gumpfp(Mobile from) : base(0,0)
 {
	PlayerMobile player = from as PlayerMobile;

	Closable = true;
	Dragable = true;

	AddPage(0);
	AddBackground( 0, 0, 245, 144, 5054);
	AddBackground( 14, 27, 211, 100, 3500);
	this.AddLabel( 40, 71, 0x35, string.Format( "Faith Points: {0}", player.AB1FaithPoints));
 }

 public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
 {

}
}
}
