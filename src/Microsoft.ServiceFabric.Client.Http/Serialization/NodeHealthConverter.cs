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
    /// Converter for <see cref="NodeHealth" />.
    /// </summary>
    internal class NodeHealthConverter
    {
        /// <summary>
        /// Deserializes the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <returns>The object Value.</returns>
        internal static NodeHealth Deserialize(JsonReader reader)
        {
            return reader.Deserialize(GetFromJsonProperties);
        }

        /// <summary>
        /// Gets the object from Json properties.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from, reader must be placed at first property.</param>
        /// <returns>The object Value.</returns>
        internal static NodeHealth GetFromJsonProperties(JsonReader reader)
        {
            var aggregatedHealthState = default(HealthState?);
            var healthEvents = default(IEnumerable<HealthEvent>);
            var unhealthyEvaluations = default(IEnumerable<HealthEvaluationWrapper>);
            var healthStatistics = default(HealthStatistics);
            var name = default(NodeName);

            do
            {
                var propName = reader.ReadPropertyName();
                if (string.Compare("AggregatedHealthState", propName, StringComparison.Ordinal) == 0)
                {
                    aggregatedHealthState = HealthStateConverter.Deserialize(reader);
                }
                else if (string.Compare("HealthEvents", propName, StringComparison.Ordinal) == 0)
                {
                    healthEvents = reader.ReadList(HealthEventConverter.Deserialize);
                }
                else if (string.Compare("UnhealthyEvaluations", propName, StringComparison.Ordinal) == 0)
                {
                    unhealthyEvaluations = reader.ReadList(HealthEvaluationWrapperConverter.Deserialize);
                }
                else if (string.Compare("HealthStatistics", propName, StringComparison.Ordinal) == 0)
                {
                    healthStatistics = HealthStatisticsConverter.Deserialize(reader);
                }
                else if (string.Compare("Name", propName, StringComparison.Ordinal) == 0)
                {
                    name = NodeNameConverter.Deserialize(reader);
                }
                else
                {
                    reader.SkipPropertyValue();
                }
            }
            while (reader.TokenType != JsonToken.EndObject);

            return new NodeHealth(
                aggregatedHealthState: aggregatedHealthState,
                healthEvents: healthEvents,
                unhealthyEvaluations: unhealthyEvaluations,
                healthStatistics: healthStatistics,
                name: name);
        }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="T: Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="obj">The object to serialize to JSON.</param>
        internal static void Serialize(JsonWriter writer, NodeHealth obj)
        {
            // Required properties are always serialized, optional properties are serialized when not null.
            writer.WriteStartObject();
            writer.WriteProperty(obj.AggregatedHealthState, "AggregatedHealthState", HealthStateConverter.Serialize);
            if (obj.HealthEvents != null)
            {
                writer.WriteEnumerableProperty(obj.HealthEvents, "HealthEvents", HealthEventConverter.Serialize);
            }

            if (obj.UnhealthyEvaluations != null)
            {
                writer.WriteEnumerableProperty(obj.UnhealthyEvaluations, "UnhealthyEvaluations", HealthEvaluationWrapperConverter.Serialize);
            }

            if (obj.HealthStatistics != null)
            {
                writer.WriteProperty(obj.HealthStatistics, "HealthStatistics", HealthStatisticsConverter.Serialize);
            }

            if (obj.Name != null)
            {
                writer.WriteProperty(obj.Name, "Name", NodeNameConverter.Serialize);
            }

            writer.WriteEndObject();
        }
    }
}
