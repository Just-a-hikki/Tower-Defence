using SpaceShooter;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;

namespace TowerDefence
{
    [RequireComponent(typeof(Destructible))]
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType { Base = 0, Magic = 1 }

        private static Func<int, TDProjectile.DamageType, int, int>[] ArmorDamageFunctions =
        { 
            (int power, TDProjectile.DamageType type, int armor) =>
            {//ArmorType.Base
                switch (type)
                {
                    case TDProjectile.DamageType.Magic: return power;
                    default: return Mathf.Max(power - armor, 1);
                }
            },
            (int power, TDProjectile.DamageType type, int armor) =>
            {//ArmorType.Magic
                if(TDProjectile.DamageType.Base == type)
                { 
                    armor = armor / 2; 
                }
                return Mathf.Max(power - armor, 1);
            }
        };

        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_gold = 1;
        [SerializeField] private int m_manna = 1;
        [SerializeField] private int m_armor = 1;
        [SerializeField] private ArmorType m_armorType;

        private Destructible m_destructible;

        private void Awake()
        {
            m_destructible = GetComponent<Destructible>();
        }

        public event Action OnEnd;
        private void OnDestroy()
        {
            OnEnd();
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.animations;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.radius;

            m_damage = asset.damage;
            m_armor = asset.armor;
            m_gold = asset.gold;
            m_manna = asset.manna;
            m_armorType = asset.armorType;
        }
        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }
        public void GivePlayerGold()
        {
            TDPlayer.Instance.ChangeGold(m_gold);
        }
        public void GivePlayerManna()
        {
            TDPlayer.Instance.ChangeManna(m_manna);
        }
        public void TakeDamage(int damage, TDProjectile.DamageType damageType)
        {
            m_destructible.ApplyDamage(
                ArmorDamageFunctions[(int)m_armorType](damage, damageType, m_armor));
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class  EnemyInspector: Editor 
    {
        public override void OnInspectorGUI()
        {
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
        
    }
#endif
}
