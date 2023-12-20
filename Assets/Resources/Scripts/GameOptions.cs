using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOptions : MonoBehaviour
{
    public Toggle option;
    public string prefRefference;

    // public void SetViewVisibility()
    // {
    //     PlayerPrefs.SetInt("enemyView", (myOption == MyOptions.EnemyViewVisible && option.isOn) ? 1 : 0);
    //     Debug.Log(PlayerPrefs.GetInt("enemyView"));
    // }

    // public void SetDangerVisibility()
    // {
    //     PlayerPrefs.SetInt("enemyDanger", (myOption == MyOptions.EnemyDangerVisible && option.isOn) ? 1 : 0);
    //     Debug.Log(PlayerPrefs.GetInt("enemyDanger"));
    // }

    // public void SetHealthVisibility()
    // {
    //     PlayerPrefs.SetInt("enemyHealth", (myOption == MyOptions.EnemyHealthVisible && option.isOn) ? 1 : 0);
    //     Debug.Log(PlayerPrefs.GetInt("enemyHealth"));
    // }

    // public void SetDamageVisibility()
    // {
    //     PlayerPrefs.SetInt("damageNumbers", (myOption == MyOptions.DamageNumbersVisible && option.isOn) ? 1 : 0);
    //     Debug.Log(PlayerPrefs.GetInt("damageNumbers"));
    // }

    public void SetSetting(bool vis)
    {
        PlayerPrefs.SetInt(prefRefference, vis ? 1 : 0);
    }
    
    public void ResetMe()
    {
        option.isOn = false;
        SetSetting(false);
    }

    void Start()
    {
        option.onValueChanged.AddListener((bool _) => {SetSetting(_);});
        if (PlayerPrefs.HasKey(prefRefference)) {option.isOn = PlayerPrefs.GetInt(prefRefference) == 1;}
        // Debug.Log(PlayerPrefs.GetInt("enemyView"));
        // Debug.Log(PlayerPrefs.GetInt("enemyDanger"));
        // Debug.Log(PlayerPrefs.GetInt("enemyHealth"));
        // Debug.Log(PlayerPrefs.GetInt("damageNumbers"));
        // if (!PlayerPrefs.HasKey("enemyView"))
        // {
        //     PlayerPrefs.SetInt("enemyView", 0);
        //     option.isOn = defaultOn;
        // }
        // else
        // {
        //     if (myOption == MyOptions.EnemyViewVisible) option.isOn = PlayerPrefs.GetInt("enemyView") == 1;
        //     if (myOption == MyOptions.EnemyViewInvisible) option.isOn = PlayerPrefs.GetInt("enemyView") == 0;
        // }

        // if (!PlayerPrefs.HasKey("enemyDanger"))
        // {
        //     PlayerPrefs.SetInt("enemyDanger", 0);
        //     option.isOn = defaultOn;
        // }
        // else
        // {
        //     if (myOption == MyOptions.EnemyDangerVisible) option.isOn = PlayerPrefs.GetInt("enemyDanger") == 1;
        //     if (myOption == MyOptions.EnemyDangerInvisible) option.isOn = PlayerPrefs.GetInt("enemyDanger") == 0;
        // }

        // if (!PlayerPrefs.HasKey("enemyHealth"))
        // {
        //     PlayerPrefs.SetInt("enemyHealth", 0);
        //     option.isOn = defaultOn;
        // }
        // else
        // {
        //     if (myOption == MyOptions.EnemyHealthVisible) option.isOn = PlayerPrefs.GetInt("enemyHealth") == 1;
        //     if (myOption == MyOptions.EnemyHealthInvisible) option.isOn = PlayerPrefs.GetInt("enemyHealth") == 0;
        // }

        // if (!PlayerPrefs.HasKey("damageNumbers"))
        // {
        //     PlayerPrefs.SetInt("damageNumbers", 0);
        //     option.isOn = defaultOn;
        // }
        // else
        // {
        //     if (myOption == MyOptions.DamageNumbersVisible) option.isOn = PlayerPrefs.GetInt("damageNumbers") == 1;
        //     if (myOption == MyOptions.DamageNumbersInvisible) option.isOn = PlayerPrefs.GetInt("damageNumbers") == 0;
        // }
    }
}
