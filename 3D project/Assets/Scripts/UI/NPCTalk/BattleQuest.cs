using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleQuest
{
    public string npcName { get; set; }
    public string title { get; set; }
    public List<string> targetObject { get; set; }
    public List<int> nowObject { get; set; }
    public int state { get; set; } // 진행중: 0, 진행중완료: 1, 퀘스트 완료: 2

    public BattleQuest()
    {

    }
    public BattleQuest(string npcName, JsonQuest quest)
    {
        this.npcName = npcName;
        title = quest.title;
        targetObject = new List<string>(quest.targetObject.ToArray());
        state = 0;
    }

    public void SetQuest(string npcName, JsonQuest quest)
    {
        this.npcName = npcName;
        title = quest.title;
        targetObject = new List<string>(quest.targetObject.ToArray());
        state = 0;
    }

    
}
