using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BuilderGame.Utils {
    public class LimitedPoolTest {
        private GameObject prefab;
        private LimitedPool pool;
        [UnitySetUp]
        public IEnumerator SetUp() {
            prefab = new GameObject();
            pool = new LimitedPool(prefab, 1, 3);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCounts() {
            Assert.AreEqual(1, pool.CountInactive);
            Assert.AreEqual(0, pool.CountActive);
            pool.Get();
            yield return null;
            Assert.AreEqual(0, pool.CountInactive);
            Assert.AreEqual(1, pool.CountActive);
            pool.Get();
            yield return null;
            Assert.AreEqual(0, pool.CountInactive);
            Assert.AreEqual(2, pool.CountActive);
            pool.Get();
            yield return null;
            Assert.AreEqual(0, pool.CountInactive);
            Assert.AreEqual(3, pool.CountActive);
            GameObject g = pool.Get();
            yield return null;
            Assert.AreEqual(0, pool.CountInactive);
            Assert.AreEqual(3, pool.CountActive);
            pool.Release(g);
            yield return null;
            Assert.AreEqual(1, pool.CountInactive);
            Assert.AreEqual(2, pool.CountActive);
        }
        
        [UnityTest]
        public IEnumerator TestFirstElementIsFromPool() {
            GameObject expected = pool._inactiveQueue.Peek();
            GameObject fromPool = pool.Get();
            Assert.AreEqual(expected, fromPool);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCreatedElementsInActive() {
            GameObject object1 = pool.Get();
            GameObject object2 = pool.Get();
            GameObject object3 = pool.Get();
            Assert.True(pool._activeQueue.Contains(object1));
            Assert.True(pool._activeQueue.Contains(object2));
            Assert.True(pool._activeQueue.Contains(object3));
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestRelease() {
            GameObject fromPool = pool.Get();
            pool.Release(fromPool);
            yield return null;
            Assert.True(pool._inactiveQueue.Contains(fromPool));
            Assert.False(pool._activeQueue.Contains(fromPool));
        }
        
        [UnityTest]
        public IEnumerator TestLimitReleases() {
            pool = new LimitedPool(prefab, 1, 1, true);
            GameObject fromPool = pool.Get();
            pool.Get();
            pool.Release(fromPool);
            yield return null;
            Assert.True(fromPool.activeSelf);
            pool.Release(fromPool);
            yield return null;
            Assert.False(fromPool.activeSelf);
        }

        [UnityTearDown]
        public IEnumerator TearDown() {
            GameObject.Destroy(prefab);
            pool.Clear();
            pool = null;
            yield return null;
        }
    }
}
