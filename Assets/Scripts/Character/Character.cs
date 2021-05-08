public class Character
{
    private int _speed;

    public int Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private int _money;

    public int Money
    {
        get { return _money; }
        set { _money = value; }
    }

    private int _health;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private int _ability;

    public int Ability
    {
        get { return _ability; }
        set { _ability = value; }
    }

    private int _killCount;

    public int KillCount
    {
        get { return _killCount; }
        set { _killCount = value; }
    }

    private Weapon _weapon;

    public Weapon Weapon
    {
        get { return _weapon; }
        set { _weapon = value; }
    }

    private PowerUps _powerUps;

    public PowerUps PowerUps
    {
        get { return _powerUps; }
        set { _powerUps = value; }
    }

    public Character()
    {
        this.Money = 10;
        this.Health = 100;
        this.Speed = 4;
        this.Ability = 0;
        this.KillCount = 0;
    }
}