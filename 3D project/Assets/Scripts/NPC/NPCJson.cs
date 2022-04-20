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
    public string title { get; set; } //����Ʈ ����
    public int questType { get; set; } //����Ʈ ����
    public List<string> talk { get; set; } //�Ϲ� ��ȭ
    public List<string> targetObject { get; set; } //��ǥ���ϴ� ������Ʈ
    public List<int> targetCount { get; set; } //��ǥ���ϴ� ������Ʈ ����
    public List<ResultObject> resultObject { get; set; } //����
    public List<string> resultFail { get; set; } //����
    public List<string> okTalk { get; set; } //���� ��ȭ
    public List<string> noTalk { get; set; } //���� ��ȭ
    public List<string> successTalk { get; set; } //�Ϸ� ��ȭ
    public List<string> notYet { get; set; } //�̿Ϸ� ��ȭ
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