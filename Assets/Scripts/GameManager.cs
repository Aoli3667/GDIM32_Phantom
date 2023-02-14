using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject farmPanel;
    [SerializeField] private GameObject displayPanel;

    [SerializeField] private Player player;

    [SerializeField] private int moneypreset = 500;
     
    void Start()
    {
        shopPanel.SetActive(false);
        farmPanel.SetActive(false);
        displayPanel.SetActive(true);

        player.SetMoney(moneypreset);

    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!shopPanel.activeSelf)
            {
                shopPanel.SetActive(true);
                farmPanel.SetActive(false);
            }
            else
            {
                farmPanel.SetActive(false);
                shopPanel.SetActive(true);
            }
        }
        if(Input.GetKeyUp(KeyCode.B))
        {
            if (!farmPanel.activeSelf)
            {
                farmPanel.SetActive(true);
                shopPanel.SetActive(false);
            }
            else
            {
                farmPanel.SetActive(false);
                shopPanel.SetActive(true);
            }
                
        }



    }
}
