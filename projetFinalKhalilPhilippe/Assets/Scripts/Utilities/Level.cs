using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{

    int level;
    int levelMax;
    float xp;
    float xpMax;
    public float neededXpForNextLvl;

    public void InitLevel(int _startLevel, int _levelMax, float _startXp, float _xpMax)
    {
        level = _startLevel;
        levelMax = _levelMax;
        xp = _startXp;
        xpMax = _xpMax;
        neededXpForNextLvl = xpMax - xp;
    }

    public void AddXp(float xpToAdd)
    {
        xp += xpToAdd; //Add xp
        if (xp >= xpMax)
        {
            if (level <= levelMax)
            { //Next level
                level += 1; //Adding level
                xp = 0; //Reset xp

                //Needed xp for next level become the double
                xpMax *= 2f;
                neededXpForNextLvl = xpMax - xp;
            }
            else
            { //Already level max
                xp = xpMax;
            }
        }
    }

}
