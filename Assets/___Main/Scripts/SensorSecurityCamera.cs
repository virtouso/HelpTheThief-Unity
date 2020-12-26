using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSecurityCamera : MonoBehaviour
{




    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        GameManager.Instance.Alerted = true;
    }

}
