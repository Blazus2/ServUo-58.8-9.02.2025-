//KRZACZKI ZIÓ£ v 1.0 by PAN POMIDOR
using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Regions;
using Server.Items;

namespace Server.Items
{

        public class BushWillowBranch : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushWillowBranch() : base( 0x0D3B ) //¿ród³o kory wierzby
		{
			this.Movable = false;
		        this.Name = "Willow branches";
                        this.Weight = 1.0;
		        this.Hue = 1153;
		}
         
		public BushWillowBranch(Serial serial) : base(serial)
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

        public class  BushValeriane : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public  BushValeriane() : base( 0x0CC3 )
		{
			this.Movable = false;
		        this.Name = "Valeriane";
                        this.Weight = 1.0;
		        this.Hue = 0x47E;
		}
         
		public  BushValeriane(Serial serial) : base(serial)
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

        public class  BushThyme : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public  BushThyme() : base( 0x0CBD )
		{
			this.Movable = false;
		        this.Name = "Thyme";
                        this.Weight = 1.0;
		        this.Hue = 1255;
		}
         
		public  BushThyme(Serial serial) : base(serial)
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

        public class BushSlipperyElm : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushSlipperyElm() : base( 0x0CC9 )
		{
			this.Movable = false;
		        this.Name = "Slippery elm";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushSlipperyElm(Serial serial) : base(serial)
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

        public class BushSandalwood : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushSandalwood() : base( 0x0C9B )
		{
			this.Movable = false;
		        this.Name = "Sandalwood";
                        this.Weight = 1.0;
		        this.Hue = 0x59D;
		}
         
		public BushSandalwood(Serial serial) : base(serial)
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
			m_UsesRemaining = (int)reader.ReadInt();;
		}
	}

        public class BushSaffron : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushSaffron() : base( 0x0C89 )
		{
			this.Movable = false;
		        this.Name = "Saffron";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushSaffron(Serial serial) : base(serial)
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

        public class BushRosemary : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushRosemary() : base( 0x0CC6 )
		{
			this.Movable = false;
		        this.Name = "Rosemary";
                        this.Weight = 1.0;
		        this.Hue = 0x594;
		}
         
		public BushRosemary(Serial serial) : base(serial)
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

        public class BushWildRose : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushWildRose() : base( 0x0C85 )
		{
			this.Movable = false;
		        this.Name = "Wild Rose";
                        this.Weight = 1.0;
		        this.Hue = 0x21;
		}
         
		public BushWildRose(Serial serial) : base(serial)
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

        public class BushPepper : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushPepper() : base( 0x0D31 )
		{
			this.Movable = false;
		        this.Name = "Pepper";
                        this.Weight = 1.0;
		        this.Hue = 1036;
		}
         
		public BushPepper(Serial serial) : base(serial)
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

        public class BushPatchouli : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushPatchouli() : base( 0x0CC3 )
		{
			this.Movable = false;
		        this.Name = "Patchouli";
                        this.Weight = 1.0;
		        this.Hue = 0x597;
		}
         
		public BushPatchouli(Serial serial) : base(serial)
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

        public class BushIris : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushIris() : base( 0x0C88 )
		{
			this.Movable = false;
		        this.Name = "Iris";
                        this.Weight = 1.0;
		        this.Hue = 0x416;
		}
         
		public BushIris(Serial serial) : base(serial)
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

        public class BushOregano : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushOregano() : base( 0x0CB4 )
		{
			this.Movable = false;
		        this.Name = "Oregano";
                        this.Weight = 1.0;
		        this.Hue = 78;
		}
         
		public BushOregano(Serial serial) : base(serial)
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

        public class BushOlive : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushOlive() : base( 0x0CC8 )
		{
			this.Movable = false;
		        this.Name = "Olive";
                        this.Weight = 1.0;
		        this.Hue = 0x588;
		}
         
		public BushOlive(Serial serial) : base(serial)
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

        public class BushMyrrh : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushMyrrh() : base( 0x0D40 )
		{
			this.Movable = false;
		        this.Name = "Myrrh";
                        this.Weight = 1.0;
		        this.Hue = 0x415;
		}
         
		public BushMyrrh(Serial serial) : base(serial)
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

        public class BushMustard : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushMustard() : base( 0x0C85 )
		{
			this.Movable = false;
		        this.Name = "Mustard";
                        this.Weight = 1.0;
		        this.Hue = 0x5E2;
		}
         
		public BushMustard(Serial serial) : base(serial)
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

        public class BushCommonMugwort : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCommonMugwort() : base( 0x0CC5 )
		{
			this.Movable = false;
		        this.Name = "Common Mugwort";
                        this.Weight = 1.0;
		        this.Hue = 0x595;
		}
         
		public BushCommonMugwort(Serial serial) : base(serial)
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

        public class BushMint : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushMint() : base( 0x0CAE )
		{
			this.Movable = false;
		        this.Name = "Mint";
                        this.Weight = 1.0;
		        this.Hue = 0x593;
		}
         
		public BushMint(Serial serial) : base(serial)
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

        public class BushAconite : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushAconite() : base( 0x0CC6 )
		{
			this.Movable = false;
		        this.Name = "Aconite";
                        this.Weight = 1.0;
		        this.Hue = 0x585;
		}
         
		public BushAconite(Serial serial) : base(serial)
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

        public class BushMarjoram : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushMarjoram() : base( 0x0C93 )
		{
			this.Movable = false;
		        this.Name = "Marjoram";
                        this.Weight = 1.0;
		        this.Hue = 0x597;
		}
         
		public BushMarjoram(Serial serial) : base(serial)
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

        public class BushLavender : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushLavender() : base( 0x0C87 )
		{
			this.Movable = false;
		        this.Name = "Lavender";
                        this.Weight = 1.0;
		        this.Hue = 0x552;
		}
         
		public BushLavender(Serial serial) : base(serial)
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

        public class BushBranchesOlibanum : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushBranchesOlibanum() : base( 0x0D3D )
		{
			this.Movable = false;
		        this.Name = "Branches";
                        this.Weight = 1.0;
		        this.Hue = 0x5A7;
		}
         
		public BushBranchesOlibanum(Serial serial) : base(serial)
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

        public class BushDragonBlood : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushDragonBlood() : base( 0x0CC5 )
		{
			this.Movable = false;
		        this.Name = "DragonBlood";
                        this.Weight = 1.0;
		        this.Hue = 0x219;
		}
         
		public BushDragonBlood(Serial serial) : base(serial)
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

        public class BushDill : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushDill() : base( 0x0CB5 )
		{
			this.Movable = false;
		        this.Name = "Dill";
                        this.Weight = 1.0;
		        this.Hue = 63;
		}
         
		public BushDill(Serial serial) : base(serial)
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

        public class BushCorianderCove : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCorianderCove() : base( 0x0CAC )
		{
			this.Movable = false;
		        this.Name = "Coriander Cove";
                        this.Weight = 1.0;
		        this.Hue = 0x5E2;
		}
         
		public BushCorianderCove(Serial serial) : base(serial)
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

        public class BushBranchesResinous : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushBranchesResinous() : base( 0x0D3D ) // daja ¿ywicê
		{
			this.Movable = false;
		        this.Name = "Branches";
                        this.Weight = 1.0;
		        this.Hue = 0x1C7;
		}
         
		public BushBranchesResinous(Serial serial) : base(serial)
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

        public class BushCarnation : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCarnation() : base( 0x0C86 )
		{
			this.Movable = false;
		        this.Name = "Carnation";
                        this.Weight = 1.0;
		        this.Hue = 1163;
		}
         
		public BushCarnation(Serial serial) : base(serial)
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

        public class BushCinnamon : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCinnamon() : base( 0x0CC7 )
		{
			this.Movable = false;
		        this.Name = "Cinnamon";
                        this.Weight = 1.0;
		        this.Hue = 0x21A;
		}
         
		public BushCinnamon(Serial serial) : base(serial)
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

        public class BushCoriander : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCoriander() : base( 0x0CAC )
		{
			this.Movable = false;
		        this.Name = "Coriander";
                        this.Weight = 1.0;
		        this.Hue = 0x58B;
		}
         
		public BushCoriander(Serial serial) : base(serial)
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

        public class BushCaraway : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushCaraway() : base( 0x0CB0 )
		{
			this.Movable = false;
		        this.Name = "Caraway";
                        this.Weight = 1.0;
		        this.Hue = 0x5E2;
		}
         
		public BushCaraway(Serial serial) : base(serial)
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

        public class BushChamomile : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushChamomile() : base( 0x0C94 )
		{
			this.Movable = false;
		        this.Name = "Chamomile";
                        this.Weight = 1.0;
		        this.Hue = 0x36;
		}
         
		public BushChamomile(Serial serial) : base(serial)
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

        public class BushLaurel : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushLaurel() : base( 0x0CA7 )
		{
			this.Movable = false;
		        this.Name = "Laurel";
                        this.Weight = 1.0;
		        this.Hue = 0x59C;
		}
         
		public BushLaurel(Serial serial) : base(serial)
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

        public class BushBasil : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushBasil() : base( 0x0C97 )
		{
			this.Movable = false;
		        this.Name = "Basil";
                        this.Weight = 1.0;
		        this.Hue = 0;
		}
         
		public BushBasil(Serial serial) : base(serial)
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

        public class BushAnise : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushAnise() : base( 0x0C8B )
		{
			this.Movable = false;
		        this.Name = "Anise";
                        this.Weight = 1.0;
		        this.Hue = 0x5E2;
		}
         
		public BushAnise(Serial serial) : base(serial)
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

        public class BushAcacia : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushAcacia() : base( 0x0CE9 )
		{
			this.Movable = false;
		        this.Name = "Acacia";
                        this.Weight = 1.0;
		        this.Hue = 155;
		}
         
		public BushAcacia(Serial serial) : base(serial)
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

        public class BushSage : Item
	{
		public int m_UsesRemaining = Utility.RandomMinMax( 2, 6 );

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}

		[Constructable]
		public BushSage() : base( 0x0C84 )
		{
			this.Movable = false;
		        this.Name = "Sage";
                        this.Weight = 1.0;
                        this.Hue = 0;	
		}
  
		public BushSage(Serial serial) : base(serial)
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
