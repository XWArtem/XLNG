using System;

namespace DataArchitecture
{
    public class ClothInteractor : Interactor
    {
        private ClothRepository clothRepository;
        public string ItemsAvailableIndex => clothRepository._itemsAvailableIndex;
        public string ItemsAvailableName => clothRepository._itemsAvailableName;

        public int CurrentItemHead => clothRepository.CurrentItemTop;
        public int CurrentItemBody => clothRepository.CurrentItemBottom;

        public ClothInteractor(ClothRepository clothRepo)
        {
            clothRepository = clothRepo;
        }

        public string GetBodyPartName(string bodyPart, int itemIndex)
        {
            if (itemIndex == 0)
            {
                return $"{bodyPart}NoTexture";
            }
            else
            {
                return $"{bodyPart}" + clothRepository.itemsNamesList[itemIndex - 1].Item2;
            }
        }

        public void AddItem(int itemIndex, string itemName)
        {
            clothRepository._itemsAvailableIndex += "," + itemIndex.ToString();
            clothRepository._itemsAvailableName += "," + itemName;
            clothRepository.Save();
        }

        public void ResetAllStats()
        {
            clothRepository._itemsAvailableIndex = "0";
            clothRepository._itemsAvailableName = "NoTexture";
            clothRepository.CurrentItemTop = 0;
            clothRepository.CurrentItemBottom = 0;
            clothRepository.Save();
        }
        public void ItemsChanged(int itemIndex, bool isBottom)
        {
            if (isBottom)
            {
                clothRepository.CurrentItemBottom = itemIndex;
            }
            else
            {
                clothRepository.CurrentItemTop = itemIndex;
            }
            clothRepository.Save();
        }

        public bool ItemIsAvailable(int itemIndex)
        {
            return Array.Exists(clothRepository.itemsAvailableIndexArr, x => x == itemIndex);
        }

        public void ReadItemsAvailable()
        {
            clothRepository.ReadItemsAvailable();
        }

    }
}
