using System;
using UnityEngine;
using SpaceShooter;


namespace TowerDefence
{
    public class Upgrades : MonoSingleton<Upgrades>
    {
        public const string filename = "upgrades.dat";
        [Serializable]

        private class UpgradeSave
        {
            public UpgradeAsset asset;
            public int level = 0;
        }
        [SerializeField] UpgradeSave[] save;
        [SerializeField] private int totalUpgrade;
        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref save);
            foreach (var upgrade in save)
            {
                totalUpgrade += upgrade.level;
            }
        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if(upgrade.asset == asset)
                {
                    upgrade.level++;
                    Saver<UpgradeSave[]>.Save(filename, Instance.save);
                }
            }
        }
        public static void UpgradeAssetInitialize(BuyUpgrade[] assets)
        {
            if (Instance.save.Length < assets.Length)
            {
                Array.Resize(ref Instance.save, assets.Length);
            }
            for (int i = 0; i < assets.Length; i++)
            {
                Instance.save[i].asset = assets[i].Asset;
            }
        }
        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.save)
            {
                for (int i = 0; i < upgrade.level; i++)
                {
                    result += upgrade.asset.costByLevel[i];
                }
            }
            return result;
        }
        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.save)
            {
                if (upgrade.asset == asset)
                {
                    return upgrade.level;
                }
            }
            return 0;
        }
    }
}
