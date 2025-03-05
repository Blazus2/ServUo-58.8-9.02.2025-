using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class VecnaTempleGump : Gump
	{
		public BookofVecna m_Stone;
      		public ArrayList m_List;
      		public int m_ListPage;
     		public ArrayList m_CountList;

		public VecnaTempleGump()
			: base( 0, 0 )
		{

			this.Closable=true;
			this.Disposable=false;
			this.Dragable=false;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 40, 500, 550, 2520);
			this.AddLabel(290, 75, 567, @"Temple of Vecna:");

			AddImage(300, 100, 2329); // Ramka
			AddImage(310, 100, 124); // Baner Vecna
			this.AddButton(140, 340, 9722, 9723, 1, GumpButtonType.Reply, 0);  //Enter your name in the book of followers of God Vecna.
			this.AddButton(140, 370, 9722, 9723, 2, GumpButtonType.Reply, 0);  //Show list of followers.
			this.AddButton(140, 400, 9722, 9723, 3, GumpButtonType.Reply, 0);  //I want to become a priest of God Vecna.
			this.AddButton(140, 430, 9722, 9723, 4, GumpButtonType.Reply, 0);  //I want to become a Necromancer.
			this.AddButton(140, 460, 9722, 9723, 5, GumpButtonType.Reply, 0);  //I want exchange faith points.
			this.AddButton(140, 490, 9722, 9723, 6, GumpButtonType.Reply, 0);  //Give up faith in God Vecna.

			this.AddButton(400, 513, 4502, 4502, 7, GumpButtonType.Reply, 0); //EXIT

			this.AddHtml( 140, 170, 400, 140, @"Vecna is a powerful deity who is often depicted as the embodiment of magic, betrayal, and the desire for immortality. His history is shrouded in darkness, and his impact on the world is both fascinating and terrifying. In many stories, Vecna was once a powerful wizard who sought knowledge and power, transcending the boundaries of life and death.", (bool)true, (bool)true);

                        this.AddLabel(180, 340, 567, @"Enter your name in the book of Followers of God Vecna.");
			AddItem(105, 340, 4032); // pen and ink

                        this.AddLabel(180, 370, 567, @"Show list of Followers.");

                        this.AddLabel(180, 400, 567, @"I want to become a Priest of God Vecna.");
			AddItem(105, 400, 4032); // pen and ink

                        this.AddLabel(180, 430, 567, @"I want to become a Necromancer.");
			AddItem(105, 430, 4032); // pen and ink

                        this.AddLabel(180, 460, 567, @"I want exchange my Faith Points.");

                        this.AddLabel(180, 490, 37, @"Give up faith in God Vecna.");

			this.AddLabel(470, 530, 37, @"EXIT");
		}
		
      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 
			PlayerMobile pm = (PlayerMobile)from;

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
                                from.PlaySound(0x051);

				if ( pm.A9Faith != A9Faith.None )
				{
				from.SendMessage( 0x35, "You must abandon another religion to become a [Follower] of the God Vecna!" );
				}

				if ( pm.A9Faith == A9Faith.None )
				{
                                from.SendMessage( 0x35, "You write your name in the book, you are now a [Follower] of the God Vecna!" );
                                pm.A9Faith = A9Faith.FaithInVecna;
				return;
				}
				if ( pm.A9Faith == A9Faith.FaithInVecna )
				{
				from.SendMessage( 0x35, "You are a [Follower] of the God Vecna!" );
				}
				if ( pm.A9Faith == A9Faith.PriestOfVecna )
				{
				from.SendMessage( 0x35, "You are a [Priest Of Vecna], you cannot change your faith." );
				}
				if ( pm.A9Faith == A9Faith.Necromancer )
				{
				from.SendMessage( 0x35, "You are a [Necromancer], you cannot change your faith." );
				}
			}
        		if ( info.ButtonID == 2 )
         		{
				from.SendGump( new ViewVecnaFollowersGump(m_Stone, m_ListPage, m_List, m_CountList));
                                from.PlaySound(0x051);
			}
        		if ( info.ButtonID == 3 )
         		{
                                from.PlaySound(0x051);

				if ( pm.A9Faith == A9Faith.FaithInVecna )
				{
					Item a = from.Backpack.FindItemByType(typeof(MarkOfPriestGodVecna));
					if (a != null)
					{
					a.Delete();
					from.SendMessage( 0x35, "You gently prick your finger and write your name in the book with blood, you are now a [Priest Of Vecna]!" );
					pm.A9Faith = A9Faith.PriestOfVecna;
					}
                                        if (a == null)
					{
					from.SendMessage( 0x35, "You don't have the [Mark Of Priest]God Vecna!" );
					}
				}
				else
				{
				from.SendMessage( 0x35, "To become a [Priest Of Vecna], you must first become a [Follower] and obtain the [Priest Mark]." );
				}
			}
        		if ( info.ButtonID == 4 )
         		{
                                from.PlaySound(0x051);

				if ( pm.A9Faith == A9Faith.FaithInVecna )
				{
					Item b = from.Backpack.FindItemByType(typeof(ProofofFaithGodVecna));
					if (b != null)
					{
					b.Delete();
					from.SendMessage( 0x35, "You have written your name in the book of Vecna with golden letters, you are now a [Necromancer]!" );
					pm.A9Faith = A9Faith.Necromancer;
					}
                                        if (b == null)
					{
					from.SendMessage( 0x35, "You don't have the [Proof Of Faith] in the God Vecna." );
					}
				}
				else
				{
				from.SendMessage( 0x35, "To become a [Necromancer], bring a [Proof Of Faith] in the God Vecna." );
				}

			}
        		if ( info.ButtonID == 5 )
         		{
				from.CloseGump(typeof(VecnaTempleGump));
				from.SendGump( new FaithPointsVecnaStoreGump());	
                                from.PlaySound(0x051);
			}
        		if ( info.ButtonID == 6 )
         		{
				from.SendGump( new GiveUpVecnaFaithGump(m_Stone, m_ListPage, m_List, m_CountList));
                                from.PlaySound(0x051);
			}
        		if ( info.ButtonID == 7 )
         		{
				from.CloseGump(typeof(VecnaTempleGump));
                                from.PlaySound(0x051);
			}
				
		}
////
	}
} 