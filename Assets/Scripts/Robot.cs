using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnglishKids.ChallengeGame
{
    public class Robot : MonoBehaviour
    {
        [Header("Parts count")]
        public int partsCount;

        [Header("Animation")] 
        public GameObject animation;

        [Header("Buttons")] 
        public GameObject leftButton;
        public GameObject rightButton;

        public void InsertPart()
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            
            partsCount--;
            if (partsCount == 0)
            {
                PlayAnimation();
            }
        }

        private void PlayAnimation()
        {
            GameObject newAnim = Instantiate(animation, transform.parent);
            newAnim.transform.position = new Vector2(transform.position.x, transform.position.y - 3.25f);
            
            gameObject.SetActive(false);
        }
    }
}
