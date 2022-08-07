using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Material selectedSkin; // skin to use for player aircraft
    string selectedMap;

    int selectedSkinIndex;
    int selectedMapIndex;

    public void setPlayerSkin(Material skin, int index)
    {
        selectedSkin = skin;
        selectedSkinIndex = index;
    }

    public Material getPlayerSkin()
    {
        return selectedSkin;
    }

    public void setSelectedMap(string mapName, int index)
    {
        selectedMap = mapName;
        selectedMapIndex = index;
    }

    public string getSelectedMap()
    {
        string map;

        if (selectedMap != null)
        {
            if (selectedMap.Contains("mountain"))
            {
                map = "MountainScene";
            }
            else
            {
                map = "MainScene";
            }
        }
        else
        {
            map = "MainScene";
        }

        return map;
    }

    private void Awake()
    {
        // let this game manager persist through the whole game
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
