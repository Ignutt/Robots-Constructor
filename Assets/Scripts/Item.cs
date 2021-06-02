using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.ChallengeGame
{
    public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        //[Header("Canvas of element")]
        //public Canvas canvas;

        [Header("Alpha factor")] 
        public float alpha = 0.6f;

        [Header("Robot part number")] 
        public int partNumber = 0;

        [Header("Type of robot")] 
        public int typeRobot = 0;

        [Header("Speed rotation")] 
        private float speedRotation = 1f;

        private CanvasGroup _canvasGroup;
        private RectTransform _rect;
        private Tape _tape;
        private Canvas _canvas;

        private Vector2 _firstPosition;
        private Vector3 _firstRotation;

        private bool _rotate = false;
        private float _seconds = 0;
        
        private void Start()
        {
            _rect = GetComponent<RectTransform>();
            _tape = transform.parent.GetComponent<Tape>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

            _firstPosition = _rect.anchoredPosition;
            _firstRotation = _rect.rotation.eulerAngles;
        }

        private void FixedUpdate()
        {
            if (_rotate)
            {
                _rect.rotation = Quaternion.Slerp(_rect.localRotation, Quaternion.Euler(Vector3.zero), _seconds /  speedRotation);
                _seconds += Time.deltaTime;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = alpha;
            
            _rotate = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1f;
            
            SetToDefaultPosition();
            SetToDefaultRotation();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rect.anchoredPosition += eventData.delta / _canvas.scaleFactor; // canvas.scaleFactor;
        }

        private void SetToDefaultPosition()
        {
            _rect.anchoredPosition = _firstPosition;
        }

        private void SetToDefaultRotation()
        {
            _rotate = false;
            _rect.rotation = Quaternion.Euler(_firstRotation);
            _seconds = 0;
        }

        private void OnDestroy()
        {
            _tape.InsertPart();
        }
    }
}
