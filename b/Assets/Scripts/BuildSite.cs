using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] buildableTowers;
        public void SetBuildableTowers(TowerAsset[] towers) 
        {
            if(towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
                
            }
            else
            {
                buildableTowers = towers;
            }
            
        }
        public static event Action<BuildSite> OnClickEvent;
        public static void HideBuildControls()
        {
            OnClickEvent(null);
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}
