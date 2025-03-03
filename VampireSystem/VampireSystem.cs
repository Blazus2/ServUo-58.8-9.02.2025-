using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
using Server.Multis;


namespace Server.Misc
{
    public class VampireSystem
    {
        //Setup Section
        private static bool m_Vampires = true;				// If false, after logout players with vampirism will lost their vampire powers and modyfication (simply vampire system reset)
		private static bool m_Werewolves = true;			// Same like before, but for werewolves
		private static bool m_ShowTitles = false;			// Showing "the Vampire" and "the Werewolf" titles

		//Vampires
		private static bool m_SunDamage = true;				// If true, vampire will receive damages being outside of player house or dungeon beetwen 6am to 6pm
        private static int m_MinSun = 2;					// Minimum damage made by sun in one hit
		private static int m_MaxSun = 10;					// Maximum damage made by sun in one hit

        private static int m_VampireStr = 15;				// Strenght bonus for vampires
        private static int m_VampireDex = 5;				// Dexterity bonus for vampires
        private static int m_VampireInt = 5;				// Inteligence bonus for vampires  

		//Werewolves
        private static int m_WerewolfStr = 40;				// Strenght bonus for werewolves
        private static int m_WerewolfDex = 50;				// Dexterity bonus for werewolves
        private static int m_WerewolfInt = 25;				// Inteligence bonus for werewolves 




        //Setup Section End

        public static bool Vampires { get { return m_Vampires; } }
		public static bool Werewolves { get { return m_Werewolves; } }
		public static bool ShowTitles { get { return m_ShowTitles; } } 
		public static bool SunDamage { get { return m_SunDamage; } } 
		public static int MinSun { get { return m_MinSun; } }
		public static int MaxSun { get { return m_MaxSun; } }
        public static int VampireStr { get { return m_VampireStr; } }
        public static int VampireDex { get { return m_VampireDex; } }
        public static int VampireInt { get { return m_VampireInt; } } 
		public static int WerewolfStr { get { return m_WerewolfStr; } }
        public static int WerewolfDex { get { return m_WerewolfDex; } }
        public static int WerewolfInt { get { return m_WerewolfInt; } }
	}
}
		