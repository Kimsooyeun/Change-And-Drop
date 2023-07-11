using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [SerializeField]
    private GameObject Support;

    [SerializeField]
    private TextMeshPro TMpro_Info;

    [SerializeField]
    private GameObject Particle_Destroy;

    Vector3 detectionRange;
    Vector3 prevSupportPos;

    public float detectionHeight;

    public float MaxCount;
    int BallCount = 0;
    int prevBallCount = 0;

    Collider colliders;

    private void Start()
    {
        TMpro_Info.text = MaxCount.ToString();
        detectionHeight = MaxCount * (2f / 13f);
        prevSupportPos = Support.transform.position;
        InvokeRepeating(nameof(detactionBall), 1f, 0.5f);
    }

    private void Awake()
    {
        colliders = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            BallCount++;
        }
    }


    private void detactionBall()
    {
        if (BallCount >= MaxCount)
        {
            Destroy(gameObject);
            Instantiate(Particle_Destroy, transform.position, Quaternion.identity);
            return;
        }

        calculateSupportPos(BallCount);
        measureVolume();
    }

    private void calculateSupportPos(int ballCount)
    {
        prevBallCount = BallCount;

        Vector3 newPos = new Vector3(prevSupportPos.x, prevSupportPos.y - (ballCount) / 10f, prevSupportPos.z);
        Support.transform.position = newPos;
    }

    private void measureVolume()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, detectionRange, Quaternion.identity, 1 << 7);
        BallCount = hitColliders.Length;

        if (BallCount - prevBallCount == 0 && BallCount != 0 && prevBallCount != 0)
        {
            Invoke(nameof(delayGameOverEvent), detectionHeight / 15f);
        }
    }

    private void delayGameOverEvent()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.GameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        detectionRange = new Vector3(16f, detectionHeight, 1);
        Gizmos.DrawWireCube
            (
                transform.position,
                detectionRange
            );
    }

    private void OnDestroy()
    {
        CancelInvoke(nameof(detactionBall));
    }
}
