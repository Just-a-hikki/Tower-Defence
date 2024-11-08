using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace TowerDefence
{
    public class UpgradeShop : MonoBehaviour
    {

        [SerializeField] private int money;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private BuyUpgrade[] sales;
        private void Start()
        {
            foreach (var slot in sales)
            {
                slot.Initialize();
                slot.transform.Find("Button").GetComponent<Button>().onClick
                    .AddListener(UpdateMoney);
            }
            UpdateMoney();

            Upgrades.UpgradeAssetInitialize(sales);
        }
        public void UpdateMoney()
        {
            money = MapCompletion.Instance.TotalScore;
            money -= Upgrades.GetTotalCost();
            moneyText.text = money.ToString();
            foreach (var slot in sales)
            {
                slot.CheckCost(money);
            }
        }
    }
}
