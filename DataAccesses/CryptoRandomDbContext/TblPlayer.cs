using System;
using System.Collections.Generic;

namespace crypto_random.DataAccesses.CryptoRandomDbContext
{
    public partial class TblPlayer
    {
        public TblPlayer()
        {
            TblTranactions = new HashSet<TblTranaction>();
        }

        public string PlayerName { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime? Modifydate { get; set; }

        public virtual ICollection<TblTranaction> TblTranactions { get; set; }
    }
}
