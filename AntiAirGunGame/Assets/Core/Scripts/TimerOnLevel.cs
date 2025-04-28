using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Core.Scripts;

public class TimerOnLevel : MonoBehaviour
{
    [SerializeField] private float _timeForEndOfLevel;
    [SerializeField] public Scenes sceneName;

    private void Start()
    {
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(_timeForEndOfLevel);
            GameManager.Instance.ChangeScene(SceneNames.StringSceneNames[sceneName]);
        }

        StartCoroutine(Timer());
    }
}