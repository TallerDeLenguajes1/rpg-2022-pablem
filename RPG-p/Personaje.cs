public class Personaje
{
    /* DATOS */
    Tipos tipo;
    string nombre;
    string apodo;
    DateTime fechaNacimiento;
    int salud; //100%

    /* CARACTERÍSTICAS */
    int velocidad; // 1 a 10
    int destreza; // 1 a 5
    int fuerza; //1 a 10
    int nivel; //1 a 10
    int armadura; //1 a 10

    public Tipos Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }

    public void generarDatos() //No aleatorio?
    {
        var rnd = new Random();
        Tipo = (Tipos)rnd.Next(0,8);
        // Edad = rnd.Next(0,109572); // Edad max = 300*365.2425 = 109572
        FechaNacimiento = DateTime.Today.AddDays(rnd.Next(-109572,0));
        Salud = 100;
    }
    public void generarCaracteristicas() 
    {
        var rnd = new Random();
        Velocidad = rnd.Next(1,10); //distribucón??
        Destreza = rnd.Next(1,10); ////le subí destreza max
        Fuerza = rnd.Next(1,10);
        Nivel = rnd.Next(1,5); ////le bajé el nivel max
        Armadura = rnd.Next(1,10);
    }
    public int calcularEdad()
    {
        return Convert.ToInt32((DateTime.Today - FechaNacimiento).Days/365.2425);
    }
    public int poderDisparo()
    {
        return Destreza*Fuerza*Nivel;
    }
    public int poderDefensa()
    {
        return Armadura*Velocidad;
    }
    public void mostrarDatos()
    {
        Console.WriteLine("\nDatos de "+Nombre+" (aka "+Apodo+")");
        Console.WriteLine("\tClase: "+Tipo);
        Console.WriteLine("\tFecha de Nacimiento: "+FechaNacimiento.ToString("dd/MM/yyyy"));
        Console.WriteLine("\tEdad: "+calcularEdad()+" años");
        Console.WriteLine("\tSalud: "+Salud+"%");
    }
    public void mostrarCaracteristicas()
    {
        Console.WriteLine("\nCaracterísticas de "+Nombre+" (aka "+Apodo+")");
        Console.WriteLine("\tNivel "+Nivel);
        Console.WriteLine("\tVelocidad: "+Velocidad);
        Console.WriteLine("\tDestreza: "+Destreza);
        Console.WriteLine("\tFuerza: "+Fuerza);
        Console.WriteLine("\tArmadura: "+Armadura);
    }
}