using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Elysium.Utils
{
    /// <summary>
    /// Eligibility defined in IPointerEnter;
    /// </summary>
    public class UI_DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public enum DropZoneType 
        {
            InventorySlot,
        }

        public DropZoneType ZoneType = default;

        private List<UI_Draggable> draggable;

        public event Action<UI_Draggable> OnReceiveDrop;

        private void Awake() => draggable = new List<UI_Draggable>();

        public virtual void OnDrop(PointerEventData eventData)
        {
            UI_Draggable d = eventData.pointerDrag.GetComponentInChildren<UI_Draggable>();
            if (d == null) { return; }

            if (!draggable.Contains(d)) { /*Debug.Log("dropped draggable wasnt in list");*/ return; }

            // If its the same object, abort mission
            if (transform == d.transform) { draggable.Remove(d); return; }

            OnReceiveDrop?.Invoke(d);
            draggable.Remove(d);
            d.Drop();            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            // Check if something is being dragged
            if (eventData.pointerDrag == null) { return; }

            // Attempt to get draggable script from event data
            UI_Draggable d = eventData.pointerDrag.GetComponentInChildren<UI_Draggable>();
            if (d == null) { return; }

            // Check if this target is an eligible drop zone based on draggable script
            if (!EligibleDropTarget(d)) { return; }

            // HIGHLIGHT THIS AREA TO SHOW IT CAN BE DROPPED
            draggable.Add(d);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            // Check if something is being dragged
            if (eventData.pointerDrag == null) { return; }

            // Attempt to get draggable script from event data
            UI_Draggable d = eventData.pointerDrag.GetComponentInChildren<UI_Draggable>();
            if (d == null) { return; }

            // Check if draggable was previously a valid target on top of this drop zone
            if (!draggable.Contains(d)) { return; }

            // REMOVE THE AREA HIGHLIGHT
            draggable.Remove(d);
        }

        protected bool EligibleDropTarget(UI_Draggable d)
        {
            if (d.gameObject == gameObject) { return false; }
            if (d.TargetZoneType != ZoneType) { return false; }
            return true;
        }
    }
}