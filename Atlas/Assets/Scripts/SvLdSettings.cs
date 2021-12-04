using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SvLdSettings : MonoBehaviour
{
    public Slider SFXVolSlider, MusicVolSlider;
    public Text UpText, DownText, LeftText, RightText, AttackText, BlockText, PauseText, InteractText, RunText;
    public AudioMixer SFX, Music;
    private List<Text> Texts = new List<Text>();
    private Text CurrentText;
    private bool bRebinding = false;
    private bool bCanSave = true;
    private Resolution[] Resolutions;
    public Dropdown ResUI;
    private int ResIndex;

    private void Start()
    {
        MusicVolSlider.value = PlayerPrefs.GetFloat("MusicVol", 1); //0 = No Volume, 1 = Max Volume
        SFXVolSlider.value = PlayerPrefs.GetFloat("SFXVol", 1);
        PlayerPrefs.GetString("ScreenResolution", Screen.currentResolution.width.ToString() + "x" + Screen.currentResolution.height.ToString());
        PlayerPrefs.GetInt("Fullscreen", 1);

        //Controls, the string will be converted to keycodes when needed
        UpText.text = PlayerPrefs.GetString("Up", "W"); DownText.text = PlayerPrefs.GetString("Down", "S");
        LeftText.text = PlayerPrefs.GetString("Left", "A"); RightText.text = PlayerPrefs.GetString("Right", "D");
        AttackText.text = PlayerPrefs.GetString("Attack", "Mouse0"); BlockText.text = PlayerPrefs.GetString("Block", "Mouse1");
        PauseText.text = PlayerPrefs.GetString("Pause", "Escape"); InteractText.text = PlayerPrefs.GetString("Interact", "E");
        RunText.text = PlayerPrefs.GetString("Run", "LeftShift");

        Texts.Add(UpText); Texts.Add(DownText); Texts.Add(LeftText); Texts.Add(RightText);
        Texts.Add(AttackText); Texts.Add(BlockText); Texts.Add(PauseText);

        SFX.SetFloat("Volume", SFXVolSlider.value);
        Music.SetFloat("Volume", MusicVolSlider.value);

        ResIndex = 0;
        Resolutions = Screen.resolutions;
        List<string> ResOptions = new List<string>();
        ResUI.ClearOptions();

        foreach(Resolution res in Resolutions)
        {
          ResOptions.Add(res.width.ToString() + "x" + res.height.ToString() + "@:" + res.refreshRate.ToString());
        }

        for (int i = 1; i < Resolutions.Length; i++)
        {
            if (Screen.currentResolution.width == Resolutions[i].width && Screen.currentResolution.height == Resolutions[i].height)
            {
                ResIndex = i;
            }
        }
        
        ResUI.AddOptions(ResOptions);
        ResUI.value = ResIndex;
        ResUI.RefreshShownValue();

        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        { Screen.fullScreen = true; }
        else { Screen.fullScreen = false; }
    }

    private void Update()
    {
        if (bRebinding && Input.anyKeyDown)
        {
            foreach (KeyCode Key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(Key))
                {
                    CurrentText.text = Key.ToString();

                    foreach (Text KeyText in Texts)
                    {
                        if (CurrentText != KeyText)
                        {
                            if (Key.ToString() == KeyText.text)
                            {
                                bCanSave = false;
                                KeyText.color = new Color(1, 0, 0, 1);
                                CurrentText.color = new Color(1, 0, 0, 1);
                                break;
                            }
                            else
                            {
                                bCanSave = true;
                                KeyText.color = new Color(0.980392157f, 0.694117647f, 0.62745098f, 1f);
                                CurrentText.color = new Color(0.980392157f, 0.694117647f, 0.62745098f, 1f);
                            }
                        }
                    }
                }
            }
            bRebinding = false;
        }
    }

    public void ChangeScreenType(bool FS)
    {
        Screen.fullScreen = FS;
        if (FS) { PlayerPrefs.SetInt("Fullscreen", 1); }
        else
        { PlayerPrefs.SetInt("Fullscreen", 0); }
    }

    public void ChangeResolution(int ResIndex)
    {
        Screen.SetResolution(Resolutions[ResIndex].width, Resolutions[ResIndex].height, Screen.fullScreen);
        Debug.Log(Resolutions[ResIndex].width + "x" + Resolutions[ResIndex].height);
        PlayerPrefs.SetString("ScreenResolution", Resolutions[ResIndex].width + "x" + Resolutions[ResIndex].height);
    }

    public void SetVolume()
    {
        SFX.SetFloat("Volume", SFXVolSlider.value);
        Music.SetFloat("Volume", MusicVolSlider.value);
    }

    public void ResetBinds()
    {
        PlayerPrefs.SetString("Down", "S");
        PlayerPrefs.SetString("Left", "A");
        PlayerPrefs.SetString("Right", "D");
        PlayerPrefs.SetString("Attack", "Mouse0");
        PlayerPrefs.SetString("Block", "Mouse1");
        PlayerPrefs.SetString("Pause", "Escape");
        PlayerPrefs.SetString("Interact", "E");
    }

    public void SaveSettings()
    {
        if (bCanSave)
        {
            PlayerPrefs.SetFloat("MusicVol", MusicVolSlider.value);
            PlayerPrefs.SetFloat("SFXVol", SFXVolSlider.value);
            PlayerPrefs.SetString("Up", UpText.text);
            PlayerPrefs.SetString("Down", DownText.text);
            PlayerPrefs.SetString("Left", LeftText.text);
            PlayerPrefs.SetString("Right", RightText.text);
            PlayerPrefs.SetString("Attack", AttackText.text);
            PlayerPrefs.SetString("Block", BlockText.text);
            PlayerPrefs.SetString("Pause", PauseText.text);
            PlayerPrefs.SetString("Interact", InteractText.text);
            PlayerPrefs.SetString("Run", RunText.text);
        }
    }

    public void RebindKey(Text cText)
    {
        if (!bRebinding)
        {
            CurrentText = cText;
            cText.text = "Enter Key";
            bRebinding = true;
        }
    }
}