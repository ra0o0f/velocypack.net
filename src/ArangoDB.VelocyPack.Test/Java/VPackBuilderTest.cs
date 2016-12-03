using ArangoDB.VelocyPack;
using ArangoDB.VelocyPack.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDB.VelocyPack.Test.Java
{
    public class BuilderTest
    {
        [Fact]
        public void Empty()
        {
            VPackSlice slice = new VPackBuilder().Slice();
            Assert.True(slice.IsType(SliceType.None));
        }

        [Fact]
        public void AddNull()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Null);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Null));
        }

        [Fact]
        public void AddBooleanTrue()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(true);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Boolean));

            Assert.True(slice.ToBoolean());
        }

        [Fact]
        public void AddBooleanFalse()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(false);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Boolean));

            Assert.False(slice.ToBoolean());
        }

        [Fact]
        public void AddDouble()
        {
            VPackBuilder builder = new VPackBuilder();
            double value = double.MaxValue;
            builder.Add(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Double));

            Assert.Equal(slice.ToDouble(), value);
        }

        [Fact]
        public void AddIntegerAsSmallIntMin()
        {
            VPackBuilder builder = new VPackBuilder();
            int value = -6;
            builder.AddCompactInt(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.SmallInt));

            Assert.Equal(slice.ToInt32(), value);
        }

        [Fact]
        public void AddIntegerAsSmallIntMax()
        {
            VPackBuilder builder = new VPackBuilder();
            int value = 9;
            builder.AddCompactInt(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.SmallInt));

            Assert.Equal(slice.ToInt32(), value);
        }

        [Fact]
        public void AddLongAsSmallIntMin()
        {
            VPackBuilder builder = new VPackBuilder();
            long value = -6;
            builder.AddCompactInt(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.SmallInt));

            Assert.Equal(slice.ToInt64(), value);
        }

        [Fact]
        public void AddLongAsSmallIntMax()
        {
            VPackBuilder builder = new VPackBuilder();
            long value = 9;
            builder.AddCompactInt(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.SmallInt));

            Assert.Equal(slice.ToInt64(), value);
        }

        [Fact]
        public void AddIntegerAsInt()
        {
            VPackBuilder builder = new VPackBuilder();
            int value = int.MaxValue;
            builder.Add(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Int));

            Assert.Equal(slice.ToInt32(), value);
        }

        [Fact]
        public void AddLongAsInt()
        {
            VPackBuilder builder = new VPackBuilder();
            long value = long.MaxValue;
            builder.Add(value);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Int));

            Assert.Equal(slice.ToInt64(), value);
        }

        [Fact]
        public void AddStringShort()
        {
            VPackBuilder builder = new VPackBuilder();
            string s = "Hallo Welt!";
            builder.Add(s);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.String));

            Assert.Equal(slice.ToStringValue(), s);
        }

        [Fact]
        public void AddStringLong()
        {
            VPackBuilder builder = new VPackBuilder();
            string s = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.";
            builder.Add(s);

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.String));

            Assert.Equal(slice.ToStringValue(), s);
        }

        [Fact]
        public void EmptyArray()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, 0);
            Assert.Throws<IndexOutOfRangeException>(() => slice[0]);
        }

        [Fact]
        public void CompactArray()
        {
            long[] expected = { 1, 16 };
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array, true);
            foreach (var l in expected)
            {
                builder.Add(l);
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, 2);
            for (int i = 0; i < expected.Length; i++)
            {
                VPackSlice at = slice[i];

                Assert.True(at.IsNumeric());

                Assert.Equal(at.ToInt64(), expected[i]);
            }
        }

        [Fact]
        public void ArrayItemsSameLength()
        {
            VPackSlice sliceNotSame;
            {
                VPackBuilder builder = new VPackBuilder();
                builder.Add(SliceType.Array);
                builder.Add("aa");
                builder.Add("a");
                builder.Close();
                sliceNotSame = builder.Slice();
            }
            VPackSlice sliceSame;
            {
                VPackBuilder builder = new VPackBuilder();
                builder.Add(SliceType.Array);
                builder.Add("aa");
                builder.Add("aa");
                builder.Close();
                sliceSame = builder.Slice();
            }
            Assert.True(sliceSame.GetByteSize() < sliceNotSame.GetByteSize());
        }

        [Fact]
        public void UnindexedArray()
        {
            long[] expected = { 1, 16 };
            VPackBuilder builder = new VPackBuilder();
            builder.GetOptions().SetBuildUnindexedArrays(true);
            builder.Add(SliceType.Array, false);
            foreach (var l in expected)
            {
                builder.Add(l);
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, 2);
            for (int i = 0; i < expected.Length; i++)
            {
                VPackSlice at = slice[i];

                Assert.True(at.IsInteger());

                Assert.Equal(at.ToInt64(), expected[i]);
            }
        }

        [Fact]
        public void IndexedArray()
        {
            long[] values = { 1, 2, 3 };
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            foreach (var l in values)
            {
                builder.Add(l);
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, 3);
        }

        [Fact]
        public void IndexedArray2ByteLength()
        {
            int valueCount = 100;
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            for (long i = 0; i < valueCount; i++)
            {
                builder.Add(
                    i + "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.");
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.Equal(slice.TypeCode, (byte)0x07);

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, valueCount);
        }

        [Fact]
        public void IndexedArray2ByteLengthNoIndexTable()
        {
            int valueCount = 100;
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            for (long i = 0; i < valueCount; i++)
            {
                builder.Add(
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.");
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.Equal(slice.TypeCode, (byte)0x03);

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, valueCount);
        }

        [Fact]
        public void IndexedArray4ByteLength()
        {
            int valueCount = 200;
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            for (long i = 0; i < valueCount; i++)
            {
                builder.Add(
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.");
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.Equal(slice.TypeCode, (byte)0x04);

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, valueCount);
        }

        [Fact]
        public void IndexedArray4ByteLengthNoIndexTable()
        {
            int valueCount = 200;
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            for (long i = 0; i < valueCount; i++)
            {
                builder.Add(
                    i + "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus.");
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.Equal(slice.TypeCode, (byte)0x08);

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, valueCount);
        }

        [Fact]
        public void ArrayInArray()
        {
            //{ { 1, 2, 3 }, { 1, 2, 3 } }
            long[][] values = new long[2][];
            values[0] = new long[] { 1, 2, 3 };
            values[1] = new long[] { 1, 2, 3 };

            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            foreach (var ls in values)
            {
                builder.Add(SliceType.Array);
                foreach (var l in ls)
                {
                    builder.Add(l);
                }
                builder.Close();
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                VPackSlice ls = slice[i];

                Assert.True(ls.IsType(SliceType.Array));

                Assert.Equal(ls.Length, values[i].Length);
                for (int j = 0; j < values[i].Length; j++)
                {
                    VPackSlice l = ls[j];

                    Assert.True(l.IsInteger());

                    Assert.Equal(l.ToInt64(), values[i][j]);
                }
            }
        }

        [Fact]
        public void ArrayInArrayInArray()
        {
            //{ { { 1, 2, 3 } } }
            long[][][] values = new long[1][][];
            values[0] = new long[1][];
            values[0][0] = new long[] { 1, 2, 3 };

            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            foreach (var lss in values)
            {
                builder.Add(SliceType.Array);
                foreach (var ls in lss)
                {
                    builder.Add(SliceType.Array);
                    foreach (var l in ls)
                    {
                        builder.Add(l);
                    }
                    builder.Close();
                }
                builder.Close();
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                VPackSlice lls = slice[i];

                Assert.True(lls.IsType(SliceType.Array));

                Assert.Equal(lls.Length, values[i].Length);
                for (int j = 0; j < values[i].Length; j++)
                {
                    VPackSlice ls = lls[i];

                    Assert.True(ls.IsType(SliceType.Array));

                    Assert.Equal(ls.Length, values[i][j].Length);
                    for (int k = 0; k < values[i][j].Length; k++)
                    {
                        VPackSlice l = ls[k];

                        Assert.True(l.IsInteger());

                        Assert.Equal(l.ToInt64(), values[i][j][k]);
                    }
                }

            }
        }

        [Fact]
        public void EmptyObject()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 0);
            VPackSlice a = slice["a"];

            Assert.True(a.IsType(SliceType.None));
            Assert.Throws<IndexOutOfRangeException>(() => slice.KeyAt(0));
            Assert.Throws<IndexOutOfRangeException>(() => slice.ValueAt(0));
        }

        [Fact]
        public void CompactObject()
        {
            // {"a": 12, "b": true, "c": "xyz"}
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object, true);
            builder.Add("a", 12);
            builder.Add("b", true);
            builder.Add("c", "xyz");
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 3);

            Assert.Equal(slice["a"].ToInt64(), 12L);

            Assert.True(slice["b"].ToBoolean());

            Assert.Equal(slice["c"].ToStringValue(), "xyz");
        }

        [Fact]
        public void UnindexedObject()
        {
            // {"a": 12, "b": true, "c": "xyz"}
            VPackBuilder builder = new VPackBuilder();
            builder.GetOptions().SetBuildUnindexedObjects(true);
            builder.Add(SliceType.Object, false);
            builder.Add("a", 12);
            builder.Add("b", true);
            builder.Add("c", "xyz");
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 3);

            Assert.Equal(slice["a"].ToInt64(), 12L);

            Assert.True(slice["b"].ToBoolean());

            Assert.Equal(slice["c"].ToStringValue(), "xyz");
        }

        [Fact]
        public void IndexedObject()
        {
            // {"a": 12, "b": true, "c": "xyz"}
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Add("a", 12);
            builder.Add("b", true);
            builder.Add("c", "xyz");
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 3);

            Assert.Equal(slice["a"].ToInt64(), 12L);

            Assert.True(slice["b"].ToBoolean());

            Assert.Equal(slice["c"].ToStringValue(), "xyz");
        }

        [Fact]
        public void ObjectInObject()
        {
            // {"a":{"a1":1,"a2":2},"b":{"b1":1,"b2":1}}
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            {
                builder.Add("a", SliceType.Object);
                builder.Add("a1", 1);
                builder.Add("a2", 2);
                builder.Close();
            }
            {
                builder.Add("b", SliceType.Object);
                builder.Add("b1", 1);
                builder.Add("b2", 2);
                builder.Close();
            }
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 2);
            {
                VPackSlice a = slice["a"];

                Assert.True(a.IsType(SliceType.Object));

                Assert.Equal(a.Length, 2);

                Assert.Equal(a["a1"].ToInt64(), 1L);

                Assert.Equal(a["a2"].ToInt64(), 2L);
            }
            {
                VPackSlice b = slice["b"];

                Assert.True(b.IsType(SliceType.Object));

                Assert.Equal(b.Length, 2);

                Assert.Equal(b["b1"].ToInt64(), 1L);

                Assert.Equal(b["b2"].ToInt64(), 2L);
            }
        }

        [Fact]
        public void ObjectInObjectInObject()
        {
            // {"a":{"b":{"c":{"d":true}}}
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Add("a", SliceType.Object);
            builder.Add("b", SliceType.Object);
            builder.Add("c", SliceType.Object);
            builder.Add("d", true);
            builder.Close();
            builder.Close();
            builder.Close();
            builder.Close();

            VPackSlice slice = builder.Slice();

            Assert.True(slice.IsType(SliceType.Object));

            Assert.Equal(slice.Length, 1);
            VPackSlice a = slice["a"];

            Assert.True(a.IsType(SliceType.Object));

            Assert.Equal(a.Length, 1);
            VPackSlice b = a["b"];

            Assert.True(b.IsType(SliceType.Object));

            Assert.Equal(b.Length, 1);
            VPackSlice c = b["c"];

            Assert.True(c.IsType(SliceType.Object));

            Assert.Equal(c.Length, 1);
            VPackSlice d = c["d"];

            Assert.True(d.IsType(SliceType.Boolean));

            Assert.True(d.ToBoolean());
        }

        [Fact]
        public void ObjectAttributeNotFound()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Add("a", "a");
            builder.Close();
            VPackSlice vpack = builder.Slice();

            Assert.True(vpack.IsType(SliceType.Object));
            VPackSlice b = vpack["b"];

            Assert.True(b.IsType(SliceType.None));
        }

        [Fact]
        public void Object1ByteOffset()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            int size = 5;
            for (int i = 0; i < size; i++)
            {
                builder.Add(i.ToString(), SliceType.Object);
                for (int j = 0; j < size; j++)
                {
                    builder.Add(j.ToString(), "test");
                }
                builder.Close();
            }
            builder.Close();
            VPackSlice vpack = builder.Slice();

            Assert.True(vpack.IsType(SliceType.Object));

            Assert.Equal(vpack.Length, size);
            for (int i = 0; i < size; i++)
            {
                VPackSlice attr = vpack[i.ToString()];

                Assert.True(attr.IsType(SliceType.Object));
                for (int j = 0; j < size; j++)
                {
                    VPackSlice childAttr = attr[j.ToString()];

                    Assert.True(childAttr.IsType(SliceType.String));
                }
            }
        }

        [Fact]
        public void Object2ByteOffset()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            int size = 10;
            for (int i = 0; i < size; i++)
            {
                builder.Add(i.ToString(), SliceType.Object);
                for (int j = 0; j < size; j++)
                {
                    builder.Add(j.ToString(), "test");
                }
                builder.Close();
            }
            builder.Close();
            VPackSlice vpack = builder.Slice();

            Assert.True(vpack.IsType(SliceType.Object));

            Assert.Equal(vpack.Length, size);
            for (int i = 0; i < size; i++)
            {
                VPackSlice attr = vpack[i.ToString()];

                Assert.True(attr.IsType(SliceType.Object));
                for (int j = 0; j < size; j++)
                {
                    VPackSlice childAttr = attr[j.ToString()];

                    Assert.True(childAttr.IsType(SliceType.String));
                }
            }
        }

        [Fact]
        public void SortObjectAttr()
        {
            int min = 0;
            int max = 9;
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            for (int i = max; i >= min; i--)
            {
                builder.Add(i.ToString(), "test");
            }
            builder.Close();
            VPackSlice vpack = builder.Slice();

            Assert.True(vpack.IsType(SliceType.Object));

            Assert.Equal(vpack.Length, max - min + 1);
            for (int i = min, j = 0; i <= max; i++, j++)
            {
                Assert.Equal(vpack.KeyAt(j).ToStringValue(), i.ToString());
            }
        }

        [Fact]
        public void SortObjectAttr2()
        {
            string[] keys = { "a", "b", "c", "d", "e", "f", "g", "h" };
            string[] keysUnsorted = { "b", "d", "c", "e", "g", "f", "h", "a" };
            Assert.Equal(keysUnsorted.Length, keys.Length);
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            for (int i = 0; i < keysUnsorted.Length; i++)
            {
                builder.Add(keysUnsorted[i].ToString(), "test");
            }
            builder.Close();
            VPackSlice vpack = builder.Slice();

            Assert.True(vpack.IsType(SliceType.Object));

            Assert.Equal(vpack.Length, keys.Length);
            for (int i = 0; i < keys.Length; i++)
            {
                Assert.Equal(vpack.KeyAt(i).ToStringValue(), keys[i].ToString());
            }
        }

        [Fact]
        public void AttributeAdapterDefaults()
        {
            VPackSlice vpackWithAttrAdapter;

            {
                VPackBuilder builder = new VPackBuilder();
                builder.Add(SliceType.Object);
                builder.Add("_key", "a");
                builder.Close();
                vpackWithAttrAdapter = builder.Slice();
                Assert.True(vpackWithAttrAdapter.IsType(SliceType.Object));
            }
            VPackSlice vpackWithoutAttrAdapter;

            {
                VPackBuilder builder = new VPackBuilder();
                builder.Add(SliceType.Object);
                builder.Add("_kay", "a");
                builder.Close();
                vpackWithoutAttrAdapter = builder.Slice();
                Assert.True(vpackWithoutAttrAdapter.IsType(SliceType.Object));
            }
            Assert.True(vpackWithAttrAdapter.GetByteSize() < vpackWithoutAttrAdapter.GetByteSize());
        }

        [Fact]
        public void CloseClosed()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Close();
            Assert.Throws<VPackBuilderNeedOpenCompoundException>(() => builder.Close());
        }

        [Fact]
        public void AddBinary()
        {
            byte[] expected = new byte[] { 49, 50, 51, 52, 53, 54, 55, 56, 57 };
            VPackBuilder builder = new VPackBuilder();
            builder.Add(expected);
            VPackSlice slice = builder.Slice();


            Assert.True(slice.IsType(SliceType.Binary));

            Assert.Equal(slice.BinaryLength(), expected.Length);

            Assert.Equal(slice.ToBinary(), expected);

            Assert.Equal(slice.GetByteSize(), 1 + 4 + expected.Length);
        }

        [Fact]
        public void AddVPack()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            builder.Add("s", new VPackBuilder().Add("test").Slice());
            builder.Close();
            VPackSlice slice = builder.Slice();

            Assert.NotNull(slice);

            Assert.True(slice.IsType(SliceType.Object));

            Assert.True(slice["s"].IsType(SliceType.String));

            Assert.Equal(slice["s"].ToStringValue(), "test");

            Assert.Equal(slice.Length, 1);
        }

        [Fact]
        public void AddVPackObject()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Object);
            {
                VPackBuilder builder2 = new VPackBuilder();
                builder2.Add(SliceType.Object);
                builder2.Add("s", "test");
                builder2.Close();
                builder.Add("o", builder2.Slice());
            }
            builder.Close();
            VPackSlice slice = builder.Slice();

            Assert.NotNull(slice);

            Assert.True(slice.IsType(SliceType.Object));

            Assert.True(slice["o"].IsType(SliceType.Object));

            Assert.True(slice["o"]["s"].IsType(SliceType.String));

            Assert.Equal(slice["o"]["s"].ToStringValue(), "test");

            Assert.Equal(slice.Length, 1);

            Assert.Equal(slice["o"].Length, 1);
        }

        [Fact]
        public void AddVPackObjectInArray()
        {
            VPackBuilder builder = new VPackBuilder();
            builder.Add(SliceType.Array);
            for (int i = 0; i < 10; i++)
            {
                VPackBuilder builder2 = new VPackBuilder();
                builder2.Add(SliceType.Object);
                builder2.Add("s", "test");
                builder2.Close();
                builder.Add(builder2.Slice());
            }
            builder.Close();
            VPackSlice slice = builder.Slice();

            Assert.NotNull(slice);

            Assert.True(slice.IsType(SliceType.Array));

            Assert.Equal(slice.Length, 10);
            for (int i = 0; i < 10; i++)
            {
                Assert.True(slice[i].IsType(SliceType.Object));

                Assert.True(slice[i]["s"].IsType(SliceType.String));

                Assert.Equal(slice[i]["s"].ToStringValue(), "test");

                Assert.Equal(slice[i].Length, 1);
            }
        }

        [Fact]
        public void NonASCII()
        {
            string s = "·ÃÂ";
            VPackSlice vpack = new VPackBuilder().Add(s).Slice();
            Assert.True(vpack.IsType(SliceType.String));
            Assert.Equal(vpack.ToStringValue(), s);
        }
    }
}