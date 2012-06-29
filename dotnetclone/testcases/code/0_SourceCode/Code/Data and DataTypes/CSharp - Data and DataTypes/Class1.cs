using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp___Data_and_DataTypes
{
    public struct ListData : ICloneable
    {
        public int Number;
        public string Name;

        public ListData(int number, string name)
        {
            Number = number;
            Name = name;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new ListData(Number, Name);
        }

        #endregion
    }

    public static class DataExamples
    {
public static List<ListData> IncrementAllInList(List<ListData> list)
{
    list.ForEach((data => data.Number += 1));
    return list;
}
    }
}
