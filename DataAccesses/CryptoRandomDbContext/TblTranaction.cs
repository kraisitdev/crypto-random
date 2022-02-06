using System;
using System.Collections.Generic;

namespace crypto_random.DataAccesses.CryptoRandomDbContext
{
    public partial class TblTranaction
    {
        public int Id { get; set; }
        public int? CoinUnit { get; set; }
        public string? CoinCode { get; set; }
        public string? CoinName { get; set; }
        public string? PlayerName { get; set; }
        public DateTime? Modifydate { get; set; }

        public virtual TblPlayer? PlayerNameNavigation { get; set; }
    }
}
