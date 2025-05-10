using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] List<Text> costText = new List<Text>();
    [SerializeField] List<GameObject> canvas;
    [SerializeField] GameObject currentSkin;
    [SerializeField] TMP_Text healingCostText;
    [SerializeField] TMP_Text healingAmountText;
    [SerializeField] Slider slider;
    public int healingAmount;
    public int healingCost;

    public Skin[] skin;
    PlayerController playerController;
    bool isOpen;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        for (var i = 0; i < skin.Length; i++)
        {
            costText[i].text = skin[i].price.ToString() + " Gold";
        }
    }

    private void Update()
    {
        
        healingAmount = ((int)slider.value);
        healingCost = healingAmount * 2;
        healingAmountText.text = "Healing amount: " + healingAmount.ToString();
        healingCostText.text = "Cost: " + healingCost;
    }
    public void OpenShop()
    {
        if (!isOpen)
        {
            canvas[0].SetActive(false);
            canvas[1].SetActive(true);
            isOpen = true;
        }
        else
        {
            canvas[1].SetActive(false);
            canvas[0].SetActive(true);
            isOpen = false;
        }
    }
    public void BuyAmmo(int count)
    {
        if (playerController.money >= count * 2)
        {
            playerController.AddAmmo(count);
            playerController.GetMoney(-count * 2);
        }
    }

    public void BuySkin(int count)
    {
        if (count > skin.Length)
        {
            return;
        }
        if (skin[count].price <= playerController.money && skin[count].isBuy == false)
        {
            //Покупаем
            costText[count].text = "Sold";
            skin[count].isBuy = true;
            playerController.GetMoney(-skin[count].price);
        }
        if (skin[count].isBuy == true)
        {
            //Переключаемся
            currentSkin.SetActive(false);
            currentSkin = skin[count].skinToBuy;
            currentSkin.SetActive(true);
        }
    }



    public void BuyHealth ()
    {
        if(playerController.money >= healingCost)
        {
            playerController.GetMoney(-healingCost);
            playerController.ChangeHealth(healingAmount);
        }
        else
        {
            print("Не хватает шекелей...");
        }
    }
}
[System.Serializable]
public class Skin
{
    public GameObject skinToBuy;
    public int price;
    public bool isBuy;
}