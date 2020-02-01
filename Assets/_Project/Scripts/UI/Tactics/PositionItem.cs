using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionItem : MonoBehaviour
{
    /// <summary>
    /// 均价
    /// </summary>
    [SerializeField]
    Text priceTx;

    /// <summary>
    /// 多空方向
    /// </summary>
    [SerializeField]
    Text dirTx;

    /// <summary>
    /// 盈利率
    /// </summary>
    [SerializeField]
    Text percentTx;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Refresh(Position data,float curPrice) {
        priceTx.text = data.V_Avg_Price.ToString("f2");
        dirTx.text = data.V_Dir > 0 ? "Long" : "Short";
        percentTx.text = data.GetPercent(curPrice).ToString("f2")+"%";
    }
}
