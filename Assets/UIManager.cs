using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    public static TextMeshProUGUI playerHealthText;

    public static int totalEnemiesKilled = 0;

    void Start() {
        playerHealthText = GameObject.Find("PlayerHealthText").GetComponent<TextMeshProUGUI>();

        // UIManager.playerHealthText.text = "";
    }

    void Update() {
        //display totalEnemiesKilled
    }
}
