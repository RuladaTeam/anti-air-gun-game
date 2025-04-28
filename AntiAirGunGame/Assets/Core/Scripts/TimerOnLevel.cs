using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TimerOnLevel : MonoBehaviour
{
    [SerializeField] private float _timeForEndOfLevel;

    //smth that contain logic of end

    private void Start()
    {
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(_timeForEndOfLevel);
            MovingObjectTraectory[] movingObjects = FindObjectsByType<MovingObjectTraectory>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
            );
            foreach (var component in movingObjects)
            {
                Destroy(component.gameObject);
            }
            //do method of end
        }
    }
}