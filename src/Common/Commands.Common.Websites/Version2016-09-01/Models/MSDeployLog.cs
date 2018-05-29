// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.WebSites.Version2016_09_01.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// MSDeploy log
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class MSDeployLog : ProxyOnlyResource
    {
        /// <summary>
        /// Initializes a new instance of the MSDeployLog class.
        /// </summary>
        public MSDeployLog()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MSDeployLog class.
        /// </summary>
        /// <param name="id">Resource Id.</param>
        /// <param name="name">Resource Name.</param>
        /// <param name="kind">Kind of resource.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="entries">List of log entry messages</param>
        public MSDeployLog(string id = default(string), string name = default(string), string kind = default(string), string type = default(string), IList<MSDeployLogEntry> entries = default(IList<MSDeployLogEntry>))
            : base(id, name, kind, type)
        {
            Entries = entries;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets list of log entry messages
        /// </summary>
        [JsonProperty(PropertyName = "properties.entries")]
        public IList<MSDeployLogEntry> Entries { get; private set; }

    }
}
