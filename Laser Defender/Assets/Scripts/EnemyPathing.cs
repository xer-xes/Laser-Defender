﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints = new List<Transform>();
    int wayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var TargetPosition = waypoints[wayPointIndex].transform.position;
            var Movement = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, TargetPosition, Movement);
            if (transform.position == TargetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            //wayPointIndex = 0;
            //transform.position = waypoints[wayPointIndex].transform.position;
            Destroy(gameObject);
        }
    }
}
