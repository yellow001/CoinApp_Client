using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTip : MonoBehaviour
{
    [SerializeField]
    CanvasGroup group;

    [SerializeField]
    Text content;

    TimeEvent hideTimeEvent;

    TimeEvent showTimeEvent;

    int hideEventID, showEventID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Refresh(string tip)
    {
        gameObject.SetActive(true);
        content.text = tip;
        StopCoroutine(Show());
        StartCoroutine(Show());
    }

    void Hide()
    {
        if (hideTimeEvent == null) {
            InitModel();
        }

        group.alpha = 1;
        this.RemoveTimeEventEx(hideEventID);
        hideEventID = this.AddTimeEventEx(hideTimeEvent);
        //this.AddTimeEvent(0.25f, () => { gameObject.SetActive(false); }, (t, p) => { group.alpha = 1 - p; }, 1);
    }

    public IEnumerator Show()
    {
        if (showTimeEvent == null) {
            InitModel();
        }

        group.alpha = 0;
        this.RemoveTimeEventEx(showEventID);
        showEventID = this.AddTimeEventEx(showTimeEvent);

        yield return new WaitForSeconds(2);

        Hide();

        //this.AddTimeEvent(0.25f, null, (t, p) => { group.alpha = p; }, 1);
    }

    void InitModel() {
        hideTimeEvent = new TimeEvent(0.5f, () => { gameObject.SetActive(false); }, (t, p) => { group.alpha = 1 - p; }, 1);

        showTimeEvent = new TimeEvent(0.5f, null, (t, p) => { group.alpha = p; }, 1);
    }
}
