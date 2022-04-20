using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class NPCJson
{
    IList<string> baseTalk { get; set; }
    List<JsonNomalTalk> talks { get; set; }
    List<JsonQuest> quest { get; set; }
}

public class JsonNomalTalk
{
    public string title { get; set; }
    public List<string> talk { get; set; }
}

public class JsonQuest
{
    public string title { get; set; } //퀘스트 제목
    public int questType { get; set; } //퀘스트 종류
    public List<string> talk { get; set; } //일반 대화
    public List<string> targetObject { get; set; } //목표로하는 오브젝트
    public List<int> targetCount { get; set; } //목표로하는 오브젝트 개수
    public List<ResultObject> resultObject { get; set; } //보상
    public List<string> resultFail { get; set; } //보상
    public List<string> okTalk { get; set; } //수락 대화
    public List<string> noTalk { get; set; } //거절 대화
    public List<string> successTalk { get; set; } //완료 대화
    public List<string> notYet { get; set; } //미완료 대화
}

public class ResultObject
{
    public ResultObject() { }
    public ResultObject(string s, int c)
    {
        objectName = s;
        count = c;
    }

    public string objectName;
    public int count;
}