using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;

public class StackController : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<PaperController> stackedPapers = new List<PaperController>();
    [SerializeField] private Transform stackPos;

    public Transform StackPos => stackPos;
    #endregion

    #region Methods 
    public Vector3 GetPaperStackPos()
    {
        Vector3 pos = stackPos.transform.localPosition;

        for (int i = 0; i < stackedPapers.Count; i++)
        {
            var paper = stackedPapers[i];
            float yPos = stackPos.transform.position.y + ((i + 1) * paper.addYPos);
            pos = new Vector3(stackPos.transform.localPosition.x, yPos, stackPos.transform.localPosition.z);
        }
        return pos;
    }

    private PaperController GetLastPaper() 
    {
        PaperController paper = default;

        foreach (var PaperController in stackedPapers)
        {
            paper = stackedPapers[stackedPapers.Count - 1];
        }
        return paper;
    }

    public void GetPaper(Vector3 pos, Transform parent, List<PaperController> paperlist)
    {
        if (stackedPapers.Count == 0) return;

        var lastPaper = GetLastPaper();
        lastPaper.MoveDesk(parent, pos);
        paperlist.Add(lastPaper);
        RemoveList(lastPaper);

    }
    public void AddList(PaperController paper)
    {
        if (!stackedPapers.Contains(paper))
        {
            stackedPapers.Add(paper);
        }
    }

    private void RemoveList(PaperController paper)
    {
        if (stackedPapers.Contains(paper))
        {
            stackedPapers.Remove(paper);
        }
    }

    
    public List<PaperController> GetPapers() 
    {
        return stackedPapers;
    }
    #endregion
}
