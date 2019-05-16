using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class ShipmentBatchRepository : GenericRepository<ShipmentBatch>
    {
        public ShipmentBatchRepository() : base(new CustomerContext()) { }

        public ShipmentBatchRepository(CustomerContext context) : base(context) { }

        public async Task<List<ShipmentBatch>> GetShipmentBatchesAsync(int whseKey)
        {
            var param = new SqlParameter(@"whsekey", whseKey);

            return await _context
                .Database
                .SqlQuery<ShipmentBatch>("spcpcGetAccountingBatches @whsekey", param)
                .ToListAsync();
        }

        public async Task<List<ShipmentCheck>> GetShipmentCheckAsync(string createDate, int whseKey, int typeKey, string packStation)
        {
            var paramArray = new SqlParameter[]
            {
                new SqlParameter("@CreateDate", createDate),
                new SqlParameter("@WhseKey", whseKey),
                new SqlParameter("@Type", typeKey),
                new SqlParameter("@UserId", packStation)
            };

            return await _context
                .Database
                .SqlQuery<ShipmentCheck>(@"exec spcpcShipmentCheck2 @CreateDate,
                                                                    @WhseKey, 
                                                                    @Type, 
                                                                    @UserId",
                                                                    new SqlParameter("@CreateDate", createDate),
                                                                    new SqlParameter("@WhseKey", whseKey),
                                                                    new SqlParameter("@Type", typeKey),
                                                                    new SqlParameter("@UserId", packStation)).ToListAsync();
        }


        // comment from Sage Assistant code
        //'     ' FreightAmt must be same in both tsoPendShipment and tsoShipLine, if they are not,
        //'     ' Sage would fail when posting shipment.To remedy this problem, we have to update
        //'     ' tsoPendShipment.FreightAmt to be same as tsoShipLine.FreightAmt.   
        // 
        // this procedure works in conjunction with spcpcShipmentCheck2 & spcpcGetAccountingBatches
        // in order to produce the type key (generated on the fly)

        public async Task BalanceFreight(int typeKey, string createDate, int whseKey, string packstation)
        {
            string shipMethWhere = "";
            string paymentTermsWhere = "";

            switch (typeKey)
            {
                case 1: //REG SHIPMENTS
                    shipMethWhere = " COALESCE((SELECT ShipMethKey FROM tciShipMethod t WHERE ShipMethKey not in (27,37,32,40,44,52,69,70,72,78,80,85) and t.ShipMethKey = tsoSalesOrder.DfltShipMethKey), -1) ";
                    paymentTermsWhere = " COALESCE((SELECT PmtTermsKey FROM tciPaymentTerms t WHERE t.PmtTermsKey not in (48) and t.PmtTermsKey = tsoSalesOrder.PmtTermsKey), -1)";
                    break;

                case 2: //WILL CALL NET
                    shipMethWhere = " COALESCE((SELECT ShipMethKey FROM tciShipMethod t WHERE ShipMethKey in (27,37,32) and t.ShipMethKey = tsoSalesOrder.DfltShipMethKey), -1)";
                    paymentTermsWhere = " COALESCE((SELECT PmtTermsKey FROM tciPaymentTerms t WHERE PmtTermsKey in (22,29,30,31,32,47) and t.PmtTermsKey = tsoSalesOrder.PmtTermsKey), -1)";
                    break;

                case 3: //WILL CALL CASH
                    shipMethWhere = " COALESCE((SELECT ShipMethKey FROM tciShipMethod t WHERE ShipMethKey in (27,37,32) and t.ShipMethKey = tsoSalesOrder.DfltShipMethKey), -1)";
                    paymentTermsWhere = "COALESCE((SELECT PmtTermsKey FROM tciPaymentTerms t WHERE PmtTermsKey in (36,37,40,41,44) and t.PmtTermsKey = tsoSalesOrder.PmtTermsKey), -1)";
                    break;

                case 4: //USPS
                    shipMethWhere = " COALESCE((SELECT ShipMethKey FROM tciShipMethod t WHERE ShipMethKey in (40,44,52,69,70,72,78,80,85,77,66,73,51,23,42,67,71,64,82,81,83,84,43,33,39,65,68,68,28,59) and t.ShipMethKey = tsoSalesOrder.DfltShipMethKey), -1)";
                    paymentTermsWhere = " COALESCE((SELECT PmtTermsKey FROM tciPaymentTerms t WHERE t.PmtTermsKey not in (48) and t.PmtTermsKey = tsoSalesOrder.PmtTermsKey), -1)";
                    break;

                case 5: //MO CRCARD
                    shipMethWhere = " COALESCE((SELECT ShipMethKey FROM tciShipMethod t WHERE t.ShipMethKey = tsoSalesOrder.DfltShipMethKey), -1)";
                    paymentTermsWhere = " COALESCE((SELECT PmtTermsKey FROM tciPaymentTerms t WHERE PmtTermsKey in (48) and t.PmtTermsKey = tsoSalesOrder.PmtTermsKey), -1)";
                    break;
            }

            string query = String.Format(@"	SELECT DISTINCT tsoPendShipment.ShipKey, 
                                                            tsoPendShipment.FreightAmt, 
                                                            tsoPendShipment.WhseKey, 
                                                            tsoSalesOrder.SalesAmt, 
                                                            tsoSalesOrder.STaxAmt , 
                                                            tciPaymentTerms.DueDayOrMonth, RTRIM(tsoPendShipment.UserFld1) UserFld1 
                                                FROM tsoPendShipment INNER JOIN
                                                tsoShipLine ON tsoPendShipment.ShipKey = tsoShipLine.ShipKey INNER JOIN 
                                                tsoSOLine ON tsoShipLine.SOLineKey = tsoSOLine.SOLineKey INNER JOIN 
                                                tsoSalesOrder ON tsoSOLine.SOKey = tsoSalesOrder.SOKey INNER JOIN 
                                                tciPaymentTerms ON tsoSalesOrder.PmtTermsKey = tciPaymentTerms.PmtTermsKey 
                                                Where convert(char, tsoPendShipment.CreateDate, 101) = '{0}'
                                                  AND tsoPendShipment.WhseKey = {1}
                                                  AND tsoSalesOrder.DfltShipMethKey = {2}
                                                  AND tsoSalesOrder.PmtTermsKey = {3}", createDate, whseKey, shipMethWhere, paymentTermsWhere);

            if (!String.IsNullOrEmpty(packstation))
            {
                query = query + String.Format("AND tsoPendShipment.CreateUserId = '{0}'", packstation);
            }

            var shipments = await _context
                .Database
                .SqlQuery<ShipmentFreightBalance>(query)
                .ToListAsync();

            foreach (var shipment in shipments)
            {
                var shipLineDists = _context
                                        .Database
                                        .SqlQuery<ShipLineDistFreightBalance>(@"SELECT tsoShipLineDist.ShipLineDistKey, tsoShipLineDist.FreightAmt FROM tsoShipLine INNER JOIN 
                                                            tsoShipLineDist ON tsoShipLine.ShipLineKey = tsoShipLineDist.ShipLineKey 
                                                            Where tsoShipLine.ShipKey = {0}", shipment.ShipKey).ToList();


                for (int index = 0; index < shipLineDists.Count; index++)
                {
                    ShipLineDistFreightBalance currentShipLineDist = shipLineDists[index];

                    currentShipLineDist.FreightAmt = (index == 0) ? (shipment.FreightAmt) : (0);

                    _context.Database.ExecuteSqlCommand(String.Format(@"UPDATE tsoShipLineDist SET tsoShipLineDist.FreightAmt = {0}
                                                                        WHERE tsoShipLineDist.ShipLineDistKey = {1}",
                                                                        currentShipLineDist.FreightAmt,
                                                                        currentShipLineDist.ShipLineDistKey));

                }
            }
        }
    }


}
