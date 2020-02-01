using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TacticsItem : MonoBehaviour
{
    /// <summary>
    /// 币种
    /// </summary>
    [SerializeField]
    Text coinTx;

    /// <summary>
    /// 持仓信息父物体
    /// </summary>
    [SerializeField]
    GameObject PositionGrid;

    /// <summary>
    /// 持仓实例化项
    /// </summary>
    [SerializeField]
    PositionItem positionItem;

    /// <summary>
    /// 运行状态
    /// </summary>
    [SerializeField]
    Text runtimeStateTx;

    /// <summary>
    /// 下单操作
    /// </summary>
    [SerializeField]
    Text orderStateTx;

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

    List<PositionItem> itemList = new List<PositionItem>();

    Tactics m_data;

    // Start is called before the first frame update
    void Awake()
    {
        runtimeDropdown.ClearOptions();
        runtimeDropdown.AddOptions(Util.GetTacticsStateStringList());

        orderDropdown.ClearOptions();
        orderDropdown.AddOptions(Util.GetOrderStateStringList());


    }

    public void Refresh(Tactics data) {

        m_data = data;

        coinTx.text = Util.GetCoinName(data.V_Instrument_id);

        runtimeStateTx.text= Util.GetTacticsName(data.V_TacticsState);

        orderStateTx.text = Util.GetOrderOperationName(data.V_OrderState);

        runtimeDropdown.SetValueWithoutNotify((int)data.V_TacticsState);
        orderDropdown.SetValueWithoutNotify((int)data.V_OrderState);

        int dataCount = 0;
        if (data.V_AccountInfo != null && data.V_AccountInfo.V_Positions != null) {
            dataCount = data.V_AccountInfo.V_Positions.Count;
        }
        for (int i = 0; i < dataCount; i++)
        {
            if (i >= itemList.Count) {
                PositionItem item = Instantiate(positionItem);
                item.transform.SetParent(PositionGrid.transform, false);
                itemList.Add(item);
            }
            itemList[i].gameObject.SetActive(true);
            itemList[i].Refresh(data.V_AccountInfo.V_Positions[i], data.V_AccountInfo.V_CurPrice);
        }

        for (int j = dataCount; j < itemList.Count; j++)
        {
            itemList[j].gameObject.SetActive(false);
        }
    }

    public void RuntimeStateChange() {
        Debug.Log(runtimeDropdown.value+" "+(EM_TacticsState)runtimeDropdown.value);
        if (m_data != null) {
            TacticsMgr.GetIns().V_NetLogic.ReqChangeTacticsState(Util.GetCoinName(m_data.V_Instrument_id), runtimeDropdown.value);
        }
    }

    public void OrderOperationChange()
    {
        Debug.Log(orderDropdown.value+"  "+(EM_OrderOperation)orderDropdown.value);
        if (m_data != null)
        {
            TacticsMgr.GetIns().V_NetLogic.ReqChangeOrderState(Util.GetCoinName(m_data.V_Instrument_id), orderDropdown.value);
        }
    }

    public void SetRuntimeState(EM_TacticsState state)
    {
        runtimeDropdown.value = (int)state;
    }

    public void SetOrderOperation(EM_OrderOperation state)
    {
        orderDropdown.value = (int)state;
    }
}
