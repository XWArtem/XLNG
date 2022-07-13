using System.Collections.Generic;
using System;

/// <summary>
/// It ONLY contains the data of all items in the game. It doesn't work with what our hero currently has etc.
/// TRUE in ITEM4 means bottom item. FALSE means top item
/// </summary>

namespace DataArchitecture
{

    public class ClothRepository : Repository
    {
        public Dictionary<int, string> BottomItemName = new Dictionary<int, string>()
        {
            {0, "BluePants"},
            {1, "DarkBluePants"},
            {2, "GreenCostume" }
        };

        public List<Tuple<int, string, int, bool>> itemsNamesList = new List<Tuple<int, string, int, bool>>
        {
            // we start from 1 due to '0' is reserved for NoTextures
                Tuple.Create(1, "BluePants", 100, true),
                Tuple.Create(2, "DarkBluePants", 200, true),
                Tuple.Create(3, "GreenCostume", 300, true),
                Tuple.Create(4, "IceHat", 500, false),


        };

        public override void Save()
        {

        }


        public override void Initialize()
        {

        }
    }
}