using NetFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLogin : MonoBehaviour
{

    [SerializeField]
    CanvasGroup group;

    public InputField V_IPInput;

    public InputField V_PortInput;

    public InputField V_KeyInput;

    [SerializeField]
    WinTacticsList winTacticsList;

    private void Start()
    {
        string ip = PlayerPrefs.GetString("IP", "");
        if (!string.IsNullOrEmpty(ip)) {
            V_IPInput.text = ip;
        }

        int port = PlayerPrefs.GetInt("Port", 0);
        if (port!=0)
        {
            V_PortInput.text = port.ToString();
        }

        string key = PlayerPrefs.GetString("Key", "");
        if (!string.IsNullOrEmpty(key))
        {
            V_KeyInput.text = key;
        }

        EventMgr.Ins.AddEventFun(EM_LoginEvent.ConnectSuccess, OnConnectSuccess);
    }


    public void OnSureBtnClick() {
        NetIO.V_IP = V_IPInput.text;
        int.TryParse(V_PortInput.text,out NetIO.V_Port) ;
        NetIO.V_ConnectKey = V_KeyInput.text;

        NetIO.Ins.Connect();
        EventMgr.Ins.InvokeDeList(EM_LoginEvent.BeginConnect);
    }

    public void OnConnectSuccess(params object[] args) {

        PlayerPrefs.SetString("IP",NetIO.V_IP);
        PlayerPrefs.SetInt("Port", NetIO.V_Port);
        PlayerPrefs.SetString("Key", NetIO.V_ConnectKey);

        Hide();
        winTacticsList.Show();

        TacticsMgr.GetIns();
        CommonMgr.GetIns();
    }

    public void Hide()
    {
        group.alpha = 1;
        this.AddTimeEventEx(0.25f, () => { gameObject.SetActive(false); }, (t, p) => { group.alpha = 1 - p; }, 1);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        group.alpha = 0;
        this.AddTimeEventEx(0.25f, null, (t, p) => { group.alpha = p; }, 1);
    }
}
