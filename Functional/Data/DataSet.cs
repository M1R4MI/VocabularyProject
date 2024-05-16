using Functional.Entities;

namespace Functional.Data;

[Serializable]
public class DataSet
{
    public List<Diction> Dictions { get; set; }

    public DataSet()
    {
        Dictions = new List<Diction>();
    }
}