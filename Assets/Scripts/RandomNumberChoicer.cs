using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomNumberChoicer
{
    public static List<int> Dice(int count, int minInclusive, int maxExclusive, List<int> excludeNums = null)
    {
        List<int> choosedNums = new List<int>();
        int curNum;

        while (choosedNums.Count < count)
        {
            if (excludeNums == null) //���ܽ��Ѿ� �� ���ڸ� �������� �ʾ��� ���
                curNum = Random.Range(minInclusive, maxExclusive);
            else //���ܽ��Ѿ��ϴ� ���ڰ� ������ �ȴٸ�, �ش� ���ڰ� ������ ���� ������ random range �� ����. 
                do { curNum = Random.Range(minInclusive, maxExclusive); } while (excludeNums.Exists(excludeNum => excludeNum == curNum));

            choosedNums.Add(curNum);
            Debug.Log(curNum);
        }

        return choosedNums;
    }
}
