using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace VT.Audio
{
    public class MenuButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField] private AudioObject clickSound;
        [SerializeField] private AudioObject hoverSound;
        private Button m_button;

        public void Click() => AudioManager.PlaySound(clickSound);
        public void Hover() => AudioManager.PlaySound(hoverSound);
        
        public void OnPointerEnter(PointerEventData eventData) => Hover();
        public void OnPointerClick(PointerEventData eventData) => Click();
    }
}