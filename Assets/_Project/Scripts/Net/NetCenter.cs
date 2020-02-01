using NetFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NetCenter: InsManager<NetCenter>
{

    Dictionary<int, Type> m_MsgTypeDic = new Dictionary<int, Type>();
    Dictionary<int, Action<BaseMessage>> m_MsgEventDic = new Dictionary<int, Action<BaseMessage>>();

    public void AddMsgEvent<T>(int pid, Action<BaseMessage> cb) where T : BaseMessage
    {
        m_MsgTypeDic[pid] = typeof(T);
        m_MsgEventDic[pid] = cb;
    }

    private void Update()
    {
        if (NetIO.Ins.msg != null && NetIO.Ins.msg.Count > 0) {
            TransModel model = NetIO.Ins.msg[0];
            OnMsgReceive(model);
            NetIO.Ins.msg.Remove(model);
        }
    }

    public void OnMsgReceive<T>(T model)where T:TransModel
    {
        try
        {
            if (m_MsgEventDic.ContainsKey(model.pID) && m_MsgTypeDic.ContainsKey(model.pID))
            {
                BaseMessage msg = Activator.CreateInstance(m_MsgTypeDic[model.pID]) as BaseMessage;
                m_MsgEventDic[model.pID](msg.ReadData(model.msgBytes));
            }
            else
            {
                //Console.WriteLine("");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public void Send<T>(int pid, T msg, int area = 0)
    {
        NetIO.Ins.Send(pid, area, msg);
    }
}
