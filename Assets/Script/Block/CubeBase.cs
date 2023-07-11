using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CubeType
{
    nomal,
    change,
    swtithing
}

public class CubeBase : MonoBehaviour
{
    public CubeType type;
    public int copyCount;
    public int currentPassCount;
    public int maxPassCount;
    private bool isAssign = false;

    [SerializeField]
    protected TextMeshPro tmCopyCount;
    protected MeshRenderer meshRenderer;

    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        tmCopyCount.text = $"X{copyCount}";
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isAssign)
        {
            maxPassCount = GameManager.Instance.CurrentBallCount;
            isAssign = true;
        }

        if(other.CompareTag("Ball"))
        {
            ++currentPassCount;

            Ball myBall = other.GetComponent<Ball>();
            myBall.SetCubeInfo(copyCount);
            myBall.CopyBall(other.gameObject);
        }
    }

    protected virtual void ChangeObject()
    {
        Debug.Log("오브젝트변화~~~~~~~~");
    }
}
