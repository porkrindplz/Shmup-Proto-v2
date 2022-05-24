using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerOverlay : MonoBehaviour
{
    private TextMeshProUGUI statsDisplay;
    private PlayerController playerController;

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        statsDisplay = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = playerController.timer;
        statsDisplay.text = Mathf.RoundToInt(timer).ToString();
    }
}
