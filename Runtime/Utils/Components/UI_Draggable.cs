using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elysium.Utils;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Elysium.Utils
{
    public class UI_Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public UI_DropZone.DropZoneType TargetZoneType = default;
        [SerializeField] private RectTransform transformToMove;
        [SerializeField] private bool duplicate = false;
        private new RectTransform transform;

        protected GameObject placeholder { get; set; }
        protected Vector2 offset { get; set; }
        protected Canvas canvas { get; set; }
        protected CanvasGroup group { get; set; }

        public event Action OnStartDrag;
        public event Action OnFinishDrag;
        public event Action OnDrop;

        private bool dragging = false;

        protected virtual void Start()
        {
            canvas = transformToMove.root.GetComponent<Canvas>();
        }

        protected virtual void SetupDraggable()
        {
            group = transform.gameObject.AddComponent<CanvasGroup>();
            group.blocksRaycasts = true;
        }

        #region ON_BEGIN_DRAG
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            if (duplicate) { transform = Instantiate(transformToMove, transformToMove.parent); }
            else { transform = transformToMove; }

            SetupDraggable();
            SetPointerOffset(eventData.position);
            SetupPlaceholderElement();
            transform.SetParent(canvas.transform);

            group.blocksRaycasts = false;
            dragging = true;
            OnStartDrag?.Invoke();
        }

        protected virtual void SetPointerOffset(Vector2 pos) => offset = (Vector2)transform.position - pos;

        protected virtual void SetupPlaceholderElement()
        {
            placeholder = new GameObject("placeholder");
            placeholder.transform.position = CanvasTools.GetPositionInCanvas(CanvasTools.CanvasMode.SCREEN_SPACE_OVERLAY, transform.position, canvas);
            placeholder.transform.SetParent(transform.parent);
            placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }
        #endregion

        #region ON_DRAG
        public virtual void OnDrag(PointerEventData eventData)
        {
            MoveObjectToMousePointer(eventData.position);
        }

        protected virtual void MoveObjectToMousePointer(Vector2 position)
        {
            var mousePosInCanvas = CanvasTools.GetPositionInCanvas(CanvasTools.CanvasMode.SCREEN_SPACE_OVERLAY, position, canvas);
            transform.position = mousePosInCanvas;
        }
        #endregion

        #region ON_END_DRAG
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            SetParentAndIndex();

            Destroy(group);
            Destroy(placeholder);
            if (duplicate) { Destroy(transform.gameObject); }

            dragging = false;
            OnFinishDrag?.Invoke();
        }

        protected virtual void SetParentAndIndex()
        {
            transform.SetParent(placeholder.transform.parent);
            transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
            transform.localPosition = placeholder.transform.localPosition;
        }
        #endregion

        public void Drop()
        {
            OnDrop?.Invoke();
        }

        private void OnDestroy()
        {
            if (!dragging) { return; }

            Destroy(group);
            Destroy(placeholder);
            Destroy(transform.gameObject);

            OnFinishDrag?.Invoke();
        }
    }
}