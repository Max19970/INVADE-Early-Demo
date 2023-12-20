using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public enum SectionNames {TheLab, Another};

public class LocationInfo : MonoBehaviour
{
    public SectionNames sectionName = SectionNames.TheLab;
    public bool popUpOnStart;

    private LocalizeStringEvent sectionLocalization;
    private LocalizeStringEvent descriptionLocalization;

    public void SetSectionName()
    {
        switch (sectionName)
        {
            case SectionNames.TheLab:
                sectionLocalization.StringReference.SetReference("UI Text", "LabSectionText");
                break;
        }
    }

    public void SetSectionDescription()
    {
        switch (sectionName)
        {
            case SectionNames.TheLab:
                descriptionLocalization.StringReference.SetReference("UI Text", "LabDescriptionText");
                break;
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        sectionLocalization = transform.Find("Section Name").GetComponent<LocalizeStringEvent>();
        descriptionLocalization = transform.Find("Description").GetComponent<LocalizeStringEvent>();
    }

    void Start()
    {
        SetSectionName();
        SetSectionDescription();
        if (popUpOnStart) GetComponent<Animator>().Play("Info Pop Up", 0, 0);
    }
}
