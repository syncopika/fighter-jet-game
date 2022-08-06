using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionMenuManager : MonoBehaviour
{
    public Dropdown mapAircraftSelectDropdown;
    public Dropdown skinSelectDropdown;
    
    // TODO: maybe make a controller script for map and aircraft carousels?
    public GameObject mapCarousel;      // carousel of all the available maps to choose from
    public GameObject aircraftCarousel; // all the aircraft to choose from

    public Material grey;
    public Material vandyOne;

    string currSelected; // map or aircraft select

    Material currSelectedAircraftSkin; // TODO: make this static to pass to main scene?

    Vector3 initialCarouselPosition;

    void hideSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 0;
    }

    void showSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 1;
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
        GameObject.Find("GameManager").transform.GetComponent<GameManager>().setPlayerSkin(currSelectedAircraftSkin);
        
        // assuming 1 renderer
        selectedAircraft.GetComponents<Renderer>()[0].material = currSelectedAircraftSkin;
    }

    void mapAircraftDropdownValueChanged(Dropdown change)
    {
        currSelected = mapAircraftSelectDropdown.options[change.value].text;
        changeThing(currSelected);
    }

    void changeThing(string selected)
    {
        // TODO: use an enum instead?
        if (selected.Equals("Aircraft"))
        {
            // display aircraft carousel
            // and skin options
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
        mapAircraftSelectDropdown.onValueChanged.AddListener(delegate {
            mapAircraftDropdownValueChanged(mapAircraftSelectDropdown);
        });

        skinSelectDropdown.onValueChanged.AddListener(delegate
        {
            skinSelectDropdownChanged(skinSelectDropdown);
        });

        currSelected = mapAircraftSelectDropdown.options[mapAircraftSelectDropdown.value].text;

        initialCarouselPosition = new Vector3(
            mapCarousel.transform.position.x,
            mapCarousel.transform.position.y,
            mapCarousel.transform.position.z
        );

        hideSkinSelectDropdown();
    }
}
