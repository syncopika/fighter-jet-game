using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuManager : MonoBehaviour
{
    public Dropdown mapAircraftSelectDropdown;
    public Dropdown skinSelectDropdown;

    public Button leftSelect;
    public Button rightSelect;
    
    // TODO: maybe make a controller script for map and aircraft carousels?
    public GameObject mapCarousel;      // carousel of all the available maps to choose from
    public GameObject aircraftCarousel; // all the aircraft to choose from

    // these are the selectable materials/aircraft skin
    public Material grey;
    public Material vandyOne;

    string currSelected; // map or aircraft
    int selectedAircraftIndex;
    int selectedAircraftSkinIndex;
    int selectedMapIndex;

    Material currSelectedAircraftSkin;

    Vector3 initialCarouselPosition;

    void hideSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 0;
    }

    void showSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 1;
    }

    void selectCarouselItem(int itemIndex)
    {
        // display the item at itemIndex
        if (currSelected.Equals("Map"))
        {
            int idx = 0;
            foreach(Transform child in mapCarousel.transform)
            {
                if(idx == itemIndex)
                {
                    child.GetComponent<MeshRenderer>().enabled = true;

                    GameObject gameManager = GameObject.Find("GameManager");
                    if (gameManager) gameManager.transform.GetComponent<GameManager>().setSelectedMap(child.name, itemIndex);
                }
                else
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }
                idx++;
            }
        }
        else if (currSelected.Equals("Aircraft"))
        {
            // TODO
        }
    }

    void clickLeftSelect()
    {
        if (currSelected.Equals("Map"))
        {
            if (selectedMapIndex > 0) selectedMapIndex--;
            selectCarouselItem(selectedMapIndex);
        }
        else if (currSelected.Equals("Aircraft"))
        {
            // TODO
        }
    }

    void clickRightSelect()
    {
        if (currSelected.Equals("Map"))
        {
            if (selectedMapIndex < mapCarousel.transform.childCount - 1) selectedMapIndex++;
            selectCarouselItem(selectedMapIndex);
        }
        else if (currSelected.Equals("Aircraft"))
        {
            // TODO
        }
    }

    Transform getCurrSelectedAircraft()
    {
        return aircraftCarousel.transform.GetChild(0).transform;
    }

    void skinSelectDropdownChanged(Dropdown change)
    {
        string selectedSkin = skinSelectDropdown.options[change.value].text;
        Transform selectedAircraft = getCurrSelectedAircraft();

        if (selectedSkin.Equals("grey"))
        {
            currSelectedAircraftSkin = grey;
        }else if(selectedSkin.Equals("Vandy 1"))
        {
            currSelectedAircraftSkin = vandyOne;
        }

        // pass to GameManager
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager) gameManager.transform.GetComponent<GameManager>().setPlayerSkin(currSelectedAircraftSkin, change.value);
        
        // assuming 1 renderer
        selectedAircraft.GetComponents<Renderer>()[0].material = currSelectedAircraftSkin;
    }

    void mapAircraftDropdownValueChanged(Dropdown change)
    {
        currSelected = mapAircraftSelectDropdown.options[change.value].text;
        changeThing(currSelected);
    }

    // TODO: rename this method
    void changeThing(string selected)
    {
        // TODO: use an enum instead?
        if (selected.Equals("Aircraft"))
        {
            // display aircraft carousel and skin options
            mapCarousel.transform.Translate(new Vector3(0, 0, 100)); // move it out of view

            aircraftCarousel.transform.position = initialCarouselPosition;

            showSkinSelectDropdown();
        }
        else if (selected.Equals("Map"))
        {
            hideSkinSelectDropdown();

            // display map carousel
            aircraftCarousel.transform.Translate(new Vector3(0, 0, 100)); // move it out of view

            mapCarousel.transform.position = initialCarouselPosition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: use the stored selected info in GameManager to show the right things when this menu page is returned to

        mapAircraftSelectDropdown.onValueChanged.AddListener(delegate {
            mapAircraftDropdownValueChanged(mapAircraftSelectDropdown);
        });

        skinSelectDropdown.onValueChanged.AddListener(delegate {
            skinSelectDropdownChanged(skinSelectDropdown);
        });
        
        leftSelect.onClick.AddListener(delegate {
            clickLeftSelect();
        });

        rightSelect.onClick.AddListener(delegate {
            clickRightSelect();
        });

        currSelected = mapAircraftSelectDropdown.options[mapAircraftSelectDropdown.value].text;

        initialCarouselPosition = new Vector3(
            mapCarousel.transform.position.x,
            mapCarousel.transform.position.y,
            mapCarousel.transform.position.z
        );

        selectCarouselItem(0); // select main scene as initial map by default

        hideSkinSelectDropdown();
    }
}
