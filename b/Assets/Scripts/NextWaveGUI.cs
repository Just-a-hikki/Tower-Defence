using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefence
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text bonusAmount;
        private EnemyWaveManager manager;
        private float timeToNextWave;

        private void Start()
        {
            manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if(bonus < 0) bonus = 0;
            bonusAmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }
    }
}
