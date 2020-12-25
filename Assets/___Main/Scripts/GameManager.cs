using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{
    #region public


    #endregion

    #region Unity References
    [SerializeField] private GameObject _thiefPrefab;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private Vector3 _cameraPffset;
    #endregion

    #region private
    private GameObject _thief;
    #endregion

    #region unity callbacks
    private void Start()
    {
        initThief();
        initCamera();
    }

    private void Update()
    {
        sendThiefToPosition();
    }

    #endregion


    #region utility
    private void initThief()
    {
        _thief = Instantiate(_thiefPrefab, _start.transform.position, _start.transform.rotation);
    }


    private void initCamera()
    {
        Camera.main.transform.position = _thief.transform.position + _cameraPffset;
        Camera.main.transform.LookAt(_thief.transform.position);
    }

    private void sendThiefToPosition()
    {

        Vector3 goalPosition;

        if (!Input.GetMouseButtonDown(0)) return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 100.0f))
        {
            print("wrong destination");
            return;
        }

        Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
        goalPosition = hit.point;
        GameObject g = new GameObject("goal");
        g.transform.position = goalPosition;


        NavMeshAgent agent = _thief.GetComponent<NavMeshAgent>();

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(goalPosition, path);
        print(path.status);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            print("wrong destination");
            return;
        }

        agent.SetDestination(goalPosition);

    }


    #endregion





}
