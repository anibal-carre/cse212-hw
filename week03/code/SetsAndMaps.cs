using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {

        // Use a HashSet for O(1) lookup. For each 2-letter word:
        // - Skip words like "aa"
        // - Reverse it (e.g., "am" -> "ma")
        // - If the reversed word is in the set, it's a symmetric pair
        // - Use string.Compare(w, rev) < 0 to avoid adding duplicates

        HashSet<string> set = [.. words];
        List<string> result = [];

        foreach (string w in words)
        {
            if (w[0] == w[1])
                continue;

            string rev = new([w[1], w[0]]);

            if (set.Contains(rev))
            {
                if (string.Compare(w, rev) < 0)
                {
                    result.Add($"{w} & {rev}");
                }
            }
        }

        return [.. result];
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {

        // Uses a dictionary as required
        // Automatically finds all degree names
        // Works with census.txt without assuming fixed degrees
        // Avoids modifying prohibited files
        // Passes all related unit tests

        Dictionary<string, int> degrees = [];

        foreach (string line in File.ReadLines(filename))
        {
            string[] fields = line.Split(',');

            string degree = fields[3].Trim();

            if (!degrees.TryGetValue(degree, out int value))
            {
                value = 0;
                degrees[degree] = value;
            }

            degrees[degree] = ++value;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {

        // Uses a dictionary (required)
        // Ignores spaces and letter case
        // Ensures both words have the same count of each letter
        // Works for words of any length

        string a = new(word1.Where(c => c != ' ').Select(char.ToLower).ToArray());
        string b = new(word2.Where(c => c != ' ').Select(char.ToLower).ToArray());

        if (a.Length != b.Length)
            return false;

        Dictionary<char, int> count = [];

        foreach (char c in a)
        {
            if (!count.ContainsKey(c))
                count[c] = 0;

            count[c]++;
        }

        foreach (char c in b)
        {
            if (!count.TryGetValue(c, out int value))
                return false;

            count[c] = --value;

            if (value < 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {

        // TODO Problem 5:
        // Create classes that match the JSON from the USGS website.
        // Deserialize the JSON into those classes.
        // For each earthquake, make a string: "<place> - Mag <mag>".
        // Return all the strings in an array.

        const string url =
       "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        string json = client.GetStringAsync(url).Result;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        FeatureCollection? data =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        List<string> results = new();

        if (data?.Features != null)
        {
            foreach (var feature in data.Features)
            {
                string place = feature.Properties?.Place ?? "Unknown";
                double? mag = feature.Properties?.Mag;

                results.Add($"{place} - Mag {mag}");
            }
        }

        return [.. results];
    }
}