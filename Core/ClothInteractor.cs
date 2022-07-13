namespace DataArchitecture
{

    public class ClothInteractor : Interactor
    {
        private ClothRepository clothRepository;

        

        public ClothInteractor(ClothRepository clothRepo)
        {
            clothRepository = clothRepo;
        }

        //public List<Tuple<int, string, int>> itemsNamesList => clothRepository.itemsNamesList;

        public string BodyChange(int itemIndex)
        {
            if (itemIndex == 0)
            {
                return "BodyNoTexture";
            }
            else
            {
                return "Body" + clothRepository.itemsNamesList[itemIndex - 1].Item2;
            }
        }
        public string LeftLegChange(int itemIndex)
        {
            if (itemIndex == 0)
            {
                return "LeftLegNoTexture";
            }
            else
            {
                return "LeftLeg" + clothRepository.itemsNamesList[itemIndex - 1].Item2; // decrease index for 1 due the clothRepo starts with 1
            }
        }
        public string RightLegChange(int itemIndex)
        {
            if (itemIndex == 0)
            {
                return "RightLegNoTexture";
            }
            else
            {
                return "RightLeg" + clothRepository.itemsNamesList[itemIndex - 1].Item2;
            }
        }
        //
        public string HeadChange(int itemIndex)
        {
            if (itemIndex == 0) // that means we have no top item on our hero
            {
                return "HeadNoTexture";
            }
            else
            {
                return "Head" + clothRepository.itemsNamesList[itemIndex - 1].Item2;
            }
        }
    }
}
