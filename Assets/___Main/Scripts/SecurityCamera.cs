using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private bool _movable;
  //  [SerializeField] private float _timeWaintIdle;
  //  [SerializeField] private Transform _forwardHandle;
  //  [SerializeField] private Transform _rightHandle;

    [SerializeField] [Range(0, 3)] private float _rotationSpeed;

    [SerializeField] private Transform _target;



    System.Action MoveAction;




    private void Start()
    {

    }


    private void Update()
    {
        if (!_movable) return;

        lookTarget();

    }

    private int _goalCounter;
    private void lookTarget()
    {

        transform.LookAt(_target.position);
    }










    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }












}
