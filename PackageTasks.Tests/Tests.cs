﻿using Xunit;

namespace PackageTasks.Tests
{
    using System;
    using System.Collections;
    using Microsoft.Build.Framework;
    using static Tests.Result;
    public class Tests
    {
        public class TestTaskItem : ITaskItem
        {
            public string ItemSpec { get; set; }

            public int MetadataCount
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public ICollection MetadataNames
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public IDictionary CloneCustomMetadata()
            {
                throw new NotImplementedException();
            }

            public void CopyMetadataTo(ITaskItem destinationItem)
            {
                throw new NotImplementedException();
            }

            public string GetMetadata(string metadataName)
            {
                throw new NotImplementedException();
            }

            public void RemoveMetadata(string metadataName)
            {
                throw new NotImplementedException();
            }

            public void SetMetadata(string metadataName, string metadataValue)
            {
                throw new NotImplementedException();
            }
        }
        public enum Result
        {
            x_smaller_than_y = -1,
            x_equals_y = 0,
            x_grater_than_y = 1
        }

        [Theory]
        [InlineData(@"a", @"a", x_equals_y)]
        [InlineData(@"a", @"A", x_equals_y)]
        [InlineData(@"a", @"aa", x_smaller_than_y)]
        [InlineData(@"a", @"AA", x_smaller_than_y)]
        [InlineData(@"A", @"aa", x_smaller_than_y)]
        [InlineData(@"A", @"AA", x_smaller_than_y)]
        [InlineData(@"aa", @"a", x_grater_than_y)]
        [InlineData(@"AA", @"a", x_grater_than_y)]
        [InlineData(@"aa", @"A", x_grater_than_y)]
        [InlineData(@"AA", @"A", x_grater_than_y)]
        [InlineData(@"aaa1aaa1", @"1aaa1aaa", x_smaller_than_y)]
        [InlineData(@"1", @"1", x_equals_y)]
        [InlineData(@"1.1", @"1.1", x_equals_y)]
        [InlineData(@"1.1.1", @"1.1.1", x_equals_y)]
        [InlineData(@"1.1", @"1.1.1", x_smaller_than_y)]
        [InlineData(@"1", @"1.0", x_equals_y)]
        [InlineData(@"1", @"1.0.0", x_equals_y)]
        [InlineData(@"1", @"01", x_equals_y)]
        [InlineData(@"1", @"0 1", x_grater_than_y)]
        [InlineData(@"01", @"1", x_equals_y)]
        [InlineData(@"10", @"1", x_grater_than_y)]
        [InlineData(@"1", @"10", x_smaller_than_y)]
        [InlineData(@"10", @"01", x_grater_than_y)]
        [InlineData(@"01", @"10", x_smaller_than_y)]
        [InlineData(@"aaaaaa", @"aaaaaa1", x_smaller_than_y)]
        [InlineData(@"aaa1aaa1aaa", @"aaa1", x_grater_than_y)]
        [InlineData(@"aaa1", @"aaa1aaa1aaa", x_smaller_than_y)]
        [InlineData(@"aaa1.0.1a100", @"aaa1.0.1aaa1aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaa1aa\bbbb1.aaa", @"xxx\AAA01AA\bbbb1.aaa", x_equals_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb1.aa", x_grater_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1aaa.aaa", @"xxx\AAAAA\bbbb1aa.aaa", x_grater_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb1.aaa", x_equals_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb1.aaaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb01.aaa", x_equals_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb10.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb2.aaa", @"xxx\AAAAA\bbbb10.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb2.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\AAAAA\bbbb1.1.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.0.aaa", @"xxx\AAAAA\bbbb1.1.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\aaaaa\bbbb1.aaa", @"xxx\bbbb\bbbb1.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.aa", @"xxx\aaaaa\bbbb1.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.aaa", @"xxx\aaaaa\bbbb1.aaa", x_equals_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.aaa", @"xxx\aaaaa\bbbb1.0.0.0.0.0.0.aaa", x_equals_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.aaa", @"xxx\aaaaa\bbbb1.0.0.0.0.0.0a.aaa", x_smaller_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.aaaa", @"xxx\aaaaa\bbbb1.aaa", x_grater_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb01.aaa", @"xxx\aaaaa\bbbb1.aaa", x_equals_y)]
        [InlineData(@"xxx\AAAAA\bbbb10.aaa", @"xxx\aaaaa\bbbb1.aaa", x_grater_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb2.aaa", @"xxx\aaaaa\bbbb1.aaa", x_grater_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb20.aaa", @"xxx\aaaaa\bbbb3.aaa", x_grater_than_y)]
        [InlineData(@"xxx\AAAAA\bbbb1.1.aaa", @"xxx\aaaaa\bbbb1.aaa", x_grater_than_y)]
        [InlineData(@"xxx\bbbb\bbbb1.aaa", @"xxx\aaaaa\bbbb1.aaa", x_grater_than_y)]
        public void sort_order_of_paths(string x, string y, Result expectedResult)
        {
            var result = (Result)new NumbersInFileNameComparer().Compare(x, y);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void task_should_sort_items_ascending()
        {
            var task = new SortItemGroup()
            {
                In = new[]
                {
                    new TestTaskItem() { ItemSpec = "110"},
                    new TestTaskItem() { ItemSpec = "10"},
                    new TestTaskItem() { ItemSpec = "1"},
                }
            };

            var result = task.Execute();
            Assert.True(result);

            Assert.Equal(task.In.Length, task.Out.Length);
            Assert.Equal("1", task.Out[0].ItemSpec);
            Assert.Equal("10", task.Out[1].ItemSpec);
            Assert.Equal("110", task.Out[2].ItemSpec);
        }
    }
}
