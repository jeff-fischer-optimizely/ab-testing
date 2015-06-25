﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Marketing.Multivariate.Dal
{
    public class MultivariateTestDal : IMultivariateTestDal
    {
        #region member variables

        protected const string Proc_MultivariateTest_Save = "MultivariateTest_Save";
        protected const string Proc_MultivariateTest_Delete = "MultivariateTest_Delete";
        protected const string Proc_MultivariateTest_Get = "MultivariateTest_Get";
        protected const string Proc_MultivariateTest_GetByOriginalItemId = "MultivariateTest_GetByOriginalItemId";

        private string _connectionString;
        private IDataOperations _dataOperations;

        #endregion

        public MultivariateTestDal(string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
                _connectionString = connectionString;
            else
            {
                throw new Exception("Connection String cannot be empty");
            }

            _dataOperations = new DataOperations(_connectionString);
        }

        internal MultivariateTestDal(string connectionString, IDataOperations dataOperations)
        {
            _connectionString = connectionString;
            _dataOperations = dataOperations;
        }

        public Guid Add(MultivariateTestParameters parameters)
        {
            var newId = Guid.NewGuid();
            SqlParameter[] sqlParams = { 
            new SqlParameter() { ParameterName = "Id", Value = newId, DbType = DbType.Guid, Size = 36},
            new SqlParameter() { ParameterName = "Title", Value = parameters.Title, DbType = DbType.String, Size = 255 },
            new SqlParameter() { ParameterName = "Owner", Value = parameters.Owner, DbType = DbType.String, Size = 100 },
            new SqlParameter() { ParameterName = "OriginalItemId", Value = parameters.OriginalItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "VariantItemId", Value = parameters.VariantItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "ConversionItemId", Value = parameters.ConversionItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "State", Value = parameters.State, DbType = DbType.String, Size = 10 },
            new SqlParameter() { ParameterName = "StartDate", Value = parameters.StartDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "EndDate", Value = parameters.EndDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "LastModifiedDate", Value = parameters.LastModifiedDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "LastModifiedBy", Value = parameters.LastModifiedBy, DbType = DbType.String, Size = 100 }
            };

            _dataOperations.ExecuteNonQuery(Proc_MultivariateTest_Save, CommandType.StoredProcedure, sqlParams);
            return newId;
        }

        public void Update(MultivariateTestParameters parameters)
        {
            SqlParameter[] sqlParams = { 
            new SqlParameter() { ParameterName = "Id", Value = parameters.Id, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "Title", Value = parameters.Title, DbType = DbType.String, Size = 255 },
            new SqlParameter() { ParameterName = "Owner", Value = parameters.Owner, DbType = DbType.String, Size = 100 },
            new SqlParameter() { ParameterName = "OriginalItemId", Value = parameters.OriginalItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "VariantItemId", Value = parameters.VariantItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "ConversionItemId", Value = parameters.ConversionItemId, DbType = DbType.Guid, Size = 36 },
            new SqlParameter() { ParameterName = "State", Value = parameters.State, DbType = DbType.String, Size = 10 },
            new SqlParameter() { ParameterName = "StartDate", Value = parameters.StartDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "EndDate", Value = parameters.EndDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "LastModifiedDate", Value = parameters.LastModifiedDate, DbType = DbType.DateTime, Size = 12 },
            new SqlParameter() { ParameterName = "LastModifiedBy", Value = parameters.LastModifiedBy, DbType = DbType.String, Size = 100 }
            };

            _dataOperations.ExecuteNonQuery(Proc_MultivariateTest_Save, CommandType.StoredProcedure, sqlParams);
        }

        public void Delete(Guid Id)
        {
            SqlParameter[] sqlParams = { 
            new SqlParameter() { ParameterName = "Id", Value = Id, DbType = DbType.Guid, Size = 36 },
            };

            _dataOperations.ExecuteNonQuery(Proc_MultivariateTest_Delete, CommandType.StoredProcedure, sqlParams);
        }

        public MultivariateTestParameters Get(Guid objectId)
        {
            MultivariateTestParameters multiVarTestParam = null;
            SqlParameter[] sqlParams = { 
            new SqlParameter() { ParameterName = "Id", Value = objectId, DbType = DbType.Guid, Size = 36 },
            };

            multiVarTestParam = MapReaderToParameters(_dataOperations.ExecuteReader(Proc_MultivariateTest_Get, CommandType.StoredProcedure, sqlParams));
            return multiVarTestParam;
        }

        public MultivariateTestParameters GetByOriginalItemId(Guid itemId)
        {
            MultivariateTestParameters multiVarTestParam = null;
            SqlParameter[] sqlParams = { 
            new SqlParameter() { ParameterName = "OriginalItemId", Value = itemId, DbType = DbType.Guid, Size = 36 },
            };

            multiVarTestParam = MapReaderToParameters(_dataOperations.ExecuteReader(Proc_MultivariateTest_GetByOriginalItemId, CommandType.StoredProcedure, sqlParams));
            return multiVarTestParam;
        }

        private MultivariateTestParameters MapReaderToParameters(DbDataReader dataReader)
        {
            if (dataReader == null)
            {
                return null;
            }

            MultivariateTestParameters multiVarTestParam = null;
            bool hasValues = false;
            hasValues = dataReader.Read();
            if (hasValues)
            {
                multiVarTestParam = new MultivariateTestParameters()
                {
                    Id = dataReader["Id"] != null ? (Guid)dataReader["Id"] : Guid.Empty,
                    Title = dataReader["Title"] != null ? dataReader["Title"].ToString() : string.Empty,
                    Owner = dataReader["Owner"] != null ? dataReader["Owner"].ToString() : string.Empty,
                    LastModifiedBy = dataReader["LastModifiedBy"] != null ? dataReader["LastModifiedBy"].ToString() : string.Empty,
                    OriginalItemId = dataReader["OriginalItemId"] != null ? (Guid)dataReader["OriginalItemId"] : Guid.Empty,
                    VariantItemId = dataReader["VariantItemId"] != null ? (Guid)dataReader["VariantItemId"] : Guid.Empty,
                    ConversionItemId = dataReader["ConversionItemId"] != null ? (Guid)dataReader["ConversionItemId"] : Guid.Empty,
                    State = dataReader["State"] != null ? dataReader["State"].ToString() : string.Empty,
                    StartDate = dataReader["StartDate"] != null ? Convert.ToDateTime(dataReader["StartDate"].ToString()) : default(DateTime),
                    EndDate = dataReader["EndDate"] != null ? Convert.ToDateTime(dataReader["EndDate"].ToString()) : default(DateTime),
                    LastModifiedDate = dataReader["LastModifiedDate"] != null ? Convert.ToDateTime(dataReader["LastModifiedDate"].ToString()) : default(DateTime)
                };
            }

            return multiVarTestParam;
        }
    }
}
