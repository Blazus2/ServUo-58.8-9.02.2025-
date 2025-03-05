using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Targeting;
using System.Collections;
using Server.Misc;
using Server.Commands;
using System.Collections.Generic;
using Server.Network;
using Server.Multis;

namespace Server.Items 
{ 
   	public class BookofPerol : Item 
   	{ 

      		[Constructable] 
      		public BookofPerol() : base( 0xA5D1 ) 
      		{ 
         		Weight = 1.0;  
         		Movable = false;
			Hue = 0;
         		Name="Book of Perol"; 
          	} 

      		public override void OnDoubleClick( Mobile from ) 
     	 	{ 

			if( from.InRange( this.GetWorldLocation(), 3 ) ) 
		        {
			    PlayerMobile pm = (PlayerMobile)from;
     				from.SendGump( new PelorTempleGump());
		        } 
		        else 
		        { 
		            from.SendLocalizedMessage( 500446 ); // That is too far away. 
		        }

      		} 

      		public BookofPerol( Serial serial ) : base( serial ) 
      		{  
      		} 

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	} 
}