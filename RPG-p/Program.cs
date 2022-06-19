using System.Text.Json; 
using System.Text.Json.Serialization;
internal class Program
{
    public static int N = 4; //Número de personajes para el torneo
    private static void Main(string[] args)
    {
        /* INICIALIZACIÓN - Generación de personajes */
        var personajesENJuego = iniciar();


        /* PANTALLA DE INICIO */

        string opcion;
        do {
            /* CARGADO DE VENCEDORES DESDE ARCHIVO */
            Console.Clear();
            Tipeo("Bienvenido al mundo pintado de Ariamis");
            Tipeo("\nElije tu destino...");
            Console.WriteLine("\n(T)orneo   Escaramuz(A)   (E)legidos   (V)encedores   (S)alir"); ///(L)ore   Vencedores --> (R)evivir
            opcion = Console.ReadLine().ToLower();
            if (opcion == "t") {
                Torneo(personajesENJuego);
                personajesENJuego = iniciar();
            }
            if (opcion == "a")
                Batalla(personajesENJuego[0],personajesENJuego[1]);
            if (opcion == "e")
                mostrarPersonajes(personajesENJuego);
            if (opcion == "v")
                mostrarGanadores(cargarCSV("ganadores.csv"));
        } while (opcion != "s");


        /* INICIALIZACIÓN */

        List<Personaje> iniciar()
        {
            /* CARGA DE NOMBRE, APODO Y TIPO DESDE EL ARCHIVO "nombres.csv" */
            var listaNombres = cargarCSV("nombres.csv");

            /* CREACIÓN DE PERSONAJES RANDOM */
            var rnd = new Random();
            int ind;
            var personajesENJuego = new List<Personaje>();
            var lineaSeparada = new List<string>();
            Personaje nuevo;
            for (int i = 0; i < N; i++) {
                nuevo = new Personaje();
                if (listaNombres.Any()) { //Hacerlo dentro de la clase Personaje
                    ind = rnd.Next(0,listaNombres.Count-1);
                    lineaSeparada = (listaNombres[ind]).Split(",").ToList();
                    listaNombres.RemoveAt(ind);
                }
                nuevo.Nombre = (lineaSeparada.Any()) ? lineaSeparada[0] : "Anónimo"+i;
                nuevo.Apodo = (lineaSeparada.Any()) ? lineaSeparada[1] : "Anónimo"+i;
                nuevo.Tipo = (lineaSeparada.Any()) ? (Tipos)Convert.ToInt16(lineaSeparada[2]) : (Tipos)9;
                nuevo.generarDatos();
                nuevo.generarCaracteristicas();
                personajesENJuego.Add(nuevo);
            }
            return personajesENJuego;
        }


        /* PANTALLAS SECUNDARIAS: Torneo */

        void Torneo(List<Personaje> personajes)
        {
            Personaje p1;
            Personaje p2;
            
            while (personajes.Count > 1) {
                p1 = personajes.First();
                personajes.Remove(p1);
                p2 = personajes.First();
                personajes.Remove(p2);

                /* FUNCION COMBATE SIMPLE */
                p1 = Batalla(p1,p2);
                p1.Levelear();
                p1.mostrarCaracteristicas();
                personajes.Add(p1);
                Console.ReadKey();
            }
            guardarGanadorCSV("ganadores.csv",personajes.First());
            guardarGanadorJson("ganadores.json",personajes.First());
            Console.Clear();
            Tipeo(personajes.First().Nombre+" resultó victorios@ en este torneo mortal\n");
            Tipeo("Su nombre quedará inmortalizado en una saponita blanca.");

            Console.ReadKey();
        }

        /* PANTALLA SECUNDARIA: batalla Singular */

        Personaje Batalla(Personaje p1, Personaje p2)
        {
            int max = 5000;
            int ataque;
            int danio;
            var rnd = new Random();
            Console.Clear();
            for (int i = 1; i < 3; i++) {
                if (p1.Salud > 0 && p2.Salud > 0) {
                    Console.WriteLine("\n"+p1.Nombre + " (" + p1.Salud + ")   vs.   (" + p2.Salud + ") " + p2.Nombre);
                    Tipeo("Round " + i);
                    Console.ReadKey();
                    if(i%2!=0) { 
                        ataque = p1.poderDisparo() * rnd.Next(0, 100);
                        danio = 100 * (ataque - p2.poderDefensa()) / max;
                        // Console.WriteLine(p2.poderDefensa());
                        if (danio < 0)
                            danio = 0;
                        Tipeo(p1.Apodo + " ataca con efectividad +" + ataque);
                        Tipeo(p2.Apodo + " recibió " + danio + " puntos de daño");
                        p2.Salud -= danio;
                    } else {                                            /// otra forma sin repetir código??
                        ataque = p2.poderDisparo() * rnd.Next(0, 100);
                        danio = 100 * (ataque - p1.poderDefensa()) / max;
                        // Console.WriteLine(p2.poderDefensa());
                        if (danio < 0)
                            danio = 0;
                        Tipeo(p2.Apodo + " contraataca consiguiendo +" + ataque + " puntos de ataque");
                        Tipeo(p1.Apodo + " recibió " + danio + " puntos de daño");
                        p1.Salud -= danio;
                    }
                    Console.ReadKey();
                }
            }
            if (p1.Salud<p2.Salud) {// (a implementar: en caso de empate...)
                Tipeo("\n"+p1.Nombre+" quedó eliminad@ de esta contienda");
                p1.Salud = 100;
                Console.ReadKey();
                return p2;
            } else {
                Tipeo("\n"+p2.Nombre+" está fuera de combate");
                p2.Salud = 100;
                Console.ReadKey();
                return p1;
            }
        }

        /* PANTALLA SECUNDARIA: Mostrar los personajes en juego */

        void mostrarPersonajes(List<Personaje> personajes)
        {
            var personajesGanadores = cargarJson("ganadores.json");
            string opcion = "";
            int i = 0;
            Personaje aux;
            do {
                Console.Clear();
                Console.WriteLine((i+1)+"/"+personajes.Count+" Personajes en juego");
                personajes.ElementAt(i).mostrarDatos(); //indexOf?
                personajes.ElementAt(i).mostrarCaracteristicas();
                Console.WriteLine("\n(G)enerar   (C)argar   (S)alir   (enter)-->");
                opcion = Console.ReadLine().ToLower();
                if(opcion == "g" || opcion == "c") {
                    if(opcion == "g")
                        personajes.ElementAt(i).generarCaracteristicas();
                    if(opcion == "c") {
                        if(personajesGanadores!=null && personajesGanadores.Any()) {
                            Personaje cargado = mostrarCargarGanador(personajesGanadores);
                            if (cargado != null) {
                                if(!personajes.Contains(cargado)) {
                                    personajes[i] = cargado;                ///mala práctica? Error?
                                    personajesGanadores.Remove(cargado);
                                } else {
                                    Console.WriteLine("(no se puede cargar, ya existe un personaje con ese nombre)");
                                    Console.ReadKey();
                                }
                            }
                        } else {
                            Console.WriteLine("(no hay personajes guardados)");
                            Console.ReadKey();
                        }
                    }
                } else {
                    i++;
                }
                if(i==personajes.Count)
                    i=0;
            } while (opcion != "s");
        }

        Personaje mostrarCargarGanador(List<Personaje> personajes)
        {
            int i;
            string opcion = "";
            Personaje nulo = new Personaje();
            do {
                i = 1;
                foreach (var persona in personajes) {
                    Console.Clear();
                    Console.WriteLine((i++)+"/"+personajes.Count+" Cargar personaje ancestral");
                    persona.mostrarDatos();
                    persona.mostrarCaracteristicas();
                    Console.WriteLine("\n(E)legir   (C)ancelar   (enter)-->"); //lore
                    opcion = Console.ReadLine().ToLower();
                    if(opcion == "c") {
                        return nulo;
                    }
                    if(opcion == "e") {
                        return persona;
                    }
                }
            } while (opcion != "c");
            return nulo;
        }
        /* PANTALLA SECUNDARIA: Mostrar lista de ganadores */

        void mostrarGanadores(List<string> personajes)
        {
            string nombre,fecha,stats;
            var lineaSeparada = new List<string>();
            Console.Clear();
            Tipeo("\nLista de vencedores encestrales:");
            if (personajes != null && personajes.Any()) {
                Console.WriteLine(String.Format("\n{0,12} {1,35} {2,25}\n", "Torneo", "Nombre","Stats[v/d/f/a]"));
                foreach (var persona in personajes) {
                    if(persona == null)
                        break;
                    lineaSeparada = persona.Split(",").ToList();
                    fecha = lineaSeparada[0];
                    nombre = lineaSeparada[1];
                    stats = lineaSeparada[2];
                    Console.WriteLine(String.Format("{0,12} {1,35} {2,25}",fecha,nombre,stats));
                }
            } else {
                Tipeo("\n(no hubo ningún ganador)");
            }
            Console.ReadKey();
        }

        /* GUARDAR GANADOR EN ARCHIVO */
        void guardarGanadorCSV(string ruta, Personaje p)
        {
            string torneo = DateTime.Today.ToString("dd-MM-yyyy");
            string nombre = p.Nombre;
            string stats = p.Velocidad+"/"+p.Destreza+"/"+p.Fuerza+"/"+p.Armadura;
           
            StreamWriter sw = new StreamWriter(ruta,true); // append = true
            sw.WriteLine(torneo+","+nombre+","+stats);
            sw.Close();
        }

        void guardarGanadorJson(string ruta, Personaje p) {
            string personajesJson;
            var personajesGanadores = cargarJson(ruta);
            if (personajesGanadores != null) {
                if (!personajesGanadores.Contains(p)) /// equals modificado: compara sólo Nombres 
                    personajesGanadores.Remove(p);
            } else {
                personajesGanadores = new List<Personaje>();
            }
            personajesGanadores.Add(p);
            personajesJson = JsonSerializer.Serialize(personajesGanadores);
            guardarJson(ruta,personajesJson);
        }

        /* FUNCIONES AUXILIARES: cargar lista */

        List<string> cargarCSV(string ruta)
        {
            var lista = new List<string>();
            if (File.Exists(ruta)) {
                FileStream fstr = new FileStream(ruta, FileMode.Open);
                StreamReader srd = new StreamReader(fstr);
                var linea = " ";
                while (linea != null) {
                    linea = srd.ReadLine(); // lee una linea completa
                    lista.Add(linea);
                }
                srd.Close();
            }
            else {
                Console.WriteLine("Archivo no encontrado: {0}",ruta);
                Console.ReadKey();
            }
            return lista;
        }

        List<Personaje> cargarJson(string ruta)
        {
            var lista = new List<Personaje>();
            string documento;
            if (File.Exists(ruta)) {
                using (var archivoOpen = new FileStream(ruta, FileMode.Open))
                {
                    using (var strReader = new StreamReader(archivoOpen))
                    {
                        documento = strReader.ReadToEnd();
                        archivoOpen.Close();
                    }
                }
                lista = JsonSerializer.Deserialize<List<Personaje>>(documento);
            }
            else {
                Console.WriteLine("Archivo no encontrado: {0}",ruta);
                Console.ReadKey();
            }
            return lista;
        }
        
        void guardarJson(string ruta, string datos)
        {
            using(var archivo = new FileStream(ruta, FileMode.Create))
            {
                using (var strWriter = new StreamWriter(archivo))
                {
                    strWriter.WriteLine("{0}", datos);
                    strWriter.Close();
                }
            }
        } 
        
        /* FUNCION AUXILIAR: efecto "tipeo" */

        void Tipeo(string texto, int delay = 20)
        {
            foreach (var c in texto)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}