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

    public void GenerarDatos(ref List<string[]> nombreApodoTipo) 
    {
        var rnd = new Random();
        FechaNacimiento = DateTime.Today.AddDays(rnd.Next(-109572,0));
        Salud = 100;
        //Generación aleatoria de una combinación Nombre + Apodo + Clase
        if (nombreApodoTipo != null && nombreApodoTipo.Any()) {
            int i = rnd.Next(0,nombreApodoTipo.Count-1);
            string[] itemLista = nombreApodoTipo.ElementAt(i);
            Nombre = itemLista[0];
            Apodo  = itemLista[1];
            Tipo   = (Tipos)Convert.ToInt16(itemLista[2]);
            nombreApodoTipo.RemoveAt(i); //Quita el nombre de la lista para que no se repita
        } else {
            Nombre = "Anónimo";
            Apodo  = "Anónimo";
            Tipo   = Tipos.Marginado;
        }
    }
    public void GenerarCaracteristicas() 
    {
        var rnd = new Random();
        Velocidad = rnd.Next(1,10); //distribucón??
        Destreza = rnd.Next(1,10); ////le subí destreza max
        Fuerza = rnd.Next(1,10);
        Nivel = rnd.Next(1,4); ////le bajé el nivel max
        Armadura = rnd.Next(1,10);
    }
    public void Levelear()
    {
        string opcion;
        Console.WriteLine("\n¿Qué mejoras elijes para {0}?",Apodo);
        Console.WriteLine("(S)alud+10   (F)uerza+1   (V)elocidad+1");
        opcion = Console.ReadLine().ToLower();
        if(opcion == "f") {
            Fuerza++;
        } else {
            if(opcion == "v") {
                Velocidad++;
            } else {
                Salud += 10;
                if(Salud > 100)
                    Salud = 100;
            }
        }
    }
    public int calcularEdad()
    {
        return Convert.ToInt32((DateTime.Today - FechaNacimiento).Days/365.2425);
    }
    public int PoderDisparo()
    {
        return Destreza*Fuerza*Nivel;
    }
    public int PoderDefensa()
    {
        return Armadura*Velocidad;
    }

    public void MostrarDatos()
    {
        Console.WriteLine("\n"+Nombre);
        Console.WriteLine("\tClase: "+Tipo);
        Console.WriteLine("\tFecha de Nacimiento: "+FechaNacimiento.ToString("dd/MM/yyyy"));
        Console.WriteLine("\tEdad: "+calcularEdad()+" años");
        Console.WriteLine("\tSalud: "+Salud+"%");
    }
    public void MostrarCaracteristicas()
    {
        Console.WriteLine("\nCaracterísticas de "+Apodo);
        Console.WriteLine("\tNivel "+Nivel);
        Console.WriteLine("\tVelocidad: "+Velocidad);
        Console.WriteLine("\tDestreza: "+Destreza);
        Console.WriteLine("\tFuerza: "+Fuerza);
        Console.WriteLine("\tArmadura: "+Armadura);
    }

    public override bool Equals(object obj) => this.Equals(obj as Personaje);

    public bool Equals(Personaje p) {
        if (p is null) 
        {
            return false;
        }
        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, p))
        {
            return true;
        }
        // If run-time types are not exactly the same, return false.
        if (this.GetType() != p.GetType())
        {
            return false;
        }
        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (Nombre == p.Nombre);
    }
    
    public override int GetHashCode() => (Nombre).GetHashCode();
    
}