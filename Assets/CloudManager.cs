using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CloudOnce;

public class CloudManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        Cloud.OnInitializeComplete += CloudOnceInitializeComplete;
        Cloud.OnCloudLoadComplete+=CloudOnceLoadComplete;
        Cloud.Initialize(true, true);
    }

    void CloudOnceInitializeComplete()
    {
        Cloud.OnInitializeComplete -= CloudOnceInitializeComplete;
        Cloud.Storage.Load();
        // load data
    }

    void CloudOnceLoadComplete(bool success)
    {
        // update our ui
        UpdateUI();
    }
   

    public void PickCoin()
    {
        CloudVariables.Coins++;
        UpdateUI();
        //Save
        Save();
    }

    public void BuyItem()
    {
        if(CloudVariables.Coins>=10)
        {
            CloudVariables.Coins -= 10;
            UpdateUI();
            Save();
        }else
        {
            Debug.LogWarning("not enough coins");
        }
    }

    public void Score()
    {
        CloudVariables.HighScore++;
        UpdateUI();
        Save();
    }

    private void UpdateUI()
    {
        //Pega informação da variavel diretamente do Cloud
        scoreText.text ="Score: " + CloudVariables.HighScore.ToString();
        coinText.text = "Coin: " + CloudVariables.Coins.ToString();

    }

    private void Save()
    {
        Cloud.Storage.Save();
    }
}
