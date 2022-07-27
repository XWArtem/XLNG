using System.Collections.Generic;
using System;
using UnityEngine;

namespace DataArchitecture
{
    public class ClothRepository : Repository
    {
        private const string KEY_CURRENT_ITEM_TOP = "KEY_CURRENT_ITEM_TOP";
        private const string KEY_CURRENT_ITEM_BOTTOM = "KEY_CURRENT_ITEM_BOTTOM";
        private const string KEY_ITEMS_AVAILABLE_INDEX = "KEY_ITEMS_AVAILABLE_INDEX";
        private const string KEY_ITEMS_AVAILABLE_NAME = "KEY_ITEMS_AVAILABLE_NAME";

        // we hold indexes as a string like '1,2,4,8,19' as PlayerPrefs doesn't support arrays
        // 'available' means our Player bought it and can put in on
        public string _itemsAvailableIndex;
        public string _itemsAvailableName;

        public int[] itemsAvailableIndexArr;
        public string[] itemsAvailableNameArr;

        public int CurrentItemTop;
        public int CurrentItemBottom;

        public readonly List<Tuple<int, string, int, bool>> itemsNamesList = new List<Tuple<int, string, int, bool>>
        {
            // we start from 1 due to '0' is reserved for NoTextures
            // TRUE means BOTTOM item. FALSE means TOP item
                Tuple.Create(1, "BluePants", 100, true),
                Tuple.Create(2, "DarkBluePants", 200, true),
                Tuple.Create(3, "GreenCostume", 300, true),
                Tuple.Create(4, "IceHat", 500, false),
        };


        public override void Initialize()
        {
            _itemsAvailableIndex = PlayerPrefs.GetString(KEY_ITEMS_AVAILABLE_INDEX, "0");
            _itemsAvailableName = PlayerPrefs.GetString(KEY_ITEMS_AVAILABLE_NAME, "NoTexture");
            CurrentItemTop = PlayerPrefs.GetInt(KEY_CURRENT_ITEM_TOP, 0);
            CurrentItemBottom = PlayerPrefs.GetInt(KEY_CURRENT_ITEM_BOTTOM, 0);
            ReadItemsAvailable();
        }

        public override void Save()
        {
            PlayerPrefs.SetString(KEY_ITEMS_AVAILABLE_INDEX, string.Join(",", _itemsAvailableIndex));
            PlayerPrefs.SetString(KEY_ITEMS_AVAILABLE_NAME, string.Join(",", _itemsAvailableName));
            PlayerPrefs.SetInt(KEY_CURRENT_ITEM_TOP, CurrentItemTop);
            PlayerPrefs.SetInt(KEY_CURRENT_ITEM_BOTTOM, CurrentItemBottom);
            ReadItemsAvailable();
        }

        public void ReadItemsAvailable()
        {
            if (_itemsAvailableIndex.Length != 0)
            {
                if (_itemsAvailableIndex[0] == ',')
                {
                    _itemsAvailableIndex = _itemsAvailableIndex.Remove(0, 1);
                }
                string[] tempItemAvailableIndex = _itemsAvailableIndex.Split(',');
                itemsAvailableIndexArr = Array.ConvertAll(tempItemAvailableIndex, int.Parse);

                if (_itemsAvailableName[0] == ',')
                {
                    _itemsAvailableName = _itemsAvailableName.Remove(0, 1);
                }
                itemsAvailableNameArr = _itemsAvailableName.Split(',');
                //tempitemsAvailableName = itemsAvailableName.Split(new[] { ',' }, System.StringSplitOptions.None);
            }
            else
            {
                itemsAvailableIndexArr = new int[] { 0 };
                itemsAvailableNameArr = new string[] {"NoTexture"};
            }
        }
    }
}