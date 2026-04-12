using UnityEngine;

public class Revolver
{
    private int bulletsInChamber;
    private int maxChambers;

    public Revolver(int maxChambers = 6, int startingBullets = 1)
    {
        this.maxChambers = maxChambers;
        this.bulletsInChamber = startingBullets;
    }

    public bool PullTrigger()
    {
        int roll = Random.Range(1, maxChambers + 1);

        if (roll <= bulletsInChamber)
        {
            return true;
        }

        IncreaseDanger();

        return false;
    }

    private void IncreaseDanger()
    {
        if (bulletsInChamber < maxChambers)
        {
            bulletsInChamber++;
        }
    }
}
