using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items 
{
	public class VampirismPotion : Item
	{ 
		[Constructable] 
		public VampirismPotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "vampirism potion"; 
			Hue = 38;
			Movable =  true;
            LootType = LootType.Regular;
		}

		public override void OnDoubleClick( Mobile m ) 
		{			
			PlayerMobile from = m as PlayerMobile;
	        if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);
                return;
            }
            else if (!from.IsVampire && !from.IsWerewolf && !from.Young && VampireSystem.Vampires)
            {
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
                from.SendMessage(33, "Czujesz sie dziwnie...");
                from.IsVampire = true;						
                if (VampireSystem.ShowTitles)
				{
					from.Title = "";
				}
                from.AddStatMod(new StatMod(StatType.Str, "Vampire Str Bonus", VampireSystem.VampireStr, TimeSpan.Zero));
                from.AddStatMod(new StatMod(StatType.Dex, "Vampire Dex Bonus", VampireSystem.VampireDex, TimeSpan.Zero));
                from.AddStatMod(new StatMod(StatType.Int, "Vampire Int Bonus", VampireSystem.VampireInt, TimeSpan.Zero));
				from.PlaySound( 0x2D6 );
				this.Delete();
            }
			else
			{
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
				from.SendMessage(0, "Nic sie nie stalo...");
				from.PlaySound( 0x2D6 );
				this.Delete();
			}
		} 

		public VampirismPotion( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}

namespace Server.Items 
{
	public class LycanthropePotion : Item
	{ 
		[Constructable] 
		public  LycanthropePotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "lycanthrope potion"; 
			Hue = 98;
			Movable =  true;
            LootType = LootType.Regular;
		}

		public override void OnDoubleClick( Mobile m ) 
		{			
			PlayerMobile from = m as PlayerMobile;
	        if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);
                return;
            }
            else if (!from.IsVampire && !from.IsWerewolf && !from.Young && VampireSystem.Werewolves)
            {
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
                from.SendMessage(33, "Czujesz sie dziwnie...");
                from.IsWerewolf = true;						
                if (VampireSystem.ShowTitles)
				{
					from.Title = "";
				}
                from.AddStatMod(new StatMod(StatType.Str, "Werewolf Str Bonus", VampireSystem.WerewolfStr, TimeSpan.Zero));
                from.AddStatMod(new StatMod(StatType.Dex, "Werewolf Dex Bonus", VampireSystem.WerewolfDex, TimeSpan.Zero));
                from.AddStatMod(new StatMod(StatType.Int, "Werewolf Int Bonus", VampireSystem.WerewolfInt, TimeSpan.Zero));
				from.PlaySound( 0x2D6 );
				this.Delete();
            }
			else
			{
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
				from.SendMessage(0, "Nic sie nie stalo...");
				from.PlaySound( 0x2D6 );
				this.Delete();
			}	
		} 

		public  LycanthropePotion( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}
//41 101
namespace Server.Items 
{
	public class VampirismCurePotion : Item
	{ 
		[Constructable] 
		public VampirismCurePotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "vampirism cure potion"; 
			Hue = 41;
			Movable =  true;
            LootType = LootType.Regular;
		}

		public override void OnDoubleClick( Mobile m ) 
		{			
			PlayerMobile from = m as PlayerMobile;
	        if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);
                return;
            }
            else if (from.IsVampire)
            {
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
                from.SendMessage(0, "You feel normal...");
                if (from.IsInVampireForm)                
				{    
					from.Send(SpeedControl.Disable);
					from.IsInVampireForm = false;
				}	
				from.IsVampire = false;
				from.BodyMod = 0;
				from.NameMod = null;
				from.Title = null;
				from.RemoveStatMod("Vampire Str Bonus");
				from.RemoveStatMod("Vampire Dex Bonus");
				from.RemoveStatMod("Vampire Int Bonus"); 					
				from.FixedEffect( 0x373A, 10, 15 );
				from.PlaySound( 0x1E0 );
				this.Delete();
            }
			else
			{
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
				from.SendMessage(0, "Nothing happened...");
				from.PlaySound( 0x2D6 );
				this.Delete();
			}
		} 

		public VampirismCurePotion( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}

namespace Server.Items 
{
	public class LycanthropeCurePotion : Item
	{ 
		[Constructable] 
		public LycanthropeCurePotion() :  base( 0xF08 ) 
		{ 
			Weight = 1.0; 			
			Name = "lycanthrope cure potion"; 
			Hue = 101;
			Movable =  true;
            LootType = LootType.Regular;
		}

		public override void OnDoubleClick( Mobile m ) 
		{			
			PlayerMobile from = m as PlayerMobile;
	        if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001);
                return;
            }
            else if (from.IsWerewolf)
            {
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
                from.SendMessage(0, "You feel normal...");
                if (from.IsInWerewolfForm)                
				{    
					from.Send(SpeedControl.Disable);
					from.IsInWerewolfForm = false;
				}		
				from.IsWerewolf = false;
				from.BodyMod = 0;
				from.NameMod = null;
				from.Title = null;
				from.RemoveStatMod("Werewolf Str Bonus");
				from.RemoveStatMod("Werewolf Dex Bonus");
				from.RemoveStatMod("Werewolf Int Bonus"); 					
				from.FixedEffect( 0x373A, 10, 15 );
				from.PlaySound( 0x1E0 );
				this.Delete();
            }
			else
			{
				if (from.Body.IsHuman && !from.Mounted )
				{
					from.Animate( 34, 5, 1, true, false, 0 );
				}
				from.SendMessage(0, "Nothing happened...");
				from.PlaySound( 0x2D6 );
				this.Delete();
			}
		} 

		public LycanthropeCurePotion( Serial serial ) : base( serial ) 
		{ 
		} 
       
		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 1 ); // version 
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	}
}