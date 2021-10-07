using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Range(0,5)]
    public float cameraInterpolationSpeed = 1f;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        Vector3 playerPosition = playerTransform.position;

        position.x = Mathf.Lerp(position.x,
                                playerPosition.x,
                                cameraInterpolationSpeed * Time.deltaTime);

        position.y = Mathf.Lerp(position.y,
                                playerPosition.y,
                                cameraInterpolationSpeed * Time.deltaTime);

        position.y = Mathf.Max(0, position.y);

        transform.position = position;
    }
}
