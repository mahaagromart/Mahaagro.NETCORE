using System.Text.Json.Serialization;
using ECOMAPP.CommonRepository;

public class DBReturnData
{
    public dynamic Dataset { get; set; }

    public DBEnums.Status Status { get; set; }

    public string Message { get; set; }

    [JsonIgnore]
    public dynamic DatasetWithoutCycles { get; set; } // Avoid this if needed for serialization
}
