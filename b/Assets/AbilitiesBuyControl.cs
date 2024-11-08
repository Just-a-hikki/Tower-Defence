using TMPro;
using TowerDefence;
using UnityEngine;
using UnityEngine.UI;
using static TowerDefence.Abilities;

public class AbilitiesBuyControl : MonoBehaviour
{
    [SerializeField] private TimeAbility m_TimeAbilities;
    [SerializeField] private FireAbility m_FireAbilities;
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private Button m_button;

    public void BuyTime()
    {
        TDPlayer.Instance.TryUseTimeButton(m_TimeAbilities);
    }
    public void BuyFire()
    {
        TDPlayer.Instance.TryUseFireButton(m_FireAbilities);
    }



}
