using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAircraftSelectionDropdown : MonoBehaviour
{
    public Dropdown skinSelectDropdown;
    public GameObject mapCarousel; // carousel of all the available maps to choose from
    public GameObject aircraftCarousel; // all the aircraft to choose from

    Dropdown dropdown;
    string currSelected;

    Vector3 initialCarouselPosition;

    void hideSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 0;
    }

    void showSkinSelectDropdown()
    {
        skinSelectDropdown.GetComponent<CanvasGroup>().alpha = 1;
    }

    void dropdownValueChanged(Dropdown change)
    {
        currSelected = dropdown.options[change.value].text;
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
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            dropdownValueChanged(dropdown);
        });

        currSelected = dropdown.options[dropdown.value].text;

        initialCarouselPosition = new Vector3(
            mapCarousel.transform.position.x,
            mapCarousel.transform.position.y,
            mapCarousel.transform.position.z
        );

        hideSkinSelectDropdown();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
