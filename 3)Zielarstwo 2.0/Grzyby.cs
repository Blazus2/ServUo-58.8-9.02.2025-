//GRZYBY v 1.0 by PAN POMIDOR
using System;

namespace Server.Items
{

////LEKKO TRUJ�CE

        public class Strz�piakCeglasty : Food
	{
		[Constructable]
		public Strz�piakCeglasty() : this( 1 )
		{
		}

		[Constructable]
		public Strz�piakCeglasty( int amount ) : base( 0x0D16 )
		{
			this.Name = "Strz�piak ceglasty";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 25;
                        this.Amount = amount;
                        this.Stackable = true;
                }
  
                public Strz�piakCeglasty( Serial serial ) : base( serial )
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

        public class Ma�lankaWi�zkowa : Food
	{
		[Constructable]
		public Ma�lankaWi�zkowa() : this( 1 )
		{
		}

		[Constructable]
		public Ma�lankaWi�zkowa( int amount ) : base( 0x0D16 )
		{
			this.Name = "Ma�lanka wi�zkowa";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 348;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public Ma�lankaWi�zkowa( Serial serial ) : base( serial )
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

        public class T�gosk�rCytrynowy : Food
	{
		[Constructable]
		public T�gosk�rCytrynowy() : this( 1 )
		{
		}

		[Constructable]
		public T�gosk�rCytrynowy( int amount ) : base( 0x0D16 )
		{
			this.Name = "T�gosk�r cytrynowy";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 353;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public T�gosk�rCytrynowy( Serial serial ) : base( serial )
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

        public class �ujakBr�zowy : Food
	{
		[Constructable]
		public �ujakBr�zowy() : this( 1 )
		{
		}

		[Constructable]
		public �ujakBr�zowy( int amount ) : base( 0x0D16 )
		{
			this.Name = "�ujak br�zowy";
                        this.Weight = 1.0;
			this.FillFactor = 1;
		        this.Poison = Poison.Lesser;
                        this.Hue = 350;
                        this.Amount = amount;
                        this.Stackable = true;
                }

                public �ujakBr�zowy( Serial serial ) : base( serial )
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

////LEKKO TRUJ�CE KONIEC

////TRUJ�CE

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

////KONIEC TRUJ�CE

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
