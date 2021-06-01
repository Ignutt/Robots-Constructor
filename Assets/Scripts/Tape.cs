using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnglishKids.ChallengeGame
{
    public class Tape : MonoBehaviour
    {
        [Header("Prefabs parts of robot")] public GameObject[] partsOfRobots;

        [Header("Points in tape")] public List<Transform> spawns;

        [Header("Moving properties")] public float stepLength = 4f;
        public float speedMoving = 2f;

        [Header("Parts properties")] public int partsCount = 9;
        public int partsInTheFact = 0;

        [Header("Result screen")] public GameObject resultWindow;

        private bool _move = true;
        private RectTransform _rectTransform;
        private Vector2 _startPosition;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = GetComponent<RectTransform>().anchoredPosition;
            GenerateContent();
            partsInTheFact = 5;
        }

        private void Update()
        {
            if (_move)
            {
                MoveToNextParts();
            }
        }

        public void InsertPart()
        {
            partsInTheFact--;
            partsCount--;
            if (partsInTheFact == 0)
            {
                MoveToNextParts();
                _startPosition = _rectTransform.anchoredPosition;
            }
            else if (partsCount == 0)
            {
                resultWindow.SetActive(true);
            }
        }

        private void MoveToNextParts()
        {
            _rectTransform.anchoredPosition = Vector2.MoveTowards(
                _rectTransform.anchoredPosition,
                _startPosition + Vector2.up * stepLength,
                Time.deltaTime * speedMoving);
        }

        private void GenerateContent()
        {
            for (int i = 0; i < partsCount; i++)
            {
                int randIndex = Random.Range(0, spawns.Count);
                partsInTheFact++;
                GameObject newPart = Instantiate(partsOfRobots[i], transform);
                newPart.GetComponent<RectTransform>().anchoredPosition =
                    spawns[randIndex].GetComponent<RectTransform>().anchoredPosition;
                spawns.RemoveAt(randIndex);
            }
        }
    }
}
