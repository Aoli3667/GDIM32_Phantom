using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text soyBean, carrot, hay, corn, insect;


    [SerializeField] private Player currentPlayer;
 



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        money.text = currentPlayer.GetMoney().ToString();
        soyBean.text = currentPlayer.GetFoodCount(FoodType.SoyBean).ToString();
        carrot.text = currentPlayer.GetFoodCount(FoodType.Carrot).ToString();
        hay.text = currentPlayer.GetFoodCount(FoodType.Hay).ToString();
        corn.text = currentPlayer.GetFoodCount(FoodType.Corn).ToString();
        insect.text = currentPlayer.GetFoodCount(FoodType.Insect).ToString();


    }
}
