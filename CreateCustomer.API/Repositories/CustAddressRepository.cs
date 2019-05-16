using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CustAddressRepository: GenericRepository<CustAddress>, ICustAddressRepository
    {
        public CustAddressRepository() : this(new CustomerContext()) { }

        public CustAddressRepository(CustomerContext context) : base(context)
        {
        }

        public Tuple<int, string, string> GetCATaxScheduleIdByCounty(string county)
        {
            var parameterList = new List<SqlParameter>();

            var countyParameter = new SqlParameter("@_oSchdDesc", county);
            parameterList.Add(countyParameter);

            var keyParameter = new SqlParameter("@_oSchdKey", 0);
            keyParameter.Direction = System.Data.ParameterDirection.Output;
            parameterList.Add(keyParameter);

            var idParameter = new SqlParameter("@_oSchdID", string.Empty);
            idParameter.Direction = System.Data.ParameterDirection.Output;
            idParameter.Size = 15;
            parameterList.Add(idParameter);

            var rateParameter = new SqlParameter("@_oRate", string.Empty);
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.SqlDbType = System.Data.SqlDbType.Decimal;
            rateParameter.Precision = 12;
            rateParameter.Scale = 6;
            parameterList.Add(rateParameter);

            _context.Database.ExecuteSqlCommand("spcpcLoadCountySalesTax @_oSchdDesc, @_oSchdKey OUT, @_oSchdID OUT, @_oRate OUT", parameterList.ToArray());

            return new Tuple<int, string, string>(
                keyParameter.Value == DBNull.Value ? 0 : int.Parse(keyParameter.Value.ToString()),
                idParameter.Value == DBNull.Value ? "" : idParameter.Value.ToString(),
                rateParameter.Value == DBNull.Value ? "" : rateParameter.Value.ToString());
        }

        public Tuple<int, string, string> GetCATaxScheduleIdByCity(string city)
        {
            var parameterList = new List<SqlParameter>();

            var countyParameter = new SqlParameter("@_oSchdDesc", city);
            parameterList.Add(countyParameter);

            var keyParameter = new SqlParameter("@_oSchdKey", 0);
            keyParameter.Direction = System.Data.ParameterDirection.Output;
            parameterList.Add(keyParameter);

            var idParameter = new SqlParameter("@_oSchdID", string.Empty);
            idParameter.Direction = System.Data.ParameterDirection.Output;
            idParameter.Size = 15;
            parameterList.Add(idParameter);

            var rateParameter = new SqlParameter("@_oRate", string.Empty);
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.SqlDbType = System.Data.SqlDbType.Decimal;
            rateParameter.Precision = 12;
            rateParameter.Scale = 6;
            parameterList.Add(rateParameter);

            _context.Database.ExecuteSqlCommand("spcpcLoadCitySalesTax @_oSchdDesc, @_oSchdKey OUT, @_oSchdID OUT, @_oRate OUT", parameterList.ToArray());

            return new Tuple<int, string, string>(
                keyParameter.Value == DBNull.Value ? 0 : int.Parse(keyParameter.Value.ToString()),
                idParameter.Value == DBNull.Value ? "" : idParameter.Value.ToString(),
                rateParameter.Value == DBNull.Value ? "" : rateParameter.Value.ToString());
        }

        public Tuple<int, string, string> GetWATaxScheduleIdByLocationCode(string locationCode)
        {
            var parameterList = new List<SqlParameter>();

            var countyParameter = new SqlParameter("@_iLocCode", locationCode);
            parameterList.Add(countyParameter);

            var keyParameter = new SqlParameter("@_oSchdKey", 0);
            keyParameter.Direction = System.Data.ParameterDirection.Output;
            parameterList.Add(keyParameter);

            var idParameter = new SqlParameter("@_oSchdID", string.Empty);
            idParameter.Direction = System.Data.ParameterDirection.Output;
            idParameter.Size = 15;
            parameterList.Add(idParameter);

            var rateParameter = new SqlParameter("@_oRate", string.Empty);
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.SqlDbType = System.Data.SqlDbType.Decimal;
            rateParameter.Precision = 12;
            rateParameter.Scale = 6;
            parameterList.Add(rateParameter);

            _context.Database.ExecuteSqlCommand("spcpcLoadWASalesTax @_iLocCode, @_oSchdKey OUT, @_oSchdID OUT, @_oRate OUT", parameterList.ToArray());

            return new Tuple<int, string, string>(
                keyParameter.Value == DBNull.Value ? 0 : int.Parse(keyParameter.Value.ToString()),
                idParameter.Value == DBNull.Value ? "" : idParameter.Value.ToString(),
                rateParameter.Value == DBNull.Value ? "" : rateParameter.Value.ToString());
        }

        public Tuple<int, string, string> GetTaxScheduleIdByZip(string zip)
        {
            var parameterList = new List<SqlParameter>();

            var countyParameter = new SqlParameter("@_iZipCode", zip);
            parameterList.Add(countyParameter);

            var keyParameter = new SqlParameter("@_oSchdKey", 0);
            keyParameter.Direction = System.Data.ParameterDirection.Output;
            parameterList.Add(keyParameter);

            var idParameter = new SqlParameter("@_oSchdID", string.Empty);
            idParameter.Direction = System.Data.ParameterDirection.Output;
            idParameter.Size = 15;
            parameterList.Add(idParameter);

            var rateParameter = new SqlParameter("@_oRate", string.Empty);
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.Direction = System.Data.ParameterDirection.Output;
            rateParameter.SqlDbType = System.Data.SqlDbType.Decimal;
            rateParameter.Precision = 12;
            rateParameter.Scale = 6;
            parameterList.Add(rateParameter);

            _context.Database.ExecuteSqlCommand("spcpcLoadSalesTaxByZip @_iZipCode, @_oSchdKey OUT, @_oSchdID OUT, @_oRate OUT", parameterList.ToArray());

            return new Tuple<int, string, string>(
                keyParameter.Value == DBNull.Value ? 0 : int.Parse(keyParameter.Value.ToString()),
                idParameter.Value == DBNull.Value ? "" : idParameter.Value.ToString(),
                rateParameter.Value == DBNull.Value ? "" : rateParameter.Value.ToString());
        }
    }
}
