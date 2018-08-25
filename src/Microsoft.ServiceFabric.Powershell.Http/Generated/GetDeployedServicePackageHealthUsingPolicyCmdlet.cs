// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Powershell.Http
{
    using System;
    using System.Collections.Generic;
    using System.Management.Automation;
    using Microsoft.ServiceFabric.Common;

    /// <summary>
    /// Gets the information about health of service package for a specific application deployed on a Service Fabric node
    /// using the specified policy.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SFDeployedServicePackageHealthUsingPolicy", DefaultParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
    public partial class GetDeployedServicePackageHealthUsingPolicyCmdlet : CommonCmdletBase
    {
        /// <summary>
        /// Gets or sets NodeName. The name of the node.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 0, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public NodeName NodeName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ApplicationId. The identity of the application. This is typically the full name of the application
        /// without the 'fabric:' URI scheme.
        /// Starting from version 6.0, hierarchical names are delimited with the "~" character.
        /// For example, if the application name is "fabric:/myapp/app1", the application identity would be "myapp~app1" in
        /// 6.0+ and "myapp/app1" in previous versions.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 1, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public string ApplicationId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ServicePackageName. The name of the service package.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, Position = 2, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public string ServicePackageName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets EventsHealthStateFilter. Allows filtering the collection of HealthEvent objects returned based on
        /// health state.
        /// The possible values for this parameter include integer value of one of the following health states.
        /// Only events that match the filter are returned. All events are used to evaluate the aggregated health state.
        /// If not specified, all entries are returned. The state values are flag-based enumeration, so the value could be a
        /// combination of these values, obtained using the bitwise 'OR' operator. For example, If the provided value is 6 then
        /// all of the events with HealthState value of OK (2) and Warning (4) are returned.
        /// 
        /// - Default - Default value. Matches any HealthState. The value is zero.
        /// - None - Filter that doesn't match any HealthState value. Used in order to return no results on a given collection
        /// of states. The value is 1.
        /// - Ok - Filter that matches input with HealthState value Ok. The value is 2.
        /// - Warning - Filter that matches input with HealthState value Warning. The value is 4.
        /// - Error - Filter that matches input with HealthState value Error. The value is 8.
        /// - All - Filter that matches input with any HealthState value. The value is 65535.
        /// </summary>
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public int? EventsHealthStateFilter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ConsiderWarningAsError. Indicates whether warnings are treated with the same severity as errors.
        /// </summary>
        [Parameter(Mandatory = false, Position = 4, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public bool? ConsiderWarningAsError
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets MaxPercentUnhealthyDeployedApplications. The maximum allowed percentage of unhealthy deployed
        /// applications. Allowed values are Byte values from zero to 100.
        /// The percentage represents the maximum tolerated percentage of deployed applications that can be unhealthy before
        /// the application is considered in error.
        /// This is calculated by dividing the number of unhealthy deployed applications over the number of nodes where the
        /// application is currently deployed on in the cluster.
        /// The computation rounds up to tolerate one failure on small numbers of nodes. Default percentage is zero.
        /// </summary>
        [Parameter(Mandatory = false, Position = 5, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public int? MaxPercentUnhealthyDeployedApplications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets DefaultServiceTypeHealthPolicy. The health policy used by default to evaluate the health of a service
        /// type.
        /// </summary>
        [Parameter(Mandatory = false, Position = 6, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public ServiceTypeHealthPolicy DefaultServiceTypeHealthPolicy
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ServiceTypeHealthPolicyMap. The map with service type health policy per service type name. The map is
        /// empty by default.
        /// </summary>
        [Parameter(Mandatory = false, Position = 7, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public IEnumerable<ServiceTypeHealthPolicyMapItem> ServiceTypeHealthPolicyMap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets ServerTimeout. The server timeout for performing the operation in seconds. This timeout specifies the
        /// time duration that the client is willing to wait for the requested operation to complete. The default value for
        /// this parameter is 60 seconds.
        /// </summary>
        [Parameter(Mandatory = false, Position = 8, ParameterSetName = "GetDeployedServicePackageHealthUsingPolicy")]
        public long? ServerTimeout
        {
            get;
            set;
        }

        /// <inheritdoc/>
        protected override void ProcessRecordInternal()
        {
            try
            {
                var applicationHealthPolicy = new ApplicationHealthPolicy(
                considerWarningAsError: this.ConsiderWarningAsError,
                maxPercentUnhealthyDeployedApplications: this.MaxPercentUnhealthyDeployedApplications,
                defaultServiceTypeHealthPolicy: this.DefaultServiceTypeHealthPolicy,
                serviceTypeHealthPolicyMap: this.ServiceTypeHealthPolicyMap);

                var result = this.ServiceFabricClient.ServicePackages.GetDeployedServicePackageHealthUsingPolicyAsync(
                    nodeName: this.NodeName,
                    applicationId: this.ApplicationId,
                    servicePackageName: this.ServicePackageName,
                    eventsHealthStateFilter: this.EventsHealthStateFilter,
                    applicationHealthPolicy: applicationHealthPolicy,
                    serverTimeout: this.ServerTimeout,
                    cancellationToken: this.CancellationToken).GetAwaiter().GetResult();

                this.WriteObject(this.FormatOutput(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <inheritdoc/>
        protected override object FormatOutput(object output)
        {
            return output;
        }
    }
}