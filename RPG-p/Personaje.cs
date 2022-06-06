public class Personaje
{
    // Tipos tipo;
    // string nombre;
    // string apodo;
    // DateTime fechaNacimiento;
    // //int Edad; //entre 0 y 300
    // int Salud; //100%
    // int velocidad; // 1 a 10
    // int destreza; // 1 a 5
    // int fuerza; //1 a 10
    // int nivel; //1 a 10
    // int armadura; //1 a 10

    Datos datos = new Datos();
    Caracteristicas caracteristicas = new Caracteristicas();

    public Datos Datos { get => datos; set => datos = value; }
    public Caracteristicas Caracteristicas { get => caracteristicas; set => caracteristicas = value; }

    public void cargarDatos()
    {
        
    }
}