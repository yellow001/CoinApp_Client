using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static string GetCoinName(string instrument) {
        string result = "";
        try
        {
            result = instrument.Split('-')[0];
        }
        catch (System.Exception ex)
        {

        }

        return result;
    }

    public static string GetTacticsName(EM_TacticsState state) {

        string result = "";

        switch (state)
        {
            case EM_TacticsState.Start:
                result = "开始";
                break;
            case EM_TacticsState.Stop:
                result = "停止";
                break;
            case EM_TacticsState.Pause:
                result = "暂停";
                break;
            case EM_TacticsState.Short:
                result = "开空";
                break;
            case EM_TacticsState.CloseShort:
                result = "平空";
                break;
            case EM_TacticsState.Long:
                result = "开多";
                break;
            case EM_TacticsState.CloseLong:
                result = "平多";
                break;
            case EM_TacticsState.CloseAll:
                result = "全平";
                break;
            case EM_TacticsState.Hedge:
                result = "对冲";
                break;
            case EM_TacticsState.Normal:
                result = "正常";
                break;
            default:
                break;
        }

        return result;
    }


    public static string GetOrderOperationName(EM_OrderOperation state)
    {

        string result = "";
        switch (state)
        {
            case EM_OrderOperation.Normal:
                result = "正常";
                break;
            case EM_OrderOperation.ShortOnly:
                result = "只开空";
                break;
            case EM_OrderOperation.LongOnly:
                result = "只开多";
                break;
            case EM_OrderOperation.ShortNoClose:
                result = "空且不平";
                break;
            case EM_OrderOperation.LongNoClose:
                result = "多且不平";
                break;
            case EM_OrderOperation.NoClose:
                result = "不平";
                break;
            default:
                break;
        }

        return result;
    }

    public static List<string> GetTacticsStateStringList() {

        List<string> result = new List<string>();

        foreach (var item in Enum.GetValues(typeof(EM_TacticsState)))
        {
            result.Add(GetTacticsName((EM_TacticsState)item));
        }

        return result;
    }

    public static List<string> GetOrderStateStringList()
    {

        List<string> result = new List<string>();

        foreach (var item in Enum.GetValues(typeof(EM_OrderOperation)))
        {
            result.Add(GetOrderOperationName((EM_OrderOperation)item));
        }

        return result;
    }

    public static void AddTip(string tip) {
        CommonMgr.GetIns().V_Model.AddTip(new ResTipsMessage() { tips = tip }) ;
    }
}
