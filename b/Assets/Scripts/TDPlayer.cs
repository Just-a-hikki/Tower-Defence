using UnityEngine;
using SpaceShooter;
using System;
using UnityEngine.U2D;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using static TowerDefence.Abilities;
using TMPro;

namespace TowerDefence
{
    public class TDPlayer : Player
    {
        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer;
            }
        }
        private event Action<int> OnGoldUpdate;
        public void GoldUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        } 
        public void GoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldUpdate -= act;
            act(Instance.m_gold);
        } 
        public event Action<int> OnLifeUpdate;
        public event Action<int> OnMannaUpdate;
        public void LifeUpdateSubscribe(Action<int> act)
        {
            Instance.OnLifeUpdate += act;
            act(Instance.NumLives);
        }
        public void MannaUpdateSubscribe(Action<int> act)
        {
            OnMannaUpdate += act;
            act(Instance.m_mannaT);
        } 
        public void MannaUpdateUnSubscribe(Action<int> act)
        {
            OnMannaUpdate -= act;
            act(Instance.m_mannaT);
        }
        public void MoneyUpdateSubscribe(Action<int> act)
        {
            OnGoldUpdate += act;
            act(Instance.m_gold);
        }

        [SerializeField] private int m_gold = 0;
        public int GoldT => m_gold;
        [SerializeField] private int m_mannaT = 0;
        public int MannaT => m_mannaT;
        public void ChangeGold(int change)
        {
            m_gold += change;
            OnGoldUpdate(m_gold);
        }
        public void ChangeManna(int change)
        {
            m_mannaT += change;
            OnMannaUpdate(m_mannaT);
        }
        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(NumLives);
        }
        [SerializeField] private Tower m_towerPrefab;
        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.goldCost);
            var tower = Instantiate(m_towerPrefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);
            Destroy(buildSite.gameObject);
        }
        public void TryUseTimeButton(TimeAbility manna)
        {
            ChangeManna(-manna.MannaCost);
        }
        public void TryUseFireButton(FireAbility gold)
        {
            ChangeGold(-gold.GoldCostFire);
        }

        [SerializeField] private UpgradeAsset healthUpgrade;
        [SerializeField] private UpgradeAsset moneyUpgrade;
        [SerializeField] private UpgradeAsset TimeUpgrade;
        [SerializeField] private UpgradeAsset FireUpgrade;
        [SerializeField] private Button Slow;
        [SerializeField] private Button Fire;
        [SerializeField] private TMP_Text SlowMoneyText;
        [SerializeField] private TMP_Text FireMoneyText;
        private void Start()
        {
            SlowMoneyText.text = m_mannaT.ToString();
            FireMoneyText.text = m_mannaT.ToString();

            var level = Upgrades.GetUpgradeLevel(healthUpgrade);
            TakeDamage(-level * 5);
            var level1 = Upgrades.GetUpgradeLevel(moneyUpgrade);
            if (level1 == 0)
            {
                return;
            }
            else if (level1 > 0)
            {
                m_gold += level1 + 2;
            }
        }

        public void CheckMannaLEVEL()
        {
            var level3 = Upgrades.GetUpgradeLevel(TimeUpgrade);
            if (level3 == 0)
            {
                SlowMoneyText.color = Color.red;
                Slow.interactable = false;
            }
            else if (level3 > 0)
            {
                SlowMoneyText.color = Color.white;
                Slow.interactable = true;
                m_mannaT += level3 + 2;
            }
        } 
        public void CheckFireLEVEL()
        {
            var level3 = Upgrades.GetUpgradeLevel(FireUpgrade);
            if (level3 == 0)
            {
                FireMoneyText.color = Color.red;
                Fire.interactable = false;
            }
            else if (level3 > 0)
            {
                FireMoneyText.color = Color.white;
                Fire.interactable = true;
                m_mannaT += level3;
            }
        }
    }
}
