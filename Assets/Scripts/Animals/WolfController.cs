using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WolfController : MonoBehaviour
{
    [SerializeField] private Transform wolf;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private Transform player;

    [SerializeField] private float yDistance;
    [SerializeField] private float yOffset;
    
    private void Update()
    {
        var position = wolf.position;
        var delta = player.position.y - (yOffset + position.y);

        var dir = direction;
        
        if (Mathf.Abs(delta) >= yDistance) 
            dir.y += dir.y * Mathf.Sign(delta);

        wolf.position = Vector3.MoveTowards(position, position + (Vector3) direction, Time.deltaTime * speed);
    }
}