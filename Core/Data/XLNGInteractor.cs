namespace DataArchitecture

{
    public class XLNGInteractor : Interactor
    {
        private XLNGRepository xlngRepository;
        public int Coins => xlngRepository.Coins;
        public int Crystals => xlngRepository.Crystals;
        public int WordsBankLevelProgress => xlngRepository.WordsBankLevelProgress;

        public string ItemsAvailableIndex => xlngRepository.ItemsAvailableIndex;
        public string ItemsAvailableName => xlngRepository.ItemsAvailableName;

        public int CurrentItemHead => xlngRepository.CurrentItemTop;
        public int CurrentItemBody => xlngRepository.CurrentItemBottom;

        public XLNGInteractor(XLNGRepository XLNGRepository)
        {
            xlngRepository = XLNGRepository;
        }
        

        public bool IsEnoughCrystals(int value)
        {
            return Crystals >= value;
        }
        
        public bool IsEnoughCoins(int value)
        {
            return Coins > value;
        }
        public void AddCrystals(object sender, int value)
        {
            xlngRepository.Crystals += value;
            xlngRepository.Save();
        }
        public void SpendCrystals(object sender, int value)
        {
            xlngRepository.Crystals -= value;
            xlngRepository.Save();
        }
        public void AddCoins(object sender, int value)
        {
            xlngRepository.Coins += value;
            xlngRepository.Save();
        }
        public void SpendCoins(object sender, int value)
        {
            xlngRepository.Coins -= value;
            xlngRepository.Save();
        }
        
        public void LevelProgressUp()
        {
            xlngRepository.WordsBankLevelProgress++;
            xlngRepository.Save();
        }

        public void AddItem(int itemIndex, string itemName)
        {
            xlngRepository.ItemsAvailableIndex += "," + itemIndex.ToString();
            xlngRepository.ItemsAvailableName += "," + itemName;
            xlngRepository.Save();
        }

        public void ResetAllStats()
        {
            xlngRepository.Crystals = 0;
            xlngRepository.Coins = 0;
            xlngRepository.WordsBankLevelProgress = 0;
            xlngRepository.ItemsAvailableIndex = "";
            xlngRepository.ItemsAvailableName = "";
            xlngRepository.CurrentItemTop = 0;
            xlngRepository.CurrentItemBottom = 0;
            xlngRepository.Save();
        }
        public void ItemsChanged(int itemIndex, bool isBottom)
        {
            if (isBottom)
            {
                xlngRepository.CurrentItemBottom = itemIndex;
            }
            else
            {
                xlngRepository.CurrentItemTop = itemIndex;
            }
            xlngRepository.Save();
        }
    }
}