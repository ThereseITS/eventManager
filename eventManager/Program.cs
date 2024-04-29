using System.Reflection.Metadata.Ecma335;

namespace eventManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../../testData2.csv";
            List<Event> events = ReadEvents(path);

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            foreach (Event e in events)
            Console.WriteLine(e.ToString());
        }


/// <summary>
/// This method will read in and create a list of events from a file.
/// Note that a fresh list is created each time. 
/// </summary>
/// <param name="path">T</param>
/// <returns></returns>
  
        static List<Event> ReadEvents(string path)
        {
            List<Event> events = new List<Event>();
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string input;
                    string name;
                    string eventType;
                    decimal price;
                    int capacity=0;
                    int seatsSold=0;

                    while ((input = sr.ReadLine()) != null)
                    {
                        string[] fields = input.Split(',');

                        // validate the line before creating the object.

                        if (   ((fields.Length == 5 && int.TryParse(fields[4], out seatsSold))
                            ||  (fields.Length==4) )
                            && CheckValidType(fields[1])
                            && decimal.TryParse(fields[2], out price)
                            && int.TryParse(fields[3], out capacity))
                            
                        {
                            name = fields[0];
                            eventType = fields[1];
                            Event ev;
                            if ((fields.Length == 4))
                            { 
                                ev = new Event(name, eventType, price, capacity);
                            }
                            else 
                            {
                                ev = new Event(name, eventType, price, capacity,seatsSold);
                            }
                            
                            events.Add(ev);
                        }
                        else  // Invalid data is reported on but not included in the list. 
                        {
                            Console.WriteLine("Error in input: ");

                            for(int i=0;i<fields.Length; i++)
                            {
                                Console.WriteLine($"{i}    {fields[i]}");
                            }
                        }
                    }
                }
            }

            catch (FileNotFoundException ex)                                            // Throw exceptions in exceptional situations- see Microsoft guidelines.
            {
                Console.WriteLine($"File: {path} not found: {ex.Message}");
            }
         
            return events;

        }
/// <summary>
///
/// This method checks the validity of the event type .
/// 
///  **Note that we could alternatively use an enumerated type here & try to parse it.
///   enum EventType { drama, comedy, sporting, concert}
///   enum.TryParse(EventType, fields[0], out eventType)
/// 
/// 
/// </summary>
/// <param name="eventType"></param>
/// <returns>true if valid, false otherwise </returns>
    public static bool CheckValidType(string eventType)
        {

            if  ((eventType.ToLower() == "drama")||(eventType.ToLower() == "comedy")|| (eventType.ToLower() == "concert")||(eventType.ToLower() == "sporting"))
            {
                return true;
            }
            else
            {
               
                return false;
            }
        }

    }
}
