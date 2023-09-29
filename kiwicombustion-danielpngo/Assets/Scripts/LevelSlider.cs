using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSlider : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject endPoint;
    private Slider thisSlider;
    float levelStartX;
    float levelEndX;


    #region Unity Functions

    private void Start()
    {
        thisSlider = GetComponent<Slider>();
        levelStartX = player.transform.position.x;  
        levelEndX = endPoint.transform.position.x;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Calculate player's current position along the X-axis
        float playerPositiionX = player.transform.position.x;
        //Calculate the remaining distance to end point
        float DistanceToFinish = levelEndX - playerPositiionX;

        //Calculate player progress
        float totalLevelWidth = levelEndX - levelStartX;
        float playerProgress = 1f - (DistanceToFinish / totalLevelWidth);

        UpdateUISlider(playerProgress);

    }

    private void UpdateUISlider(float playerProgress)
    {
        thisSlider.value = playerProgress;
    }
    #endregion
}
