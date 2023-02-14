using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDecorator : Decorator
{
    public enum ItemType
    {
        Animal,
        Food
    }

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemtype;
    [SerializeField] private ItemType type;
    [SerializeField] private Slider slider;
    [SerializeField] private Toggle toggleChoicePrefab;

    private AnimalInstance AnimalInst;
    public SOAnimalDefinition AnimalDef { get; set; }
    public SOFoodDefinition FoodDef { get; set; }

    public Button btn;
    private SOFoodDefinition[] foodDefs;

    void Start()
    {
        foodDefs = Resources.LoadAll<SOFoodDefinition>("FoodDefinitions");
    }

    //public void Initialize(IGroupable a, IDecoratorManager manager)
    //{
    //    if(a is AnimalInstance)
    //    {
    //        Group = a;
    //        animalInst = a as AnimalInstance;
    //        nameDisplay.text = animalInst.name;
    //        costDisplay.text = "$" + animalInst.GetValue();
    //        icon.sprite = animalInst.Icon;
    //        base.Initialize(manager);
    //    }
    //    else
    //    {
    //        Debug.LogError("Trying to decorate a non Animal as a Aniaml");
    //    }
    //}

    public void Initialize(IGroupable a, IDecoratorManager manager)
    {
        if (a is AnimalInstance)
        {
            Group = a;
            AnimalInst = a as AnimalInstance;
            nameDisplay.text = AnimalInst.name;
            icon.sprite = AnimalInst.Icon;

            RefreshGrowSlider();
            base.Initialize(manager);
        }
        else
        {
            Debug.LogError("Trying to decorate a non Animal as a Aniaml");
        }
    }

    public void Initialize(SOAnimalDefinition a, IDecoratorManager manager)
    {
        AnimalDef = a;
        nameDisplay.text = a.GetName();
        costDisplay.text = "$" + a.GetCost();
        descriptionDisplay.text = a.GetDescription();
        icon.sprite = a.GetIcon();
        type = ItemType.Animal;
        itemtype.text = "Animal";
        base.Initialize(manager);
    }

    public void Initialize(SOFoodDefinition f, IDecoratorManager manager)
    {
        FoodDef = f;
        nameDisplay.text = f.GetName();
        costDisplay.text = "$" + f.GetCost();
        descriptionDisplay.text = f.GetDescription();
        icon.sprite = f.GetIcon();
        type = ItemType.Food;
        itemtype.text = "Food";
        base.Initialize(manager);
    }



    public override void Refresh()
    {

    }

    public void OnBuyBtnClicked()
    {
        if (displayManager is StoreManager)
            if (type == ItemType.Animal)
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < AnimalDef.GetCost()) { return; }
                (displayManager as StoreManager).GetPlayer().BuyItem(AnimalDef);
            }
            else
            {
                if ((displayManager as StoreManager).GetPlayer().GetMoney() < FoodDef.GetCost()) { return; }
                (displayManager as StoreManager).GetPlayer().BuyItem(FoodDef);
            }
    }


    public void OnFeedBtnClicked(Decorator decorator)
    {
        //(displayManager as FarmManager).feedPanel.SetActive(true); // move to last line to aviod conflict
        (displayManager as FarmManager).currentDecorator = decorator as ItemDecorator;

        foreach(FoodType type in AnimalInst.GetPreferedFood())
        {
            foreach (SOFoodDefinition t in foodDefs)
            {
                if (type == t.GetFoodType())
                {
                    if ((displayManager as FarmManager).GetPlayer().GetFoodCount(type) <= 0) continue;

                    Toggle go = Instantiate(toggleChoicePrefab, (displayManager as FarmManager).toggleGroup.transform);

                    go.SetIcon(t.GetIcon());
                    go.GetComponent<FoodChoice>().SetFoodDef(t);
                }
            }
        }
        (displayManager as FarmManager).feedPanel.SetActive(true);
    }

    public void OnSellBtnClicked(Decorator decorator)
    {
        if(AnimalInst.GetGrowthRate() < 1) return;

        (displayManager as FarmManager).sellPanel.SetActive(true);
        (displayManager as FarmManager).SetSellPriceText(AnimalInst.GetValue());

        (displayManager as FarmManager).currentDecorator = decorator as ItemDecorator;

    }

    public AnimalInstance GetAnimalInst()
    {
        return AnimalInst;
    }

    public void RefreshGrowSlider()
    {
        slider.value = AnimalInst.GetGrowthRate();
    }

}