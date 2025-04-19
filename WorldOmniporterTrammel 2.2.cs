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
            Trammel = 0x00000001,
            TramDungeons = 0x00000002,
            Felucca = 0x00000004,
            PublicMoongates = 0x00000010,
            AllowReds = 0x00000200,
            UseGlobal = 0x00000800,
            WO_OnlyAllowYoung = 0x00001000,
            WO_FeluccaNoYoung = 0x00002000,
            WO_DungeonsNoYoung = 0x00004000,
            WO_AllPayToUse = 0x00008000,
            WO_SkillsCost = 0x00010000,
            WO_KarmaDiscount = 0x00020000,
            WO_TimeLimit = 0x00040000,
            WO_UsesLimit = 0x00080000,
            WO_DelayAfterUse = 0x00100000,
            WO_PayBeforeDelay = 0x00200000,
            WO_PetsMayTravel = 0x00400000,
            WO_PetsMustPay = 0x00800000
        }

        private static int m_GlobalBasePrice;
        private int m_BasePrice;
        private static int m_GlobalSkillsCostBonus;
        private int m_SkillsCostBonus;
        private static int m_GlobalKarmaDiscountBonus;
        private int m_KarmaDiscountBonus;
        private static TimeSpan m_GlobalTimeExpiration;
        private TimeSpan m_TimeExpiration;
        private static int m_GlobalMaxUses;
        private int m_MaxUses;
        private static TimeSpan m_GlobalTimeDelay;
        private TimeSpan m_TimeDelay;
        public static Hashtable GlobalMobileUse = new Hashtable();
        private Hashtable m_MobileUse = new Hashtable();
        private int m_LocalUses;
        private DateTime m_Birth = DateTime.Now;

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
            catch { }
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
            catch { }
            return ((UseEntry)GlobalMobileUse["0"]);
        }

        public void RegisterUse(Mobile from, UseEntry entry)
        {
            PlayerMobile m;
            if (from is PlayerMobile) m = from as PlayerMobile;
            else { return; }
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
            catch { }
        }
    }
}