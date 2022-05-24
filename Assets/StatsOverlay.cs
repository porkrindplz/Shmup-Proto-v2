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
    private bool isHealth;
    private bool isStreak;
    private bool isPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        statsDisplay = GetComponent<TextMeshProUGUI>();
        if (gameObject.name.Contains("Streak"))
        {
            isStreak = true;
        }
        else if (gameObject.name.Contains("Health"))
        {
            isHealth = true;
        }
        else if (gameObject.name.Contains("Points"))
        {
            isPoints = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStreak)
        {
            streak = playerController.streak;
            statsDisplay.text = "Streak\n" + streak.ToString();
        }
        else if (isHealth)
        {
            health = playerController.health;
            statsDisplay.text = "Health\n" + health.ToString();
        } 
        else if (isPoints)
        {
            points = PlayerController.points;
            statsDisplay.text = points.ToString();
        }
    }

}
