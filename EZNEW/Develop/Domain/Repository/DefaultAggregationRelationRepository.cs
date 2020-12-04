﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EZNEW.Develop.CQuery;
using EZNEW.Develop.DataAccess;
using EZNEW.Develop.Domain.Aggregation;
using EZNEW.Develop.Entity;
using EZNEW.Develop.UnitOfWork;

namespace EZNEW.Develop.Domain.Repository
{
    /// <summary>
    /// Default aggregation relation repository
    /// </summary>
    /// <typeparam name="TModel">Aggregation model</typeparam>
    /// <typeparam name="TFirstRelationModel">The first relation model</typeparam>
    /// <typeparam name="TSecondRelationModel">The second relation model</typeparam>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TDataAccess">Data access</typeparam>
    public abstract class DefaultAggregationRelationRepository<TModel, TFirstRelationModel, TSecondRelationModel, TEntity, TDataAccess> : BaseAggregationRelationRepository<TModel, TFirstRelationModel, TSecondRelationModel, TEntity, TDataAccess> where TModel : AggregationRoot<TModel> where TSecondRelationModel : IAggregationRoot<TSecondRelationModel> where TFirstRelationModel : IAggregationRoot<TFirstRelationModel> where TEntity : BaseEntity<TEntity>, new() where TDataAccess : IDataAccess<TEntity>
    {
        #region Query

        /// <summary>
        /// Get list by first
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return datas</returns>
        public sealed override List<TModel> GetListByFirst(IEnumerable<TFirstRelationModel> datas)
        {
            return GetListByFirstAsync(datas).Result;
        }

        /// <summary>
        /// Get list by first
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return datas</returns>
        public sealed override async Task<List<TModel>> GetListByFirstAsync(IEnumerable<TFirstRelationModel> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return new List<TModel>(0);
            }
            var query = CreateQueryByFirst(datas);
            return await GetListAsync(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Get list by second
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return datas</returns>
        public sealed override List<TModel> GetListBySecond(IEnumerable<TSecondRelationModel> datas)
        {
            return GetListBySecondAsync(datas).Result;
        }

        /// <summary>
        /// Get list by second
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return datas</returns>
        public sealed override async Task<List<TModel>> GetListBySecondAsync(IEnumerable<TSecondRelationModel> datas)
        {
            if (datas.IsNullOrEmpty())
            {
                return new List<TModel>(0);
            }
            var query = CreateQueryBySecond(datas);
            return await GetListAsync(query).ConfigureAwait(false);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Remove by first datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <param name="activationOption">Activation option</param>
        public sealed override void RemoveByFirst(IEnumerable<TFirstRelationModel> datas, ActivationOptions activationOption = null)
        {
            if (datas.IsNullOrEmpty())
            {
                return;
            }
            IQuery query = CreateQueryByFirst(datas);
            Remove(query, activationOption);
        }

        /// <summary>
        /// Remove by second datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <param name="activationOption">Activation option</param>
        public sealed override void RemoveBySecond(IEnumerable<TSecondRelationModel> datas, ActivationOptions activationOption = null)
        {
            if (datas.IsNullOrEmpty())
            {
                return;
            }
            IQuery query = CreateQueryBySecond(datas);
            Remove(query, activationOption);
        }

        /// <summary>
        /// Remove by first
        /// </summary>
        /// <param name="query">Query object</param>
        /// <param name="activationOption">Activation option</param>
        public sealed override void RemoveByFirst(IQuery query, ActivationOptions activationOption = null)
        {
            var removeQuery = CreateQueryByFirst(query);
            Remove(removeQuery, activationOption);
        }

        /// <summary>
        /// Remove by first
        /// </summary>
        /// <param name="query">Query object</param>
        /// <param name="activationOption">Activation option</param>
        public sealed override void RemoveBySecond(IQuery query, ActivationOptions activationOption = null)
        {
            var removeQuery = CreateQueryBySecond(query);
            Remove(removeQuery, activationOption);
        }

        #endregion

        #region Functions

        /// <summary>
        /// Create query by first type datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return query object</returns>
        public abstract IQuery CreateQueryByFirst(IEnumerable<TFirstRelationModel> datas);

        /// <summary>
        /// Create query by second type datas
        /// </summary>
        /// <param name="datas">Datas</param>
        /// <returns>Return query object</returns>
        public abstract IQuery CreateQueryBySecond(IEnumerable<TSecondRelationModel> datas);

        /// <summary>
        /// Create query by first type datas query object
        /// </summary>
        /// <param name="query">Query object</param>
        /// <returns>Return query object</returns>
        public abstract IQuery CreateQueryByFirst(IQuery query);

        /// <summary>
        /// Create query by second type datas query object
        /// </summary>
        /// <param name="query">Query object</param>
        /// <returns>Return query object</returns>
        public abstract IQuery CreateQueryBySecond(IQuery query);

        #endregion
    }
}
