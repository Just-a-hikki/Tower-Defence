using SpaceShooter;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] private int m_goldcost = 5;
            public int GoldCostFire => m_goldcost;
            [SerializeField] private int m_damage = 5;
            [SerializeField] private Color m_TargetingColor;
            [SerializeField] private TMP_Text m_textGold;
            
            
            public void Use()
            {
                if (TDPlayer.Instance.GoldT < GoldCostFire)
                {
                    Instance.m_FireButton.interactable = false;
                    m_textGold.color = Color.red;
                }
                if (TDPlayer.Instance.GoldT > GoldCostFire)
                {
                    Instance.m_FireButton.interactable = true;
                    m_textGold.color = Color.white;
                    ClickProtection.Instance.Activate((Vector2 v) =>
                    {
                        Vector3 position = v;
                        position.z = -Camera.main.transform.position.z;
                        position = Camera.main.ScreenToWorldPoint(position);
                        foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                        {
                            if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                            {
                                enemy.TakeDamage(m_damage, TDProjectile.DamageType.Magic);
                            }
                        }
                    });
                }
            }
        }
        [Serializable]
        public class TimeAbility
        {
            [SerializeField] private int m_mannaCost = 5; 
            public int MannaCost => m_mannaCost;
            [SerializeField] private float m_coolDown = 15f;
            private float per;
            [SerializeField] private float m_duration = 5;
            [SerializeField] private TMP_Text m_text;
            public void Use()
            {
                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfMaxLinearVelocity();
                }
                foreach(var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfMaxLinearVelocity();

                EnemyWaveManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWaveManager.OnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator TimeAbilityButton()
                {
                    Instance.m_TimeButton.interactable = false;
                    m_text.color = Color.red;
                        yield return new WaitForSeconds(m_coolDown);
                    if(TDPlayer.Instance.MannaT >= m_mannaCost)
                    {
                        m_text.color = Color.white;
                        Instance.m_TimeButton.interactable = true;
                    }
                    else if (TDPlayer.Instance.MannaT < m_mannaCost)
                    {
                        Instance.m_TimeButton.interactable = false;
                        m_text.color = Color.red;
                    }

                }
                Instance.StartCoroutine(TimeAbilityButton());

            }

        }
        [SerializeField] private Image m_targetingCircle; 
        [SerializeField] public Button m_TimeButton; 
        [SerializeField] public Button m_FireButton; 
        [SerializeField] private FireAbility m_FireAbility; 
        [SerializeField] private TimeAbility m_Abilities;
        public void UseFireAbility() => m_FireAbility.Use();

        [SerializeField] private TimeAbility m_TimeAbility;
        public void UseTimeAbility() => m_TimeAbility.Use();

        private void Start()
        {
            TDPlayer.Instance.CheckMannaLEVEL();
            TDPlayer.Instance.CheckFireLEVEL();

        }
    }
}
