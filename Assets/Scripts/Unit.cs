using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int currentHP;
    public int maxHP;

    public TextMeshProUGUI healthBar;

    public void Update()
    {
        healthBar.text = currentHP + "/" + maxHP;
    }
}
