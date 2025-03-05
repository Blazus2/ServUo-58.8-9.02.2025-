using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class GiveUpVecnaFaithGump : Gump
	{
		private BookofVecna m_Stone;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public GiveUpVecnaFaithGump( BookofVecna stone, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
		{
			m_Stone = stone;
         		m_List = list;
         		m_ListPage = listPage;   
         		m_CountList = count;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(32, 27, 267, 65, 5120);
			AddLabel(42, 30, 1149, @"Give up your belief in God Vecna?");
			AddButton(175, 55, 247, 248, 2, GumpButtonType.Reply, 0);
			AddButton(85, 55, 242, 243, 1, GumpButtonType.Reply, 0);


		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
                                from.PlaySound(0x051);
				from.SendGump( new VecnaTempleGump());
			}

        		if ( info.ButtonID == 2 )
         		{
////
                                from.PlaySound(0x051);

				if ( m_List == null )
				{
					ArrayList a = new ArrayList();

					foreach ( Mobile mobile in World.Mobiles.Values )
					{
						if (mobile is PlayerMobile )
						{
							PlayerMobile player = (PlayerMobile)from;
							if ( player.A9Faith == A9Faith.FaithInVecna || player.A9Faith == A9Faith.PriestOfVecna || player.A9Faith == A9Faith.Necromancer )
							{
							player.A9Faith = A9Faith.None;
							player.AB1FaithPoints = 0;
							}

						}
					}

					m_List = a;
				}
////
			}
		}
	}
}