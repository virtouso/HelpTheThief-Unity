using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScurityCameraTarget : MonoBehaviour
{

    [SerializeField] private Transform[] _destinations;
    [SerializeField] private float _speed;

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, gameObject.name);
    }





    int destinationCounter = 0;
    private void Update()
    {
        if (_destinations == null) return;
        if (_destinations.Length <= 0) return;


        transform.position = Vector3.MoveTowards(transform.position,_destinations[destinationCounter].position,_speed );
        if (Vector3.Distance(transform.position, _destinations[destinationCounter].position) < _speed)
        {

            if (destinationCounter < _destinations.Length - 1) destinationCounter++;
            else destinationCounter = 0;


        }

    }

}
