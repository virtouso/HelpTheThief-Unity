﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Guard : MonoBehaviour
{



    [SerializeField] private float _viewRadius = 10;
    [Range(0, 360)] [SerializeField] private float _viewAngle = 0;
    [SerializeField] private int _sightResolution;
    [SerializeField] private MeshFilter _sight;
    [SerializeField] private LayerMask _detectionMask;



    [SerializeField] private bool movable;
    [SerializeField] private Transform[] _destinations;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }


    private void Start()
    {
        if (!movable) return;
        _agent.SetDestination(_destinations[destinationCounter].position);
    }

    private void Update()
    {
        CalculateEyeSight();
        MoveGuard();
    }

    private void CalculateEyeSight()
    {

        UnityEngine.Debug.DrawRay(transform.position, transform.forward * 10);
        UnityEngine.Debug.DrawLine(transform.position, transform.position + transform.forward * 10);



        float rightX = transform.position.x + _viewRadius * Mathf.Sin((transform.eulerAngles.y + _viewAngle) * Mathf.Deg2Rad);
        float rightZ = transform.position.z + _viewRadius * Mathf.Cos((transform.eulerAngles.y + _viewAngle) * Mathf.Deg2Rad);
        Vector3 rightPosition = new Vector3(rightX, transform.position.y, rightZ);
        UnityEngine.Debug.DrawLine(transform.position, rightPosition);

        float leftX = transform.position.x + _viewRadius * Mathf.Sin((transform.eulerAngles.y + _viewAngle * -1) * Mathf.Deg2Rad);
        float leftZ = transform.position.z + _viewRadius * Mathf.Cos((transform.eulerAngles.y + _viewAngle * -1) * Mathf.Deg2Rad);
        Vector3 leftPosition = new Vector3(leftX, transform.position.y, leftZ);
        UnityEngine.Debug.DrawLine(transform.position, leftPosition);





        float whileSight = _viewAngle - (-_viewAngle);
        float triangleSize = whileSight / _sightResolution;

        Mesh mesh = _sight.mesh;
        mesh.Clear();

        var vertices = new Vector3[_sightResolution + 1];
        var triangles = new int[(_sightResolution + 1) * 3];
        var uv = new Vector2[_sightResolution + 1];
        var normals = new Vector3[_sightResolution + 1];
        int indexCounter = 0;
        for (int i = 1; i < _sightResolution + 1; i++)
        {
            float vertexX = _viewRadius * Mathf.Sin((((-1 * _viewAngle) + i * triangleSize)) * Mathf.Deg2Rad);
            float vertexZ = _viewRadius * Mathf.Cos((((-1 * _viewAngle) + i * triangleSize)) * Mathf.Deg2Rad);


            Vector3 vertexPosition = new Vector3(vertexX, 0, vertexZ);




            float vertexXe = transform.position.x + _viewRadius * Mathf.Sin(((transform.eulerAngles.y + (-1 * _viewAngle) + i * triangleSize)) * Mathf.Deg2Rad);
            float vertexZe = transform.position.z + _viewRadius * Mathf.Cos(((transform.eulerAngles.y + (-1 * _viewAngle) + i * triangleSize)) * Mathf.Deg2Rad);


            Vector3 vertexPositione = new Vector3(vertexXe, transform.position.y, vertexZe);


            vertices[i] = vertexPosition;
            RaycastHit hit;
            Vector3 rayCheck = (vertexPositione - transform.position);
            bool hitWall = Physics.Raycast(transform.position, rayCheck.normalized, out hit, _viewRadius, _detectionMask.value);


            Vector3 V = transform.TransformPoint(vertices[i]);
            if (hitWall)
            {
                vertexPositione = hit.point;
                vertices[i] = transform.InverseTransformPoint ( vertexPositione); //+ _sight.transform.localPosition;//    _sight.transform.position;
                // check player detection
                if (hit.collider.tag == "Thief") { GameManager.Instance.Alerted = true; }

            }


            UnityEngine.Debug.DrawLine(transform.position, vertexPositione);


            if ((i >= _sightResolution)) break;
            triangles[indexCounter] = 0;
            indexCounter++;
            triangles[indexCounter] = i;
            indexCounter++;
            triangles[indexCounter] = i + 1;
            indexCounter++;
        }


        mesh.vertices = vertices;
        // mesh.uv = uv;
        mesh.triangles = triangles;
        //  mesh.normals = normals;
        //   mesh.RecalculateBounds();
        // mesh.RecalculateNormals();
        //  mesh.RecalculateTangents();
    }



    private int destinationCounter;
    private void MoveGuard()
    {

        if (Vector3.Distance(transform.position, _destinations[destinationCounter].position) < _speed)
        {

            if (destinationCounter < _destinations.Length - 1) destinationCounter++;
            else destinationCounter = 0;

            _agent.SetDestination(_destinations[destinationCounter].position);
        }
    }


















}
