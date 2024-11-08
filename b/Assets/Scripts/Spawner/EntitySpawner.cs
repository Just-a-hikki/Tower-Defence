using UnityEngine;
using TowerDefence;
using Unity.VisualScripting;

namespace SpaceShooter
{
    public class EntitySpawner : Spawner
    {
        [SerializeField] private GameObject[] m_EntityPrefabs;
        protected override GameObject GenerateSpawnEntity()
        {
            return(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }

    }
}