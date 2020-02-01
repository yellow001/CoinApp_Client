using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonModel
{
    List<ResTipsMessage> tipList = new List<ResTipsMessage>();

    public void AddTip(ResTipsMessage msg) {
        tipList.Add(msg);
    }

    public List<ResTipsMessage> GetTipList() {
        return tipList;
    }
}
