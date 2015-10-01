﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ScenarioTest.SqlTests;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;

namespace Microsoft.Azure.Commands.Sql.Test.ScenarioTests
{
    public class ElasticPoolCrudTests : SqlTestsBase
    {
        // Currently the test runs too long to be marked as a check-in test.
        [Fact]
        public void TestElasticPoolCreate()
        {
            RunPowerShellTest("Test-CreateElasticPool");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestElasticPoolUpdate()
        {
            RunPowerShellTest("Test-UpdateElasticPool");
        }

        [Fact(Skip = "TODO: Need to fix the script. Re-record fails because of a server error 'Resource not found for the segment 'operationResults' when creating elastic pool")]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestElasticPoolGet()
        {
            RunPowerShellTest("Test-GetElasticPool");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestElasticPoolMetricGet()
        {
            RunPowerShellTest("Test-GetElasticPoolMetric");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestElasticPoolRemove()
        {
            RunPowerShellTest("Test-RemoveElasticPool");
        }
    }
}
