using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelButtonScript : MonoBehaviour
{
    private string currentLevelName { get; set; }
    private int currentLevelNumber { get; set; }
    private bool hasBeenClicked = false;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentLevelName = SceneManager.GetActiveScene().name;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        string numberSelectionPattern = @"\d+";
        currentLevelNumber = int.Parse(System.Text.RegularExpressions.Regex.Match(currentLevelName, numberSelectionPattern).Value);
        LevelEvents.OnLevelFinish += RenderFinishButton;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
             //OnMouseDown();
        }
    }
    private void RenderFinishButton()
    {
        spriteRenderer.enabled = true;
    }
    private void OnMouseDown()
    {
        if(!hasBeenClicked)
        {
            hasBeenClicked = true;
            LevelEvents.LoadLevel(++currentLevelNumber);
        }

    }
}
