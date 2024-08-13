using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SpriteScroller : MonoBehaviour
{ 
    float offset = 2f;
    Tilemap tilemap;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void Update()
    {
        tilemap.transform.position = new Vector3(0, Mathf.PingPong(Time.time, offset), tilemap.transform.position.z);
    }
}
