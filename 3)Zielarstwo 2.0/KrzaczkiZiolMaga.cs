//KRZACZKI ZIӣ MAGA v 2.0 by PAN POMIDOR
using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{

        public class BushGarlic : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushGarlic() : base( 0x0C68 )
		{
			this.Movable = false;
		        this.Name = "Garlic Bush";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushGarlic(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 1 );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_UsesRemaining = (int)reader.ReadInt(); 
		}
	}

        public class BushGinseng : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushGinseng() : base( Utility.RandomList( 0x18E9, 0x18EA ) ) 
		{
			this.Movable = false;
		        this.Name = "Ginseng Bush";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushGinseng(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 1 );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_UsesRemaining = (int)reader.ReadInt(); 
		}
	}

        public class BushBloodyMoss : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushBloodyMoss() : base( Utility.RandomList( 0x18DF, 0x18E0 ) )
		{
			this.Movable = false;
		        this.Name = "BloodyMoss Bush";
                        this.Weight = 1.0;
		        this.Hue = 0x21;
		}
         
		public BushBloodyMoss(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 1 );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_UsesRemaining = (int)reader.ReadInt(); 
		}
	}

        public class BushMandrake : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushMandrake() : base( Utility.RandomList( 0x18DF, 0x18E0 ) )
		{
			this.Movable = false;
		        this.Name = "Mandrake Bush";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushMandrake(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 1 );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_UsesRemaining = (int)reader.ReadInt(); 
		}
	}

        public class BushNightshade : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushNightshade() : base(Utility.RandomList(0x18E5, 0x18E6))
		{
			this.Movable = false;
		        this.Name = "Nightshade Bush";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushNightshade(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int) 1 );
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			m_UsesRemaining = (int)reader.ReadInt(); 
		}
	}
}
