using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    public Animator circle;
    public float secondsToFakeLoad;

    // Start is called before the first frame update
    // IEnumerator Start()
    // {

    // }

    public IEnumerator OnLoadOver(AsyncOperation sceneChange = null)
    {
        yield return new WaitForSecondsRealtime(secondsToFakeLoad);
        circle.Play("LoadCircle Hide", 0, 0);
        if (sceneChange != null)
        {
            yield return new WaitForSecondsRealtime(1/2.99f);
            sceneChange.allowSceneActivation = true;
        }
    }
}
