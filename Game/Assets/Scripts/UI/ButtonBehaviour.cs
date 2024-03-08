using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

public class Buttonbehavior : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IEndDragHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<AudioSource>().Play();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<AudioSource>().Play();
    }
}