using System.Dynamic;

namespace Kontroll.Library.Exel;

public class Exel
{
    public static Task<List<ExpandoObject>> ExelToObjects(IFormFile file)
    {
        var records = new List<ExpandoObject>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            string? firstLine = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(firstLine))
                return Task.FromResult(records); // Tom fil

            // 🔍 Auto-detekter skilletegn
            char delimiter = firstLine.Contains(';') ? ';' : ',';

            var headers = firstLine.Split(delimiter);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var fields = line.Split(delimiter);
                if (fields.Length == 0) continue;

                dynamic record = new ExpandoObject();
                var dict = (IDictionary<string, object>)record;

                for (int i = 0; i < headers.Length && i < fields.Length; i++)
                {
                    // Rens header og verdi
                    string header = headers[i].Trim().Trim('"').Replace(" ", "");
                    string value = fields[i].Trim().Trim('"');

                    // Unngå duplikate keys
                    if (!dict.ContainsKey(header))
                        dict[header] = value;
                }

                records.Add(record);
            }
        }

        return Task.FromResult(records);
    }

    
}