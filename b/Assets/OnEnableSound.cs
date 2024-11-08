using UnityEngine;

namespace TowerDefence
{
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] private Sound m_sound;
        private void OnEnable()
        {
            m_sound.Play();
        }
    }
}
