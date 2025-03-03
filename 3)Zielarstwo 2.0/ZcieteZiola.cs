//ZCIÊTE ZIO£A v 2.0 by PAN POMIDOR
using System;

namespace Server.Items
{
	public class BaseHerb : Item
	{
		[Constructable]
		public BaseHerb( int amount, int itemID ) : base( itemID )
		{
            Weight = 0.1;
            Stackable = true;
            Amount = amount;
            Movable = true;
		}

		public BaseHerb( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Sage : BaseHerb
	{
		[Constructable]
		public Sage() : this( 1 ) { }

		[Constructable]
		public Sage( int amount ) : base( amount, 0xC3D )
		{
            Name = "Sage";
		}

		public Sage( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Acacia : BaseHerb
	{
		[Constructable]
		public Acacia() : this( 1 ) { }

		[Constructable]
		public Acacia( int amount ) : base( amount, 0xDE1 )
		{
            Name = "Acacia";
		}

		public Acacia( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Anise : BaseHerb
	{
		[Constructable]
		public Anise() : this( 1 ) { }

		[Constructable]
		public Anise( int amount ) : base( amount, 0xF2C )
		{
            Name = "Anise";
            Hue = 0x5E2;
		}

		public Anise( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Basil : BaseHerb
	{
		[Constructable]
		public Basil() : this( 1 ) { }

		[Constructable]
		public Basil( int amount ) : base( amount, 0xC3E )
		{
            Name = "Basil";
		}

		public Basil( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Laurel : BaseHerb
	{
		[Constructable]
		public Laurel() : this( 1 ) { }

		[Constructable]
		public Laurel( int amount ) : base( amount, 0xF78 )
		{
            Name = "Laurel";
            Hue = 0x59C;
		}

		public Laurel( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Chamomile : BaseHerb
	{
		[Constructable]
		public Chamomile() : this( 1 ) { }

		[Constructable]
		public Chamomile( int amount ) : base( amount, 0xF8D )
		{
            Name = "Chamomile";
            Hue = 0x36;
		}

		public Chamomile( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Caraway : BaseHerb
	{
		[Constructable]
		public Caraway() : this( 1 ) { }

		[Constructable]
		public Caraway( int amount ) : base( amount, 0xF29 )
		{
            Name = "Caraway";
            Hue = 0x5E2;
		}

		public Caraway( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Coriander : BaseHerb
	{
		[Constructable]
		public Coriander() : this( 1 ) { }

		[Constructable]
		public Coriander( int amount ) : base( amount, 0x1020 )
		{
            Name = "Coriander";
            Hue = 0x58B;
		}

		public Coriander( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Cinnamon : BaseHerb
	{
		[Constructable]
		public Cinnamon() : this( 1 ) { }

		[Constructable]
		public Cinnamon( int amount ) : base( amount, 0xF80 )
		{
            Name = "Cinnamon";
            Hue = 0x21A;
		}

		public Cinnamon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Carnation : BaseHerb
	{
		[Constructable]
		public Carnation() : this( 1 ) { }

		[Constructable]
		public Carnation( int amount ) : base( amount, 0xF8E )
		{
            Name = "Carnation";
            Hue = 0x39A;
		}

		public Carnation( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Resin : BaseHerb
	{
		[Constructable]
		public Resin() : this( 1 ) { }

		[Constructable]
		public Resin( int amount ) : base( amount, 0xF21 )
		{
            Name = "Resin";
            Hue = 0x1C7;
		}

		public Resin( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CorianderCove : BaseHerb
	{
		[Constructable]
		public CorianderCove() : this( 1 ) { }

		[Constructable]
		public CorianderCove( int amount ) : base( amount, 0xF15 )
		{
            Name = "Coriander Cove";
            Hue = 0x5E2;
		}

		public CorianderCove( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Dill : BaseHerb
	{
		[Constructable]
		public Dill() : this( 1 ) { }

		[Constructable]
		public Dill( int amount ) : base( amount, 0xF1B )
		{
            Name = "Dill";
            Hue = 0x1D7;
		}

		public Dill( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class DragonBloodHerb : BaseHerb
	{
		[Constructable]
		public DragonBloodHerb() : this( 1 ) { }

		[Constructable]
		public DragonBloodHerb( int amount ) : base( amount, 0xF8F )
		{
            Name = "Dragon Blood Herb";
            Hue = 0x219;
		}

		public DragonBloodHerb( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Olibanum : BaseHerb
	{
		[Constructable]
		public Olibanum() : this( 1 ) { }

		[Constructable]
		public Olibanum( int amount ) : base( amount, 0xF91 )
		{
            Name = "Olibanum";
            Hue = 0x5A7;
		}

		public Olibanum( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Lavender : BaseHerb
	{
		[Constructable]
		public Lavender() : this( 1 ) { }

		[Constructable]
		public Lavender( int amount ) : base( amount, 0xC3B )
		{
            Name = "Lavender";
            Hue = 0x552;
		}

		public Lavender( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Marjoram : BaseHerb
	{
		[Constructable]
		public Marjoram() : this( 1 ) { }

		[Constructable]
		public Marjoram( int amount ) : base( amount, 0xC3E )
		{
            Name = "Marjoram";
            Hue = 0x597;
		}

		public Marjoram( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Aconite : BaseHerb
	{
		[Constructable]
		public Aconite() : this( 1 ) { }

		[Constructable]
		public Aconite( int amount ) : base( amount, 0xF88 )
		{
            Name = "Aconite";
            Hue = 0x585;
		}

		public Aconite( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Mint : BaseHerb
	{
		[Constructable]
		public Mint() : this( 1 ) { }

		[Constructable]
		public Mint( int amount ) : base( amount, 0xC41 )
		{
            Name = "Mint";
            Hue = 0x593;
		}

		public Mint( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CommonMugwort : BaseHerb
	{
		[Constructable]
		public CommonMugwort() : this( 1 ) { }

		[Constructable]
		public CommonMugwort( int amount ) : base( amount, 0xC42 )
		{
            Name = "Common Mugwort";
            Hue = 0x595;
		}

		public CommonMugwort( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Mustard : BaseHerb
	{
		[Constructable]
		public Mustard() : this( 1 ) { }

		[Constructable]
		public Mustard( int amount ) : base( amount, 0xF2C )
		{
            Name = "Mustard";
            Hue = 0x5E2;
		}

		public Mustard( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Myrrh : BaseHerb
	{
		[Constructable]
		public Myrrh() : this( 1 ) { }

		[Constructable]
		public Myrrh( int amount ) : base( amount, 0xF7B )
		{
            Name = "Myrrh";
            Hue = 0x415;
		}

		public Myrrh( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Olive : BaseHerb
	{
		[Constructable]
		public Olive() : this( 1 ) { }

		[Constructable]
		public Olive( int amount ) : base( amount, 0xF8D )
		{
            Name = "Olive";
            Hue = 0x588;
		}

		public Olive( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Oregano : BaseHerb
	{
		[Constructable]
		public Oregano() : this( 1 ) { }

		[Constructable]
		public Oregano( int amount ) : base( amount, 0xC3D )
		{
            Name = "Oregano";
		}

		public Oregano( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Iris : BaseHerb
	{
		[Constructable]
		public Iris() : this( 1 ) { }

		[Constructable]
		public Iris( int amount ) : base( amount, 0xF85 )
		{
            Name = "Iris";
            Hue = 0x416;
		}

		public Iris( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Patchouli : BaseHerb
	{
		[Constructable]
		public Patchouli() : this( 1 ) { }

		[Constructable]
		public Patchouli( int amount ) : base( amount, 0x18E4 )
		{
            Name = "Patchouli";
            Hue = 0x597;
		}

		public Patchouli( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Pepper : BaseHerb
	{
		[Constructable]
		public Pepper() : this( 1 ) { }

		[Constructable]
		public Pepper( int amount ) : base( amount, 0xF87 )
		{
            Name = "Pepper";
            Hue = 0x3D6;
		}

		public Pepper( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WildRose : BaseHerb
	{
		[Constructable]
		public WildRose() : this( 1 ) { }

		[Constructable]
		public WildRose( int amount ) : base( amount, 0xC3D )
		{
            Name = "Wild Rose";
		}

		public WildRose( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Rosemary : BaseHerb
	{
		[Constructable]
		public Rosemary() : this( 1 ) { }

		[Constructable]
		public Rosemary( int amount ) : base( amount, 0x1020 )
		{
            Name = "Rosemary";
            Hue = 0x594;
		}

		public Rosemary( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Saffron : BaseHerb
	{
		[Constructable]
		public Saffron() : this( 1 ) { }

		[Constructable]
		public Saffron( int amount ) : base( amount, 0xC3C )
		{
            Name = "Saffron";
		}

		public Saffron( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Sandalwood : BaseHerb
	{
		[Constructable]
		public Sandalwood() : this( 1 ) { }

		[Constructable]
		public Sandalwood( int amount ) : base( amount, 0x979 )
		{
            Name = "Sandalwood";
            Hue = 0x59D;
		}

		public Sandalwood( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class SlipperyElm : BaseHerb
	{
		[Constructable]
		public SlipperyElm() : this( 1 ) { }

		[Constructable]
		public SlipperyElm( int amount ) : base( amount, 0xF89 )
		{
            Name = "Slippery Elm";
		}

		public SlipperyElm( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Thyme : BaseHerb
	{
		[Constructable]
		public Thyme() : this( 1 ) { }

		[Constructable]
		public Thyme( int amount ) : base( amount, 0xC3D )
		{
            Name = "Thyme";
		}

		public Thyme( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class Valeriane : BaseHerb
	{
		[Constructable]
		public Valeriane() : this( 1 ) { }

		[Constructable]
		public Valeriane( int amount ) : base( amount, 0xF86 )
		{
            Name = "Valeriane";
            Hue = 0x47E;
		}

		public Valeriane( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class WillowBranches : BaseHerb
	{
		[Constructable]
		public WillowBranches() : this( 1 ) { }

		[Constructable]
		public WillowBranches( int amount ) : base( amount, 0xF79 )
		{
            Name = "Willow Branches";
		}

		public WillowBranches( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
