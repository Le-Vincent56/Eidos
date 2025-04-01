using System;
using System.Collections.Generic;

namespace Eidos.Eidras
{
    public enum Stat
    {
        Vigor,
        Harmony,
        Might,
        Defense,
        Anima,
        Resolve,
        Grace
    }

    public class Statistics
    {
        private Dictionary<Stat, int> stats = new Dictionary<Stat, int>();

        public Statistics()
        {
            // ITerate through each Stat enum value
            foreach (Stat stat in Enum.GetValues(typeof(Stat)))
            {
                // Add the state with a base of 10
                stats.Add(stat, 10);
            }
        }

        /// <summary>
        /// Set the value of a stat
        /// </summary>
        public int GetStat(Stat stat)
        {
            // Exit case - if the stat does not exist, return 0
            if (!stats.TryGetValue(stat, out int value)) return 0;

            // Return the stat's value
            return value;
        }

        /// <summary>
        /// Set the value of a stat
        /// </summary>
        public void ModifyStat(Stat stat, int amount)
        {
            // Exit case - if the stat does not exist, return 0
            if (!stats.TryGetValue(stat, out int value)) return;

            // Update the value
            int newValue = value + amount;

            // Set the new value for the stat
            stats[stat] = newValue;
        }
    }
}
