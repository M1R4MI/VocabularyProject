using Functional.Entities;

namespace Functional.Data;

public class DataContext
{
    private readonly DataSet _dataSet = new DataSet();
    
    public ICollection<Diction> Dictions => _dataSet.Dictions;
}