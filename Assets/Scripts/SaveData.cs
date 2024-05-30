using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateSaveData()
    {
        string source = Application.persistentDataPath + "/SaveData/collectionItems.json";

    }
   private void SaveGameData()
    {
        string destination = Application.persistentDataPath + "/SaveData/gamesave.json";
        GameSaveData saveData = new GameSaveData();
        saveData.DateOfSave = DateTime.Now;
        string json = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(destination, json);

    }
}

public class GameSaveData
{
  
    public List<CollectionItem> CollectedItems = new List<CollectionItem>();
    public DateTime DateOfSave;
}
public class CollectionItem
{
    public enum CollectedItemType
    {
        FreeTimeBook,
        ClassBook,
        Videogame,
        Console,
        Movie,
    };
    public int CollectedItemId;

}