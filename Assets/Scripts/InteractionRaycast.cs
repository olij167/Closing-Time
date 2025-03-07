using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InteractionRaycast : MonoBehaviour
{
    [SerializeField] private KeyCode selectInput = KeyCode.F;

    [SerializeField] private float reachDistance = 5f;
    [SerializeField] private float selectionSize = 1f;

    private ShoppingList shoppingList;
    private GameTimer gameTimer;

    public Image interactionAimIndicator;

    public LayerMask uiLayer;
    public LayerMask playerLayer;

    public bool isItem;
    public bool isFrontCounter;
    [SerializeField] private float selectionTimer = 1f;
    private float selectionTimerReset;
    [SerializeField] private float delayTime = 1f;

    public GameObject selectedObject;
    public Item selectedItem;

    public TextMeshProUGUI interactPromptText;

    public bool isItemInteracted;
    public bool isCounterInteracted;

    private void Start()
    {
        shoppingList = FindObjectOfType<ShoppingList>();
        gameTimer = FindObjectOfType<GameTimer>();
        selectionTimerReset = selectionTimer;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.SphereCast(ray, selectionSize, out hit, reachDistance, ~uiLayer | ~playerLayer))
        {
            if (hit.transform.GetComponent<ItemInWorld>())
            {
                isItem = true;
                selectedObject = hit.transform.gameObject;
                selectedItem = selectedObject.GetComponent<ItemInWorld>().item;

                interactionAimIndicator.color = Color.red;

                interactPromptText.text = selectedItem.itemName;

                selectionTimer -= Time.deltaTime;
            }
            else
            {
                isItem = false;
                interactPromptText.text = "";
                selectionTimer = selectionTimerReset;

                interactionAimIndicator.color = Color.white;
            }

            if (hit.transform.CompareTag("Counter"))
            {
                isFrontCounter = true;
                selectedObject = hit.transform.gameObject;
                selectionTimer = selectionTimerReset;

                interactionAimIndicator.color = Color.red;

                if (selectInput != KeyCode.Mouse0)
                    interactPromptText.text = "Press [" + selectInput + "] to Pay \n (End Level)";
                else
                    interactPromptText.text = "Press [LMB] to Pay \n (End Level)";

            }
            else if (!isItem)
            {
                isFrontCounter = false;
                interactPromptText.text = "";

                interactionAimIndicator.color = Color.white;
            }

            if (selectedObject != null)
            {

                if (isItem && (Input.GetKeyDown(selectInput) || selectionTimer <= 0f))
                {

                    isItemInteracted = true;

                    AddToCart();
                }

                if (isFrontCounter && Input.GetKeyDown(selectInput))
                {
                    isCounterInteracted = true;

                    gameTimer.LeaveStore();
                }

            }
        }
        else
        {
            isItem = false;
            isFrontCounter = false;
            interactPromptText.text = "";
            selectionTimer = selectionTimerReset;

            interactionAimIndicator.color = Color.white;
        }
    }

    public void AddToCart()
    {
        shoppingList.AddToCart(selectedItem);
        selectionTimer = selectionTimerReset;

        Destroy(selectedObject);
    }

    public IEnumerator DelaySettingFalseVariables()
    {
        if (isItemInteracted)
        {
            yield return new WaitForSeconds(delayTime);

            isItemInteracted = false;
        }

        
    }
}
