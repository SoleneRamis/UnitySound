using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public GameObject sphere;
    private GameObject[] _plateform;

    void Start()
    {
        _plateform = Plateform.Instance.GetTiles();
    }

    void Update()
    {
        //for (int i = 0; i < _plateform.Length; i++)
        //{
        //    if (Mathf.Abs(_plateform[i].transform.eulerAngles.z) >= 180)
        //    {
        //        _plateform[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        //        _plateform[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        //    }
        //}

        //if (sphere.transform.position.y < -5)
        //{
        //    sphere.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //    sphere.transform.position = new Vector3(-0.5f, 5, 0);
        //}
    }
}
