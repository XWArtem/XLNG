using System;
using UnityEngine;

namespace DataArchitecture
{
    public class XLNGInteractor : Interactor
    {
        private XLNGRepository xlngRepository;
        public int Coins => xlngRepository.Coins;
        public int Crystals => xlngRepository.Crystals;
        public int WordsBankLevelProgress => xlngRepository.WordsBankLevelProgress;

        public delegate void CoinsAdded(int value);
        public CoinsAdded OnCoinsAdded;



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
            return Coins >= value;
        }
        public void AddCrystals(object sender, int value)
        {
            if (value > 0)
            {
                xlngRepository.Crystals += value;
                xlngRepository.Save();
            }
        }
        public void SpendCrystals(object sender, int value)
        {
            if (value > 0 && Crystals >= value)
            {
                xlngRepository.Crystals -= value;
                xlngRepository.Save();
            }
        }
        public void AddCoins(object sender, int value)
        {
            if (value > 0)
            {
                xlngRepository.Coins += value;
                OnCoinsAdded?.Invoke(value);
                xlngRepository.Save();
            }
        }
        public void SpendCoins(object sender, int value)
        {
            if (value > 0)
            {
                xlngRepository.Coins -= value;
                xlngRepository.Save();
            }
        }
        
        public void LevelProgressUp()
        {
            xlngRepository.WordsBankLevelProgress++;
            xlngRepository.Save();
        }

        public void ResetAllStats()
        {
            xlngRepository.Crystals = 0;
            xlngRepository.Coins = 0;
            xlngRepository.WordsBankLevelProgress = 0;
            xlngRepository.Save();
        }

    }
}