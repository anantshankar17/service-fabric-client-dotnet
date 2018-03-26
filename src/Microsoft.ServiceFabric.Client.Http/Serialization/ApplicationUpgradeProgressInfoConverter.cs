// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Client.Http.Serialization
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ServiceFabric.Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converter for <see cref="ApplicationUpgradeProgressInfo" />.
    /// </summary>
    internal class ApplicationUpgradeProgressInfoConverter
    {
        /// <summary>
        /// Deserializes the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <returns>The object Value.</returns>
        internal static ApplicationUpgradeProgressInfo Deserialize(JsonReader reader)
        {
            return reader.Deserialize(GetFromJsonProperties);
        }

        /// <summary>
        /// Gets the object from Json properties.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from, reader must be placed at first property.</param>
        /// <returns>The object Value.</returns>
        internal static ApplicationUpgradeProgressInfo GetFromJsonProperties(JsonReader reader)
        {
            var name = default(string);
            var typeName = default(string);
            var targetApplicationTypeVersion = default(string);
            var upgradeDomains = default(IEnumerable<UpgradeDomainInfo>);
            var upgradeState = default(UpgradeState?);
            var nextUpgradeDomain = default(string);
            var rollingUpgradeMode = default(UpgradeMode?);
            var upgradeDescription = default(ApplicationUpgradeDescription);
            var upgradeDurationInMilliseconds = default(string);
            var upgradeDomainDurationInMilliseconds = default(string);
            var unhealthyEvaluations = default(IEnumerable<HealthEvaluationWrapper>);
            var currentUpgradeDomainProgress = default(CurrentUpgradeDomainProgressInfo);
            var startTimestampUtc = default(string);
            var failureTimestampUtc = default(string);
            var failureReason = default(FailureReason?);
            var upgradeDomainProgressAtFailure = default(FailureUpgradeDomainProgressInfo);
            var upgradeStatusDetails = default(string);

            do
            {
                var propName = reader.ReadPropertyName();
                if (string.Compare("Name", propName, StringComparison.Ordinal) == 0)
                {
                    name = reader.ReadValueAsString();
                }
                else if (string.Compare("TypeName", propName, StringComparison.Ordinal) == 0)
                {
                    typeName = reader.ReadValueAsString();
                }
                else if (string.Compare("TargetApplicationTypeVersion", propName, StringComparison.Ordinal) == 0)
                {
                    targetApplicationTypeVersion = reader.ReadValueAsString();
                }
                else if (string.Compare("UpgradeDomains", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeDomains = reader.ReadList(UpgradeDomainInfoConverter.Deserialize);
                }
                else if (string.Compare("UpgradeState", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeState = UpgradeStateConverter.Deserialize(reader);
                }
                else if (string.Compare("NextUpgradeDomain", propName, StringComparison.Ordinal) == 0)
                {
                    nextUpgradeDomain = reader.ReadValueAsString();
                }
                else if (string.Compare("RollingUpgradeMode", propName, StringComparison.Ordinal) == 0)
                {
                    rollingUpgradeMode = UpgradeModeConverter.Deserialize(reader);
                }
                else if (string.Compare("UpgradeDescription", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeDescription = ApplicationUpgradeDescriptionConverter.Deserialize(reader);
                }
                else if (string.Compare("UpgradeDurationInMilliseconds", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeDurationInMilliseconds = reader.ReadValueAsString();
                }
                else if (string.Compare("UpgradeDomainDurationInMilliseconds", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeDomainDurationInMilliseconds = reader.ReadValueAsString();
                }
                else if (string.Compare("UnhealthyEvaluations", propName, StringComparison.Ordinal) == 0)
                {
                    unhealthyEvaluations = reader.ReadList(HealthEvaluationWrapperConverter.Deserialize);
                }
                else if (string.Compare("CurrentUpgradeDomainProgress", propName, StringComparison.Ordinal) == 0)
                {
                    currentUpgradeDomainProgress = CurrentUpgradeDomainProgressInfoConverter.Deserialize(reader);
                }
                else if (string.Compare("StartTimestampUtc", propName, StringComparison.Ordinal) == 0)
                {
                    startTimestampUtc = reader.ReadValueAsString();
                }
                else if (string.Compare("FailureTimestampUtc", propName, StringComparison.Ordinal) == 0)
                {
                    failureTimestampUtc = reader.ReadValueAsString();
                }
                else if (string.Compare("FailureReason", propName, StringComparison.Ordinal) == 0)
                {
                    failureReason = FailureReasonConverter.Deserialize(reader);
                }
                else if (string.Compare("UpgradeDomainProgressAtFailure", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeDomainProgressAtFailure = FailureUpgradeDomainProgressInfoConverter.Deserialize(reader);
                }
                else if (string.Compare("UpgradeStatusDetails", propName, StringComparison.Ordinal) == 0)
                {
                    upgradeStatusDetails = reader.ReadValueAsString();
                }
                else
                {
                    reader.SkipPropertyValue();
                }
            }
            while (reader.TokenType != JsonToken.EndObject);

            return new ApplicationUpgradeProgressInfo(
                name: name,
                typeName: typeName,
                targetApplicationTypeVersion: targetApplicationTypeVersion,
                upgradeDomains: upgradeDomains,
                upgradeState: upgradeState,
                nextUpgradeDomain: nextUpgradeDomain,
                rollingUpgradeMode: rollingUpgradeMode,
                upgradeDescription: upgradeDescription,
                upgradeDurationInMilliseconds: upgradeDurationInMilliseconds,
                upgradeDomainDurationInMilliseconds: upgradeDomainDurationInMilliseconds,
                unhealthyEvaluations: unhealthyEvaluations,
                currentUpgradeDomainProgress: currentUpgradeDomainProgress,
                startTimestampUtc: startTimestampUtc,
                failureTimestampUtc: failureTimestampUtc,
                failureReason: failureReason,
                upgradeDomainProgressAtFailure: upgradeDomainProgressAtFailure,
                upgradeStatusDetails: upgradeStatusDetails);
        }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="T: Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="obj">The object to serialize to JSON.</param>
        internal static void Serialize(JsonWriter writer, ApplicationUpgradeProgressInfo obj)
        {
            // Required properties are always serialized, optional properties are serialized when not null.
            writer.WriteStartObject();
            writer.WriteProperty(obj.UpgradeState, "UpgradeState", UpgradeStateConverter.Serialize);
            writer.WriteProperty(obj.RollingUpgradeMode, "RollingUpgradeMode", UpgradeModeConverter.Serialize);
            writer.WriteProperty(obj.FailureReason, "FailureReason", FailureReasonConverter.Serialize);
            if (obj.Name != null)
            {
                writer.WriteProperty(obj.Name, "Name", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.TypeName != null)
            {
                writer.WriteProperty(obj.TypeName, "TypeName", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.TargetApplicationTypeVersion != null)
            {
                writer.WriteProperty(obj.TargetApplicationTypeVersion, "TargetApplicationTypeVersion", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.UpgradeDomains != null)
            {
                writer.WriteEnumerableProperty(obj.UpgradeDomains, "UpgradeDomains", UpgradeDomainInfoConverter.Serialize);
            }

            if (obj.NextUpgradeDomain != null)
            {
                writer.WriteProperty(obj.NextUpgradeDomain, "NextUpgradeDomain", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.UpgradeDescription != null)
            {
                writer.WriteProperty(obj.UpgradeDescription, "UpgradeDescription", ApplicationUpgradeDescriptionConverter.Serialize);
            }

            if (obj.UpgradeDurationInMilliseconds != null)
            {
                writer.WriteProperty(obj.UpgradeDurationInMilliseconds, "UpgradeDurationInMilliseconds", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.UpgradeDomainDurationInMilliseconds != null)
            {
                writer.WriteProperty(obj.UpgradeDomainDurationInMilliseconds, "UpgradeDomainDurationInMilliseconds", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.UnhealthyEvaluations != null)
            {
                writer.WriteEnumerableProperty(obj.UnhealthyEvaluations, "UnhealthyEvaluations", HealthEvaluationWrapperConverter.Serialize);
            }

            if (obj.CurrentUpgradeDomainProgress != null)
            {
                writer.WriteProperty(obj.CurrentUpgradeDomainProgress, "CurrentUpgradeDomainProgress", CurrentUpgradeDomainProgressInfoConverter.Serialize);
            }

            if (obj.StartTimestampUtc != null)
            {
                writer.WriteProperty(obj.StartTimestampUtc, "StartTimestampUtc", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.FailureTimestampUtc != null)
            {
                writer.WriteProperty(obj.FailureTimestampUtc, "FailureTimestampUtc", JsonWriterExtensions.WriteStringValue);
            }

            if (obj.UpgradeDomainProgressAtFailure != null)
            {
                writer.WriteProperty(obj.UpgradeDomainProgressAtFailure, "UpgradeDomainProgressAtFailure", FailureUpgradeDomainProgressInfoConverter.Serialize);
            }

            if (obj.UpgradeStatusDetails != null)
            {
                writer.WriteProperty(obj.UpgradeStatusDetails, "UpgradeStatusDetails", JsonWriterExtensions.WriteStringValue);
            }

            writer.WriteEndObject();
        }
    }
}