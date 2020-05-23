namespace Codefarts.Tests.ExtensionMethods
{
    using System;
    using System.Collections;
    using System.Security.Cryptography.X509Certificates;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass, TestCategory("Array")] 
    public class ArrayGetSet3DExtensionMethodTests
    {
        private struct IndexModel
        {
            public int X;
            public int Y;
            public int Z;
        }

        [TestMethod]
        public void Get3DDepth()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            Assert.AreEqual(3, items.Get3DDepth(3, 3));
        }

        [TestMethod]
        public void Remove3DDepth_At_End()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            var expectedItems = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                            };

            Assert.AreEqual(3, items.Get3DDepth(3, 3));
            try
            {
                items = items.Remove3DDepth(3, 3, 2);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception occurred!", ex);
            }

            Assert.AreEqual(2, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 2; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Remove3DDepth_At_Middle()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            var expectedItems = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            Assert.AreEqual(3, items.Get3DDepth(3, 3));
              try
            {
                items = items.Remove3DDepth(3, 3, 1);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception occurred!", ex);
            }
            
            Assert.AreEqual(2, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 2; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Remove3DDepth_At_Start()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            var expectedItems = new[]
                            {
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            Assert.AreEqual(3, items.Get3DDepth(3, 3));
            try
            {
                items = items.Remove3DDepth(3, 3, 0);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception occurred!", ex);
            } 
            
            Assert.AreEqual(2, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 2; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Set3DDepth()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            items = items.Set3DDepth(3, 3, 3);
            Assert.AreEqual(3, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(index <= 17 ? index : 0, items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Insert3D_At_End()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            items = items.Insert3D(3, 3, 2, new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
            Assert.AreEqual(3, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(index, items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Insert3D_At_End_With_Shorter_Source_Array()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            var expectedItems = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20,  0,  0,  0,  0,  0,  0
                            };

            var sourceArray = new[] { 18, 19, 20 };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            items = items.Insert3D(3, 3, 2, sourceArray);
            Assert.AreEqual(3, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Insert3D_At_End_With_Longer_Source_Array()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            var sourceArray = new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 20, 31, 32 };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            try
            {
                items = items.Insert3D(3, 3, 2, sourceArray);
                Assert.Fail("Expected a ArgumentException to be thrown.");
            }
            catch (ArgumentException ae)
            {
                // do nothing expected to be here
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception thrown.");
            }
        }

        [TestMethod]
        public void Insert3D_One_Passed_End()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            try
            {
                items = items.Insert3D(3, 3, 3, new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
                Assert.Fail("Should have thrown ArgumentOutOfRangeException!");
            }
            catch (ArgumentOutOfRangeException aor)
            {
                // do nothing we expect to be here
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception thrown!");
            }
        }

        [TestMethod]
        public void Insert3D_At_Middle()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            var expectedItems = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                               18, 19, 20, 21, 22, 23, 24, 25, 26,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            items = items.Insert3D(3, 3, 1, new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
            Assert.AreEqual(3, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Insert3D_At_Start()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            var expectedItems = new[]
                            {
                               18, 19, 20, 21, 22, 23, 24, 25, 26,
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            items = items.Insert3D(3, 3, 0, new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
            Assert.AreEqual(3, items.Get3DDepth(3, 3));

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(expectedItems[index], items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Insert3D_At_Negative_One()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17
                            };

            Assert.AreEqual(2, items.Get3DDepth(3, 3));
            try
            {
                items = items.Insert3D(3, 3, -1, new[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 });
                Assert.Fail("Should have thrown ArgumentOutOfRangeException!");
            }
            catch (ArgumentOutOfRangeException aor)
            {
                // do nothing we expect to be here
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception thrown!");
            }
        }

        [TestMethod]
        public void Get3D_RangeExceptions()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            var indexes = new[]
                {
                    new IndexModel { X = -1 },
                    new IndexModel { Y = -1 },
                    new IndexModel { Z = -1 },

                    new IndexModel { X = -1, Y = -1 },
                    new IndexModel { X = -1, Z = -1 },
                 
                    new IndexModel { Y = -1, Z = -1 },

                    new IndexModel { X = 100 },
                    new IndexModel { Y = 100 },
                    new IndexModel { Z = 100 },

                    new IndexModel { X = 100, Y = 100 },
                    new IndexModel { X = 100, Z = 100 },
                 
                    new IndexModel { Y = 100, Z = 100 },
                };

            /*
             * x
             * y
             * z
             * xy
             * xz
             * yz
             */

            for (var i = 0; i < indexes.Length; i++)
            {
                // test to see if negative x values throw exception
                try
                {
                    var index = indexes[i];
                    var value = items.Get3D(3, 3, index.X, index.Y, index.Z);
                    Assert.Fail("Should have thrown index out of range exception!");
                }
                catch (IndexOutOfRangeException ior)
                {
                    // do nothing we expected to be here
                }
                catch (Exception)
                {
                    Assert.Fail("Unexpected exception type thrown!");
                }
            }
        }

        [TestMethod]
        public void Get3D_Values()
        {
            var items = new[]
                            {
                                0,  1,  2,  3,  4,  5,  6,  7,  8,
                                9, 10, 11, 12, 13, 14, 15, 16, 17,
                               18, 19, 20, 21, 22, 23, 24, 25, 26
                            };

            var index = 0;
            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(index, items.Get3D(3, 3, x, y, z));
                        index++;
                    }
                }
            }
        }

        [TestMethod]
        public void Get3D_RandomValues()
        {
            var randomIitems = new int[3 * 3 * 3];
            var random = new Random((int)DateTime.Now.Ticks);
            for (var i = 0; i < randomIitems.Length; i++)
            {
                randomIitems[i] = random.Next();
            }

            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(randomIitems[(z * (3 * 3)) + ((y * 3) + x)], randomIitems.Get3D(3, 3, x, y, z));
                    }
                }
            }
        }

        [TestMethod]
        public void Set3D_Values()
        {
            var items = new[]
                            {
                                // top layer
                                1,  0,  0,  
                                0,  0,  0,  
                                0,  0,  0,
                                
                                // mid layer
                                0,  0,  0,
                                0,  1,  0,
                                0,  0,  0,

                                // bottom layer
                                0,  0,  0,
                                0,  0,  0,
                                0,  0,  1
                            };

            var values = new int[3 * 3 * 3];
            values.Set3D(3, 3, 0, 0, 0, 1);
            values.Set3D(3, 3, 1, 1, 1, 1);
            values.Set3D(3, 3, 2, 2, 2, 1);

            Assert.AreEqual(values.Length, items.Length);
            for (var i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(items[i], values[i]);
            }
        }


        [TestMethod]
        public void Set3D_RandomValues()
        {
            var randomIitems = new int[3 * 3 * 3];
            var indexes = new int[3 * 3 * 3];
            var random = new Random((int)DateTime.Now.Ticks);

            // build sequence of indexes
            for (var i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i;
            }

            // randomize index sequence
            for (var i = 0; i < indexes.Length; i++)
            {
                var indexA = random.Next(0, indexes.Length);
                var indexB = random.Next(0, indexes.Length);
                indexes.Swap(indexA, indexB);
            }

            for (int i = 0; i < indexes.Length; i++)
            {

            }


            for (var z = 0; z < 3; z++)
            {
                for (var y = 0; y < 3; y++)
                {
                    for (var x = 0; x < 3; x++)
                    {
                        Assert.AreEqual(randomIitems[(z * (3 * 3)) + ((y * 3) + x)], randomIitems.Get3D(3, 3, x, y, z));
                    }
                }
            }
        }
    }
}