using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class BatchRepository : GenericRepository<Batch>
    {
        public BatchRepository(CustomerContext context) : base(context) { }

        public BatchRepository() : base(new CustomerContext()) { }

        public async Task<List<Batch>> GetARBatchesByBatchID(string batchID)
        {
            var parameter = new SqlParameter("@BatchID", batchID);

            return await _context.Database
                .SqlQuery<Batch>("spcpcTurboTenARLoad @BatchID", parameter)
                .ToListAsync();
        }

        public async Task<List<Batch>> GetAPBatchesAsync(string batchID)
        {
            return await _context.Database
                .SqlQuery<Batch>(String.Format(@" SELECT tapBatch.BatchCmnt, tapPendVoucher.TranNo, tapPendVoucher.TranAmt
                                                    FROM tapBatch 
                                                    INNER JOIN tapPendVoucher ON tapBatch.BatchKey = tapPendVoucher.BatchKey 
                                                    INNER JOIN tciBatchLog ON tapBatch.BatchKey = tciBatchlog.BatchKey
                                                    WHERE tciBatchlog.BatchID = '{0}'", batchID))
                .ToListAsync();
        }

        public async Task<List<Batch>> GetPOBatchesAsync(string batchID)
        {
            return await _context.Database
                .SqlQuery<Batch>(String.Format(@" SELECT tpoBatch.BatchCmnt, tapPendVoucher.TranNo, tapPendVoucher.TranAmt
                                                    FROM tpoBatch 
                                                    INNER JOIN tapPendVoucher ON tpoBatch.BatchKey = tapPendVoucher.BatchKey 
                                                    INNER JOIN tciBatchLog ON tpoBatch.BatchKey = tciBatchlog.BatchKey
                                                    WHERE tciBatchlog.BatchID = '{0}'", batchID))
                .ToListAsync();
        }
    }
}
