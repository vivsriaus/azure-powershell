// ----------------------------------------------------------------------------------
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

using Newtonsoft.Json;

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Entities.Resources
{
    /// <summary>
    /// The resource identity object.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Gets or sets the principal Id.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string PrincipalId { get; set; }

        /// <summary>
        /// Gets or sets the tenant Id.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or sets the identity type.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Type { get; set; }
    }
}

