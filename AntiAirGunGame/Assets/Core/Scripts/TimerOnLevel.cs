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
            //do method of end
        }
    }

}