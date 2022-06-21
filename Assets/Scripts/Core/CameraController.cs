using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _offset;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.LookAt(player.transform.position);
        transform.position = player.transform.position + _offset;
        //transform.rotation = player.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
