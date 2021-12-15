using Business.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    public class FindexService : IFindexService {
        public IDataResult<int> GetFindexScore(int customerId) {
            int score = 1500;
            return new SuccessDataResult<int>(score);
        }
    }
}
