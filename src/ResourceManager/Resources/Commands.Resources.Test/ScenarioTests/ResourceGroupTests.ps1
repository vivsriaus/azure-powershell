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
Tests creating new simple resource group.
#>
function Test-CreatesNewSimpleResourceGroup
{
    # Setup
    $rgname = Get-ResourceGroupName
    $location = Get-ProviderLocation ResourceManagement

    try 
    {
        # Test
        $actual = New-AzureRMResourceGroup -Name $rgname -Location $location -Tags @{Name = "testtag"; Value = "testval"}
        $expected = Get-AzureRMResourceGroup -Name $rgname

        # Assert
        Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName	
        Assert-AreEqual $expected.Tags[0]["Name"] $actual.Tags[0]["Name"]
    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgname
    }
}

<#
.SYNOPSIS
Tests updates existing resource group.
#>
function Test-UpdatesExistingResourceGroup
{
    # Setup
    $rgname = Get-ResourceGroupName
    $location = Get-ProviderLocation ResourceManagement

    try 
    {
        # Test update without tag
        Assert-Throws { Set-AzureRMResourceGroup -Name $rgname -Tags @{"testtag" = "testval"} -Force }
        
        $new = New-AzureRMResourceGroup -Name $rgname -Location $location

        $actual = Set-AzureRMResourceGroup -Name $rgname -Tags @{Name = "testtag"; Value = "testval"}
        $expected = Get-AzureRMResourceGroup -Name $rgname

        # Assert
        Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName	
        Assert-AreEqual 0 $new.Tags.Count
        Assert-AreEqual $expected.Tags[0]["Name"] $actual.Tags[0]["Name"]
    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgname
    }
}

<#
.SYNOPSIS
Tests updates existing resource group via piping
#>
function Test-UpdatesExistingResourceGroupViaPiping
{
    # Setup
    $rgname = Get-ResourceGroupName
    $location = Get-ProviderLocation ResourceManagement

    try
    {
        New-AzureResourceGroup -Name $rgname -Location $location
        $new = Get-AzureRMResourceGroup -Name $rgname
        $actual = $new | Set-AzureRMResourceGroup -Tags @{Name = "testtag"; Value = "testval"}
        $expected = Get-AzureRMResourceGroup -Name $rgname

        # Assert
        Assert-AreEqual $expected.ResourceGroupName $actual.ResourceGroupName
        Assert-AreEqual 0 $new.Tags.Count
        Assert-AreEqual $expected.Tags[0]["Name"] $actual.Tags[0]["Name"]
    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgname
    }
}

<#
.SYNOPSIS
Tests creating new simple resource group and deleting it via piping.
#>
function Test-CreatesAndRemoveResourceGroupViaPiping
{
    # Setup
    $rgname1 = Get-ResourceGroupName
    $rgname2 = Get-ResourceGroupName
    $location = Get-ProviderLocation ResourceManagement

    # Test
    New-AzureRMResourceGroup -Name $rgname1 -Location $location
    New-AzureRMResourceGroup -Name $rgname2 -Location $location

    Get-AzureRMResourceGroup | where {$_.ResourceGroupName -eq $rgname1 -or $_.ResourceGroupName -eq $rgname2} | Remove-AzureRMResourceGroup -Force

    # Assert
    Assert-Throws { Get-AzureRMResourceGroup -Name $rgname1 }
    Assert-Throws { Get-AzureRMResourceGroup -Name $rgname2 }
}

<#
.SYNOPSIS
Tests getting non-existing resource group.
#>
function Test-GetNonExistingResourceGroup
{
    # Setup
    $rgname = Get-ResourceGroupName

    Assert-Throws { Get-AzureRMResourceGroup -Name $rgname }
}

<#
.SYNOPSIS
Negative test. New resource group in non-existing location throws error.
#>
function Test-NewResourceGroupInNonExistingLocation
{
    # Setup
    $rgname = Get-ResourceGroupName

    Assert-Throws { New-AzureRMResourceGroup -Name $rgname -Location 'non-existing' }
}

<#
.SYNOPSIS
Negative test. New resource group in non-existing location throws error.
#>
function Test-RemoveNonExistingResourceGroup
{
    # Setup
    $rgname = Get-ResourceGroupName

    Assert-Throws { Remove-AzureRMResourceGroup $rgname -Force }
}

<#
.SYNOPSIS
Negative test. New resource group in non-existing location throws error.
#>
function Test-AzureTagsEndToEnd
{
    # Setup
    $tag1 = getAssetName
    $tag2 = getAssetName
    Clean-Tags

    # Create tag without values
    New-AzureRMTag $tag1

    $tag = Get-AzureTag $tag1
    Assert-AreEqual $tag1 $tag.Name

    # Add value to the tag (adding same value should pass)
    New-AzureRMTag $tag1 value1
    New-AzureRMTag $tag1 value1
    New-AzureRMTag $tag1 value2

    $tag = Get-AzureTag $tag1
    Assert-AreEqual 2 $tag.Values.Count

    # Create tag with values
    New-AzureRMTag $tag2 value1
    New-AzureRMTag $tag2 value2
    New-AzureRMTag $tag2 value3

    $tags = Get-AzureTag
    Assert-AreEqual 2 $tags.Count

    # Remove entire tag
    $tag = Remove-AzureRMTag $tag1 -Force -PassThru

    $tags = Get-AzureTag
    Assert-AreEqual $tag1 $tag.Name

    # Remove tag value
    $tag = Remove-AzureRMTag $tag2 value1 -Force -PassThru

    $tags = Get-AzureTag
    Assert-AreEqual 0 $tags.Count

    # Get a non-existing tag
    Assert-Throws { Get-AzureRMTag "non-existing" }

    Clean-Tags
}

<#
.SYNOPSIS
Tests registration of required template provider
#>
function Test-NewDeploymentAndProviderRegistration
{
    # Setup
    $rgname = Get-ResourceGroupName
    $rname = Get-ResourceName
    $location = Get-ProviderLocation ResourceManagement
    $template = "Microsoft.Cache.0.4.0-preview"
    $provider = "microsoft.cache"

    try 
    {
        # Unregistering microsoft.cache to have clean state
        $subscription = [Microsoft.WindowsAzure.Commands.Utilities.Common.AzureProfile]::Instance.CurrentSubscription
        $client = New-Object Microsoft.Azure.Commands.Resources.Models.ResourcesClient $subscription
         
        # Verify provider is registered
        $providers = [Microsoft.WindowsAzure.Commands.Utilities.Common.AzureProfile]::Instance.CurrentSubscription.RegisteredResourceProvidersList
        if( $providers -Contains $provider )
        {
            $client.UnregisterProvider($provider) 
        }

        # Test
        New-AzureRMResourceGroup -Name $rgname -Location $location
        $deployment = New-AzureRMResourceGroupDeployment -ResourceGroupName $rgname -Location $location -GalleryTemplateIdentity $template -cacheName $rname -cacheLocation $location

        # Assert
        $client = New-Object Microsoft.Azure.Commands.Resources.Models.ResourcesClient $subscription
        $providers = [Microsoft.WindowsAzure.Commands.Utilities.Common.AzureProfile]::Instance.CurrentSubscription.RegisteredResourceProvidersList
        
        Assert-True { $providers -Contains $provider }

    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgname
    }
}

<#
.SYNOPSIS
Tests deployment delete is successful
#>
function Test-RemoveDeployment
{
    # Setup
    $deploymentName = "Test"
    $templateUri = "https://gallery.azure.com/artifact/20140901/Microsoft.ResourceGroup.1.0.0/DeploymentTemplates/Template.json"
    $rgName = "TestSDK"

    try
    {
        # Test
        New-AzureRMResourceGroup -Name $rgName -Location "west us"
        $deployment = New-AzureRMResourceGroupDeployment -ResourceGroupName $rgName -Name $deploymentName -TemplateUri $templateUri
        Assert-True { Remove-AzureRMResourceGroupDeployment -ResourceGroupName $deployment.ResourceGroupName -Name $deployment.DeploymentName -Force -PassThru }
    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgName
    }
}

function Test-NewResourceGroupWithTemplateThenGetWithAndWithoutDetails
{
    # Setup
    $rgname = Get-ResourceGroupName
    $websiteName = Get-ResourceName
    $location = Get-ProviderLocation ResourceManagement
    $templateFile = "Resources\EmptyWebsiteTemplate.json"

    try
    {
        # Test
        New-AzureRMResourceGroup -Name $rgname -Location $location
        $actual = New-AzureRMResourceGroupDeployment -ResourceGroupName $rgname -TemplateFile $templateFile `
                    -siteName $websiteName -hostingPlanName "test" -siteLocation "West US" `
                    -Tag @{ Name = "testtag"; Value = "testval" }

        $expected1 = Get-AzureRMResourceGroup -Name $rgname
        # Assert
        Assert-AreEqual $expected1.ResourceGroupName $actual.ResourceGroupName
        Assert-AreEqual $expected1.Tags[0]["Name"] $actual.Tags[0]["Name"]
        Assert-AreEqual $expected1.Resources.Count 2

        $expected2 = Get-AzureRMResourceGroup
        # Assert
        Assert-AreEqual $expected2[0].Resources.Count 0

        $expected3 = Get-AzureRMResourceGroup -Detailed
        $names = $expected3 | Select-Object -ExpandProperty ResourceGroupName
        $index = [Array]::IndexOf($names, $expected1.ResourceGroupName)
        # Assert
        Assert-AreEqual $expected3[$index].Resources.Count 2
    }
    finally
    {
        # Cleanup
        Clean-ResourceGroup $rgname
    }
}