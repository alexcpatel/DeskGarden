using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIPlantControl : MonoBehaviour
{

    public static int plant_chosen;
    public static bool info_mode;

    public Button plant_button, sunflower_button, cactus_button, rose_button, bonsai_button, grass_button, water_button, dig_button, more_info_button;
    public Button exit_button;
    public Image button_bar, more_info_panel;
    public static Text data_text;

    public static float scale = 1.5f;
    public static float down = scale * -90f;

    public Sprite plantSprite, sunflowerSprite, cactusSprite, roseSprite, bonsaiSprite, grassSprite;

    List<Button> list_of_plant_buttons;
    List<Button> list_of_visible;

    // Use this for initialization
    void Start()
    {

        data_text = gameObject.transform.GetChild(gameObject.transform.childCount - 1).GetComponent<Text>();

        list_of_plant_buttons = new List<Button>();
        list_of_visible = new List<Button>();

        list_of_plant_buttons.Add(plant_button);
        list_of_plant_buttons.Add(sunflower_button);
        list_of_plant_buttons.Add(cactus_button);
        list_of_plant_buttons.Add(rose_button);
        list_of_plant_buttons.Add(bonsai_button);
        list_of_plant_buttons.Add(grass_button);


        //setting positions of the buttons
        for (int i = 0; i < list_of_plant_buttons.Count; i++)
        {
            if (i < 3)
            {
                list_of_plant_buttons[i].gameObject.SetActive(true);
                list_of_plant_buttons[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(140 * scale * (i - 1), down, 0);
                list_of_visible.Add(list_of_plant_buttons[i]);
            }
            else
            {
                list_of_plant_buttons[i].gameObject.SetActive(false);
            }

            //changes the scale of the objects
            list_of_plant_buttons[i].transform.localScale = new Vector3(scale, scale, scale);
        }

        dig_button.transform.localScale = new Vector3(scale, scale, scale);
        water_button.transform.localScale = new Vector3(scale, scale, scale);
        dig_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(140 * scale * -2, down, 0);
        water_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(140 * scale * 2, down, 0);

        button_bar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, down, 0);
        button_bar.transform.localScale = new Vector3(scale, scale, scale);

        exit_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(-down, down, 0);
        exit_button.transform.localScale = new Vector3(scale, scale, scale);
        exit_button.gameObject.SetActive(false);

        more_info_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(down, down, 0);
        more_info_button.transform.localScale = new Vector3(scale, scale, scale);

        more_info_panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -60 * scale, 0);
        more_info_panel.transform.localScale = new Vector3(scale, scale, scale);
        more_info_panel.gameObject.SetActive(false);
    }

    void getTouches()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            scroll(true, 5);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            scroll(false, 5);
        }
#endif

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float touchTime = Input.GetTouch(0).deltaTime;
            float touchSpeed = touchDeltaPosition.x / touchTime;

            if (Math.Abs(touchSpeed) > 2000)
            {
                if (touchSpeed < 0)
                {
                    UIPlantControl.data_text.text = "v: " + touchSpeed.ToString();
                    scroll(false, Math.Min(20, Math.Abs(touchSpeed / 800)));
                }
                else
                {
                    UIPlantControl.data_text.text = "v: " + touchSpeed.ToString();
                    scroll(true, Math.Min(20, Math.Abs(touchSpeed / 800)));
                }
            }
        }
    }

    void scroll(bool right, float mag)
    {

        if (right)
        {
            foreach (Button plant in list_of_visible)
            {
                float prev_x = plant.GetComponent<RectTransform>().anchoredPosition.x;
                float new_x = prev_x + 10 * mag;

                plant.GetComponent<RectTransform>().anchoredPosition = new Vector3(new_x, down, 0);
            }

            Button plant_ = list_of_visible[0];

            if (plant_ == list_of_plant_buttons[0])
            {
                if (plant_.GetComponent<RectTransform>().anchoredPosition.x > -140 * scale)
                {

                    list_of_visible[0].GetComponent<RectTransform>().anchoredPosition = new Vector3(-140 * scale, down, 0);
                    list_of_visible[1].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, down, 0);
                    list_of_visible[2].GetComponent<RectTransform>().anchoredPosition = new Vector3(140 * scale, down, 0);

                    if (list_of_visible.Count == 4)
                    {
                        list_of_visible[3].gameObject.SetActive(false);
                        list_of_visible.RemoveAt(3);
                    }
                }
                return;
            }

            plant_ = list_of_visible[list_of_visible.Count - 1];
            int index = list_of_plant_buttons.IndexOf(list_of_visible[0]);
            float x_of_prev = list_of_visible[0].GetComponent<RectTransform>().anchoredPosition.x;

            if (plant_.GetComponent<RectTransform>().anchoredPosition.x > 140 * scale)
            {
                if (list_of_visible.Count < 4)
                {
                    Button new_plant = list_of_plant_buttons[index - 1];
                    new_plant.GetComponent<RectTransform>().anchoredPosition = new Vector3(x_of_prev - 140 * scale, down, 0);
                    new_plant.gameObject.SetActive(true);
                    list_of_visible.Insert(0, new_plant);
                }
                if (plant_.GetComponent<RectTransform>().anchoredPosition.x >= 280 * scale)
                {
                    list_of_visible.Remove(plant_);
                    plant_.gameObject.SetActive(false);
                }
            }

        }
        else
        {
            foreach (Button plant in list_of_visible)
            {
                float prev_x = plant.GetComponent<RectTransform>().anchoredPosition.x;
                float new_x = prev_x - 10 * mag;

                plant.GetComponent<RectTransform>().anchoredPosition = new Vector3(new_x, down, 0);
            }

            Button plant_ = list_of_visible[list_of_visible.Count - 1];

            if (plant_ == list_of_plant_buttons[list_of_plant_buttons.Count - 1])
            {
                if (plant_.GetComponent<RectTransform>().anchoredPosition.x < 140 * scale)
                {

                    int adder = 0;

                    if (list_of_visible.Count > 3)
                    {
                        adder = 1;
                    }
                    list_of_visible[adder].GetComponent<RectTransform>().anchoredPosition = new Vector3(-140 * scale, down, 0);
                    list_of_visible[1 + adder].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, down, 0);
                    list_of_visible[2 + adder].GetComponent<RectTransform>().anchoredPosition = new Vector3(140 * scale, down, 0);

                    if (list_of_visible.Count == 4)
                    {
                        list_of_visible[0].gameObject.SetActive(false);
                        list_of_visible.RemoveAt(0);
                    }
                }
                return;
            }

            plant_ = list_of_visible[0];
            int index = list_of_plant_buttons.IndexOf(list_of_visible[list_of_visible.Count - 1]);
            float x_of_prev = list_of_visible[list_of_visible.Count - 1].GetComponent<RectTransform>().anchoredPosition.x;

            if (plant_.GetComponent<RectTransform>().anchoredPosition.x < -140 * scale)
            {
                if (list_of_visible.Count < 4)
                {
                    Button new_plant = list_of_plant_buttons[index + 1];
                    new_plant.GetComponent<RectTransform>().anchoredPosition = new Vector3(x_of_prev + 140 * scale, down, 0);
                    new_plant.gameObject.SetActive(true);
                    list_of_visible.Add(new_plant);
                }
                if (plant_.GetComponent<RectTransform>().anchoredPosition.x <= -280 * scale)
                {
                    list_of_visible.Remove(plant_);
                    plant_.gameObject.SetActive(false);
                }
            }
        }
    }

    public void on_info_clicked()
    {
        UIPlantControl.info_mode = true;
        exit_button.gameObject.SetActive(true);
    }

    public void activateMoreInfoPanel(string name)
    {
        
        switch (name.Substring(0, 3))
        {
            case "pla":
                more_info_panel.sprite = plantSprite;
                break;
            case "obj":
                more_info_panel.sprite = sunflowerSprite;
                break;
            case "Cac":
                more_info_panel.sprite = cactusSprite;
                break;
            case "F_f":
                more_info_panel.sprite = roseSprite;
                break;
            case "Mes":
                more_info_panel.sprite = bonsaiSprite;
                break;
            case "gra":
                more_info_panel.sprite = grassSprite;
                break;
        }

        more_info_panel.gameObject.SetActive(true);
    }

    public void on_exit_clicked()
    {
        UIPlantControl.info_mode = false;
        exit_button.gameObject.SetActive(false);
        more_info_panel.gameObject.SetActive(false);
    }

    public void plant_button_clicked()
    {

        if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == dig_button)
        {
            plant_chosen = 0;
        }
        else if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() == water_button)
        {
            plant_chosen = 1;
        }
        else
        {
            plant_chosen = list_of_plant_buttons.IndexOf(EventSystem.current.currentSelectedGameObject.GetComponent<Button>()) + 2;
        }
    }


    // Update is called once per frame
    void Update()
    {
        getTouches();
    }
}
