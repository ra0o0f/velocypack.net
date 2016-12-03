using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArangoDB.VelocyPack.Test.Models
{
    public class ComplexObject
    {
        public string String { get; set; }

        public string NullableString { get; set; }

        public sbyte SByte { get; set; }

        public byte Byte { get; set; }

        public short Short { get; set; }

        public ushort UShort { get; set; }

        public int Int { get; set; }

        public int? NullableInt { get; set; }

        public long Long { get; set; }

        public ulong ULong { get; set; }

        public double Double { get; set; }

        public float Single { get; set; }

        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }

        public TimeSpan TimeSpan { get; set; }

        public dynamic Dynamic { get; set; }

        public ComplexObject Nested { get; set; }

        public List<int> ListInt { get; set; }

        public Dictionary<int, int> DictionaryIntInt { get; set; }
        
        public ComplexObject()
        {
            Dynamic = new ExpandoObject();
        }

        public static ComplexObject Create()
        {
            dynamic d = new ExpandoObject();
            d.a = 1;
            d.b = "some string";

            return new ComplexObject
            {
                Byte = 0x1,
                DateTime = new DateTime(2000,9,8,7,6,5,4),
                DateTimeOffset = new DateTimeOffset(2000,9,8,7,6,5,TimeSpan.Zero),
                TimeSpan = TimeSpan.FromMinutes(90),
                DictionaryIntInt = new Dictionary<int, int>
                {
                    [1]=1,
                    [2]=2,
                    [3]=3
                },
                Double = 3d,
                Dynamic = d,
                Int = -5,
                ListInt = new List<int> { 1,2,3},
                Long = 8L,
                Nested = new ComplexObject
                {
                    Int = 55,
                    Nested = new ComplexObject
                    {
                        String = "some string"
                    }
                },
                NullableInt = null,
                NullableString = null,
                String = "some string 1",
                SByte = 0x4,
                Short = -200,
                Single = 500,
                ULong = 50000,
                UShort = 3000
            };
        }

        public static bool IsSame(ComplexObject c)
        {
            var o = Create();

            if (o.Byte != c.Byte)
                return false;

            if (o.DateTime != c.DateTime)
                return false;

            if (o.DateTimeOffset != c.DateTimeOffset)
                return false;
            
            foreach (var k in o.DictionaryIntInt.Keys)
            {
                if (c.DictionaryIntInt.ContainsKey(k) == false)
                    return false;

                if (o.DictionaryIntInt[k] != c.DictionaryIntInt[k])
                    return false;
            }

            if (o.Double != c.Double)
                return false;

            if (o.Dynamic.a != c.Dynamic.a)
                return false;

            if (o.Dynamic.b != c.Dynamic.b)
                return false;

            if (o.Int != c.Int)
                return false;

            if (o.ListInt.Except(c.ListInt).Count() != 0)
                return false;

            if (o.Long != c.Long)
                return false;

            if (c.Nested.Int == default(int))
                return false;

            if (o.Nested.Int != c.Nested.Int)
                return false;

            if (string.IsNullOrEmpty(c.Nested.Nested.String))
                return false;

            if (o.Nested.Nested.String != c.Nested.Nested.String)
                return false;

            if (c.NullableInt != null)
                return false;

            if (o.NullableInt != c.NullableInt)
                return false;

            if (c.NullableString != null)
                return false;

            if (o.NullableString != c.NullableString)
                return false;

            if (o.SByte != c.SByte)
                return false;

            if (o.Short != c.Short)
                return false;

            if (o.Single != c.Single)
                return false;

            if (o.String != c.String)
                return false;

            if (o.TimeSpan != c.TimeSpan)
                return false;

            if (o.ULong != c.ULong)
                return false;

            if (o.UShort != c.UShort)
                return false;

            return true;
        }
    }
}
