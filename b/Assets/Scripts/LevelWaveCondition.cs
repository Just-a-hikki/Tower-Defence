using SpaceShooter;
using UnityEngine;

namespace TowerDefence
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        public bool isCompleted;
        void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                isCompleted = true;
            };
        }
        public bool IsCompleted { get { return isCompleted; } }
    }
}
