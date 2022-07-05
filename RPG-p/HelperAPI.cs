using System.Net;
using System.Text.Json;

public static class HelperAPI
{
    /* Se recupera una lista de Emojis de acuerdo al grupo o categoría solicitada */
    
    public static List<Emoji>? CargarEmoji(string grupo)
    {
        var url = $"https://emojihub.herokuapp.com/api/all/group_{grupo}";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        var nuevo = new List<Emoji>();
        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            nuevo = JsonSerializer.Deserialize<List<Emoji>>(responseBody);
                        }
                    }
                }
            }
        }
        catch (WebException) {
        }
        return nuevo;
    } 
}

/* Categorías --> Grupos 

smileys_and_people	

    body
    cat_face
    clothing
    creature_face
    emotion
    face_negative
    face_neutral
    face_positive
    face_positive
    face_role
    face_sick
    family
    monkey_face
    person
    person_activity
    person_gesture
    person_role
    skin_tone

animals_and_nature	

    animal_amphibian
    animal_bird
    animal_bug
    animal_mammal
    animal_marine
    animal_reptile
    plant_flower
    plant_other

food_and_drink	

    dishware
    drink
    food_asian
    food_fruit
    food_prepared
    food_sweat
    food_vegetable

travel_and_places	

activities	

objects

symbols

flags

*/
