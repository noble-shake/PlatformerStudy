using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas;
    Transform beforeParent;
    RectTransform rect;
    CanvasGroup canvasGroup;
    Image img;

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParent = transform.parent;


        // 밑으로 갈 수록 맨 위에 있을 것임.
        // transform.SetAsFirstSibling
        // transform.SetAsLastSibling();
        transform.SetParent(canvas);

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent == canvas) {
            transform.SetParent(beforeParent);
            rect.position = beforeParent.GetComponent<RectTransform>().position;
        }

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void SetItem(Sprite _spr) {
        //img.SetNativeSize();
        img.sprite = _spr;

    }

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
