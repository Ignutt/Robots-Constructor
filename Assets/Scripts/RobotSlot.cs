using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EnglishKids.ChallengeGame
{
    public class RobotSlot : MonoBehaviour, IDropHandler
    {
        [Header("Robot part number")]
        public int partNumber = 0;
        
        [Header("Type of robot")] 
        public int typeRobot = 0;

        [Header("Color")] 
        public string nameColor = "Orange";

        [Header("Start animation")] 
        public GameObject startAnimation;
        public float timeToDestroy = 2f;
        private GameObject _newAnim;

        private SoundManager _soundManager;
        private Robot _robot;

        private void Start()
        {
            _soundManager = GameObject.FindWithTag("MainCamera").GetComponent<SoundManager>();
            _robot = transform.parent.GetComponent<Robot>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null && !eventData.pointerDrag.GetComponent<Item>()) return;

            InsertPart(eventData.pointerDrag.GetComponent<Item>());
        }

        private void InsertPart(Item item)
        {
            if (item.partNumber == partNumber && item.typeRobot == typeRobot)
            {
                item.GetComponent<RectTransform>().anchoredPosition =
                    GetComponent<RectTransform>().anchoredPosition;
                GetComponent<Image>().color = Color.white;

                PlayAnimation();
                
                Destroy(item.gameObject);
                _soundManager.Play(nameColor);
                
                _robot.InsertPart();
            }
        }

        private void PlayAnimation()
        {
            _newAnim = Instantiate(startAnimation);
            _newAnim.transform.SetParent(transform);
            _newAnim.transform.position = new Vector2(transform.position.x, transform.position.y);
            StartCoroutine(RemoveAnimation());
        }

        IEnumerator RemoveAnimation()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(_newAnim);
        }
    }
}
