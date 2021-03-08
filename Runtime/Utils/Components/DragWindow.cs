using Elysium.Utils.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Elysium.Utils.Components
{
    public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField] private Canvas canvas = default;
        [SerializeField] private RectTransform rect = default;
        [SerializeField] private bool getCanvasInRoot = true;
        [SerializeField] private bool getRectInParent = false;

        private void Start()
        {
            if (getCanvasInRoot && canvas == null) { FindCanvasInRoot(); }
            if (getRectInParent && rect == null) { FindRectTransformInParent(); }
        }

        [ContextMenu("Get Canvas In Root")]
        private void FindCanvasInRoot()
        {
            if (canvas == null)
            {
                Transform testCanvasTransform = transform.parent;
                while (testCanvasTransform != null)
                {
                    canvas = testCanvasTransform.GetComponent<Canvas>();
                    if (canvas != null) { break; }
                    testCanvasTransform = testCanvasTransform.parent;
                }
            }
            canvas = transform.parent.root.GetComponent<Canvas>();
        }

        [ContextMenu("Get Rect In Parent")]
        private void FindRectTransformInParent()
        {
            if (rect == null)
            {
                rect = transform.parent.GetComponent<RectTransform>();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (transform is RectTransform == false) { throw new System.Exception($"No rectTransform attached to object {gameObject}. DragWindow script requires a rectTransform."); }
            if (canvas == null) { FindCanvasInRoot(); };
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            rect.SetAsLastSibling();
        }
    }
}