using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallPool : MonoBehaviour
{
    public static BallPool Instance;

    [SerializeField]
    private GameObject BallPf;

    [SerializeField]
    private int initActivationCount;

    private Queue<Ball> Q = new Queue<Ball>();

    private void Awake()
    {
        Instance = this;
        Initilize(initActivationCount);
    }

    private Ball CreateNewObject()
    {
        var newObj = Instantiate(BallPf, transform).GetComponent<Ball>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initilize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Q.Enqueue(CreateNewObject());
        }
    }

    public static Ball GetObject(Vector3 pos)
    {
        if (Instance.Q.Count > 0)
        {
            var obj = Instance.Q.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.position = pos;
            return obj;
        }

        else
        {
            var newObj = Instance.CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.position = pos;
            return newObj;
        }
    }

    public static void ReturnObject(Ball obj)
    {
        obj.gameObject.SetActive(false);
        Instance.Q.Enqueue(obj);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
