﻿/* Copyright 2010 10gen Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.DriverOnlineTests {
    [TestFixture]
    public class MongoCollectionTests {
        private MongoServer server;
        private MongoDatabase database;
        private MongoCollection<BsonDocument> collection;

        [TestFixtureSetUp]
        public void Setup() {
            server = MongoServer.Create();
            server.Connect();
            database = server["onlinetests"];
            collection = database["testcollection"];
        }

        // TODO: more tests for MongoCollection

        [Test]
        public void TestCountZero() {
            collection.RemoveAll();
            var count = collection.Count();
            Assert.AreEqual(0, count);
        }

        [Test]
        public void TestCountOne() {
            collection.RemoveAll();
            collection.Insert(new BsonDocument());
            var count = collection.Count();
            Assert.AreEqual(1, count);
        }

        [Test]
        public void TestSetFields() {
            collection.RemoveAll();
            collection.Insert(new BsonDocument { { "x", 1 }, { "y", 2 } });
            var result = collection.FindAll().SetFields("x").FirstOrDefault();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("_id", result.GetElement(0).Name);
            Assert.AreEqual("x", result.GetElement(1).Name);
        }
    }
}
