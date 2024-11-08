using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TowerDefence
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset asset;
        [SerializeField] private Image upgradeIcon;
        private int costNumber = 0;
        [SerializeField] private TMP_Text level, costText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button buyMannaButton;

        public UpgradeAsset Asset => asset;
        public void Initialize()
        {
            upgradeIcon.sprite = asset.sprite;
            var savedLevel = Upgrades.GetUpgradeLevel(asset);

            if (savedLevel >= asset.costByLevel.Length)
            {
                level.text = $"Lvl: {savedLevel} (Max)";
                buyButton.interactable = false;
                buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
                buyButton.transform.Find("Text (TMP)").gameObject.SetActive(false);
                costText.text = "X";
                costNumber = int.MaxValue;
            }
            else
            {
                level.text = $"Lvl: {savedLevel + 1}";
                costNumber = asset.costByLevel[savedLevel];
                costText.text = costNumber.ToString();
                
            }
        }     

        public void CheckCost(int money)
        {
            buyButton.interactable = money >= costNumber;
        }
        public void CheckMannaCost(int manna)
        {
            buyMannaButton.interactable = manna >= costNumber;
        }
        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }
    }
}
