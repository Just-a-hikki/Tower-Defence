using SpaceShooter;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence
{
    public class TDPatrolController : AIController
    {
        private Path m_path;
        private int pathIndex;
        [SerializeField] private UnityEvent OnEndPath;
        public void SetPath(Path NewPath)
        {
            m_path = NewPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_path[pathIndex]);
        }
        protected override void GetNewPoint()
        {
            pathIndex++;
            if (m_path.Length > pathIndex)
            {
                SetPatrolBehaviour(m_path[pathIndex]);
            }
            else
            {
                OnEndPath.Invoke();
                Destroy(gameObject);
            }
            base.GetNewPoint();
        }
    }
}
