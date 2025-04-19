using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Items
{

	public class UseEntry
	{
		private string m_Name;
		private int m_Uses;
		private DateTime m_LastUse;

		public string Name { get { return m_Name; } }
		public int Uses { get { return m_Uses; } }
		public DateTime LastUse { get { return m_LastUse; } }

		public UseEntry(string name, int uses, DateTime lastUse)
		{
			m_Name = name;
			m_Uses = uses;
			m_LastUse = lastUse;
		}
	}

	public class WorldOmniporter : Item
	{
		[Flags]
		public enum OptFlags
		{
			None = 0x00000000,
			Trammel = 0x00000001, ///-- When 'true' transport is allowed to the 'Trammel' region
			TramDungeons = 0x00000002, ///-- When 'true' transport is allowed to the 'TramDungeons' region
			Felucca = 0x00000004, ///-- When 'true' transport is allowed to the 'Felucca' region
			//FelDungeons = 0x00000008, ///-- When 'true' transport is allowed to the 'FelDungeons' region
			PublicMoongates = 0x00000010, ///-- When 'true' transport is allowed to the 'PublicMoongates' region
			//Ilshenar = 0x00000020, ///-- When 'true' transport is allowed to the 'Ilshenar' region
			//IlshenarShrines = 0x00000040, ///-- When 'true' transport is allowed to the 'IlshenarShrines' region
			//Malas = 0x00000080, ///-- When 'true' transport is allowed to the 'Malas' region
			//Tokuno = 0x00000100, ///-- When 'true' transport is allowed to the 'Tokuno' region
			AllowReds = 0x00000200, ///-- When 'true' transport is allowed by Reds ( 5+ Murdercount )
			//Custom = 0x00000400, ///-- When 'true' transport is allowed to the 'Custom' region
			UseGlobal = 0x00000800, ///-- When 'true' changes made to this Omniporter affect all others, and vice versa
			WO_OnlyAllowYoung = 0x00001000, //##### -- % -- When 'true' only YOUNG players can use the Omniporter 
			WO_FeluccaNoYoung = 0x00002000, //##### -- % -- When 'true' YOUNG players may not travel to Felucca
			WO_DungeonsNoYoung = 0x00004000, //##### -- % -- When 'true' YOUNG players may not travel to Dungeons
			WO_AllPayToUse = 0x00008000, //##### -- % -- When 'true' the Omniporter costs Gold to use each trip
			WO_SkillsCost = 0x00010000, //##### -- % -- When 'true' higher skill totals make Omniporters more expensive
			WO_KarmaDiscount = 0x00020000, //##### -- % -- When 'true' higher Karma gives players discounts on the cost
			WO_TimeLimit = 0x00040000, //##### -- % -- When 'true' the Omniporter will disappear at the end of the TimeExpiration
			WO_UsesLimit = 0x00080000, //##### -- % -- When 'true' the Omniporter will disappear after MaxUses
			WO_DelayAfterUse = 0x00100000, //##### -- % -- When 'true' players must wait a TimeDelay after using the Omniporter
			WO_PayBeforeDelay = 0x00200000, //##### -- % -- When 'true' players can pay to avoid waiting for the TimeDelay
			WO_PetsMayTravel = 0x00400000, //##### -- % -- When 'true' pets may follow their owners through the Omniporter
			WO_PetsMustPay = 0x00800000, //##### -- % -- When 'true' owners must pay for pets also
			//TerMur = 0x01000000, ///-- When 'true' transport is allowed to the 'TerMur' region
			//GuardiaTowns = 0x02000000,
			//GuardiaSites = 0x04000000,
			//GuardiaDungeons = 0x08000000,
			//GuardiaShrines = 0x10000000
		}

		// These variables are Local or Global (static) and which one is used depends on the UseGlobal variable.
		private static int m_GlobalBasePrice;                   // Sets the Base price to travel - Global
		private int m_BasePrice;                        //  - Local
		private static int m_GlobalSkillsCostBonus;         // Adjusts Skill premium for travel - Global
		private int m_SkillsCostBonus;                  //  - Local
		private static int m_GlobalKarmaDiscountBonus;          // Adjusts Karma discount for travel - Global
		private int m_KarmaDiscountBonus;               //  - Local
		private static TimeSpan m_GlobalTimeExpiration;             // Sets amount of time until Omni expires - Global
		private TimeSpan m_TimeExpiration;                  //  - Local - This is enabled by 'WO_TimeLimit'
		private static int m_GlobalMaxUses;                 // Sets number of uses until any Omni expires - Global
		private int m_MaxUses;                          //  - Local - This is enabled by 'WO_UsesLimit'
		private static TimeSpan m_GlobalTimeDelay;                  // Sets Delay between uses for each user.
		private TimeSpan m_TimeDelay;                       //  - Local
		public static Hashtable GlobalMobileUse = new Hashtable();  // Stores usage of all Omniporters as a group
		private Hashtable m_MobileUse = new Hashtable();        // Stores usage of a single Omni by users

		// These variables are Local only.
		private int m_LocalUses;                        //  - Local - The number of uses of this Omni
		private DateTime m_Birth = DateTime.Now;                //  - Local - The DateTime that the Omni was created

		[CommandProperty(AccessLevel.Administrator)]
		public int LocalUses { get { return m_LocalUses; } set { m_LocalUses = value < 0 ? 0 : value; } }

		[CommandProperty(AccessLevel.Administrator)]
		public DateTime Birth { get { return m_Birth; } set { m_Birth = value; } }

		public string EntryName(Mobile m)
		{
			return GetUseEntry(m).Name;
		}

		public int EntryUses(Mobile m)
		{
			return GetUseEntry(m).Uses;
		}

		public void RaiseUses(Mobile m, int raise)
		{
			int uses;
			try
			{
				uses = EntryUses(m);
				if (uses < 1) RegisterUse(m, new UseEntry(m.Name, raise, DateTime.Now));
				else RegisterUse(m, new UseEntry(m.Name, uses + raise, DateTime.Now));
			}
			catch 
			{ 
			//Console.WriteLine("Exception trapped in RaiseUses()."); 
			}
			DeleteExpiredOmni();
		}

		private static void DeleteExpiredOmni()
		{
			ArrayList olist = new ArrayList();
			WorldOmniporter wo;

			foreach (Item item in World.Items.Values)
			{
				if (item is WorldOmniporter && !item.Movable)
				{
					wo = item as WorldOmniporter;
					if (wo.WO_UsesLimit && wo.LocalUses >= wo.MaxUses) { olist.Add(wo); continue; }
					if (wo.WO_TimeLimit && (wo.Birth < (DateTime.Now - wo.TimeExpiration))) olist.Add(wo);
				}
			}

			foreach (Item item in olist)
				item.Delete();

			if (olist.Count > 0)
				World.Broadcast(0x35, true, "{0} world Omniporters expired.", olist.Count);
		}

		public UseEntry GetUseEntry(Mobile m)
		{
			try
			{
				if (UseGlobal && GlobalMobileUse.ContainsKey(m.Serial.ToString()))
					return ((UseEntry)GlobalMobileUse[m.Serial.ToString()]);
				else if (!UseGlobal && m_MobileUse.ContainsKey(m.Serial.ToString()))
					return ((UseEntry)m_MobileUse[m.Serial.ToString()]);
				else return ((UseEntry)GlobalMobileUse["0"]);
			}
			catch { Console.WriteLine("Exception trapped in GetUseEntry()."); }
			return ((UseEntry)GlobalMobileUse["0"]);
		}

		public void RegisterUse(Mobile from, UseEntry entry)
		{
			PlayerMobile m;
			if (from is PlayerMobile) m = from as PlayerMobile;
			else { Console.WriteLine("Not a PlayerMobile using the Omniporter?"); return; }
			try
			{
				if (UseGlobal)
				{
					if (GlobalMobileUse.ContainsKey(m.Serial.ToString()))
						GlobalMobileUse.Remove(m.Serial.ToString());
					GlobalMobileUse.Add(m.Serial.ToString(), entry);
				}
				else
				{
					if (m_MobileUse.ContainsKey(m.Serial.ToString()))
						m_MobileUse.Remove(m.Serial.ToString());
					m_MobileUse.Add(m.Serial.ToString(), entry);
				}
			}
			catch (Exception e)
			{ Console.WriteLine(String.Format("Error in RegisterUse: {0}.", e.ToString())); }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public int BasePrice
		{
			get { return UseGlobal ? m_GlobalBasePrice : m_BasePrice; }
			set { if (UseGlobal) m_GlobalBasePrice = value; else m_BasePrice = value; }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public int SkillsCostBonus
		{
			get { return UseGlobal ? m_GlobalSkillsCostBonus : m_SkillsCostBonus; }
			set { if (UseGlobal) m_GlobalSkillsCostBonus = value; else m_SkillsCostBonus = value; }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public int KarmaDiscountBonus
		{
			get { return UseGlobal ? m_GlobalKarmaDiscountBonus : m_KarmaDiscountBonus; }
			set { if (UseGlobal) m_GlobalKarmaDiscountBonus = value; else m_KarmaDiscountBonus = value; }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public TimeSpan TimeExpiration
		{
			get { return UseGlobal ? m_GlobalTimeExpiration : m_TimeExpiration; }
			set { if (UseGlobal) m_GlobalTimeExpiration = value; else m_TimeExpiration = value; }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public int MaxUses
		{
			get { return UseGlobal ? m_GlobalMaxUses : m_MaxUses; }
			set { if (UseGlobal) m_GlobalMaxUses = value; else m_MaxUses = value; }
		}

		[CommandProperty(AccessLevel.Administrator)]
		public TimeSpan TimeDelay
		{
			get { return UseGlobal ? m_GlobalTimeDelay : m_TimeDelay; }
			set { if (UseGlobal) m_GlobalTimeDelay = value; else m_TimeDelay = value; }
		}

		public int GetPrice(Mobile m)
		{
			return GetPrice(m, 1, false);
		}

		public int GetPrice(Mobile m, int num, bool pets)
		{
			double k = (double)KarmaDiscountBonus;
			double s = (double)SkillsCostBonus;
			double price = (double)BasePrice;
			double cap = (double)m.Skills.Cap;
			double tot = (double)m.Skills.Total;
			double delay = TimeDelay.TotalMinutes;
			double next = (NextUseTime(m) - DateTime.Now).TotalMinutes;
			tot = tot > cap ? cap : tot;
			if (WO_AllPayToUse || (WO_DelayAfterUse && WO_PayBeforeDelay && NextUseTime(m) > DateTime.Now))
			{
				if (WO_SkillsCost) price = ((price + s) / (1.05 - (tot / cap)));
				if (WO_KarmaDiscount) price = ((price - k) / (m.Karma > 9999 ? 4 : m.Karma > 4999 ? 3 : m.Karma > 1999 ? 2 : 1));
				if (!WO_AllPayToUse) price *= (delay + (next < 0 ? 0 : next)) / delay;
				if (price < 0.0) price = 0.0;
				if (pets && WO_PetsMustPay) price *= num;
				return (int)Math.Ceiling(price);
			}
			return 0;
		}


		public DateTime NextUseTime(Mobile m)
		{
			if (!WO_DelayAfterUse) return DateTime.Now;
			DateTime dt = GetUseEntry(m).LastUse;
			if (dt == DateTime.MinValue) return DateTime.Now;
			else return (dt + TimeDelay);
		}

		public string NextUseMessage(Mobile m)
		{
			return NextUseMessage(m, 1, false);
		}

		public string NextUseMessage(Mobile m, int num, bool pets)
		{
			string delay = "";
			string pay = "";
			string reason = "";
			if (pets && !WO_PetsMayTravel) reason = " because your pets may try to follow you";
			if (CanUse(m, pets))
			{
				if (!MustWait(m) && !MustPay(m, pets))
					return "You can now use Omniporter.";
				int minutes = (int)Math.Ceiling(((TimeSpan)(NextUseTime(m) - DateTime.Now)).TotalMinutes);
				if (MustPay(m, pets))
				{
					if (!CanDelay(m)) pay = String.Format("\nTo use {1} Omniportal, you need to pay {0} Gold.",
						GetPrice(m, num, pets), UseGlobal ? "any" : "this");
					else delay = String.Format(" or pay now {0} Gold", GetPrice(m, num, pets));
				}
				if (MustWait(m)) pay = String.Format("You must wait {0} minutes{1} to use {2} Omniportal{3}.{4}",
					minutes, minutes == 1 ? "" : "", UseGlobal ? "any" : "this", delay, pay);
				return (String.Format(pay));
			}
			else
				return String.Format("You cannot use {0}Omniporter{1}.", UseGlobal ? "any" : "this", reason);
		}

		public bool CanDelay(Mobile m)
		{
			if (WO_DelayAfterUse && !WO_AllPayToUse) return true;
			return false;
		}

		public bool MustWait(Mobile m)
		{
			if (WO_DelayAfterUse && !WO_PayBeforeDelay && NextUseTime(m) > DateTime.Now) return true;
			return false;
		}

		public bool MustPay(Mobile m, bool pets)
		{
			if (pets && WO_PetsMayTravel && WO_PetsMustPay) return true;

			if (WO_AllPayToUse || (WO_DelayAfterUse && WO_PayBeforeDelay && NextUseTime(m) > DateTime.Now)) return true;
			return false;
		}

		public bool MustPay(Mobile m)
		{
			return MustPay(m, false);
		}

		public bool CanUse(Mobile from, bool dungeon, bool felucca)
		{
			PlayerMobile m;
			if (from is PlayerMobile) m = from as PlayerMobile; else return false;
			if (!AllowReds && m.Kills > 4) return false;
			if (WO_OnlyAllowYoung && !m.Young) return false;
			if (WO_FeluccaNoYoung && felucca && m.Young) return false;
			if (WO_DungeonsNoYoung && dungeon && m.Young) return false;
			return true;
		}

		public bool CanUse(Mobile from, bool pets)
		{
			if (pets && !WO_PetsMayTravel) return false;
			return CanUse(from);
		}

		public bool CanUse(Mobile from)
		{
			PlayerMobile m;
			if (from is PlayerMobile) m = from as PlayerMobile; else return false;
			if (!AllowReds && m.Kills > 4) return false;
			if (WO_OnlyAllowYoung && !m.Young) return false;
			return true;
		}

		public static void Initialize()
		{

			// Disabled Flags
			SetOptFlag(ref m_GlobalFlags, OptFlags.Trammel, false);
			SetOptFlag(ref m_GlobalFlags, OptFlags.TramDungeons, false);
			SetOptFlag(ref m_GlobalFlags, OptFlags.PublicMoongates, false);

			//Global Default Flags for WorldOmniGen: 0x1FF
			if (m_GlobalFlags == OptFlags.None)
			{
				SetOptFlag(ref m_GlobalFlags, OptFlags.Trammel, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.TramDungeons, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.Felucca, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.FelDungeons, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.PublicMoongates, false);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.Ilshenar, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.IlshenarShrines, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.Malas, Core.AOS);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.Tokuno, Core.SE);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.TerMur, Core.SA);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.Custom, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.UseGlobal, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_OnlyAllowYoung, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_FeluccaNoYoung, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_DungeonsNoYoung, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_AllPayToUse, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_SkillsCost, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_KarmaDiscount, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_TimeLimit, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_UsesLimit, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_DelayAfterUse, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_PayBeforeDelay, false);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_PetsMayTravel, true);
				SetOptFlag(ref m_GlobalFlags, OptFlags.WO_PetsMustPay, false);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.GuardiaTowns, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.GuardiaSites, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.GuardiaDungeons, true);
				//SetOptFlag(ref m_GlobalFlags, OptFlags.GuardiaShrines, true);
			}

			GlobalEntries.Add("Trammel", new OmniEntry[]
			{
				new OmniEntry("Britain", new Point3D(1434, 1699, 2), Map.Trammel ),
				new OmniEntry("Bucs Den", new Point3D(2705, 2162, 0), Map.Trammel ),
				new OmniEntry("Cove", new Point3D(2237, 1214, 0), Map.Trammel ),
				new OmniEntry("Delucia", new Point3D(5274, 3991, 37), Map.Trammel ),
				new OmniEntry("Village of Exiles", new Point3D(3493, 2577, 14), Map.Trammel ),
				new OmniEntry("Jhelom", new Point3D(1417, 3821, 0), Map.Trammel ),
				new OmniEntry("Magincia", new Point3D(3728, 2164, 20), Map.Trammel ),
				new OmniEntry("Minoc", new Point3D(2525, 582, 0), Map.Trammel ),
				new OmniEntry("Moonglow", new Point3D(4471, 1177, 0), Map.Trammel ),
				new OmniEntry("Nujel'm", new Point3D(3770, 1308, 0), Map.Trammel ),
				new OmniEntry("Papua", new Point3D(5729, 3208, -6), Map.Trammel ),
				new OmniEntry("Serpents Hold", new Point3D(2895, 3479, 15), Map.Trammel ),
				new OmniEntry("Skara Brae", new Point3D(596, 2138, 0), Map.Trammel ),
				new OmniEntry("Trinsic", new Point3D(1823, 2821, 0), Map.Trammel ),
				new OmniEntry("Vesper", new Point3D(2899, 676, 0), Map.Trammel ),
				new OmniEntry("Wind", new Point3D(1361, 895, 0), Map.Trammel ),
				new OmniEntry("Yew", new Point3D(542, 985, 0), Map.Trammel ),
				new OmniEntry("Seaport", new Point3D(4551, 2397, -2), Map.Trammel )
			});

			GlobalEntries.Add("Trammel Dungeons", new OmniEntry[]
			{
				new OmniEntry("Blighted Grove", new Point3D(586, 1643, -5), Map.Trammel ),
				new OmniEntry("Covetous", new Point3D(2498, 921, 0), Map.Trammel ),
				new OmniEntry("Daemon Temple", new Point3D(4591, 3647, 80), Map.Trammel ),
				new OmniEntry("Deceit", new Point3D(4111, 434, 5), Map.Trammel ),
				new OmniEntry("Despise", new Point3D(1301, 1080, 0), Map.Trammel ),
				new OmniEntry("Destard", new Point3D(1176, 2640, 2), Map.Trammel ),
				new OmniEntry("Fire", new Point3D(2923, 3409, 8), Map.Trammel ),
				new OmniEntry("Hythloth", new Point3D(4721, 3824, 0), Map.Trammel ),
				new OmniEntry("Ice", new Point3D(1999, 81, 4), Map.Trammel ),
				new OmniEntry("Ophidian Temple", new Point3D(5766, 2634, 43), Map.Trammel ),
				new OmniEntry("Orc Caves", new Point3D(1017, 1429, 0), Map.Trammel ),
				new OmniEntry("Painted Caves", new Point3D(1716, 2993, 0), Map.Trammel ),
				new OmniEntry("Paroxysmus", new Point3D(5569, 3019, 31), Map.Trammel ),
				new OmniEntry("Prism of Light", new Point3D(3789, 1095, 20), Map.Trammel ),
				new OmniEntry("Sanctuary", new Point3D(759, 1642, 0), Map.Trammel ),
				new OmniEntry("Shame", new Point3D(511, 1565, 0), Map.Trammel ),
				new OmniEntry("Solen Hive", new Point3D(2607, 763, 0), Map.Trammel ),
				new OmniEntry("Terathan Keep", new Point3D(5451, 3143, -60), Map.Trammel ),
				new OmniEntry("Wrong", new Point3D(2043, 238, 10), Map.Trammel )
			});

			GlobalEntries.Add("Public Omniportals", new OmniEntry[]
			{
				new OmniEntry("Britain", new Point3D(1336, 1997, 5), Map.Trammel, true ),
				new OmniEntry("Village of Exiles", new Point3D(3450, 2677, 25), Map.Trammel ),
				new OmniEntry("Jhelom", new Point3D(1499, 3771, 5), Map.Trammel, true ),
				new OmniEntry("Magincia", new Point3D(3563, 2139, 34), Map.Trammel, true ),
				new OmniEntry("Minoc", new Point3D(2701, 692, 5), Map.Trammel, true ),
				new OmniEntry("Moonglow", new Point3D(4467, 1283, 5), Map.Trammel, true ),
				new OmniEntry("Skara Brae", new Point3D(643, 2067, 5), Map.Trammel, true ),
				new OmniEntry("Trinsic", new Point3D(1828, 2948, -20), Map.Trammel, true ),
				new OmniEntry("Yew", new Point3D(771, 752, 5), Map.Trammel, true ),
				new OmniEntry("Moongate Hub (CUSTOM)", new Point3D(4307, 1016, 0), Map.Trammel, true ),
				new OmniEntry("Britain", new Point3D(1336, 1997, 5), Map.Felucca, true ),
				new OmniEntry("Buccaneer's Den", new Point3D(2711, 2234, 0), Map.Felucca, true ),
				new OmniEntry("Jhelom", new Point3D(1499, 3771, 5), Map.Felucca, true ),
				new OmniEntry("Magincia", new Point3D(3563, 2139, 34), Map.Felucca, true ),
				new OmniEntry("Minoc", new Point3D(2701, 692, 5), Map.Felucca, true ),
				new OmniEntry("Moonglow", new Point3D(4467, 1283, 5), Map.Felucca, true ),
				new OmniEntry("Skara Brae", new Point3D(643, 2067, 5), Map.Felucca, true ),
				new OmniEntry("Trinsic", new Point3D(1828, 2948, -20), Map.Felucca, true ),
				new OmniEntry("Yew", new Point3D(771, 752, 5), Map.Felucca, true ),
				new OmniEntry("Moongate Hub (CUSTOM)", new Point3D(4307, 1016, 0), Map.Felucca, true )
			});

			GlobalEntries.Add("Edon Island", new OmniEntry[]
			{
				new OmniEntry("Kingdom of Edonia", new Point3D(1355, 982, 10), Map.Felucca ),
				new OmniEntry("Alafar", new Point3D(1530, 398, 12), Map.Felucca )
			});

			GlobalEntries.Add("Elven Island", new OmniEntry[]
			{
				new OmniEntry("Moon Grove Village", new Point3D(865, 1600, 0), Map.Felucca ),
			});

			GlobalEntries.Add("Tokuno Island", new OmniEntry[]
			{
				new OmniEntry("Kage no Machi", new Point3D(2803, 3040, 6), Map.Felucca ),
			});

			GlobalEntries.Add("Eterna Island", new OmniEntry[]
			{
			    new OmniEntry("Near the Underdark", new Point3D(4025, 821, 0), Map.Felucca ),
			});

			GlobalEntries.Add("Narolia Island", new OmniEntry[]
			{
			    new OmniEntry("Unknown Place", new Point3D(4182, 3148, 0), Map.Felucca ),
			});
////TUTAJ NIEAKTYWNE			
/*
			GlobalEntries.Add("Felucca Dungeons", new OmniEntry[]
			{
				new OmniEntry("Blighted Grove", new Point3D(586, 1643, -5), Map.Felucca ),
				new OmniEntry("Covetous", new Point3D(2498, 921, 0), Map.Felucca ),
				new OmniEntry("Daemon Temple", new Point3D(4591, 3647, 80), Map.Felucca ),
				new OmniEntry("Deceit", new Point3D(4111, 434, 5), Map.Felucca ),
				new OmniEntry("Despise", new Point3D(1301, 1080, 0), Map.Felucca ),
				new OmniEntry("Destard", new Point3D(1176, 2640, 2), Map.Felucca ),
				new OmniEntry("Fire", new Point3D(2923, 3409, 8), Map.Felucca ),
				new OmniEntry("Hythloth", new Point3D(4721, 3824, 0), Map.Felucca ),
				new OmniEntry("Ice", new Point3D(1999, 81, 4), Map.Felucca ),
				new OmniEntry("Ophidian Temple", new Point3D(5766, 2634, 43), Map.Felucca ),
				new OmniEntry("Orc Caves", new Point3D(1017, 1429, 0), Map.Felucca ),
				new OmniEntry("Painted Caves", new Point3D(1716, 2993, 0), Map.Felucca ),
				new OmniEntry("Paroxysmus", new Point3D(5569, 3019, 31), Map.Felucca ),
				new OmniEntry("Prism of Light", new Point3D(3789, 1095, 20), Map.Felucca ),
				new OmniEntry("Sanctuary", new Point3D(759, 1642, 0), Map.Felucca ),
				new OmniEntry("Shame", new Point3D(511, 1565, 0), Map.Felucca ),
				new OmniEntry("Solen Hive", new Point3D(2607, 763, 0), Map.Felucca ),
				new OmniEntry("Terathan Keep", new Point3D(5451, 3143, -60), Map.Felucca ),
				new OmniEntry("Wrong", new Point3D(2043, 238, 10), Map.Felucca )
			});

			GlobalEntries.Add("Ilshenar", new OmniEntry[]
			{
				new OmniEntry("Ankh Dungeon", new Point3D(576, 1150, -100), Map.Ilshenar ),
				new OmniEntry("Blood Dungeon", new Point3D(1747, 1171, -2), Map.Ilshenar ),
				new OmniEntry("Exodus Dungeon", new Point3D(854, 778, -80), Map.Ilshenar ),
				new OmniEntry("Gargoyle City", new Point3D(852, 602, -40), Map.Ilshenar ),
				new OmniEntry("Lakeshire", new Point3D(1203, 1124, -25), Map.Ilshenar ),
				new OmniEntry("Mistas", new Point3D(819, 1130, -29), Map.Ilshenar ),
				new OmniEntry("Montor", new Point3D(1706, 205, 104), Map.Ilshenar ),
				new OmniEntry("Rock Dungeon", new Point3D(1787, 572, 69), Map.Ilshenar ),
				new OmniEntry("Savage Camp", new Point3D(1151, 659, -80), Map.Ilshenar ),
				new OmniEntry("Sorceror's Dungeon", new Point3D(548, 462, -53), Map.Ilshenar ),
				new OmniEntry("Spectre Dungeon", new Point3D(1363, 1033, -8), Map.Ilshenar ),
				new OmniEntry("Spider Cave", new Point3D(1420, 913, -16), Map.Ilshenar ),
				new OmniEntry("Wisp Dungeon", new Point3D(651, 1302, -58), Map.Ilshenar )
			});
			
			GlobalEntries.Add("Ilshenar Shrines", new OmniEntry[]
			{
				new OmniEntry("Compassion", new Point3D(1215, 467, -13), Map.Ilshenar ),
				new OmniEntry("Honesty", new Point3D(722, 1366, -60), Map.Ilshenar ),
				new OmniEntry("Honor", new Point3D(744, 724, -28), Map.Ilshenar ),
				new OmniEntry("Humility", new Point3D(281, 1016, 0), Map.Ilshenar ),
				new OmniEntry("Justice", new Point3D(987, 1011, -32), Map.Ilshenar ),
				new OmniEntry("Sacrifice", new Point3D(1174, 1286, -30), Map.Ilshenar ),
				new OmniEntry("Spirituality", new Point3D(1532, 1340, -3), Map.Ilshenar ),
				new OmniEntry("Valor", new Point3D(528, 216, -45), Map.Ilshenar ),
				new OmniEntry("Choas", new Point3D(1721, 218, 96), Map.Ilshenar )
			});
			
			GlobalEntries.Add("Malas", new OmniEntry[]
			{
				new OmniEntry("Doom", new Point3D(2368, 1267, -85), Map.Malas ),
				new OmniEntry("Labyrinth", new Point3D(1730, 981, -80), Map.Malas ),
				new OmniEntry("Luna", new Point3D(1015, 527, -65), Map.Malas, true ),
				new OmniEntry("Orc Fort 1", new Point3D(912, 215, -90), Map.Malas ),
				new OmniEntry("Orc Fort 2", new Point3D(1678, 374, -50), Map.Malas ),
				new OmniEntry("Orc Fort 3", new Point3D(1375, 621, -86), Map.Malas ),
				new OmniEntry("Orc Fort 4", new Point3D(1184, 715, -89), Map.Malas ),
				new OmniEntry("Orc Fort 5", new Point3D(1279, 1324, -90), Map.Malas ),
				new OmniEntry("Orc Fort 6", new Point3D(1598, 1834, -107), Map.Malas ),
				new OmniEntry("Ruined Temple", new Point3D(1598, 1762, -110), Map.Malas ),
				new OmniEntry("Umbra", new Point3D(1997, 1386, -85), Map.Malas, true )
			});
			
			GlobalEntries.Add("Tokuno", new OmniEntry[]
			{
				new OmniEntry("Bushido Dojo", new Point3D(322, 430, 32), Map.Tokuno ),
				new OmniEntry("Crane Marsh", new Point3D(203, 985, 18), Map.Tokuno ),
				new OmniEntry("Fan Dancer's Dojo", new Point3D(970, 222, 23), Map.Tokuno ),
				new OmniEntry("Isamu-Jima", new Point3D(1169, 998, 41), Map.Tokuno ),
				new OmniEntry("Makoto-Jima", new Point3D(802, 1204, 25), Map.Tokuno ),
				new OmniEntry("Homare-Jima", new Point3D(270, 628, 15), Map.Tokuno ),
				new OmniEntry("Makoto Desert", new Point3D(724, 1050, 33), Map.Tokuno ),
				new OmniEntry("Makoto Zento", new Point3D(741, 1261, 30), Map.Tokuno ),
				new OmniEntry("Mt. Sho Castle", new Point3D(1234, 772, 3), Map.Tokuno ),
				new OmniEntry("Valor Shrine", new Point3D(1044, 523, 15), Map.Tokuno ),
				new OmniEntry("Yomotsu Mine", new Point3D(257, 786, 63), Map.Tokuno )
			});
			
			GlobalEntries.Add("TerMur", new OmniEntry[]
			{
				new OmniEntry("Royal City", new Point3D(852, 3526, -43), Map.TerMur),
				new OmniEntry("Holy City", new Point3D(926, 3989, -36), Map.TerMur),
				new OmniEntry("Fisherman's Reach", new Point3D(612, 3038, 35), Map.TerMur),
				new OmniEntry("Tomb of Kings", new Point3D(997, 3843, -41), Map.TerMur),
				new OmniEntry("Underworld", new Point3D(4194, 3268, 0), Map.Trammel)
			});
			
			GlobalEntries.Add("Custom", new OmniEntry[] //add locations to the custom map here
			{
				new OmniEntry("Auction House", new Point3D(1045, 570, -90), Map.Malas ),
				new OmniEntry("Dueling Room", new Point3D(5250, 1756, 0), Map.Felucca ),
				new OmniEntry("House Gates", new Point3D(616, 2113, 0), Map.Trammel ),
				new OmniEntry("Hue Room", new Point3D(5379, 1094, 0), Map.Trammel ),
				new OmniEntry("Mook Town", new Point3D(1069, 1443, -90), Map.Malas ),
				new OmniEntry("Taming Forest", new Point3D(902, 912, 0), Map.Trammel ),
				new OmniEntry("Gate Room", new Point3D(6079, 451, -22), Map.Felucca ),
				new OmniEntry("Champ Spawn Abyss", new Point3D(5256, 3325, 4), Map.Felucca ),
				new OmniEntry("Event Area", new Point3D(4703, 1146, 0), Map.Trammel ),
				new OmniEntry("Welcome Room", new Point3D(572, 536, -73), Map.Malas),
				new OmniEntry("Lycaeum", new Point3D(897, 1123, -90), Map.Malas),
				new OmniEntry("Luna Hub", new Point3D(1128, 536, -90), Map.Malas),
				new OmniEntry("Star Market", new Point3D(571, 218, -93), Map.Malas)
			});
			
			GlobalEntries.Add("Guardia Towns", new OmniEntry[] //add locations to the custom map here
			{
				new OmniEntry("Village of Selvington", new Point3D(1562, 232, -90), Map.Malas),
				new OmniEntry("City of Roma", new Point3D(823, 470, -90), Map.Malas),
				new OmniEntry("Oasis", new Point3D(1692, 1458, -110), Map.Malas),
				new OmniEntry("Summer Villa", new Point3D(1688, 1404, -108), Map.Malas),
				new OmniEntry("Old Forge", new Point3D(1462, 170, -90), Map.Malas),
				new OmniEntry("Pixie Treehouse", new Point3D(567, 1192, 0), Map.Ilshenar),
				new OmniEntry("Palancia Forest Village", new Point3D(836, 1205, 0), Map.Trammel),
				new OmniEntry("Village Starburst", new Point3D(1872, 2513, 0), Map.Trammel),
				new OmniEntry("Ethereal City", new Point3D(1832, 65, -95), Map.Trammel),
				new OmniEntry("Town of Shimabuku", new Point3D(409, 541, -15), Map.Trammel),
				new OmniEntry("Mining Town Prism", new Point3D(1548,373541, -50), Map.Trammel),
				new OmniEntry("Sand City Navidia", new Point3D(1714,447, -110), Map.Trammel),
				new OmniEntry("Yasha's Treehouse", new Point3D(668,973, 0), Map.Trammel),
				new OmniEntry("Ocean Village Pakua", new Point3D(1661,1814, 12), Map.Trammel),
				new OmniEntry("Village Ravine", new Point3D(876,1838, 3), Map.TerMur),
				new OmniEntry("Sarai the Bold", new Point3D(925,1195, -90), Map.Malas),
				new OmniEntry("Elven City Eldar", new Point3D(1799,500, -58), Map.Malas)
			});
			
			GlobalEntries.Add("Guardia Sites", new OmniEntry[] //add locations to the custom map here
			{
				new OmniEntry("Arena Soeldner", new Point3D(706, 518, -89), Map.Malas),
				new OmniEntry("Luna Meeting Center", new Point3D(877, 579, -90), Map.Malas),
				new OmniEntry("Tower Archer", new Point3D(1701, 203, -90), Map.Malas),
				new OmniEntry("Lord Edward's Estate", new Point3D(695, 476, -90), Map.Malas),
				new OmniEntry("Ice Alchemist", new Point3D(1490, 353, -43), Map.Malas),
				new OmniEntry("Mines", new Point3D(1505, 1881, 0), Map.Malas),
				new OmniEntry("Town of Requiem", new Point3D(1445, 143, -90), Map.Malas),
				new OmniEntry("Necromancer Academy", new Point3D(627, 601, -90), Map.Malas),
				new OmniEntry("Luna Guilds", new Point3D(1742, 221, -85), Map.Malas),
				new OmniEntry("Orc Fort Scowl", new Point3D(673, 1349, 0), Map.Trammel),
				new OmniEntry("Orc Fort Slander", new Point3D(1002, 1905, 0), Map.Trammel)
			});
			
			GlobalEntries.Add("Guardia Dungeons", new OmniEntry[] //add locations to the custom map here
			{
				new OmniEntry("Demon Workshop", new Point3D(1235, 477, -90), Map.Malas),
				new OmniEntry("Ethereal Castle", new Point3D(865, 278, -92), Map.Malas),
				new OmniEntry("Demon Arena", new Point3D(1285, 449, -90), Map.Malas),
				new OmniEntry("Dungeon Hatred", new Point3D(2015, 127, -95), Map.Malas),
				new OmniEntry("Desert Deadhouse", new Point3D(1635, 1524, -110), Map.Malas),
				new OmniEntry("Impenetrable Prison", new Point3D(2461, 489, -95), Map.Malas),
				new OmniEntry("Dungeon Blackwater", new Point3D(1003, 2691, -21), Map.TerMur),
				new OmniEntry("Navidia Ruins", new Point3D(1794, 1519, -110), Map.Malas),
				new OmniEntry("Dungeon Hatred", new Point3D(1795, 2280, 0), Map.Trammel),
				new OmniEntry("Dungeon Darkwater", new Point3D(1228, 475, -90), Map.Malas),
				new OmniEntry("Dungeon Antaria", new Point3D(464, 103, -24), Map.Ilshenar),
				new OmniEntry("Dungeon Horrid", new Point3D(1067, 29910, -17), Map.Ilshenar),
				new OmniEntry("Blackbeard's Castle", new Point3D(1831, 1867, 10), Map.Trammel),
				new OmniEntry("Water Dungeon Agua", new Point3D(2662, 3424, 10), Map.Trammel),
				new OmniEntry("Dungeon Ifrit", new Point3D(330, 3778, -84), Map.TerMur),
				new OmniEntry("Dungeon Abraxar", new Point3D(1152, 308, -17), Map.TerMur),
				new OmniEntry("Dungeon Hyrule", new Point3D(596, 958, -95), Map.Malas)
			});
			
			GlobalEntries.Add("Guardia Shrines", new OmniEntry[] //add locations to the custom map here
			{
				new OmniEntry("Dungeon Hyrule2", new Point3D(597, 958, -95), Map.Malas)
			});
*/
////TUTAJ NIEAKTYWNE	

		}

		public static int GenerateWorldOmniporters()
		{
			int gen = 0;

			if (GetOptFlag(m_GlobalFlags, OptFlags.Trammel)) gen += GenerateEntry("Trammel");
			if (GetOptFlag(m_GlobalFlags, OptFlags.TramDungeons)) gen += GenerateEntry("Trammel Dungeons");
			if (GetOptFlag(m_GlobalFlags, OptFlags.PublicMoongates)) gen += GenerateEntry("Public Omniportals");
			if (GetOptFlag(m_GlobalFlags, OptFlags.Felucca)) gen += GenerateEntry("Edon Island");
			if (GetOptFlag(m_GlobalFlags, OptFlags.Felucca)) gen += GenerateEntry("Elven Island");
			if (GetOptFlag(m_GlobalFlags, OptFlags.Felucca)) gen += GenerateEntry("Tokuno Island");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.FelDungeons)) gen += GenerateEntry("Felucca Dungeons");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.Ilshenar)) gen += GenerateEntry("Ilshenar");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.IlshenarShrines)) gen += GenerateEntry("Ilshenar Shrines");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.Malas) && Core.AOS) gen += GenerateEntry("Malas");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.Tokuno) && Core.SE) gen += GenerateEntry("Tokuno");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.TerMur) && Core.SA) gen += GenerateEntry("TerMur");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.Custom)) gen += GenerateEntry("Custom");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.GuardiaTowns)) gen += GenerateEntry("Guardia Towns");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.GuardiaSites)) gen += GenerateEntry("Guardia Sites");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.GuardiaDungeons)) gen += GenerateEntry("Guardia Dungeons");
			//if (GetOptFlag(m_GlobalFlags, OptFlags.GuardiaShrines)) gen += GenerateEntry("Guardia Shrines");

			return gen;
		}

		private static int GenerateEntry(string map)
		{
			OmniEntry[] oe = (OmniEntry[])GlobalEntries[map];
			if (oe != null)
			{
				for (int i = 0; i < oe.Length; i++)
					new WorldOmniporter(oe[i].Moongate ? true : false).MoveToWorld(oe[i].Destination, oe[i].Map);
				return oe.Length;
			}
			return 0;
		}

		public static Hashtable GlobalEntries = new Hashtable();
		private OptFlags m_Flags;
		private static OptFlags m_GlobalFlags;

		[CommandProperty(AccessLevel.Administrator)]
		public bool Trammel { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Trammel); } set { SetOptFlag(OptFlags.Trammel, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool TramDungeons { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.TramDungeons); } set { SetOptFlag(OptFlags.TramDungeons, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool Felucca { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Felucca); } set { SetOptFlag(OptFlags.Felucca, value); } }
		
		//[CommandProperty(AccessLevel.Administrator)]
		//public bool FelDungeons { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.FelDungeons); } set { SetOptFlag(OptFlags.FelDungeons, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool PublicMoongates { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.PublicMoongates); } set { SetOptFlag(OptFlags.PublicMoongates, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool Ilshenar { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Ilshenar); } set { SetOptFlag(OptFlags.Ilshenar, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool IlshenarShrines { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.IlshenarShrines); } set { SetOptFlag(OptFlags.IlshenarShrines, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool Malas { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Malas); } set { SetOptFlag(OptFlags.Malas, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool Tokuno { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Tokuno); } set { SetOptFlag(OptFlags.Tokuno, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool TerMur { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.TerMur); } set { SetOptFlag(OptFlags.TerMur, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool AllowReds { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.AllowReds); } set { SetOptFlag(OptFlags.AllowReds, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool Custom { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.Custom); } set { SetOptFlag(OptFlags.Custom, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool GuardiaTowns { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.GuardiaTowns); } set { SetOptFlag(OptFlags.GuardiaTowns, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool GuardiaSites { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.GuardiaSites); } set { SetOptFlag(OptFlags.GuardiaSites, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool GuardiaDungeons { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.GuardiaDungeons); } set { SetOptFlag(OptFlags.GuardiaDungeons, value); } }

		//[CommandProperty(AccessLevel.Administrator)]
		//public bool GuardiaShrines { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.GuardiaShrines); } set { SetOptFlag(OptFlags.GuardiaShrines, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool UseGlobal { get { return GetOptFlag(m_Flags, OptFlags.UseGlobal); } set { SetOptFlag(ref m_Flags, OptFlags.UseGlobal, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_OnlyAllowYoung { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_OnlyAllowYoung); } set { SetOptFlag(OptFlags.WO_OnlyAllowYoung, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_FeluccaNoYoung { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_FeluccaNoYoung); } set { SetOptFlag(OptFlags.WO_FeluccaNoYoung, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_DungeonsNoYoung { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_DungeonsNoYoung); } set { SetOptFlag(OptFlags.WO_DungeonsNoYoung, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_AllPayToUse { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_AllPayToUse); } set { SetOptFlag(OptFlags.WO_AllPayToUse, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_SkillsCost { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_SkillsCost); } set { SetOptFlag(OptFlags.WO_SkillsCost, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_KarmaDiscount { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_KarmaDiscount); } set { SetOptFlag(OptFlags.WO_KarmaDiscount, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_TimeLimit { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_TimeLimit); } set { SetOptFlag(OptFlags.WO_TimeLimit, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_UsesLimit { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_UsesLimit); } set { SetOptFlag(OptFlags.WO_UsesLimit, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_DelayAfterUse { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_DelayAfterUse); } set { SetOptFlag(OptFlags.WO_DelayAfterUse, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_PayBeforeDelay { get { if (!WO_DelayAfterUse) return false; return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_PayBeforeDelay); } set { SetOptFlag(OptFlags.WO_PayBeforeDelay, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_PetsMayTravel { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_PetsMayTravel); } set { SetOptFlag(OptFlags.WO_PetsMayTravel, value); } }

		[CommandProperty(AccessLevel.Administrator)]
		public bool WO_PetsMustPay { get { return GetOptFlag((UseGlobal ? m_GlobalFlags : m_Flags), OptFlags.WO_PetsMustPay); } set { SetOptFlag(OptFlags.WO_PetsMustPay, value); } }

		public void SetOptFlag(OptFlags toSet, bool value)
		{
			if (UseGlobal)
			{
				if (value)
					m_GlobalFlags |= toSet;
				else
					m_GlobalFlags &= ~toSet;
			}
			else
			{
				if (value)
					m_Flags |= toSet;
				else
					m_Flags &= ~toSet;
			}
		}

		public static void SetOptFlag(ref OptFlags flags, OptFlags toSet, bool value)
		{
			if (value)
				flags |= toSet;
			else
				flags &= ~toSet;
		}

		public static bool GetOptFlag(OptFlags flags, OptFlags flag)
		{
			return ((flags & flag) != 0);
		}

		[Constructable]
		public WorldOmniporter() : this((int)m_GlobalFlags)
		{
		}

		[Constructable]
		public WorldOmniporter(bool moongate) : this((int)m_GlobalFlags, moongate)
		{
		}

		[Constructable]
		public WorldOmniporter(int flags) : this(flags, false)
		{
		}

		[Constructable]
		//public WorldOmniporter(int flags, bool moongate) : base(moongate ? 0xF6C : 7107)
		public WorldOmniporter(int flags, bool moongate) : base(0xF6C)
		{
			Movable = false;
			Hue = 1264;
			Name = "Omniporter";
			Light = LightType.Circle300;
			m_Flags = (OptFlags)flags;
			m_MobileUse = (Hashtable)GlobalMobileUse;
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (!from.Player)
				return;
			UseOmniporter(from);
		}

		public override bool OnMoveOver(Mobile from)
		{
			return !from.Player || UseOmniporter(from);
		}

		private bool LoadUses(Mobile m)
		{
			OmniEntry[] oe = new OmniEntry[m_Entries.Length];

			try
			{
				for (int x = 0; x < m_Entries.Length; x++)
				{
					oe = (OmniEntry[])WorldOmniporter.GlobalEntries[m_Entries[x]];

					for (int y = 0; y < oe.Length; y++)
					{
						OmniEntry entry = oe[y];
						try
						{
							entry.ToCount = loadTo[x][y];
							entry.FromCount = loadFrom[x][y];
						}
						catch (Exception)
						{
							if (InitializeCounts())
							{
								entry.ToCount = loadTo[x][y];
								entry.FromCount = loadFrom[x][y];
							}
							else
							{
								entry.ToCount = 0;
								entry.FromCount = 0;
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				//This is a legacy catch - should never happen...
				m.SendMessage("Omniporters have not been initialized. Please restart server.");
				Console.WriteLine(e.ToString());
				return false;
			}

			loaded = true;
			return true;
		}

		public bool InitializeCounts()
		{
			Console.WriteLine("Initializing Counts...");
			try
			{
				OmniEntry[] oe;

				int outlength = m_Entries.Length;
				int inlength;

				loadTo = new int[outlength][];
				loadFrom = new int[outlength][];

				for (int x = 0; x < outlength; x++)
				{
					oe = (OmniEntry[])WorldOmniporter.GlobalEntries[m_Entries[x]];

					inlength = oe.Length;
					loadTo[x] = new int[inlength];
					loadFrom[x] = new int[inlength];

					for (int y = 0; y < oe.Length; y++)
					{
						loadTo[x][y] = 0;
						loadFrom[x][y] = 0;
					}
				}
				return true;
			}
			catch (Exception e) { Console.WriteLine(e.ToString()); }
			return false;
		}

		public bool UseOmniporter(Mobile m)
		{
			if (!loaded && !LoadUses(m)) return false;
			if (m.Criminal)
				m.SendLocalizedMessage(1005561, "", 0x22); // Thou'rt a criminal and cannot escape so easily.
			else if (Server.Spells.SpellHelper.CheckCombat(m))
				m.SendLocalizedMessage(1005564, "", 0x22); // Wouldst thou flee during the heat of battle??
			else if (m.Spell != null)
				m.SendLocalizedMessage(1049616); // You are too busy to do that at the moment.
			else
			{
				m.CloseGump(typeof(WorldOmniporterGump));
				m.SendGump(new WorldOmniporterGump(m, this, 0));

				if (!m.Hidden || m.AccessLevel == AccessLevel.Player)
					Effects.PlaySound(m.Location, m.Map, 0x20E);
				return true;
			}
			return false;
		}

		public WorldOmniporter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)3); // version

			OmniEntry[] oe;

			for (int x = 0; x < m_Entries.Length; x++)
			{
				oe = (OmniEntry[])WorldOmniporter.GlobalEntries[m_Entries[x]];
				writer.Write((int)oe.Length);
				for (int y = 0; y < oe.Length; y++)
				{
					OmniEntry entry = oe[y];
					writer.Write((int)entry.ToCount);
					writer.Write((int)entry.FromCount);
				}
			}

			writer.Write((int)m_LocalUses); // version 2

			writer.Write((int)m_GlobalBasePrice); // version 1
			writer.Write((int)m_BasePrice);
			writer.Write((int)m_GlobalSkillsCostBonus);
			writer.Write((int)m_SkillsCostBonus);
			writer.Write((int)m_GlobalKarmaDiscountBonus);
			writer.Write((int)m_KarmaDiscountBonus);
			writer.Write((int)m_GlobalTimeExpiration.TotalSeconds);
			writer.Write((int)m_TimeExpiration.TotalSeconds);
			writer.Write((int)m_GlobalMaxUses);
			writer.Write((int)m_MaxUses);
			writer.Write((int)m_GlobalTimeDelay.TotalSeconds);
			writer.Write((int)m_TimeDelay.TotalSeconds);

			writer.Write((int)m_Flags); // version 0
			writer.Write((int)m_GlobalFlags);
		}

		private int[][] loadTo;
		private int[][] loadFrom;
		private static bool loaded;  // Global variable: we load all uses at once.

		public override void Deserialize(GenericReader reader)
		{
			loaded = false;
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 3:
					{
						int outlength = m_Entries.Length;
						int inlength;

						loadTo = new int[outlength][];
						loadFrom = new int[outlength][];

						for (int x = 0; x < outlength; x++)
						{
							inlength = reader.ReadInt();
							loadTo[x] = new int[inlength];
							loadFrom[x] = new int[inlength];

							for (int y = 0; y < loadTo[x].Length; y++)
							{
								loadTo[x][y] = reader.ReadInt();
								loadFrom[x][y] = reader.ReadInt();
							}
						}
						goto case 2;
					}
				case 2:
					{
						m_LocalUses = reader.ReadInt();
						goto case 1;
					}
				case 1:
					{
						m_GlobalBasePrice = reader.ReadInt();
						m_BasePrice = reader.ReadInt();
						m_GlobalSkillsCostBonus = reader.ReadInt();
						m_SkillsCostBonus = reader.ReadInt();
						m_GlobalKarmaDiscountBonus = reader.ReadInt();
						m_KarmaDiscountBonus = reader.ReadInt();
						m_GlobalTimeExpiration = TimeSpan.FromSeconds(reader.ReadInt());
						m_TimeExpiration = TimeSpan.FromSeconds(reader.ReadInt());
						m_GlobalMaxUses = reader.ReadInt();
						m_MaxUses = reader.ReadInt();
						m_GlobalTimeDelay = TimeSpan.FromSeconds(reader.ReadInt());
						m_TimeDelay = TimeSpan.FromSeconds(reader.ReadInt());
						GlobalMobileUse = new Hashtable();
						m_MobileUse = new Hashtable();
						try { GlobalMobileUse.Add("0", new UseEntry("None", 0, DateTime.Now)); }
						catch { Console.WriteLine("Exception caught Deserializing GlobalMobileUse."); }
						m_MobileUse.Add("0", new UseEntry("None", 0, DateTime.Now));
						goto case 0;
					}
				case 0:
					{
						m_Flags = (OptFlags)reader.ReadInt();
						m_GlobalFlags = (OptFlags)reader.ReadInt();
						break;
					}
			}
		}

		private static string[] m_Entries = new string[]
		{
			"Trammel", "Trammel Dungeons", "Public Omniportals"
		};
	}

	public class WorldOmniporterGump : Gump
	{
		private WorldOmniporter m_WO;
		private Mobile m;
		private OmniEntry m_Entry;
		private int m_Page;
		private bool m_Reds, m_HasLBR, m_HasAOS, m_HasSE, m_HasSA, m_Staff;
		private List<Mobile> move = new List<Mobile>();
		private int go;
		private bool pets;

		public void AddBlackAlpha(int x, int y, int width, int height)
		{
			AddImageTiled(58, 38, 384, 429, 2624);
			AddImageTiled(58, 8, 384, 24, 2624);
		}

		public WorldOmniporterGump(Mobile from, WorldOmniporter WO, int page) : base(100, 100)
		{
			ClientFlags flags = from.NetState == null ? 0 : from.NetState.Flags;

			m = from;
			m_WO = WO;
			m_Page = page;
			m_HasLBR = (flags & ClientFlags.Felucca) != 0;
			m_HasLBR = (flags & ClientFlags.Trammel) != 0;
			m_HasAOS = (flags & ClientFlags.Ilshenar) != 0;
			m_HasSE = (flags & ClientFlags.Tokuno) != 0;
			m_HasSA = (flags & ClientFlags.TerMur) != 0;
			m_Reds = (m.Kills < 5 || WO.AllowReds);
			m_Staff = m.AccessLevel > AccessLevel.Player;

			//Did they press an invalid button or supply an invalid argument?
			if (page < 0 || page > m_Entries.Length + 1)
				page = 0;

			AddPage(0);
			AddBackground(50, 30, 400, 445, 5054);
			AddBackground(50, 0, 400, 475, 5054);
			AddBlackAlpha(50, 30, 384, 429);
			AddBlackAlpha(50, 0, 384, 24);
			AddAlphaRegion(58, 38, 384, 429);
			AddAlphaRegion(58, 8, 384, 24);
			AddImage(0, 0, 10440);  //Left side dragon
			AddImage(418, 0, 10441);  //Right side dragon


			if (m_Staff) AddHtml(363, 40, 50, 15, "<basefont color=#FFFFFF>TO</basefont>", false, false);                   // staff see Omniporter usage
			if (m_Staff) AddHtml(396, 40, 50, 15, "<basefont color=#FFFFFF>FROM</basefont>", false, false);                 // staff see Omniporter usage 
			AddHtml(75, 40, 150, 15, "<basefont color=#FFFFFF>Select Location:</basefont>", false, false);
			AddHtml(125, 12, 280, 15, "<basefont color=#FFFFFF>Magical Omniportal!</basefont>", false, false);

			int p = 1;

			if (WO.Trammel)
			{
				GenerateMapListing(1);
				AddPageButton("Trammel", Map.Trammel, p++, 1);
			}

			if (WO.TramDungeons && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(2);
				AddPageButton("Trammel Dungeons", Map.Trammel, p++, 2);
			}

			if (WO.PublicMoongates && (WO.Felucca || (WO.Trammel && m_Reds)))		
			{
				GenerateMapListing(3);
				AddPageButton("<basefont color=#ccff66>Public-Portals</basefont>", null, p++, 3);
			}
////
			if (WO.Felucca && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(4);
				AddPageButton("<basefont color=#ccff66>Edon Island</basefont>", Map.Felucca, p++, 4);
			}

			if (WO.Felucca && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(5);
				AddPageButton("<basefont color=#ccff66>Elven Island</basefont>", Map.Felucca, p++, 5);
			}

			if (WO.Felucca && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(6);
				AddPageButton("<basefont color=#ccff66>Tokuno Island</basefont>", Map.Felucca, p++, 6);
			}

			if (WO.Felucca && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(7);
				AddPageButton("<basefont color=#ccff66>Eterna Island</basefont>", Map.Felucca, p++, 7);
			}

			if (WO.Felucca && m_Reds && WO.CanUse(m, true, false))
			{
				GenerateMapListing(8);
				AddPageButton("<basefont color=#ccff66>Narolia Island</basefont>", Map.Felucca, p++, 8);
			}
////
			//if (WO.FelDungeons && WO.CanUse(m, true, true))
			//{
			//	GenerateMapListing(4);
			//	AddPageButton("Felucca Dungeons", Map.Felucca, p++, 4);
			//}

			//if (WO.Ilshenar && m_Reds && m_HasLBR)
			//{
			//	GenerateMapListing(6);
			//	AddPageButton("Ilshenar", Map.Ilshenar, p++, 6);
			//}
			//
			//if (WO.IlshenarShrines && m_Reds && m_HasLBR)
			//{
			//	GenerateMapListing(7);
			//	AddPageButton("Ilshenar Shrines", Map.Ilshenar, p++, 7);
			//}
			//
			//if (WO.Malas && m_Reds && Core.AOS && m_HasAOS)
			//{
			//	GenerateMapListing(8);
			//	AddPageButton("Malas", Map.Malas, p++, 8);
			//}
			//
			//if (WO.Tokuno && m_Reds && Core.SE && m_HasSE)
			//{
			//	GenerateMapListing(9);
			//	AddPageButton("Tokuno", Map.Tokuno, p++, 9);
			//}
			//
			//if (WO.TerMur && m_Reds && Core.SA && m_HasSA)
			//{
			//	GenerateMapListing(10);
			//	AddPageButton("TerMur", Map.TerMur, p++, 10);
			//}
			//
			//if (WO.Custom)
			//{
			//	GenerateMapListing(11);
			//	AddPageButton("<basefont color=#99ffcc>Custom Destinations</basefont>", null, p++, 11);
			//}
			//if (WO.GuardiaTowns)
			//{
			//	GenerateMapListing(12);
			//	AddPageButton("<basefont color=#FFFFFF>Guardia Towns</basefont>", null, p++, 12);
			//}
			//if (WO.GuardiaSites)
			//{
			//	GenerateMapListing(13);
			//	AddPageButton("<basefont color=#FFFFFF>Guardia Sites</basefont>", null, p++, 13);
			//}
			//if (WO.GuardiaDungeons)
			//{
			//	GenerateMapListing(14);
			//	AddPageButton("<basefont color=#FFFFFF>Guardia Dungeons</basefont>", null, p++, 14);
			//}
			//if (WO.GuardiaShrines)
			//{
			//	GenerateMapListing(15);
			//	AddPageButton("<basefont color=#FFFFFF>Guardia Shrines</basefont>", null, p++, 15);
			//}
		}

		private void AddPageButton(string text, Map map, int offset, int page)
		{
			string label;
			if (map != null)
				label = String.Format("<basefont color=#{0}>{1}</basefont>", MapHue(map), text);

			else
				label = text;
			AddButton(67, 70 + ((offset - 1) * 25), 2117, 2118, page, GumpButtonType.Reply, 0);  //Map/Facet Button
			AddHtml(87, 70 + ((offset - 1) * 25), 150, 20, label, false, false);  // Map/Facet Name

		}

		private static OmniEntry GetEntry(string name, int id)
		{
			OmniEntry[] oe = (OmniEntry[])WorldOmniporter.GlobalEntries[name];

			if (oe != null)
			{
				if (id < 0 || id >= oe.Length)
					id = 0;
				return oe[id];
			}

			return null;
		}

		private void GenerateMapListing(int page)
		{
			if (m_Page == 0)
				m_Page = page;
			else if (page != m_Page)
				return;

			string name = m_Entries[page - 1];

			OmniEntry[] oe = (OmniEntry[])WorldOmniporter.GlobalEntries[name];
			if (oe == null)
				return;

			int offset = m_Page * 100;
			bool gates = name == "Public Omniportals";
			for (int i = 0, l = 0; i < oe.Length; i++)
			{
				OmniEntry entry = oe[i];

				if ((gates || name == "Trammel") && entry.Map == Map.Trammel && (!m_WO.Trammel || !m_Reds))
					continue;
				else if ((gates || name == "Edon Island") && entry.Map == Map.Felucca && (!m_WO.Felucca || !m_Reds))
					continue;
				else if ((gates || name == "Elven Island") && entry.Map == Map.Felucca && (!m_WO.Felucca || !m_Reds))
					continue;
				else if ((gates || name == "Tokuno Island") && entry.Map == Map.Felucca && (!m_WO.Felucca || !m_Reds))
					continue;
				else if ((gates || name == "Eterna Island") && entry.Map == Map.Felucca && (!m_WO.Felucca || !m_Reds))
					continue;
				else if ((gates || name == "Narolia Island") && entry.Map == Map.Felucca && (!m_WO.Felucca || !m_Reds))
					continue;
				//else if (entry.Map == Map.Ilshenar && (!m_WO.Ilshenar || !m_HasLBR || !m_Reds))
				//	continue;
				//else if (entry.Map == Map.Malas && (!Core.AOS || !m_HasAOS || !m_WO.Malas || !m_Reds))
				//	continue;
				//else if (entry.Map == Map.Tokuno && (!Core.SE || !m_HasSE || !m_WO.Tokuno || !m_Reds))
				//	continue;
				//else if (entry.Map == Map.TerMur && (!Core.SA || !m_HasSA || !m_WO.TerMur || !m_Reds))
				//	continue;

				else
				{
					string label = String.Format("<basefont color=#{0}>{1}</basefont>", MapHue(entry.Map), entry.Name);
					if (m_Staff) AddHtml(243, 70 + (l * 20), 150, 20, label, false, false); //Staff Location Display Name
					else AddHtml(273, 70 + (l * 20), 150, 20, label, false, false);  //Player Location Display Name
					if (m_Staff) AddLabel(365, 70 + (l * 20), 1152, entry.ToCount.ToString());  // Staff - Player Location Usage Count To
					if (m_Staff) AddLabel(405, 70 + (l * 20), 1152, entry.FromCount.ToString());  // Staff - Player Location Usage Count From
					if (m_Staff) AddButton(208, 70 + (l * 20), 4015, 4016, (i + offset), GumpButtonType.Reply, 0);  //Staff Location Buttons
					else AddButton(238, 70 + (l * 20), 4015, 4016, (i + offset), GumpButtonType.Reply, 0);  //Player Location Buttons
					l++;
				}
			}
		}

		private string MapHue(Map map)
		{
			if (map == Map.Felucca)
				return "ff9999";
			else
				return "FFFFFF";
		}

		private static string[] m_Entries = new string[]
		{
			"Trammel", "Trammel Dungeons", "Public Omniportals", "Edon Island", "Elven Island", "Tokuno Island", 
			"Eterna Island", "Narolia Island", 
			"Felucca Dungeons", "Ilshenar", "Ilshenar Shrines", "Malas",
			"Tokuno", "TerMur", "Custom", "Guardia Towns", "Guardia Sites",
			"Guardia Dungeons", "Guardia Shrines"
		};

		public override void OnResponse(NetState state, RelayInfo info)
		{
			pets = false;
			go = 1;
			Mobile from = state.Mobile;
			string msg = "You may use the Omniporter now.";

			foreach (Mobile mob in from.GetMobilesInRange(2))
			{
				if (mob is BaseCreature)
				{
					BaseCreature pet = (BaseCreature)mob;

					if (pet.Controlled && pet.ControlMaster == from)
					{
						if (pet.ControlOrder == OrderType.Guard || pet.ControlOrder == OrderType.Follow || pet.ControlOrder == OrderType.Come)
						{
							move.Add(pet);
							go++;
						}
					}
				}
			}
			if (go > 1) pets = true;

			string message = m_WO.NextUseMessage(from, go, pets);

			if (info.ButtonID <= 0 || from == null || from.Deleted || m_WO == null || m_WO.Deleted)
				return;

			int id = info.ButtonID / 100;
			int count = info.ButtonID % 100;

			if (id == 0 && count <= m_Entries.Length + 1)
			{
				from.SendGump(new WorldOmniporterGump(from, m_WO, count));
				return;
			}

			//Invalid checks
			if (id < 1 || id > m_Entries.Length + 1
			/* || (id == 10 && !m_Staff) */ /// -- This is only needed if you have a Staff-only location on the Omniporter
			)
				id = 1;

			string name = m_Entries[id - 1];

			m_Entry = GetEntry(name, count);

			bool gates = name == "Public Omniportals";

			if (m_Entry == null)
				from.SendMessage("Error: Invalid Button Response - No Map Entries");
			else if (((gates || name == "Edon Island") && m_Entry.Map == Map.Felucca && !m_WO.Felucca))
				from.SendMessage("Error: Invalid Button Response - Felucca Disabled");
			else if (((gates || name == "Elven Island") && m_Entry.Map == Map.Felucca && !m_WO.Felucca))
				from.SendMessage("Error: Invalid Button Response - Felucca Disabled");
			else if (((gates || name == "Tokuno Island") && m_Entry.Map == Map.Felucca && !m_WO.Felucca))
				from.SendMessage("Error: Invalid Button Response - Felucca Disabled");
			else if (((gates || name == "Eterna Island") && m_Entry.Map == Map.Felucca && !m_WO.Felucca))
				from.SendMessage("Error: Invalid Button Response - Felucca Disabled");
			else if (((gates || name == "Narolia Island") && m_Entry.Map == Map.Felucca && !m_WO.Felucca))
				from.SendMessage("Error: Invalid Button Response - Felucca Disabled");
			else if ((gates || name == "Trammel") && m_Entry.Map == Map.Trammel && (!m_WO.Trammel || !m_Reds))
				from.SendMessage("Error: Invalid Button Response - Trammel Disabled");
			//else if ((name == "Ilshenar") && m_Entry.Map == Map.Ilshenar && (!m_WO.Ilshenar || !m_HasLBR || !m_Reds))
			//	from.SendMessage("Error: Invalid Button Response - Ilshenar Disabled");
			//else if (m_Entry.Map == Map.Malas && (!Core.AOS || !m_HasAOS || !m_WO.Malas || !m_Reds))
			//	from.SendMessage("Error: Invalid Button Response - Malas Disabled");
			//else if (m_Entry.Map == Map.Tokuno && (!Core.SE || !m_HasSE || !m_WO.Tokuno || !m_Reds))
			//	from.SendMessage("Error: Invalid Button Response - Tokuno Disabled");
			//else if (m_Entry.Map == Map.TerMur && (!Core.SA || !m_HasSA || !m_WO.TerMur || !m_Reds))
			//	from.SendMessage("Error: Invalid Button Response - TerMur Disabled");
			else if (!from.InRange(m_WO.GetWorldLocation(), 1) || from.Map != m_WO.Map)
				from.SendLocalizedMessage(1019002);                 // You are too far away to use the gate.
			else if (from.Criminal)
				from.SendLocalizedMessage(1005561, "", 0x22);   // Thou'rt a criminal and cannot escape so easily.
			else if (Server.Spells.SpellHelper.CheckCombat(from))
				from.SendLocalizedMessage(1005564, "", 0x22);   // Wouldst thou flee during the heat of battle??
			else if (from.Spell != null)
				from.SendLocalizedMessage(1049616);                 // You are too busy to do that at the moment.
			else if (from.Map == m_Entry.Map && from.InRange(m_Entry.Destination, 1))
				from.SendLocalizedMessage(1019003);                 // You are already there.
			else if (message == msg)
				DoTravel(from);                                 // --- Travel ---
			else if (m_WO.MustWait(from))
				from.SendMessage(message);                      // Have to wait for some reason...
			else if (!m_WO.CanUse(from, pets))
				from.SendMessage(message);                      // You may not use the Omniporter...
			else if (m_WO.MustPay(from, pets))                  // Have to pay to use the Omniporter...
				from.SendGump(new ConfirmGump("Confirm!", 32767, message,
					32767, 300, 300, new ConfirmGumpCallback(PaymentConfirm_Callback)));
			else
				from.SendMessage(message);                      // You may not use the Omniporter...
		}

		public void PaymentConfirm_Callback(Mobile from, bool okay)
		{
			if (okay)
			{
				Container pack = from.Backpack;
				//Item[] items = pack.FindItemsByType(typeof(MasterStorage));

				if (pack != null && pack.ConsumeTotal(typeof(Gold), m_WO.GetPrice(from, go, pets)))
					DoTravel(from);

				// --- Travel ---
				else if (from.Account.TotalGold >= m_WO.GetPrice(from, go, pets))
				{
					from.SendMessage("Safe Travels - funds taken from Bank balance");
					from.Account.WithdrawGold(m_WO.GetPrice(from, go, pets));
					DoTravel(from);
					return;
				}
				//else if (items.Length != 0)
				//{
				//	MasterStorage masterStorage = (MasterStorage)items[0];
				//	if (masterStorage.GoldAmount >= (ulong)m_WO.GetPrice(from, go, pets))
				//	{
				//		from.SendMessage("Safe Travels - funds taken from Master storage");
				//		masterStorage.GoldAmount -= (ulong)m_WO.GetPrice(from, go, pets);
				//		DoTravel(from);
				//	}
				//	else
				//	{
				//		from.SendLocalizedMessage(500192);          //Begging thy pardon...
				//	}
				//}
				else
				{
					from.SendLocalizedMessage(500192);          //Begging thy pardon...
				}
			}
			else
				from.SendMessage("You decide to stay.");
		}

		public OmniEntry SourceEntry(Mobile from)
		{
			OmniEntry[] oe;

			for (int x = 0; x < WorldOmniporter.GlobalEntries.Count; x++)
			{
				oe = (OmniEntry[])WorldOmniporter.GlobalEntries[m_Entries[x]];
				//for (int y = 0; y < oe.Length; y++)
				//{
				//	OmniEntry entry = oe[y];
				//	if (from.Map == entry.Map && from.InRange(entry.Destination, 2))
				//	{
				//		return entry;
				//	}
				//}
			}
			return null;
		}

		public void DoTravel(Mobile from)
		{
			OmniEntry source = SourceEntry(from);
			from.MoveToWorld(m_Entry.Destination, m_Entry.Map);

			if (pets) foreach (Mobile mob in move)
					mob.MoveToWorld(m_Entry.Destination, m_Entry.Map);

			if (!from.Hidden || from.AccessLevel == AccessLevel.Player)
				Effects.PlaySound(m_Entry.Destination, m_Entry.Map, 0x1FE);
			from.Combatant = null;
			m_WO.LocalUses += go;
			m_Entry.ToCount += go;
			if (source != null) source.FromCount += go;
			//else Console.WriteLine("Source Omniporter was null.");
			m_WO.RaiseUses(from, go);
		}
	}

	public class OmniEntry
	{
		private string m_Name;
		private Point3D m_Destination;
		private Map m_Map;
		private int m_ToCount;
		private int m_FromCount;

		public string Name { get { return m_Name; } }
		public Point3D Destination { get { return m_Destination; } }
		public Map Map { get { return m_Map; } }
		public int ToCount { get { return m_ToCount; } set { m_ToCount = value; } }
		public int FromCount { get { return m_FromCount; } set { m_FromCount = value; } }

		private bool m_Moongate;
		public bool Moongate { get { return m_Moongate; } set { m_Moongate = value; } }

		public OmniEntry(string name, Point3D p, Map map) : this(name, p, map, false)
		{
		}

		public OmniEntry(string name, Point3D p, Map map, bool moongate)
		{
			m_Name = name;
			m_Destination = p;
			m_Map = map;
			m_ToCount = 0;
			m_FromCount = 0;
			m_Moongate = moongate;
		}
	}

	public class WorldOmniGenCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register("WorldOmniDel", AccessLevel.Administrator, new CommandEventHandler(WorldOmniDel_OnCommand));
			CommandSystem.Register("WorldOmniGen", AccessLevel.Administrator, new CommandEventHandler(WorldOmniGen_OnCommand));
			CommandSystem.Register("WorldOmniGen2", AccessLevel.Administrator, new CommandEventHandler(WorldOmniGen2_OnCommand));
		}

		[Usage("WorldOmniDel")]
		[Description("Deletes Nonmovable world Omniporters.")]
		public static void WorldOmniDel_OnCommand(CommandEventArgs e)
		{
			DeleteWorldOmni();
		}

		[Usage("WorldOmniGen")]
		[Description("Generates world Omniporters. Removes all old non movable world Omniporters.")]
		public static void WorldOmniGen_OnCommand(CommandEventArgs e)
		{
			World.Broadcast(0x35, true, "Generating world Omniporters.");
			DeleteWorldOmni();
			World.Broadcast(0x35, true, "Finished generating {0} world Omniporters.", WorldOmniporter.GenerateWorldOmniporters());
			//World.Broadcast(0x35, true, "Finished generating {0} world Omniporters.", WorldOmniporterFelucca.GenerateWorldOmniporterFeluccas());
		}
		[Usage("WorldOmniGen2")]
		[Description("Lets spice this b!tch up")]
		private static void WorldOmniGen2_OnCommand(CommandEventArgs e)
		{
			var prefixes = new[] { "worldomniporter", "worldomniporterfelucca", "worldomniportercustom", "worldomniportermalas" };
			var commands = new[]
			{
				"WorldOmniGen",
				"global set useglobal false itemid 19403 hue 1923 WO_AllPayToUse true WO_OnlyAllowYoung false where worldomniporter",
				//Lets remove duplicate worldomniporter Trammel public moon gates
				"global remove where worldomniporter x = 1336 y = 1997 z = 5 map = 1",
				"global remove where worldomniporter x = 3450 y = 2677 z = 25 map = 1",
				"global remove where worldomniporter x = 1499 y = 3771 z = 5 map = 1",
				"global remove where worldomniporter x = 3563 y = 2139 z = 34 map = 1",
				"global remove where worldomniporter x = 2701 y = 692 z = 5 map = 1",
				"global remove where worldomniporter x = 4467 y = 1283 z = 5 map = 1",
				"global remove where worldomniporter x = 643 y = 2067 z = 5 map = 1",
				"global remove where worldomniporter x = 1828 y = 2948 z = -20 map = 1",
				"global remove where worldomniporter x = 771 y = 752 z = 5 map = 1",
				//Lets remove duplicate worldomniporter Felucca public moon gates
				"global remove where worldomniporter x = 1336 y = 1997 z = 5 map = 0",
				"global remove where worldomniporter x = 2711 y = 2234 z = 0 map = 0",
				"global remove where worldomniporter x = 1499 y = 3771 z = 5 map = 0",
				"global remove where worldomniporter x = 3563 y = 2139 z = 34 map = 0",
				"global remove where worldomniporter x = 2701 y = 692 z = 5 map = 0",
				"global remove where worldomniporter x = 4467 y = 1283 z = 5 map = 0",
				"global remove where worldomniporter x = 643 y = 2067 z = 5 map = 0",
				"global remove where worldomniporter x = 1828 y = 2948 z = -20 map = 0",
				"global remove where worldomniporter x = 771 y = 752 z = 5 map = 0",

				//Lets remove duplicate worldomniporterFelucca Trammel public moon gates
				"global remove where worldomniporterFelucca x = 1336 y = 1997 z = 5 map = 1",
				"global remove where worldomniporterFelucca x = 3450 y = 2677 z = 25 map = 1",
				"global remove where worldomniporterFelucca x = 1499 y = 3771 z = 5 map = 1",
				"global remove where worldomniporterFelucca x = 3563 y = 2139 z = 34 map = 1",
				"global remove where worldomniporterFelucca x = 2701 y = 692 z = 5 map = 1",
				"global remove where worldomniporterFelucca x = 4467 y = 1283 z = 5 map = 1",
				"global remove where worldomniporterFelucca x = 643 y = 2067 z = 5 map = 1",
				"global remove where worldomniporterFelucca x = 1828 y = 2948 z = -20 map = 1",
				"global remove where worldomniporterFelucca x = 771 y = 752 z = 5 map = 1",
				//Lets remove duplicate worldomniporterFelucca Felucca public moon gates
				"global remove where worldomniporterFelucca x = 1336 y = 1997 z = 5 map = 0",
				"global remove where worldomniporterFelucca x = 2711 y = 2234 z = 0 map = 0",
				"global remove where worldomniporterFelucca x = 1499 y = 3771 z = 5 map = 0",
				"global remove where worldomniporterFelucca x = 3563 y = 2139 z = 34 map = 0",
				"global remove where worldomniporterFelucca x = 2701 y = 692 z = 5 map = 0",
				"global remove where worldomniporterFelucca x = 4467 y = 1283 z = 5 map = 0",
				"global remove where worldomniporterFelucca x = 643 y = 2067 z = 5 map = 0",
				"global remove where worldomniporterFelucca x = 1828 y = 2948 z = -20 map = 0",
				"global remove where worldomniporterFelucca x = 771 y = 752 z = 5 map = 0",

				//Lets remove duplicate worldomniporterIlshenar Ilshenar public moon gates
				"global remove where worldomniporter x = 1215 y = 467 z = -13 map = 2",
				"global remove where worldomniporter x = 722 y = 1366 z = -60 map = 2",
				"global remove where worldomniporter x = 744 y = 724 z = -28 map = 2",
				"global remove where worldomniporter x = 281 y = 1016 z = 0 map = 2",
				"global remove where worldomniporter x = 987 y = 1011 z = -32 map = 2",
				"global remove where worldomniporter x = 1174 y = 1286 z = -30 map = 2",
				"global remove where worldomniporter x = 1532 y = 1340 z = -3 map = 2",
				"global remove where worldomniporter x = 528 y = 216 z = -45 map = 2",
				"global remove where worldomniporter x = 1721 y = 218 z = 96 map = 2",

				//Lets remove duplicate Malas public moon gates
				"global remove where worldomniporter x = 1997 y = 1386 z = -85 map = 3",
				"global remove where worldomniporter x = 1015 y = 527 z = -65 map = 3",
				"global remove where worldomniporter x = 951 y = 494 z = -70 map = 3",
				//Lets remove duplicate Tokuno public moon gates
				"global remove where worldomniporter x = 1169 y = 998 z = 41 map = 4",
				"global remove where worldomniporter x = 802 y = 1204 z = 25 map = 4",
				"global remove where worldomniporter x = 270 y = 628 z = 15 map = 4",
				//Lets remove duplicate TerMur public moon gates
				"global remove where worldomniporter x = 852 y = 3526 z = -43 map = 5",
				"global remove where worldomniporter x = 719 y = 1863 z = 40 map = 5",
				//Lets set some baseprices to help build some RPG
				"global set baseprice 0 where worldomniporter map is trammel",
				"global set baseprice 100 where worldomniporter map is felucca",
				"global set baseprice 150 where worldomniporter map is malas",
				"global set baseprice 200 where worldomniporter map is termur",
				"global set baseprice 250 where worldomniporter map is tokuno",
				"global set baseprice 250 where worldomniporter map is ilshenar",
			};

			foreach (var cmd in commands)
			{
				var success = CommandSystem.Handle(e.Mobile, cmd, MessageType.Command);

				if (!success)
				{
					// failure case?
				}
			}
		}
		private static void DeleteWorldOmni()
		{
			ArrayList olist = new ArrayList();

			foreach (Item item in World.Items.Values)
			{
				if ((item is WorldOmniporter) && !item.Movable)
					olist.Add(item);
			}

			foreach (Item item in olist)
				item.Delete();

			if (olist.Count > 0)
				World.Broadcast(0x35, true, "{0} world Omniporters removed.", olist.Count);
		}
	}
}