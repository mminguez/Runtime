/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Runtime.Events.Store;

namespace Dolittle.Runtime.Events.Relativity
{
    /// <summary>
    /// Represents an implementation of <see cref="IEventHorizon"/>
    /// </summary>
    [Singleton]
    public class EventHorizon : IEventHorizon
    {
        readonly List<ISingularity> _singularities = new List<ISingularity>();
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="EventHorizon"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public EventHorizon(IExectionContextManagerILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void PassThrough(Dolittle.Runtime.Events.Processing.CommittedEventStreamWithContext committedEventStream)
        {
            lock(_singularities)
            {
                _logger.Information($"Committed eventstream entering event horizon with {_singularities.Count} singularities");
                _singularities
                    .Where(_ => _.CanPassThrough(committedEventStream)).AsParallel()
                    .ForEach(_ =>
                    {
                        _logger.Information($"Passing committed eventstream through singularity identified with bounded context '{_.BoundedContext}' in application '{_.Application}'");
                        _.PassThrough(committedEventStream);
                    });
            }
        }

        /// <inheritdoc/>
        public void Collapse(ISingularity singularity)
        {
            lock(_singularities)
            {
                _logger.Information($"Quantum tunnel collapsed for singularity identified with bounded context '{singularity.BoundedContext}' in application '{singularity.Application}'");
                _singularities.Remove(singularity);
            }
        }

        /// <inheritdoc/>
        public void GravitateTowards(ISingularity singularity, IEnumerable<TenantOffset> tenantOffsets)
        {
            lock(_singularities)
            {
                _logger.Information($"Gravitate events in the event horizon towards singularity identified with bounded context '{singularity.BoundedContext}' in application '{singularity.Application}'");
                _singularities.Add(singularity);

            }
        }

        /// <inheritdoc/>
        public IEnumerable<ISingularity> Singularities
        {
            get
            {
                lock(_singularities) return _singularities;
            }
        }


        void FetchAndQueueCommits(IEnumerable<TenantOffset> tenantOffsets)
        {
            var commits = GetCommits(tenantOffsets);
            PassThroughSingularity(commits);
        }


        IEnumerable<Commits> GetCommits(IEnumerable<TenantOffset> tenantOffsets)
        {
            List<Commits> commits = new List<Commits>();
            Parallel.ForEach(tenantOffsets, (_) => {
                _executionContextManager.CurrentFor(_.Tenant);
                commits.Add(_unprocessedCommitFetcher.GetUnprocessedCommits(_.Offset));
            });
            return commits;
        }

        void PassThroughSingularity(IEnumerable<Commits> commits)
        {
            throw new NotImplementedException();
        }
        
    }
}