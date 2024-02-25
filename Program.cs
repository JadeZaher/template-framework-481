using System;
using System.ComponentModel;
using System.Reflection;

public class Program
{

    public static void Main()
    {
        var q = new BOEQuery();

        string val = q
            .Where(BOEQuery.Q.SIVALUE, BOEQuery.Operation.GreaterThan, "BUH")
            .AndWhere(BOEQuery.Q.SIVALUE1, BOEQuery.Operation.LessThan, "UHH")
            .OrderBy(BOEQuery.Q.SIVALUE2, BOEQuery.Order.Ascending)
            .Query;

        Console.WriteLine(val);
        Console.ReadLine();
    }
}

public class BOEQuery
{
    public enum Q
    {
        [Description(" SI_VALUE ")]
        SIVALUE,
        [Description(" SI_VALUE3 ")]
        SIVALUE3,
        [Description(" SI_VALUE2 ")]
        SIVALUE2,
        [Description(" SI_VALUE1 ")]
        SIVALUE1
    }
    public enum Operation
    {
        [Description(" > ")]
        GreaterThan,
        [Description(" < ")]
        LessThan,
        [Description(" => ")]
        GreaterThanOrEqualTo,
        [Description(" <= ")]
        LessThanOrEqualTo,
        [Description(" = ")]
        Equals
    }

    public enum Order
    {
        [Description(" DESC ")]
        Descending,
        [Description(" ASC ")]
        Ascending,
    }

    public string Query { get; set; }

    public BOEQuery()
    {
        Query = "BASE_QUERY";
    }

    public BOEQuery Where(Q typeToAdd, Operation operation, string value)
    {
        Query = Query + " WHERE " + A.GetDescription(typeToAdd) + A.GetDescription(operation) + "'" + value + "'";
        return this;
    }

    public BOEQuery AndWhere(Q typeToAdd, Operation operation, string value)
    {
        Query = Query + " AND WHERE " + A.GetDescription(typeToAdd) + A.GetDescription(operation) + "'" + value + "'";
        return this;
    }
    public BOEQuery OrderBy(Q typeToAdd, Order operation)
    {
        Query = Query + " AND ORDER BY " + A.GetDescription(typeToAdd) + A.GetDescription(operation);
        return this;
    }

    public BOEQuery Reset()
    {
        Query = "BASE_QUERY";
        return this;
    }

    public BOEQuery Clear()
    {
        Query = "";
        return this;
    }

}

public static class A
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attr =
                       Attribute.GetCustomAttribute(field,
                         typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
        }
        return null;
    }
}