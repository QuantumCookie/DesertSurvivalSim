using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftableItem : MonoBehaviour
{
    public Sprite productSlotBackground;
    public Sprite ingredientSlotBackground;
    public Sprite disabledSlotBackground;
    public Sprite blankSprite;

    private int N;
    private Image productBackground;
    private Image productIcon;
    private TextMeshProUGUI productCount;
    private Image[] ingredientIcons;
    private Image[] ingredientBackgrounds;
    private TextMeshProUGUI[] ingredientsCount;
    private Button craftButton;

    private RecipeObject recipe;


    private void OnEnable()
    {
        N = transform.GetChild(1).childCount;
        
        productBackground = transform.GetChild(0).GetComponent<Image>();
        productIcon = productBackground.transform.GetChild(0).GetComponent<Image>();
        productCount = productBackground.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        
        productBackground.sprite = productSlotBackground;
        productIcon.sprite = blankSprite;
        productCount.text = "";
        
        ingredientBackgrounds = new Image[N];
        ingredientIcons = new Image[N];
        ingredientsCount = new TextMeshProUGUI[N];
        
        for (int i = 0; i < N; i++)
        {
            ingredientIcons[i] = transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Image>();
            ingredientBackgrounds[i] = transform.GetChild(1).GetChild(i).GetComponent<Image>();
            ingredientsCount[i] = transform.GetChild(1).GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();

            ingredientBackgrounds[i].sprite = disabledSlotBackground;
            ingredientIcons[i].sprite = blankSprite;
            ingredientsCount[i].text = "";
        }

        craftButton = transform.GetChild(2).GetComponent<Button>();
    }

    public void Set(RecipeObject _recipe)
    {
        recipe = _recipe;

        productIcon.sprite = _recipe.product.item.displaySprite;
        productCount.text = _recipe.product.quantity.ToString();

        for (int i = 0; i < _recipe.ingredients.Length; i++)
        {
            ingredientBackgrounds[i].sprite = ingredientSlotBackground;
            ingredientIcons[i].sprite = _recipe.ingredients[i].item.displaySprite;
            ingredientsCount[i].text = _recipe.ingredients[i].quantity.ToString();
        }
    }

    public Button GetButton()
    {
        return craftButton;
    }
}
