using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    internal class CreditCardRepository
    {
        private CustomerContext _context;
        public CreditCardRepository() : this(new CustomerContext())
        {

        }

        public CreditCardRepository(CustomerContext context)
        {
            _context = context;
        }

        internal CreditCard LoadCreditCardByKey(int creditCardKey)
        {
            var sqlParameter = new SqlParameter("@creditCardKey", creditCardKey);

            return  _context
                .Database
                .SqlQuery<CreditCard>("spcpcCreditCardLoad @creditCardKey", sqlParameter)
                .FirstOrDefault();
        }

        internal List<int> LoadCreditCardKeysByCustKey(int custKey)
        {
            return _context.Database.SqlQuery<int>($@"SELECT CCKey FROM tcpCreditCard WHERE CustKey = {custKey}").ToList();
        }

        internal int AddNewCreditCard(CreditCard creditCard, int custKey)
        {
            var parameterList = new List<SqlParameter>
            {
                new SqlParameter("@InParamCardTypeKey", creditCard.CrCardTypeKey),
                new SqlParameter("@InParamCardNo", creditCard.CrCardNo),
                new SqlParameter("@InParamCodedCCNo", creditCard.CodedCCNo),
                new SqlParameter("@InParamCardExp", creditCard.CrCardExp),
                new SqlParameter("@InParamCardHolderName", creditCard.CardHolderName),
                new SqlParameter("@InParamCardStreetNbrZip", creditCard.CrCardStreetNbrZip),
                new SqlParameter("@InParamCardZipCode", creditCard.CrCardZipCode),
                new SqlParameter("@InParamCardPreferred", creditCard.Preferred),
                new SqlParameter("@InParamCustKey", custKey),
                new SqlParameter("@OutParamCCKey", SqlDbType.Int) { Direction = ParameterDirection.Output}
            };

            var result =_context
                       .Database
                       .ExecuteSqlCommand(@"spCPC_CCEditorInsert @InParamCardTypeKey, 
                                                                 @InParamCardNo,
                                                                 @InParamCodedCCNo,
                                                                 @InParamCardExp,
                                                                 @InParamCardHolderName,
                                                                 @InParamCardStreetNbrZip,
                                                                 @InParamCardZipCode,
                                                                 @InParamCardPreferred,
                                                                 @InParamCustKey,
                                                                 @OutParamCCKey
                                                                 ", parameterList.ToArray());


            return result;
        }

        internal void UpdateCreditCard(CreditCard creditCard)
        {
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@InParamCardTypeKey", creditCard.CrCardTypeKey),
                new SqlParameter("@InParamCardNo", creditCard.CrCardNo),
                new SqlParameter("@InParamCodedCCNo", creditCard.CodedCCNo),
                new SqlParameter("@InParamCardExp", creditCard.CrCardExp),
                new SqlParameter("@InParamCardHolderName", creditCard.CardHolderName),
                new SqlParameter("@InParamCardStreetNbrZip", creditCard.CrCardStreetNbrZip),
                new SqlParameter("@InParamCardZipCode", creditCard.CrCardZipCode),
                new SqlParameter("@InParamCardPreferred", creditCard.Preferred),
                new SqlParameter("@InParamKey", creditCard.CCKey),
            };

            _context
               .Database
               .ExecuteSqlCommand(@"spCPC_CCEditorUpdate @InParamCardTypeKey,
                                                         @InParamCardNo,
                                                         @InParamCodedCCNo,
                                                         @InParamCardExp,
                                                         @InParamCardHolderName,
                                                         @InParamCardStreetNbrZip,
                                                         @InParamCardZipCode,
                                                         @InParamCardPreferred,
                                                         @InParamKey
                                                         ", paramArray);
        }


        internal void DeleteCreditCard(CreditCard creditCard, int custKey)
        {
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@InParamCustKey", custKey),
                new SqlParameter("@InParamCCKey", creditCard.CCKey)
            };

            _context
               .Database
               .ExecuteSqlCommand("spCPC_CCEditorDelete @InParamCustKey, @InParamCCKey", paramArray);
        }

        internal void UpdateCreditCardStatusToInActive(int cardKey)
        {
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@InParamCustKey", 1111111),
                new SqlParameter("@InParamCCKey", cardKey)
            };
            _context
               .Database
               .ExecuteSqlCommand("spCPC_CCEditorUpdateStatus @InParamCustKey, @InParamCCKey", paramArray );
        }

    }
}
