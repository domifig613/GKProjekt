using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasGameSceneController : MonoBehaviour
{
    [SerializeField] private QuestsController questsController;
    [SerializeField] private GasStationController gasStationController;
    [SerializeField] private MechanicController mechanicController;
    [SerializeField] private GarageController garageController;
    [SerializeField] private CarController carController;
    [SerializeField] private TMPro.TMP_Text cashText;
    [SerializeField] private TMPro.TMP_Text currentQuestInfo;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject fuelPopup;
    [SerializeField] private GameObject mechanicPopup;
    [SerializeField] private GameObject helpPopup;
    [SerializeField] private GameObject garagePopup;
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject defeatPopup;
    [SerializeField] private Image durabilityImage;
    [SerializeField] private Image fuelImage;

    [SerializeField] private List<GameObject> questBigMapTags;

    [SerializeField] private GameObject garageView;
    [SerializeField] private List<GameObject> carsPages;

    [SerializeField] CityBuildingsController cityBuildingsConntroller;
    [SerializeField] MinimapController minimapController;
    [SerializeField] CameraController cameraController;
    [SerializeField] MinimapCamera minimapCamera;
    [SerializeField] PlayerIconController playerIconController;

    private const string NO_ACTIVE_QUEST_INFO = "No active quest";
    private bool isQuestActive = false;
    private string currentQuestFinishPlace = "";
    private TimeSpan deadlineToFinishQuest;
    private int cashForOrder = 0;

    private void Start()
    {
        DisableGarage();
        CloseMap();
        ClosePopups();
        RefreshCash();
        RefreshQuestInfo();
        PlayerController.OnCashRefresh += RefreshCash;
        PlayerController.OnQuestStateChanged += RefreshQuestInfo;

        foreach (var tag in questBigMapTags)
        {
            tag.SetActive(false);
        }

        StartCoroutine(UpadateVisualFuelCoroutine());
        StartCoroutine(UpadateVisualDurabilityCoroutine());
        StartCoroutine(UpdatePopupsState());
    }

    private IEnumerator UpdatePopupsState()
    {
        while (true)
        {
            winPopup.SetActive(PlayerController.IsPlayerWin() && !defeatPopup.activeSelf);
            defeatPopup.SetActive(carController.CurrentSpeed <= 1f
                && ((gasStationController.PriceForFuel > PlayerController.Cash && carController.GetCurrentFuelPart() <= 0f)
                || (mechanicController.PriceForFixCar > PlayerController.Cash && carController.GetCurrentDurability() <= 0f)) && !winPopup.activeSelf);

            fuelPopup.SetActive(gasStationController.CanGetFuel() && carController.GetCurrentFuelPart() != 1f && PlayerController.Cash >= gasStationController.PriceForFuel && !winPopup.activeSelf && !defeatPopup.activeSelf);
            mechanicPopup.SetActive(mechanicController.CanFixCar() && carController.GetCurrentDurability() != 1f && PlayerController.Cash >= mechanicController.PriceForFixCar && !winPopup.activeSelf && !defeatPopup.activeSelf);
            garagePopup.SetActive(garageController.CanOpenGarage() && !winPopup.activeSelf && !defeatPopup.activeSelf);
            helpPopup.SetActive(!fuelPopup.activeSelf && !mechanicPopup.activeSelf && (carController.GetCurrentFuelPart() <= 0f || carController.GetCurrentDurability() <= 0f) && !winPopup.activeSelf && !defeatPopup.activeSelf);

            if (winPopup.activeSelf || defeatPopup.activeSelf)
            {
                Time.timeScale = 0;
            }

            yield return 30;
        }
    }

    private void ClosePopups()
    {
        fuelPopup.SetActive(false);
        mechanicPopup.SetActive(false);
        garagePopup.SetActive(false);
        helpPopup.SetActive(false);
        winPopup.SetActive(false);
        defeatPopup.SetActive(false);
    }

    private IEnumerator UpadateVisualFuelCoroutine()
    {
        while (true)
        {
            fuelImage.fillAmount = carController.GetCurrentFuelPart();
            yield return 10;
        }
    }

    private IEnumerator UpadateVisualDurabilityCoroutine()
    {
        while (true)
        {
            durabilityImage.fillAmount = carController.GetCurrentDurability();
            yield return 10;
        }
    }

    private void RefreshCash()
    {
        cashText.text = PlayerController.Cash.ToString() + "$";
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (isQuestActive)
        {
            string additionalZeroInSeconds = deadlineToFinishQuest.Seconds < 10 ? "0" : "";
            currentQuestInfo.text = currentQuestFinishPlace + "\n time left: " + deadlineToFinishQuest.Minutes + ":" + additionalZeroInSeconds + deadlineToFinishQuest.Seconds + "\n" +
                " cash for order: " + cashForOrder;
            deadlineToFinishQuest -= TimeSpan.FromSeconds(Time.deltaTime);
        }
    }

    private void RefreshQuestInfo()
    {
        isQuestActive = PlayerController.QuestIsActive();

        if (isQuestActive)
        {
            Quest currentQuest = PlayerController.currentQuest;
            deadlineToFinishQuest = new TimeSpan();
            deadlineToFinishQuest += TimeSpan.FromSeconds(currentQuest.GetSecoundToEndQuest());
            cashForOrder = currentQuest.Prize;
            currentQuestFinishPlace = currentQuest.GetQuestEndPlaceName();
            currentQuestInfo.color = questsController.QuestConfig.GetStartColor(currentQuest.questType);
        }
        else
        {
            currentQuestInfo.text = NO_ACTIVE_QUEST_INFO;
            currentQuestInfo.color = Color.white;
        }
    }

    public void RestartGame()
    {
        PlayerController.RestartGame();
    }

    #region map
    private void OpenMap()
    {
        map.SetActive(true);
    }

    public void CloseMap()
    {
        map.SetActive(false);
    }

    public void ChangeMapState()
    {
        map.SetActive(!map.activeSelf);
    }

    public bool MapIsOpen()
    {
        return map.activeSelf;
    }

    public void AddTagToMap(int index, Color tagColor)
    {
        questBigMapTags[index].SetActive(true);
        questBigMapTags[index].GetComponent<Image>().color = tagColor;
    }

    public void RemoveTagFromMap(int index)
    {
        questBigMapTags[index].SetActive(false);
    }

    public void ChangeTagOnQuestStart(int startIndex, Color finishColor, int finishIndex)
    {
        RemoveTagFromMap(startIndex);
        AddTagToMap(finishIndex, finishColor);
    }

    #endregion


    public void DisableGarage()
    {
        Time.timeScale = 1f;

        garageView.SetActive(false);

        foreach (var carPage in carsPages)
        {
            carPage.SetActive(false);
        }
    }

    public void OpenGarage()
    {
        Time.timeScale = 0f;
        garageView.SetActive(true);
        carsPages[0].SetActive(true);
        carsPages[0].GetComponent<CategoryInShopManager>().RefreshButtons(garageController.CheckCarBuyingStatus(0));
    }

    public void NextPageInGarage()
    {
        int index = carsPages.FindIndex(x => x.activeSelf);

        if (index != carsPages.Count - 1)
        {
            carsPages[index].SetActive(false);
            carsPages[index + 1].SetActive(true);
            carsPages[index + 1].GetComponent<CategoryInShopManager>().RefreshButtons(garageController.CheckCarBuyingStatus(index+1));
        }
    }

    public void PreviousPageInGarage()
    {
        int index = carsPages.FindIndex(x => x.activeSelf);

        if (index != 0)
        {
            carsPages[index].SetActive(false);
            carsPages[index - 1].SetActive(true);
            carsPages[index - 1].GetComponent<CategoryInShopManager>().RefreshButtons(garageController.CheckCarBuyingStatus(index-1));
        }
    }

    public void OnBuyCarButton(int index)
    {
        int price = carsPages[index].GetComponent<CategoryInShopManager>().Price;

        if (PlayerController.Cash >= price)
        {
            PlayerController.RemoveCash(price);
            carsPages[index].GetComponent<CategoryInShopManager>().RefreshButtons(true);
            garageController.BuyCar(index);
        }
    }

    public void OnEquipButton(int index)
    {
        if (index != carController.Index)
        {
            Vector3 carPosition = new Vector3(carController.GetComponentInChildren<Rigidbody>().transform.position.x, carController.GetComponentInChildren<Rigidbody>().transform.position.y, carController.GetComponentInChildren<Rigidbody>().transform.position.z);
            GameObject player = carController.gameObject.transform.parent.gameObject;
            Destroy(carController.gameObject);
            GameObject newCarObject = garageController.GetCar(index);
            newCarObject.transform.SetParent(player.transform);
            newCarObject.GetComponentInChildren<Rigidbody>().transform.position = new Vector3(carPosition.x, carPosition.y, carPosition.z);
            carController = newCarObject.GetComponent<CarController>();

            cityBuildingsConntroller.SetNewCarTransfor(carController.GetComponentInChildren<Rigidbody>().transform);
            minimapCamera.player = carController.GetComponentInChildren<Rigidbody>().transform;
            cameraController.SetNewTarget(carController.GetComponentInChildren<Rigidbody>().transform);
            minimapController.SetNewPlayer(carController.GetComponentInChildren<Rigidbody>().transform);
            playerIconController.SetNewPlayer(carController.GetComponentInChildren<Rigidbody>().transform);
            PlayerInputController inputController = carController.transform.GetComponentInParent<PlayerInputController>();

            inputController.SetNewCar(carController, carController.transform.GetChild(0).transform.Find("FrontBumper").GetComponent<TriggerCollider>(), carController.transform.GetChild(0).transform.Find("RearBumper").GetComponent<TriggerCollider>());
        }
    }
}
