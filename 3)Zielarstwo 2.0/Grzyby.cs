//GRZYBY v 1.0 by PAN POMIDOR
using System;

namespace Server.Items
{

////LEKKO TRUJĄCE

        public class StrzępiakCeglasty : Food
	{
		[Constructable]
		public StrzępiakCeglasty() : this( 1 )
		{
		}

		[Constructable]
		public StrzępiakCeglasty( int amount ) : base( 0x0D16 )
		{
			this.Name = "Strzępiak ceglasty";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 25;
                        this.Amount = amount;
                        this.Stackable = true;
                }
  
                public StrzępiakCeglasty( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

        public class MaślankaWiązkowa : Food
	{
		[Constructable]
		public MaślankaWiązkowa() : this( 1 )
		{
		}

		[Constructable]
		public MaślankaWiązkowa( int amount ) : base( 0x0D16 )
		{
			this.Name = "Maślanka wiązkowa";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 348;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public MaślankaWiązkowa( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

        public class Mleczaj : Food
	{
		[Constructable]
		public Mleczaj() : this( 1 )
		{
		}

		[Constructable]
		public Mleczaj( int amount ) : base( 0x0D16 )
		{
			this.Name = "Mleczaj";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 347;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public Mleczaj( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

        public class TęgoskórCytrynowy : Food
	{
		[Constructable]
		public TęgoskórCytrynowy() : this( 1 )
		{
		}

		[Constructable]
		public TęgoskórCytrynowy( int amount ) : base( 0x0D16 )
		{
			this.Name = "Tęgoskór cytrynowy";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 353;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public TęgoskórCytrynowy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

        public class ŻujakBrązowy : Food
	{
		[Constructable]
		public ŻujakBrązowy() : this( 1 )
		{
		}

		[Constructable]
		public ŻujakBrązowy( int amount ) : base( 0x0D16 )
		{
			this.Name = "Żujak brązowy";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 350;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public ŻujakBrązowy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

////LEKKO TRUJĄCE KONIEC

////TRUJĄCE

        public class MuchomorPlamisty : Food
	{
		[Constructable]
		public MuchomorPlamisty() : this( 1 )
		{
		}

		[Constructable]
		public MuchomorPlamisty( int amount ) : base( 0x0D16 )
		{
                        this.Name = "Muchomor plamisty";
                        this.Weight = 1.0;
			this.FillFactor = 1;  
		        this.Poison = Poison.Regular;
                        this.Hue = 20;
                        this.Amount = amount;
                        this.Stackable = true;  
                }

		public MuchomorPlamisty( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

////KONIEC TRUJĄCE

        public class MuchomorJadowity : Food
	{
		[Constructable]
		public MuchomorJadowity() : this( 1 )
		{
		}

		[Constructable]
		public MuchomorJadowity( int amount ) : base( 0x0D16 )
		{
			this.Name = "Muchomor jadowity";
                        this.Weight = 1.0;
			this.FillFactor = 1;  
		        this.Poison = Poison.Greater;
                        this.Hue = 18;
                        this.Amount = amount;
                        this.Stackable = true;
                }

		public MuchomorJadowity( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

        public class MuchomorSromotnikowy : Food
	{
		[Constructable]
		public MuchomorSromotnikowy() : this( 1 )
		{
		}

		[Constructable]
		public MuchomorSromotnikowy( int amount ) : base( 0x0D16 )
		{
			this.Name = "Muchomor sromotnikowy";
                        this.Weight = 1.0;
			this.FillFactor = 1;  
		        this.Poison = Poison.Deadly;
                        this.Hue = 15;
                        this.Amount = amount;
                        this.Stackable = true;   
                }

		public MuchomorSromotnikowy( Serial serial ) : base( serial )
		{
		}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}


}
