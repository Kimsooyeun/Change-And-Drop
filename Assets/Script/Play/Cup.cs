using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cup : MonoBehaviour
{
    public static Action<Transform> onCreateMasterBallEvent;
    public float moveSpeed = 5.0f;
    Transform[] wayPoints;


    int targetIndex = 0;
    public float rotationSpeed = 90.0f; // 회전 속도
    public bool isCupMove = true;
    private bool isGmaestart = false;
    private Ball myBall;

    ClickController clickController;

    [SerializeField]
    Transform spawn;

    [SerializeField]
    private GameObject MasterBall;

    [SerializeField]
    private int InitBallCount;

    private void Awake()
    {
        wayPoints = new Transform[2];
        wayPoints[0] = GameObject.Find("waypoint1").transform;
        wayPoints[1] = GameObject.Find("waypoint2").transform;

        clickController = FindObjectOfType<ClickController>();
    }

    private void OnEnable()
    {
        clickController.onDrop += StartGame;
    }

    private void OnDisable()
    {
        clickController.onDrop -= StartGame;
    }

    private void Start()
    {
        transform.position = wayPoints[0].position;
    }

    private void Update()
    {
        if (isCupMove == true)
        {
            StartCoroutine("MoveCup");
        }
    }

    IEnumerator MoveCup()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[targetIndex].position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[targetIndex].position) < 0.1f)
        {
            GoNextWaypoint();
        }
        yield return null;
    }

    void GoNextWaypoint()
    {
        if (targetIndex == wayPoints.Length - 1)
        {
            targetIndex = 0;
        }
        else
        {
            targetIndex++;
        }
    }

    private void StartGame()
    {
        if (isGmaestart == false)
        {
            isCupMove = false;
            // 현재 위치로 이동 고정
            StopCoroutine("MoveCup");

            // z축으로 90도 회전
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 180f);
            StartCoroutine(RotateObject(targetRotation));
        }
        isGmaestart = true;
    }

    IEnumerator RotateObject(Quaternion targetRotation)
    {
        // 시작 회전값
        Quaternion startRotation = transform.rotation;

        // 회전 시간
        float rotationTime = 1.0f;

        // 경과 시간
        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            // 보간된 회전값 계산
            Quaternion newRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / rotationTime);

            // 회전값 적용
            transform.rotation = newRotation;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        transform.rotation = targetRotation;

        StartCoroutine("DropBall");
    }
    
    IEnumerator DropBall()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < InitBallCount; i++)
        {
            Vector3 newPos = new Vector3(spawn.position.x + i * 0.5f, spawn.position.y, spawn.position.z);
            Color ballColor;

            if (i % 2 == 0)
            {
                ballColor = Color.blue;
            }
            else
            {
                ballColor = Color.Lerp(Color.red, Color.yellow, 0.5f);
            }
            myBall = BallPool.GetObject(newPos);
            myBall.SetRadius(0.1f);
        }
        Vector3 spawnPosition = spawn.position;
        spawnPosition.y -= 2f;
        GameObject mBall = Instantiate(MasterBall, spawnPosition, Quaternion.identity);
        onCreateMasterBallEvent?.Invoke(mBall.transform);
    }
}
