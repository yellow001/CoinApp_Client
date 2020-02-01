using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WinTacticsList : MonoBehaviour
{
    [SerializeField]
    CanvasGroup group;

    [SerializeField]
    LayoutSize TacticsLayout;

    [SerializeField]
    TacticsItem tacticsItem;

    /// <summary>
    /// 改变运行状态
    /// </summary>
    [SerializeField]
    Dropdown runtimeDropdown;

    /// <summary>
    /// 改变下单状态
    /// </summary>
    [SerializeField]
    Dropdown orderDropdown;

    List<TacticsItem> itemList = new List<TacticsItem>();

    // Start is called before the first frame update
    void Awake()
    {
        runtimeDropdown.ClearOptions();
        runtimeDropdown.AddOptions(Util.GetTacticsStateStringList());

        orderDropdown.ClearOptions();
        orderDropdown.AddOptions(Util.GetOrderStateStringList());

        EventMgr.Ins.AddEventFun(EM_AccountEvent.UpdateTactics, Refresh);


        Refresh();


        this.AddTimeEventEx(3, () =>
        {
            TacticsMgr.GetIns().V_NetLogic.ReqTacticsInfo();
        },-1,true);
    }



    public void Refresh(params object[] args) {

        List<Tactics> data = TacticsMgr.GetIns().V_Model.GetAccountInfoDic().Values.ToList();

        for (int i = 0; i < data.Count; i++)
        {
            if (i >= itemList.Count)
            {
                TacticsItem item = Instantiate(tacticsItem);
                item.transform.SetParent(TacticsLayout.transform, false);
                itemList.Add(item);
            }
            itemList[i].gameObject.SetActive(true);
            itemList[i].Refresh(data[i]);
        }

        for (int j = data.Count; j < itemList.Count; j++)
        {
            itemList[j].gameObject.SetActive(false);
        }

        TacticsLayout.RefreshSize();

        //CommonMgr.GetIns().V_Model.AddTip(new ResTipsMessage() { tips = "test" });
    }

    public void RuntimeStateChange()
    {
        foreach (var item in itemList)
        {
            item.SetRuntimeState((EM_TacticsState)runtimeDropdown.value);
        }
    }

    public void OrderOperationChange()
    {
        foreach (var item in itemList)
        {
            item.SetOrderOperation((EM_OrderOperation)orderDropdown.value);
        }
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
        this.AddTimeEventEx(0.25f,null, (t, p) => { group.alpha = p; }, 1);
    }
}
