using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArangoDB.VelocyPack.Test
{
    public class BuilderSimple
    {
        [Fact]
        public void AddBoolTrue()
        {
            var slice = new VPackBuilder()
                .Add(true)
                .Slice();

            Assert.True(slice.IsType(SliceType.Boolean));
            Assert.Equal(slice.Value().GetType(), typeof(bool));
            Assert.Equal(slice.Value(), true);
            Assert.Equal(slice.ToBoolean(), true);
        }

        [Fact]
        public void AddBoolFalse()
        {
            var slice = new VPackBuilder()
                .Add(false)
                .Slice();

            Assert.True(slice.IsType(SliceType.Boolean));
            Assert.Equal(slice.Value().GetType(), typeof(bool));
            Assert.Equal(slice.Value(), false);
            Assert.Equal(slice.ToBoolean(), false);
        }

        [Fact]
        public void AddBoolNull()
        {
            var slice = new VPackBuilder()
                .Add((bool?)null)
                .Slice();

            Assert.True(slice.IsType(SliceType.Null));
            Assert.Equal(slice.Value(), null);
            Assert.Equal(slice.ToBoolean(), null);
        }

        [Fact]
        public void AddDoubleMin()
        {
            var slice = new VPackBuilder()
                .Add(double.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Double));
            Assert.Equal(slice.Value().GetType(), typeof(double));
            Assert.Equal(slice.Value(), double.MinValue);
            Assert.Equal(slice.ToDouble(), double.MinValue);
        }

        [Fact]
        public void AddDoubleMax()
        {
            var slice = new VPackBuilder()
                .Add(double.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Double));
            Assert.Equal(slice.Value().GetType(), typeof(double));
            Assert.Equal(slice.Value(), double.MaxValue);
            Assert.Equal(slice.ToDouble(), double.MaxValue);
        }

        [Fact]
        public void AddDoubleNull()
        {
            var slice = new VPackBuilder()
                .Add((double?)null)
                .Slice();

            Assert.True(slice.IsType(SliceType.Null));
            Assert.Equal(slice.Value(), null);
            Assert.Equal(slice.ToDouble(), null);
        }

        [Fact]
        public void AddSingleMin()
        {
            var slice = new VPackBuilder()
                .Add(float.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Double));
            Assert.Equal(slice.Value().GetType(), typeof(double));
            Assert.Equal(slice.ToSingle(), float.MinValue);
        }

        [Fact]
        public void AddSingleMax()
        {
            var slice = new VPackBuilder()
                .Add(float.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Double));
            Assert.Equal(slice.Value().GetType(), typeof(double));
            Assert.Equal(slice.ToSingle(), float.MaxValue);
        }

        [Fact]
        public void AddSByteMax()
        {
            var slice = new VPackBuilder()
                .Add(sbyte.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x20);
            Assert.Equal(slice.Value().GetType(), typeof(sbyte));
            Assert.Equal(slice.Value(), sbyte.MaxValue);
            Assert.Equal(slice.ToSByte(), sbyte.MaxValue);
        }

        [Fact]
        public void AddSByteMin()
        {
            var slice = new VPackBuilder()
                .Add(sbyte.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x20);
            Assert.Equal(slice.Value().GetType(), typeof(sbyte));
            Assert.Equal(slice.Value(), sbyte.MinValue);
            Assert.Equal(slice.ToSByte(), sbyte.MinValue);
        }

        [Fact]
        public void AddShortMax()
        {
            var slice = new VPackBuilder()
                .Add(short.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x21);
            Assert.Equal(slice.Value().GetType(), typeof(short));
            Assert.Equal(slice.Value(), short.MaxValue);
            Assert.Equal(slice.ToInt16(), short.MaxValue);
        }

        [Fact]
        public void AddShortMin()
        {
            var slice = new VPackBuilder()
                .Add(short.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x21);
            Assert.Equal(slice.Value().GetType(), typeof(short));
            Assert.Equal(slice.Value(), short.MinValue);
            Assert.Equal(slice.ToInt16(), short.MinValue);
        }

        [Fact]
        public void AddIntMax()
        {
            var slice = new VPackBuilder()
                .Add(int.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x23);
            Assert.Equal(slice.Value().GetType(), typeof(int));
            Assert.Equal(slice.Value(), int.MaxValue);
            Assert.Equal(slice.ToInt32(), int.MaxValue);
        }

        [Fact]
        public void AddIntMin()
        {
            var slice = new VPackBuilder()
                .Add(int.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x23);
            Assert.Equal(slice.Value().GetType(), typeof(int));
            Assert.Equal(slice.Value(), int.MinValue);
            Assert.Equal(slice.ToInt32(), int.MinValue);
        }

        [Fact]
        public void AddLongMax()
        {
            var slice = new VPackBuilder()
                .Add(long.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x27);
            Assert.Equal(slice.Value().GetType(), typeof(long));
            Assert.Equal(slice.Value(), long.MaxValue);
            Assert.Equal(slice.ToInt64(), long.MaxValue);
        }

        [Fact]
        public void AddLongMin()
        {
            var slice = new VPackBuilder()
                .Add(long.MinValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.TypeCode, 0x27);
            Assert.Equal(slice.Value().GetType(), typeof(long));
            Assert.Equal(slice.Value(), long.MinValue);
            Assert.Equal(slice.ToInt64(), long.MinValue);
        }

        [Fact]
        public void AddByteMax()
        {
            var slice = new VPackBuilder()
                .Add(byte.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.UInt));
            Assert.Equal(slice.TypeCode, 0x28);
            Assert.Equal(slice.Value().GetType(), typeof(byte));
            Assert.Equal(slice.Value(), byte.MaxValue);
            Assert.Equal(slice.ToByte(), byte.MaxValue);
        }

        [Fact]
        public void AddUShortMax()
        {
            var slice = new VPackBuilder()
                .Add(ushort.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.UInt));
            Assert.Equal(slice.TypeCode, 0x29);
            Assert.Equal(slice.Value().GetType(), typeof(ushort));
            Assert.Equal(slice.Value(), ushort.MaxValue);
            Assert.Equal(slice.ToUInt16(), ushort.MaxValue);
        }

        [Fact]
        public void AddUIntMax()
        {
            var slice = new VPackBuilder()
                .Add(uint.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.UInt));
            Assert.Equal(slice.TypeCode, 0x2b);
            Assert.Equal(slice.Value().GetType(), typeof(uint));
            Assert.Equal(slice.Value(), uint.MaxValue);
            Assert.Equal(slice.ToUInt32(), uint.MaxValue);
        }

        [Fact]
        public void AddULongMax()
        {
            var slice = new VPackBuilder()
                .Add(ulong.MaxValue)
                .Slice();

            Assert.True(slice.IsType(SliceType.UInt));
            Assert.Equal(slice.TypeCode, 0x2f);
            Assert.Equal(slice.Value().GetType(), typeof(ulong));
            Assert.Equal(slice.Value(), ulong.MaxValue);
            Assert.Equal(slice.ToUInt64(), ulong.MaxValue);
        }

        [Fact]
        public void AddDateTimeLocal()
        {
            var date = new DateTime(2000, 10, 9, 8, 7, 6);

            var slice = new VPackBuilder()
                .Add(date)
                .Slice();

            Assert.True(slice.IsType(SliceType.UtcDate));
            Assert.Equal(slice.Value().GetType(), typeof(DateTimeOffset));
            Assert.Equal(slice.Value(), new DateTimeOffset(date));
            Assert.Equal(slice.ToDateTime(), date);
        }

        [Fact]
        public void AddDateTimeUtc()
        {
            var date = new DateTime(2000, 10, 9, 8, 7, 6, DateTimeKind.Utc);

            var slice = new VPackBuilder()
                .Add(date)
                .Slice();

            Assert.True(slice.IsType(SliceType.UtcDate));
            Assert.Equal(slice.Value().GetType(), typeof(DateTimeOffset));
            Assert.Equal(slice.Value(), new DateTimeOffset(date));
            Assert.Equal(slice.ToDateTime(DateTimeKind.Utc), date);
        }

        [Fact]
        public void AddDateTimeOffset()
        {
            var date = new DateTimeOffset(2000, 10, 9, 8, 7, 6, TimeSpan.FromMinutes(150));

            var slice = new VPackBuilder()
                .Add(date)
                .Slice();

            Assert.True(slice.IsType(SliceType.UtcDate));
            Assert.Equal(slice.Value().GetType(), typeof(DateTimeOffset));
            Assert.Equal(slice.Value(), date);
            Assert.Equal(slice.ToDateTimeOffset(TimeSpan.FromMinutes(-30)), date);
        }

        [Fact]
        public void AddTimeSpan()
        {
            var timeSpan = TimeSpan.FromSeconds(321);

            var slice = new VPackBuilder()
                .Add(timeSpan)
                .Slice();

            Assert.True(slice.IsType(SliceType.Int));
            Assert.Equal(slice.Value().GetType(), typeof(long));
            Assert.Equal(slice.Value(), timeSpan.Ticks);
            Assert.Equal(slice.ToTimeSpan(), timeSpan);
        }

        [Fact]
        public void AddArrayNotSameType()
        {
            var slice = new VPackBuilder()
                   .Add(SliceType.Array)
                   .Add(1d)
                   .Add((int?)2)
                   .Add(3L)
                   .Close()
                   .Slice();

            Assert.True(slice.IsType(SliceType.Array));
            Assert.Equal(slice.Length, 3);

            Assert.True(slice[0].IsType(SliceType.Double));
            Assert.Equal(slice[0].Value().GetType(), typeof(double));
            Assert.Equal(slice[0].Value(), 1d);
            Assert.Equal(slice[0].ToDouble(), 1d);

            Assert.True(slice[1].IsType(SliceType.Int));
            Assert.Equal(slice[1].TypeCode, 0x23);
            Assert.Equal(slice[1].Value().GetType(), typeof(int));
            Assert.Equal(slice[1].Value(), 2);
            Assert.Equal(slice[1].ToDouble(), 2);

            Assert.True(slice[2].IsType(SliceType.Int));
            Assert.Equal(slice[2].TypeCode, 0x27);
            Assert.Equal(slice[2].Value().GetType(), typeof(long));
            Assert.Equal(slice[2].Value(), 3L);
            Assert.Equal(slice[2].ToDouble(), 3L);
        }
    }
}
