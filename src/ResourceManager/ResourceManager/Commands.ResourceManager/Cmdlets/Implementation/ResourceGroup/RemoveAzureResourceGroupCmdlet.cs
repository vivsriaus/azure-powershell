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

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.Implementation
{
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ResourceManager.Cmdlets.Components;

    /// <summary>
    /// Removes the resource group.
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRMResourceGroup", DefaultParameterSetName = RemoveAzureResourceGroupCmdlet.ResourceGroupNameParameterSet), OutputType(typeof(bool))]
    public class RemoveAzureResourceGroupCmdlet : ResourceManagerCmdletBase
    {
        /// <summary>
        /// The resource Id parameter set.
        /// </summary>
        internal const string ResourceIdParameterSet = "The resource Id parameter set.";

        /// <summary>
        /// The resource name parameter set.
        /// </summary>
        internal const string ResourceGroupNameParameterSet = "The resource name and location parameter set.";

        /// <summary>
        /// Gets or sets the resource group name parameter.
        /// </summary>
        [Parameter(ParameterSetName = RemoveAzureResourceGroupCmdlet.ResourceGroupNameParameterSet, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The resource group name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the resource id parameter
        /// </summary>
        [Alias("ResourceId")]
        [Parameter(ParameterSetName = RemoveAzureResourceGroupCmdlet.ResourceIdParameterSet, Mandatory = true, ValueFromPipelineByPropertyName = true, HelpMessage = "The fully qualified resource group Id, including the subscription. e.g. /subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}")]
        [ValidateNotNullOrEmpty]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the force parameter.
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Do not ask for confirmation.")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Executes the cmdlet.
        /// </summary>
        protected override void OnProcessRecord()
        {
            base.OnProcessRecord();

            this.RunCmdlet();
        }

        /// <summary>
        /// Contains the cmdlet's execution logic.
        /// </summary>
        private void RunCmdlet()
        {
            base.OnProcessRecord();
            string resourceId = this.Id ?? this.GetResourceId();
            this.ConfirmAction(
                this.Force,
                string.Format("Are you sure you want to delete the following resource group: {0}", resourceId),
                "Deleting the resource group...",
                resourceId,
                () =>
                {
                    var apiVersion = this.DetermineApiVersion(resourceId: resourceId).Result;

                    var operationResult = this.GetResourcesClient()
                        .DeleteResource(
                            resourceId: resourceId,
                            apiVersion: apiVersion,
                            cancellationToken: this.CancellationToken.Value,
                            odataQuery: null)
                        .Result;

                    var managementUri = this.GetResourcesClient()
                        .GetResourceManagementRequestUri(
                            resourceId: resourceId,
                            apiVersion: apiVersion,
                            odataQuery: null);

                    var activity = string.Format("DELETE {0}", managementUri.PathAndQuery);

                    this.GetLongRunningOperationTracker(activityName: activity, isResourceCreateOrUpdate: false)
                        .WaitOnOperation(operationResult: operationResult);

                    this.WriteObject(true);
                });
        }

        /// <summary>
        /// Gets the resource Id from the supplied PowerShell parameters.
        /// </summary>
        protected string GetResourceId()
        {
            return ResourceIdUtility.GetResourceId(DefaultContext.Subscription.Id, this.Name, null, null);
        }
    }
}