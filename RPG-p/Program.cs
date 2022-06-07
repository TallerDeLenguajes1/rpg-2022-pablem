
var personajesENJuego = new List<Personaje>();
Personaje nuevo; 
for (int i = 0; i < 4; i++) {
    nuevo = new Personaje();
    nuevo.Nombre = "Persona-"+i;
    nuevo.Apodo = "Apodo-"+i;
    nuevo.generarDatos();
    nuevo.generarCaracteristicas();
    personajesENJuego.Add(nuevo);
}
foreach (var persona in personajesENJuego)
{
    persona.mostrarDatos();
    persona.mostrarCaracteristicas();
}
