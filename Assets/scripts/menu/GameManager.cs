using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Material selectedSkin; // skin to use for player aircraft

    public void setPlayerSkin(Material skin)
    {
        selectedSkin = skin;
    }

    public Material getPlayerSkin()
    {
        return selectedSkin;
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
