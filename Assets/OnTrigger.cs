using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent enter;
    private void OnTriggerEnter(Collider other)
    {
        // if (other.transform.CompareTag("Player"))
        // {
            Invoke(nameof(startMovingPlane),3f);
        // }
    }
    void startMovingPlane()
    {
        enter.Invoke();
    }

}
