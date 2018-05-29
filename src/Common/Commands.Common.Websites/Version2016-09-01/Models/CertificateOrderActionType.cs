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
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for CertificateOrderActionType.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CertificateOrderActionType
    {
        [EnumMember(Value = "CertificateIssued")]
        CertificateIssued,
        [EnumMember(Value = "CertificateOrderCanceled")]
        CertificateOrderCanceled,
        [EnumMember(Value = "CertificateOrderCreated")]
        CertificateOrderCreated,
        [EnumMember(Value = "CertificateRevoked")]
        CertificateRevoked,
        [EnumMember(Value = "DomainValidationComplete")]
        DomainValidationComplete,
        [EnumMember(Value = "FraudDetected")]
        FraudDetected,
        [EnumMember(Value = "OrgNameChange")]
        OrgNameChange,
        [EnumMember(Value = "OrgValidationComplete")]
        OrgValidationComplete,
        [EnumMember(Value = "SanDrop")]
        SanDrop,
        [EnumMember(Value = "FraudCleared")]
        FraudCleared,
        [EnumMember(Value = "CertificateExpired")]
        CertificateExpired,
        [EnumMember(Value = "CertificateExpirationWarning")]
        CertificateExpirationWarning,
        [EnumMember(Value = "FraudDocumentationRequired")]
        FraudDocumentationRequired,
        [EnumMember(Value = "Unknown")]
        Unknown
    }
    internal static class CertificateOrderActionTypeEnumExtension
    {
        internal static string ToSerializedValue(this CertificateOrderActionType? value)
        {
            return value == null ? null : ((CertificateOrderActionType)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this CertificateOrderActionType value)
        {
            switch( value )
            {
                case CertificateOrderActionType.CertificateIssued:
                    return "CertificateIssued";
                case CertificateOrderActionType.CertificateOrderCanceled:
                    return "CertificateOrderCanceled";
                case CertificateOrderActionType.CertificateOrderCreated:
                    return "CertificateOrderCreated";
                case CertificateOrderActionType.CertificateRevoked:
                    return "CertificateRevoked";
                case CertificateOrderActionType.DomainValidationComplete:
                    return "DomainValidationComplete";
                case CertificateOrderActionType.FraudDetected:
                    return "FraudDetected";
                case CertificateOrderActionType.OrgNameChange:
                    return "OrgNameChange";
                case CertificateOrderActionType.OrgValidationComplete:
                    return "OrgValidationComplete";
                case CertificateOrderActionType.SanDrop:
                    return "SanDrop";
                case CertificateOrderActionType.FraudCleared:
                    return "FraudCleared";
                case CertificateOrderActionType.CertificateExpired:
                    return "CertificateExpired";
                case CertificateOrderActionType.CertificateExpirationWarning:
                    return "CertificateExpirationWarning";
                case CertificateOrderActionType.FraudDocumentationRequired:
                    return "FraudDocumentationRequired";
                case CertificateOrderActionType.Unknown:
                    return "Unknown";
            }
            return null;
        }

        internal static CertificateOrderActionType? ParseCertificateOrderActionType(this string value)
        {
            switch( value )
            {
                case "CertificateIssued":
                    return CertificateOrderActionType.CertificateIssued;
                case "CertificateOrderCanceled":
                    return CertificateOrderActionType.CertificateOrderCanceled;
                case "CertificateOrderCreated":
                    return CertificateOrderActionType.CertificateOrderCreated;
                case "CertificateRevoked":
                    return CertificateOrderActionType.CertificateRevoked;
                case "DomainValidationComplete":
                    return CertificateOrderActionType.DomainValidationComplete;
                case "FraudDetected":
                    return CertificateOrderActionType.FraudDetected;
                case "OrgNameChange":
                    return CertificateOrderActionType.OrgNameChange;
                case "OrgValidationComplete":
                    return CertificateOrderActionType.OrgValidationComplete;
                case "SanDrop":
                    return CertificateOrderActionType.SanDrop;
                case "FraudCleared":
                    return CertificateOrderActionType.FraudCleared;
                case "CertificateExpired":
                    return CertificateOrderActionType.CertificateExpired;
                case "CertificateExpirationWarning":
                    return CertificateOrderActionType.CertificateExpirationWarning;
                case "FraudDocumentationRequired":
                    return CertificateOrderActionType.FraudDocumentationRequired;
                case "Unknown":
                    return CertificateOrderActionType.Unknown;
            }
            return null;
        }
    }
}
