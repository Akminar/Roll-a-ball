using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference player GameObject
    public GameObject player;

    // Distance between camera and player
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate initial offset between camera and player position
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame after Update functions are complete
    void LateUpdate()
    {
        // Maintain offset between camera and player 
        transform.position = player.transform.position + offset;
    }
}
