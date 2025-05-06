using System.Dynamic;

namespace Kontroll.Library.Exel;

public class Exel
{
    public static Task<List<ExpandoObject>> ExelToObjects(string file)
    {
        var records = new List<ExpandoObject>();

        using (var reader = new StreamReader(file))
        {
            var headers = reader.ReadLine().Split(';');

            while (!reader.EndOfStream)
            {
                var fields = reader.ReadLine().Split(';');
                dynamic record = new ExpandoObject();        

                
                for (int i = 0; i < headers.Length; i++)
                {
                    string header = headers[i].Replace(" ", "");
                    string field = fields[i].Trim();
                    ((IDictionary<string, object>)record).Add(header, field);
                }
                records.Add(record);
            }
        } 
        return Task.FromResult(records);
    }
    
}