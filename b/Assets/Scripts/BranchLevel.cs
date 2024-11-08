using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefence
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_pointText;
        [SerializeField] private MapLevel m_rootLevel;
        [SerializeField] private int m_needPoints = 3;

        /// <summary>
        /// Попытка активации ответвлённого уровня
        /// Активация требует наличия очков и выполнения прошлого уровня
        /// </summary>
        public void TryActivate()
        {
            gameObject.SetActive(m_rootLevel.IsComplete);
            if(m_needPoints > MapCompletion.Instance.TotalScore)
            {
                m_pointText.text = m_needPoints.ToString();
            }
            else
            {
                m_pointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
        }
    }
}
