using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class ViewPerolFollowersGump : Gump
	{
		private BookofPerol m_Stone;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public ViewPerolFollowersGump( BookofPerol stone, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
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
			AddBackground(18, 15, 336, 243, 9300);

			if ( m_List == null )
			{
				ArrayList a = new ArrayList();


				foreach ( Mobile mobile in World.Mobiles.Values )
				{
					if (mobile is PlayerMobile)
					{
						PlayerMobile player = (PlayerMobile)mobile;
						if ( player.A9Faith == A9Faith.FaithInPerol || player.A9Faith == A9Faith.PriestOfPerol || player.A9Faith == A9Faith.ClericOfPerol )
						{
						a.Add(mobile);
						}

					}
				}

				m_List = a;
			}

         		if ( listPage > 0 )
			{
				AddButton(30, 223, 4014, 4015, 1, GumpButtonType.Reply, 0);
				AddLabel(66, 223, 1149, @"Last Page");
			}

         		if ( (listPage + 1) * 7 < m_List.Count )
			{
				AddButton(145, 223, 4005, 4006, 2, GumpButtonType.Reply, 0);
				AddLabel(182, 224, 1149, @"Next Page");
			}

         		int k = 0;

         		for ( int i = 0, j = 0, index=((listPage*7)+k) ; i < 7 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index )
         		{
            			Mobile mob = m_List[index] as Mobile;
				
				if ( mob is PlayerMobile )
				{
					PlayerMobile m = (PlayerMobile)mob;
////
					int offset = 30 + ( i * 25 );

                if (m.A9Faith == A9Faith.FaithInPerol)
                {
					AddLabel(35, offset, 1149, m.Name.ToString() + @" [Follower]" );
		}

                if (m.A9Faith == A9Faith.PriestOfPerol)
                {
					AddLabel(35, offset, 32, m.Name.ToString() + @" [Priest]" );
		}

                if (m.A9Faith == A9Faith.ClericOfPerol)
                {
					AddLabel(35, offset, 0x35, m.Name.ToString() + @" [Cleric]" );
		}
////
				}
			}
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 0 ) // Close
         		{
			}

        		if ( info.ButtonID == 1 ) // Previous page
         		{
         			if ( m_ListPage > 0 )
					from.SendGump( new ViewPerolFollowersGump( m_Stone, m_ListPage - 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID == 2 ) // Next page
         		{ 
         			if ( (m_ListPage + 1) * 7 < m_List.Count )
					from.SendGump( new ViewPerolFollowersGump( m_Stone, m_ListPage + 1, m_List, m_CountList ) );
			}
		}
	}
}
