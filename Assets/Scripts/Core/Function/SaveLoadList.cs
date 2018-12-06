﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveLoadList : MonoBehaviour {
    private GameObject _Button;

    private void Start()
    {
        _Button = (GameObject)Resources.Load("Prefabs/UI/SaveButton");
        transform.root.Find("SaveLoadMask").gameObject.SetActive(false);
    }

    public void CreateLoadList()
    {
        GameObject button;
        var allButtons = new List<GameObject>();
        var saves = Global.GetInstance().saves;

        foreach (var save in saves)
        {
            button = GameObject.Instantiate(_Button, transform);
            button.name = Global.GetInstance().IndexToString(save.ID);
            //button.GetComponentInChildren<Text>().alignment = TextAnchor.MiddleLeft;
            //button.GetComponentInChildren<Text>().text = tempSkill.CName;
            //button.GetComponentInChildren<Text>().resizeTextForBestFit = false;
            //button.GetComponentInChildren<Text>().fontSize = 45;
            //button.GetComponentInChildren<Text>().GetComponent<RectTransform>().sizeDelta = new Vector2(-30, 0);

            button.transform.Find("SaveName").GetComponent<Text>().text = save.saveName;
            button.transform.Find("SaveTime").GetComponent<Text>().text = Global.GetInstance().StampToDateTime(save.timeStamp);

            button.GetComponent<Button>().onClick.AddListener(OnLoadButtonClick);
            //button.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 72);
            button.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
            button.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            button.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            allButtons.Add(button);
            transform.root.Find("SaveLoadMask").gameObject.SetActive(true);
        }

        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].transform.localPosition = new Vector3(0, -(int)(i * (allButtons[i].GetComponent<RectTransform>().sizeDelta.y + 5)), 0);
        }
    }

    private void OnLoadButtonClick()
    {
        var btn = EventSystem.current.currentSelectedGameObject;
        Global.GetInstance().Load(btn.name);
        Destroy(gameObject);
    }
}