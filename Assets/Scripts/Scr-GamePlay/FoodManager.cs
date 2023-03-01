using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public static bool isReplayAgain;

    [SerializeField] private GameObject[] foodItems;
    
    [SerializeField] private Sprite[] goSprites;
    [SerializeField] private Sprite[] growSprites;
    [SerializeField] private Sprite[] glowSprites;
    [SerializeField] private Sprite[] junkSprites;

    private string[] foodCategory = { "Go", "Grow", "Glow", "Junk" };
    public int[] itemPosition;

    public static float elapsedTime;

    private void Start()
    {
        if (isReplayAgain)
        {
            StartCoroutine(WaitFor3Seconds());
        }
        else 
        {

            if (PowerUpManager.typeOfPowerUp == "GO")
            {
                #region FOR GO POWER UP FOOD SPAWN
                int noOfFoods = foodItems.Length;
                int goSelectedFood = Random.Range(0, goSprites.Length);

                for (int i = 0; i < noOfFoods; i++)
                {
                    
                    GameObject goFoodItem = foodItems[i];
                    goFoodItem.SetActive(true);

                    SpriteRenderer spriteRenderer = goFoodItem.GetComponent<SpriteRenderer>();

                    goFoodItem.tag = "Go";
                    spriteRenderer.sprite = goSprites[goSelectedFood];

                }

            }
                #endregion
            else
            {
                #region FOR RANDOM NON POWER UP FOOD SPAWN

                int noOfFoods;

                if (PowerUpManager.typeOfPowerUp == "GROW" || PowerUpManager.typeOfPowerUp == "GLOW")
                {
                    noOfFoods = Random.Range(2, 4);
                }
                else
                {
                    noOfFoods = Random.Range(1, 3);
                }

                itemPosition = new int[noOfFoods];

                for (int i = 0; i < noOfFoods; i++)
                {
                    int randomFoodItemPosition = Random.Range(0, foodItems.Length);
                    itemPosition[i] = randomFoodItemPosition;

                    GameObject randomFoodItem = foodItems[randomFoodItemPosition];
                    randomFoodItem.SetActive(true);

                    SpriteRenderer spriteRenderer = randomFoodItem.GetComponent<SpriteRenderer>();

                    string randomFoodCategory = foodCategory[Random.Range(0, foodCategory.Length)];
                    randomFoodItem.tag = randomFoodCategory;

                    switch (randomFoodCategory)
                    {
                        case "Go":
                            spriteRenderer.sprite = goSprites[Random.Range(0, goSprites.Length)];
                            break;

                        case "Grow":
                            spriteRenderer.sprite = growSprites[Random.Range(0, goSprites.Length)];
                            break;

                        case "Glow":
                            spriteRenderer.sprite = glowSprites[Random.Range(0, goSprites.Length)];
                            break;

                        case "Junk":
                            spriteRenderer.sprite = junkSprites[Random.Range(0, goSprites.Length)];
                            break;
                    }

                }
                #endregion
            }
        }
    }

    IEnumerator WaitFor3Seconds()
    {
        Debug.Log("Wait for 3 Secs");

        yield return new WaitForSeconds(1);

        Debug.Log("Time's Up");

        isReplayAgain = false;

    }

}

