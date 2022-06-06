public class Caracteristicas
{
    int velocidad; // 1 a 10
    int destreza; // 1 a 5
    int fuerza; //1 a 10
    int nivel; //1 a 10
    int armadura; //1 a 10

    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }

    public void generarCaracteristicas() 
    {
        var rnd = new Random();

        Velocidad = rnd.Next(1,10); //distribucón?? gauss?
        Destreza = rnd.Next(1,5);
        Fuerza = rnd.Next(1,10);
        Nivel = 1;
        Armadura = rnd.Next(1,10);
    }
}