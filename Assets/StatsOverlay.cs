using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsOverlay : MonoBehaviour
{

    private TextMeshProUGUI statsDisplay;
    private PlayerController playerController;
    private int health;
    private int points;
    private int streak;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();


        statsDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        health = playerController.health;
        points = playerController.points;
        streak = playerController.streak;

        statsDisplay.text = "Health: " + health.ToString() + "\nPoints: "+ points.ToString() + "\nStreak: "+ streak.ToString();
    }
}
