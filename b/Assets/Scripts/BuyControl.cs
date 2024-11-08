using TowerDefence;
using UnityEngine;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerByControl m_TowerBuyPrefab;
        [SerializeField] private UpgradeAsset m_MagicTowerUpgrade;
        private List<TowerByControl> m_ActiveControl;
        private RectTransform m_RectTransform;
        //..//UnityEvents
        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
        //..//
        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                m_ActiveControl = new List<TowerByControl>();
                foreach (var asset in buildSite.buildableTowers)
                {
                    if (asset.IsAvailable())
                    {

                        var newControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (m_ActiveControl.Count > 0)
                {
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var Offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                        m_ActiveControl[i].transform.position += Offset;
                    }
                }

                foreach (var tbc in GetComponentsInChildren<TowerByControl>())
                {
                    tbc.SetBuildSite(buildSite.transform.root);
                }
            }
            else
            {
                if(m_ActiveControl != null)
                {
                    foreach (var control in m_ActiveControl) Destroy(control.gameObject);
                    m_ActiveControl?.Clear();
                }
                gameObject.SetActive(false);
            }
        }
    } 
}

