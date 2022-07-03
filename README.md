# EL MUNDO PINTADO DE ARIAMIS
## Sobre este juego
Inspirado en el RPG **Dark Souls**, El Mundo Pintado de Ariamis, hace referencia a un área escondida en el RPG original que, con el tiempo, resultó ser uno de los lugares preferidos para las luchas multijugador.

## Mecánicas de juego
Inicialmente, se generan N personajes aleatorios pero con **Nombre Único**.

Si bien los datos y características generados son aleatorios, la combinación *Nombre-Apodo-Tipo* de cada personaje no lo es. Se guarda cierta congruencia con los peronajes del juego original.

Existen dos modos de juego: **Torneo** y **Escaramuza**.

**-> Torneo:** Los personajes se enfrentan por pares en batallas individuales, el perdedor queda eliminado y el ganador pasa a la siguiente ronda. El proceso se repite hasta que sólo queda un vencedor.

El vencedor se guarda en la base de datos y puede ser elegido para posteriores torneos.
Durante el torneo, la salud y/o características de los personajes pueden ser modificadas.

El torneo admite cualquier número de jugadores entre 2 y 8. Pero se recomienda 2, 4 u 8 para que las condiciones sean equitativas. Por ejemplo, si se eligen 3 personajes, el ganador de la batalla entre 1 y 2, posiblemente tendrá menos salud y jugará contra el jugador 3 (con salud completa). 

**-> Escaramuza:** es un combate individual "amistoso" entre dos personajes. No se elimina el perdedor ni tampoco se modifican salud o características al finalizar el combate.

El sistema de combate (en torneo o escaramuza) está basado en turnos y por un cálculo matemático/probabilístico utilizando las habilidades de cada personaje.
El combate se divide en 3 Rounds, en los cuales cada personaje ataca una vez. Al finalizar las rondas, el que mejor salud tenga será declarado ganador.
Un personaje puede ser eliminado en cualquier momento si su salud llega a cero.

## Pantallas/Secciones

+   Menú Principal

    Muestra un mensaje de bienvenida y la lista de personajes generados. **Esta lista sólo se modifica una vez terminado el torneo.**

+   Menú Principal --> (V)encedores

    Mustra una tabla con el historial de personajes que han ganado el torneo, y sus características.

+   Menú Principal --> (E)legidos

    Muestra uno por uno, los datos y características de cada personaje en juego.

    **--> (G)enerar:** Permite modificar las características del peronaje visualizado de forma aleatoria.

    **--> (R)eemplazar:** Permite intercambiar el personaje visualizado por un personaje guardado en la base de datos, **sólo si los perosnajes a intercambiar poseen el mismo nombre o si el personaje a cargar (nombre) no se encuentra en otra posición de la lista de personajes en juego**

+   Menú Principal --> (T)orneo --> Mejoras

    Luego de cada batalla, se permite incrementar una característica del personaje ganador. **Por defecto se incrementa la Salud**

## Upgrades

### TP10 - Servicios Web
Fuente: https://github.com/cheatsnake/emojihub

Implementación de Emojis aleatorios (Dark Souls ahora es menos darks 😜).

Se consume una API de emojis. Por cada pantalla se recupera una o más listas de emojis que corresponden a una categoría (simbolos, lugares, acciones, expresiones, etc). Según el mensaje que se muestre por pantalla, se elige de forma aleatoria un emoji dentro de una categoría.

Al hacer esto, se evita la monotonía durante el proceso de batallas por turnos, y se incorpora ambientación (con emojis de lugares).

### TP9 - Archivos Json
Por cada partida se actualiza una lista de objetos tipo Personaje en el archivo "ganadores.json". Este archivo se puede leer desde el menú ListarGanadores. **Los personajes con el mismo nombre son reemplazados.**

### TP8 - Archivos Csv
Se agrega un archivo "ganadores.csv" en el cual se irán guardando los ganadores de cada torneo. Los datos de cada personaje son: nombre, fecha del combate y características.

Las líneas del archivo "nombres.csv" son combinaciones de nombres, apodos y tipos de personajes. Se usa el archivo para generar los datos de cada personaje.












    


