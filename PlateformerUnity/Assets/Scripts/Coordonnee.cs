using System;

public class Coordonnee  {
    private int x;

    private int y;

    public Coordonnee(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public double getDistance(Coordonnee distant) {
        int x1 = this.getX();
        int x2 = distant.getX();
        int y1 = this.getY();
        int y2 = distant.getY();

        return Math.Sqrt( Math.Pow( x1 - x2, 2) + Math.Pow(y1-y2 , 2) );
    }

    public int getX()
    {
        return this.x;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return this.y;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public String toString() {

        return "Coordonnee [ (X = " + this.getX() + "), (Y = " + this.getY() + ")]";
    }

    public bool Equals(Coordonnee toCompar) {

        return this.getX() == toCompar.getX() && this.getY() == toCompar.getY();

    }
}