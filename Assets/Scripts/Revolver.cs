using UnityEngine;

public class Revolver
{
    private int bullets;
    private int currentChambers;

    public Revolver(int startingBullets = 1, int totalChambers = 6)
    {
        bullets = startingBullets;
        currentChambers = totalChambers;
    }

    public int GetRemainingChambers()
    {
        return currentChambers;
    }

    public bool PullTrigger()
    {
        int roll = Random.Range(1, currentChambers + 1);

        if (roll <= bullets)
        {
            return true;
        }

        DecreaseChambers();

        return false;
    }

    private void DecreaseChambers()
    {
        if (currentChambers > bullets)
        {
            currentChambers--;
        }
    }
}
