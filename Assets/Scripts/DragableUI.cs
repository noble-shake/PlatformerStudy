using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableUi : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas;
    Transform beforeParent;
    RectTransform rect;
    CanvasGroup canvasGroup;
    Image img;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParent = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas)//�߸� ��������
        {
            transform.SetParent(beforeParent);    
            rect.position = beforeParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    private void Awake()//�� ����� �����Ҷ�
    {
        //img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()//Ÿ ��ũ��Ʈ�� ����� �����ö�
    {
        canvas = FindObjectOfType<Canvas>().transform;
    }

    public void SetItem(Sprite _spr)
    {
        if (img == null)
        {
            img = GetComponent<Image>();
        }

        img.sprite = _spr;
        //img.SetNativeSize();
    }
}
