using UnityEngine;

/// <summary>
/// Items available are held as strings for indexes and names.
/// For example, we have two items: 0 - blue_pants and 2 - green_pants.
/// Thus, we keep ItemsAvailableIndex as "0,2" and
/// ItemsAvailableName as "blue_pants,green_pants"
/// Then, in GameManager we split those strings in order to get arrays of indexes and names
/// and use them in the future
/// </summary>

namespace DataArchitecture
{
    public class XLNGRepository : Repository
    {
        private const string KEY_CRYSTALS = "KEY_CRYSTALS";
        private const string KEY_COINS = "KEY_COINS";
        private const string KEY_WORDSBANKLEVELPROGRESS = "KEY_WORDSBANKLEVELPROGRESS";
        


        public int Crystals { get; set; }
        public int Coins { get; set; }
        public int WordsBankLevelProgress { get; set; }

        public override void Initialize()
        {
            Crystals = PlayerPrefs.GetInt(KEY_CRYSTALS, 0);
            Coins = PlayerPrefs.GetInt(KEY_COINS, 0);
            WordsBankLevelProgress = PlayerPrefs.GetInt(KEY_WORDSBANKLEVELPROGRESS, 0);
            
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(KEY_CRYSTALS, Crystals);
            PlayerPrefs.SetInt(KEY_COINS, Coins);
            PlayerPrefs.SetInt(KEY_WORDSBANKLEVELPROGRESS, WordsBankLevelProgress);


        }
    }
}
