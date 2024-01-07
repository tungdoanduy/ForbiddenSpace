public enum PlayerType
{
    WATER_SUPPLIER=10,
    ARCHAEOLOGIST=20,
    NAVIGATOR=30,
    FORTUNE_TELLER=40,
    METEOROLOGIST=50,
    ENGINEER=60,
}

public enum TileType
{
    NONE,
    TUNNEL = 10,
    OASIS= 20,
    GEAR=30,
    //DUNE_BLASTER = 31,
    //JET_PACK = 32,
    //BOTTLE_OF_WATER = 33,
    //SUNSCREEN = 34,
    //TERRASCOPE = 35,
    //CAPSULE = 36,
    PIECE_TRACKER_HORIZONTAL =40,
    PIECE_TRACKER_VERTICAL = 41,
    PORTAL =50,
    PIECE=60,
}

public enum BlockType
{
    SAND = 0,
    BLOCKED_SAND = 1,
    EXCAVATE = 2,
    TORNADO = 10,
}

public enum GearType
{
    NONE,
    DUNE_BLASTER = 10,
    TELEPORTER = 20,
    BOTTLE_OF_WATER = 30,
    SOLAR_SHIELD = 40,
    TERRASCOPE = 50,
    CAPSULE = 60,
}

public enum CardType
{
    SUNBURN=10,
    STORM_REACHING=20,
    TORNADO_MOVING=30,
}

public enum Direction//for tornado
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
}

public enum BookType
{
    EARTH, 
    WATER,
    FIRE,
    WIND,
}