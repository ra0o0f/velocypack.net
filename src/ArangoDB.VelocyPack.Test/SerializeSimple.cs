using ArangoDB.VelocyPack.Test.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDB.VelocyPack.Test
{
    public class SerializeSimple
    {
        [Fact]
        public void SerializeNull()
        {
            var s = VPack.Serialize(null);
            var d = VPack.Deserialize<object>(s);

            Assert.Null(d);
        }
        
        [Fact]
        public void SerializeBool()
        {
            var s = VPack.Serialize(true);
            var d = VPack.Deserialize<bool>(s);

            Assert.True(d);
        }

        [Fact]
        public void SerializeBoolNul()
        {
            var s = VPack.Serialize((bool?)null);
            var d = VPack.Deserialize<bool?>(s);

            Assert.Null(d);
        }

        [Fact]
        public void SerializeDouble()
        {
            var s = VPack.Serialize(double.MaxValue);
            var d = VPack.Deserialize<double>(s);

            Assert.Equal(double.MaxValue, d);
        }

        [Fact]
        public void SerializeSingle()
        {
            var s = VPack.Serialize(float.MaxValue);
            var d = VPack.Deserialize<float>(s);

            Assert.Equal(float.MaxValue, d);
        }

        [Fact]
        public void SerializeSbyte()
        {
            var s = VPack.Serialize(sbyte.MaxValue);
            var d = VPack.Deserialize<sbyte>(s);

            Assert.Equal(sbyte.MaxValue, d);
        }

        [Fact]
        public void SerializeByte()
        {
            var s = VPack.Serialize(byte.MaxValue);
            var d = VPack.Deserialize<byte>(s);

            Assert.Equal(byte.MaxValue, d);
        }

        [Fact]
        public void SerializeShort()
        {
            var s = VPack.Serialize(short.MaxValue);
            var d = VPack.Deserialize<short>(s);

            Assert.Equal(short.MaxValue, d);
        }

        [Fact]
        public void SerializeUShort()
        {
            var s = VPack.Serialize(ushort.MaxValue);
            var d = VPack.Deserialize<ushort>(s);

            Assert.Equal(ushort.MaxValue, d);
        }

        [Fact]
        public void SerializeInt()
        {
            var s = VPack.Serialize(int.MaxValue);
            var d = VPack.Deserialize<int>(s);

            Assert.Equal(int.MaxValue, d);
        }

        [Fact]
        public void SerializeUInt()
        {
            var s = VPack.Serialize(uint.MaxValue);
            var d = VPack.Deserialize<uint>(s);

            Assert.Equal(uint.MaxValue, d);
        }

        [Fact]
        public void SerializeLong()
        {
            var s = VPack.Serialize((long.MaxValue));
            var d = VPack.Deserialize<long>(s);

            Assert.Equal(long.MaxValue, d);
        }

        [Fact]
        public void SerializeULong()
        {
            var s = VPack.Serialize(ulong.MaxValue);
            var d = VPack.Deserialize<ulong>(s);

            Assert.Equal(ulong.MaxValue, d);
        }

        [Fact]
        public void SerializeString()
        {
            var s = VPack.Serialize("Hello ,?");
            var d = VPack.Deserialize<string>(s);

            Assert.Equal("Hello ,?", d);
        }

        [Fact]
        public void SerializeChar()
        {
            var s = VPack.Serialize('a');
            var d = VPack.Deserialize<char>(s);

            Assert.Equal('a', d);
        }

        [Fact]
        public void SerializeByteArray()
        {
            var bytes = new byte[] { 1, 3, 2 };

            var s = VPack.Serialize(bytes);
            var d = VPack.Deserialize<byte[]>(s);

            for (int i = 0; i < bytes.Length; i++)
                Assert.Equal(bytes[i], d[i]);
        }

        [Fact]
        public void SerializeDateTime()
        {
            var date = new DateTime(2000, 8, 7, 6, 5, 4);
            var s = VPack.Serialize(date);
            var d = VPack.Deserialize<DateTime>(s);

            Assert.Equal(date, d);
        }

        [Fact]
        public void SerializeDateTimeOffset()
        {
            var date = new DateTimeOffset(2000, 8, 7, 6, 5, 4, TimeSpan.Zero);
            var s = VPack.Serialize(date);
            var d = VPack.Deserialize<DateTimeOffset>(s);

            Assert.Equal(date, d);
        }

        [Fact]
        public void SerializeTimeSpan()
        {
            var timeSpan = TimeSpan.FromMinutes(90);
            var s = VPack.Serialize(timeSpan);
            var d = VPack.Deserialize<TimeSpan>(s);

            Assert.Equal(timeSpan.Ticks, d.Ticks);
        }

        [Fact]
        public void SerializeArrayInt()
        {
            int[] array = new int[] { 1, 2, 3 };
            var s = VPack.Serialize(array);
            var d = VPack.Deserialize<int[]>(s);

            for (int i = 0; i < array.Length; i++)
                Assert.Equal(array[i], d[i]);
        }

        [Fact]
        public void SerializeListDouble()
        {
            List<double> list = new List<double> { 1, 2, 3 };
            var s = VPack.Serialize(list);
            var d = VPack.Deserialize<List<double>>(s);

            for (int i = 0; i < list.Count; i++)
                Assert.Equal(list[i], d[i]);
        }

        [Fact]
        public void SerializeHashSetNotSameType()
        {
            HashSet<object> hashSet = new HashSet<object> { double.MaxValue, int.MaxValue, long.MaxValue };
            var s = VPack.Serialize(hashSet);
            var d = VPack.Deserialize<HashSet<object>>(s);

            for (int i = 0; i < hashSet.Count; i++)
                Assert.Equal(hashSet.ElementAt(i), d.ElementAt(i));
        }

        [Fact]
        public void SerializeDictionary()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>
            {
                ["a"] = 1,
                ["b"] = 2,
                ["c"] = 3
            };

            var s = VPack.Serialize(dic);
            var d = VPack.Deserialize<Dictionary<string, int>>(s);

            for (int i = 0; i < dic.Count; i++)
            {
                Assert.Equal(dic.Keys.ElementAt(i), d.Keys.ElementAt(i));
                Assert.Equal(dic[dic.Keys.ElementAt(i)], d[d.Keys.ElementAt(i)]);
            }
        }

        [Fact]
        public void SerializeDictionaryNotSameType()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>
            {
                ["a"] = 1,
                ["b"] = "some string",
                ["c"] = new DateTimeOffset(2000, 8, 7, 6, 5, 4, 3,TimeSpan.FromHours(4))
            };

            var s = VPack.Serialize(dic);
            var d = VPack.Deserialize<Dictionary<string, object>>(s);

            for (int i = 0; i < dic.Count; i++)
            {
                Assert.Equal(dic.Keys.ElementAt(i), d.Keys.ElementAt(i));
                Assert.Equal(dic[dic.Keys.ElementAt(i)], d[d.Keys.ElementAt(i)]);
            }
        }

        [Fact]
        public void SerializeAnonymous()
        {
            var an = new
            {
                a = 1,
                b = "some string",
                c = new DateTimeOffset(2000, 8, 7, 6, 5, 4, 3, TimeSpan.FromHours(4))
            };

            var s = VPack.Serialize(an);
            dynamic d = VPack.Deserialize<ExpandoObject>(s);

            Assert.Equal(an.a, d.a);
            Assert.Equal(an.b, d.b);
            Assert.Equal(an.c, d.c);
        }

        [Fact]
        public void SerializeDynamic()
        {
            dynamic an = new ExpandoObject();
            an.a = 1;
            an.b = "some string";
            an.c = new DateTimeOffset(2000, 8, 7, 6, 5, 4, 3, TimeSpan.FromHours(4));
            
            var s = VPack.Serialize(an);
            dynamic d = VPack.Deserialize<ExpandoObject>(s);

            Assert.Equal(an.a, d.a);
            Assert.Equal(an.b, d.b);
            Assert.Equal(an.c, d.c);
        }

        [Fact]
        public void SerializeComplexObject()
        {
            var c = ComplexObject.Create();

            Assert.True(ComplexObject.IsSame(c));

            var s = VPack.Serialize(c);
            var d = VPack.Deserialize<ComplexObject>(s);

            Assert.True(ComplexObject.IsSame(d));
        }
    }
}
