﻿# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
    .SYNOPSIS
    Tests registering and a resource provider feature.
#>
function Test-AzureProviderFeature
{
    $defaultProviderFeatures = Get-AzureRMProviderFeature

    $allProviderFeatures = Get-AzureRMProviderFeature -ListAvailable

    Assert-True { $allProviderFeatures.Length -gt $defaultProviderFeatures.Length }

    $batchFeatures = Get-AzureRMProviderFeature -ProviderName "Microsoft.Batch"

    Assert-True { $batchFeatures.Length -eq 0 }

    $batchFeatures = Get-AzureRMProviderFeature -ProviderName "Microsoft.Batch" -ListAvailable

    Assert-True { $batchFeatures.Length -gt 0 }

    Register-AzureRMProviderFeature -ProviderName "Microsoft.Cache" -FeatureName "betaAccess3" -Force

    $cacheRegisteredFeatures = Get-AzureRMProviderFeature -ProviderName "Microsoft.Cache"

    Assert-True { $cacheRegisteredFeatures.Length -gt 0 }
}
