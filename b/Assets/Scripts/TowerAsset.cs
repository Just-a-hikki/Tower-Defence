using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int goldCost;
        public int mannaCost;
        public Sprite sprite;
        public Sprite GUISprite;
        public TurretProperties TurretProperties;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private int requiredUpgradeLevel;
        public bool IsAvailable() => !requiredUpgrade || 
            requiredUpgradeLevel <= Upgrades.GetUpgradeLevel(requiredUpgrade);
        public TowerAsset[] m_UpgradesTo;
    }
}
