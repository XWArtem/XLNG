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
        private const string KEY_ITEMS_AVAILABLE_INDEX = "KEY_ITEMS_AVAILABLE_INDEX";
        private const string KEY_ITEMS_AVAILABLE_NAME = "KEY_ITEMS_AVAILABLE_NAME";
        private const string KEY_CURRENT_ITEM_TOP = "KEY_CURRENT_ITEM_TOP";
        private const string KEY_CURRENT_ITEM_BOTTOM = "KEY_CURRENT_ITEM_BOTTOM";

        public int Crystals { get; set; }
        public int Coins { get; set; }
        public int WordsBankLevelProgress { get; set; }


        // we hold indexes as a string like '1,2,4,8,19' as PlayerPrefs doesn't support arrays
        public string ItemsAvailableIndex;
        public string ItemsAvailableName;
        public int CurrentItemTop;
        public int CurrentItemBottom;

        public override void Initialize()
        {
            Crystals = PlayerPrefs.GetInt(KEY_CRYSTALS, 0);
            Coins = PlayerPrefs.GetInt(KEY_COINS, 0);
            WordsBankLevelProgress = PlayerPrefs.GetInt(KEY_WORDSBANKLEVELPROGRESS, 0);
            ItemsAvailableIndex = PlayerPrefs.GetString(KEY_ITEMS_AVAILABLE_INDEX, "");
            ItemsAvailableName = PlayerPrefs.GetString(KEY_ITEMS_AVAILABLE_NAME, "");
            CurrentItemTop = PlayerPrefs.GetInt(KEY_CURRENT_ITEM_TOP, 0);
            CurrentItemBottom = PlayerPrefs.GetInt(KEY_CURRENT_ITEM_BOTTOM, 0);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(KEY_CRYSTALS, Crystals);
            PlayerPrefs.SetInt(KEY_COINS, Coins);
            PlayerPrefs.SetInt(KEY_WORDSBANKLEVELPROGRESS, WordsBankLevelProgress);
            PlayerPrefs.SetString(KEY_ITEMS_AVAILABLE_INDEX, 
                string.Join("," ,ItemsAvailableIndex));
            
            PlayerPrefs.SetString(KEY_ITEMS_AVAILABLE_NAME,
                string.Join(",", ItemsAvailableName));

            PlayerPrefs.SetInt(KEY_CURRENT_ITEM_TOP, CurrentItemTop);
            PlayerPrefs.SetInt(KEY_CURRENT_ITEM_BOTTOM, CurrentItemBottom);

            Debug.Log("Now ItemsAvailableIndex is: " + ItemsAvailableIndex 
                + "\n and ItemsAvailableName is: " + ItemsAvailableName);

        }
    }
}
